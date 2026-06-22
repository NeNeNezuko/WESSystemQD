using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;
using WmsPlus.Api.Utils;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SecondarySortController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<SecondarySortController> _logger;

    public SecondarySortController(WarehouseDbContext context, ILogger<SecondarySortController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询二次分拣单列表（以表身TF_PKFJ为主，LEFT JOIN表头MF_PKFJ）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<SecondarySortDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? sortNumber,
        [FromQuery] string? outboundNoticeNo,
        [FromQuery] string? bizOrderNumber,
        [FromQuery] string? applyOrderNumber,
        [FromQuery] string? deptCode)
    {
        try
        {
            var query = from t in _context.TfPkfjs
                        join m in _context.MfPkfjs on t.PKFJ_NO equals m.PKFJ_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 分拣日期范围筛选（使用表头的PKFJ_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.PKFJ_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.PKFJ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 分拣单号模糊匹配
            if (!string.IsNullOrWhiteSpace(sortNumber))
                query = query.Where(x => x.T.PKFJ_NO.Contains(sortNumber));

            // 出库通知单号模糊匹配（表头字段）
            if (!string.IsNullOrWhiteSpace(outboundNoticeNo))
                query = query.Where(x => x.M != null && (x.M.CK_TZ_NO != null && x.M.CK_TZ_NO.Contains(outboundNoticeNo)));

            // 业务单号模糊匹配（表头字段）
            if (!string.IsNullOrWhiteSpace(bizOrderNumber))
                query = query.Where(x => x.M != null && (x.M.BIL_NO != null && x.M.BIL_NO.Contains(bizOrderNumber)));

            // 申请单号模糊匹配（表头字段）
            if (!string.IsNullOrWhiteSpace(applyOrderNumber))
                query = query.Where(x => x.M != null && (x.M.APPLY_NO != null && x.M.APPLY_NO.Contains(applyOrderNumber)));

            // 部门代号模糊匹配（表头字段）
            if (!string.IsNullOrWhiteSpace(deptCode))
                query = query.Where(x => x.M != null && (x.M.DEP != null && x.M.DEP.Contains(deptCode)));

            // 按分拣单号+项次排序
            query = query.OrderBy(x => x.T.PKFJ_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影，关联部门名称
            var deptCodes = rawList.Select(x => x.M?.DEP).Where(d => !string.IsNullOrEmpty(d)).Distinct().ToList();
            var deptDict = await _context.Depts
                .Where(d => deptCodes.Contains(d.DEP))
                .ToDictionaryAsync(d => d.DEP, d => d.NAME);

            var list = rawList.Select(x => new SecondarySortDto
            {
                ItemNo = x.T.ITM,
                SortDate = x.M?.PKFJ_DD ?? DateTime.MinValue,
                SortNumber = x.T.PKFJ_NO,
                DeptName = x.M?.DEP != null && deptDict.ContainsKey(x.M.DEP) ? deptDict[x.M.DEP] : (x.M?.DEP ?? ""),
                OperatorName = x.M?.USR_NAME ?? "",
                SourceOrderId = x.M?.OTH_BIL_ID ?? "",
                SourceOrderNo = x.M?.OTH_BIL_NO ?? ""
            }).ToList();

            return Ok(new ApiResult<List<SecondarySortDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询二次分拣单时发生错误");
            return StatusCode(500, new ApiResult<List<SecondarySortDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据分拣单号获取二次分拣单详情（含表身明细）
    /// </summary>
    [HttpGet("{pkfjNo}")]
    public async Task<ActionResult<ApiResult<SecondarySortDetailDto>>> GetDetail(string pkfjNo)
    {
        try
        {
            var header = await _context.MfPkfjs.FindAsync(pkfjNo);
            if (header == null)
                return NotFound(new ApiResult<SecondarySortDetailDto>
                {
                    Success = false,
                    Message = "未找到该二次分拣单"
                });

            var details = await _context.TfPkfjs
                .Where(t => t.PKFJ_NO == pkfjNo)
                .OrderBy(t => t.ITM)
                .Select(t => new TfPkfjDto
                {
                    ItemNo = t.ITM,
                    PrdNo = t.PRD_NO ?? "",
                    PrdName = t.PRD_NAME ?? "",
                    PrdSpec = t.PRD_MARK ?? "",
                    BatNo = t.BAT_NO ?? "",
                    Wh = t.WH ?? "",
                    Unit = t.UNIT ?? "",
                    Qty = t.QTY ?? 0,
                    PickQty = t.PICK_QTY ?? 0,
                    Rem = t.REM ?? ""
                })
                .ToListAsync();

            return Ok(new ApiResult<SecondarySortDetailDto>
            {
                Success = true,
                Data = new SecondarySortDetailDto
                {
                    Header = MySqlDateHelper.SafeConvert(header),
                    Details = details
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取二次分拣单详情时发生错误");
            return StatusCode(500, new ApiResult<SecondarySortDetailDto>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>二次分拣单列表行数据（对应前端表格）</summary>
public class SecondarySortDto
{
    public int ItemNo { get; set; }
    public DateTime SortDate { get; set; }
    public string SortNumber { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string OperatorName { get; set; } = "";
    public string SourceOrderId { get; set; } = "";
    public string SourceOrderNo { get; set; } = "";
}

/// <summary>二次分拣单详情（含表头+表身）</summary>
public class SecondarySortDetailDto
{
    public MfPkfj Header { get; set; } = new();
    public List<TfPkfjDto> Details { get; set; } = new();
}

/// <summary>表身明细DTO</summary>
public class TfPkfjDto
{
    public int ItemNo { get; set; }
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdSpec { get; set; } = "";
    public string BatNo { get; set; } = "";
    public string Wh { get; set; } = "";
    public string Unit { get; set; } = "";
    public decimal Qty { get; set; }
    public decimal PickQty { get; set; }
    public string Rem { get; set; } = "";
}
