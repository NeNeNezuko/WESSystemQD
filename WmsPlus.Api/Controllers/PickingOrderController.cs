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
public class PickingOrderController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<PickingOrderController> _logger;

    public PickingOrderController(WarehouseDbContext context, ILogger<PickingOrderController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询拣货单列表（以表身TF_PK为主，LEFT JOIN表头MF_PK，LEFT JOIN DEPT获取部门名称）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<PickingOrderDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? pickNumber,
        [FromQuery] string? outboundNoticeNumber,
        [FromQuery] string? businessOrderNumber,
        [FromQuery] string? applyOrderNumber,
        [FromQuery] string? deptCode)
    {
        try
        {
            var query = from t in _context.TfPks
                        join m in _context.MfPks on t.PK_NO equals m.PK_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 拣货日期范围筛选
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.PK_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.PK_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 拣货单号模糊匹配
            if (!string.IsNullOrWhiteSpace(pickNumber))
                query = query.Where(x => x.T.PK_NO.Contains(pickNumber));

            // 出库通知单号模糊匹配
            if (!string.IsNullOrWhiteSpace(outboundNoticeNumber))
                query = query.Where(x => x.M != null && x.M.CK_TZ_NO != null && x.M.CK_TZ_NO.Contains(outboundNoticeNumber));

            // 业务单号模糊匹配
            if (!string.IsNullOrWhiteSpace(businessOrderNumber))
                query = query.Where(x => x.M != null && x.M.BIL_NO != null && x.M.BIL_NO.Contains(businessOrderNumber));

            // 申请单号模糊匹配
            if (!string.IsNullOrWhiteSpace(applyOrderNumber))
                query = query.Where(x => x.M != null && x.M.APPLY_NO != null && x.M.APPLY_NO.Contains(applyOrderNumber));

            // 部门代号筛选
            if (!string.IsNullOrWhiteSpace(deptCode))
                query = query.Where(x => x.M != null && x.M.DEP == deptCode);

            // 按拣货单号+项次排序
            query = query.OrderBy(x => x.T.PK_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 获取部门名称映射
            var deptCodes = rawList
                .Where(x => x.M?.DEP != null)
                .Select(x => x.M!.DEP!)
                .Distinct()
                .ToList();
            var deptDict = await _context.Depts
                .Where(d => deptCodes.Contains(d.DEP))
                .ToDictionaryAsync(d => d.DEP, d => d.NAME ?? "");

            // 在内存中做投影
            var list = rawList.Select(x => new PickingOrderDto
            {
                ItemNo = x.T.ITM,
                PickDate = x.M?.PK_DD ?? DateTime.MinValue,
                PickNumber = x.T.PK_NO,
                DeptCode = x.M?.DEP ?? "",
                DeptName = x.M != null && x.M.DEP != null && deptDict.ContainsKey(x.M.DEP)
                    ? deptDict[x.M.DEP] : "",
                UserName = x.M?.USR ?? "",
                SourceType = x.M?.OTH_BIL_ID ?? "",
                SourceNo = x.M?.OTH_BIL_NO ?? ""
            }).ToList();

            return Ok(new ApiResult<List<PickingOrderDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询拣货单时发生错误");
            return StatusCode(500, new ApiResult<List<PickingOrderDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据拣货单号获取拣货单详情（含表头+表身）
    /// </summary>
    [HttpGet("{pkNo}")]
    public async Task<ActionResult<ApiResult<PickingOrderDetailDto>>> GetDetail(string pkNo)
    {
        try
        {
            var header = await _context.MfPks.FindAsync(pkNo);
            if (header == null)
                return NotFound(new ApiResult<PickingOrderDetailDto>
                {
                    Success = false,
                    Message = "未找到该拣货单"
                });

            var details = await _context.TfPks
                .Where(t => t.PK_NO == pkNo)
                .OrderBy(t => t.ITM)
                .Select(t => new TfPkDto
                {
                    ItemNo = t.ITM,
                    PrdNo = t.PRD_NO ?? "",
                    PrdName = t.PRD_NAME ?? "",
                    PrdSpec = t.PRD_MARK ?? "",
                    Qty = t.QTY ?? 0,
                    QtyPk = t.QTY_PK ?? 0,
                    Unit = t.UNIT ?? "",
                    LotNo = t.BAT_NO ?? "",
                    Wh = t.WH ?? "",
                    Chuw = t.CHUW ?? "",
                    Rem = t.REM ?? ""
                })
                .ToListAsync();

            return Ok(new ApiResult<PickingOrderDetailDto>
            {
                Success = true,
                Data = new PickingOrderDetailDto
                {
                    Header = MySqlDateHelper.SafeConvert(header),
                    Details = details
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取拣货单详情时发生错误");
            return StatusCode(500, new ApiResult<PickingOrderDetailDto>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>拣货单列表行数据（对应前端表格）</summary>
public class PickingOrderDto
{
    public int ItemNo { get; set; }
    public DateTime PickDate { get; set; }
    public string PickNumber { get; set; } = "";
    public string DeptCode { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string UserName { get; set; } = "";
    public string SourceType { get; set; } = "";
    public string SourceNo { get; set; } = "";
}

/// <summary>拣货单详情（含表头+表身）</summary>
public class PickingOrderDetailDto
{
    public MfPk Header { get; set; } = new();
    public List<TfPkDto> Details { get; set; } = new();
}

/// <summary>拣货单表身明细DTO</summary>
public class TfPkDto
{
    public int ItemNo { get; set; }
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdSpec { get; set; } = "";
    public decimal Qty { get; set; }
    public decimal QtyPk { get; set; }
    public string Unit { get; set; } = "";
    public string LotNo { get; set; } = "";
    public string Wh { get; set; } = "";
    public string Chuw { get; set; } = "";
    public string Rem { get; set; } = "";
}
