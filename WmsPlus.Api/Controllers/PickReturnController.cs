using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PickReturnController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<PickReturnController> _logger;

    public PickReturnController(WarehouseDbContext context, ILogger<PickReturnController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询拣货退回单列表（以表身TF_JT为主，LEFT JOIN表头MF_JT）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<PickReturnDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? pickReturnNo,
        [FromQuery] string? outboundNoticeNo,
        [FromQuery] string? businessOrderNo,
        [FromQuery] string? applyOrderNumber,
        [FromQuery] string? deptCode)
    {
        try
        {
            var query = from t in _context.TfJts
                        join m in _context.MfJts on t.JT_NO equals m.JT_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 退回日期范围筛选
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.JT_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.JT_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 拣货退回单号模糊匹配
            if (!string.IsNullOrWhiteSpace(pickReturnNo))
                query = query.Where(x => x.T.JT_NO.Contains(pickReturnNo));

            // 出库通知单号模糊匹配
            if (!string.IsNullOrWhiteSpace(outboundNoticeNo))
                query = query.Where(x => x.M != null && (x.M.CK_TZ_NO != null && x.M.CK_TZ_NO.Contains(outboundNoticeNo)));

            // 业务单号模糊匹配
            if (!string.IsNullOrWhiteSpace(businessOrderNo))
                query = query.Where(x => x.M != null && (x.M.BIL_NO != null && x.M.BIL_NO.Contains(businessOrderNo)));

            // 申请单号模糊匹配
            if (!string.IsNullOrWhiteSpace(applyOrderNumber))
                query = query.Where(x => x.M != null && (x.M.APPLY_NO != null && x.M.APPLY_NO.Contains(applyOrderNumber)));

            // 部门代号模糊匹配
            if (!string.IsNullOrWhiteSpace(deptCode))
                query = query.Where(x => x.M != null && (x.M.DEP != null && x.M.DEP.Contains(deptCode)));

            // 按单据号+项次排序
            query = query.OrderBy(x => x.T.JT_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new PickReturnDto
            {
                ItemNo = x.T.ITM,
                ReturnDate = x.M?.JT_DD ?? DateTime.MinValue,
                PickReturnNo = x.T.JT_NO,
                DeptCode = x.M?.DEP ?? "",
                DeptName = "", // 需要关联DEPT表获取
                SalName = "",  // 需要关联获取经办人名称
                OthId = x.M?.OTH_ID ?? "",
                OthBilNo = x.M?.OTH_BIL_NO ?? "",
                // 表身明细字段
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                PrdSpec = x.T.PRD_MARK ?? "",
                Qty = x.T.QTY ?? 0,
                Unit = x.T.UNIT ?? ""
            }).ToList();

            return Ok(new ApiResult<List<PickReturnDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询拣货退回单时发生错误");
            return StatusCode(500, new ApiResult<List<PickReturnDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据单据号获取拣货退回单详情（含表身明细）
    /// </summary>
    [HttpGet("{jtNo}")]
    public async Task<ActionResult<ApiResult<PickReturnDetailDto>>> GetDetail(string jtNo)
    {
        try
        {
            var header = await _context.MfJts.FindAsync(jtNo);
            if (header == null)
                return NotFound(new ApiResult<PickReturnDetailDto>
                {
                    Success = false,
                    Message = "未找到该拣货退回单"
                });

            var details = await _context.TfJts
                .Where(t => t.JT_NO == jtNo)
                .OrderBy(t => t.ITM)
                .Select(t => new TfJtDto
                {
                    ItemNo = t.ITM,
                    PrdNo = t.PRD_NO ?? "",
                    PrdName = t.PRD_NAME ?? "",
                    PrdSpec = t.PRD_MARK ?? "",
                    Qty = t.QTY ?? 0,
                    Unit = t.UNIT ?? "",
                    LotNo = t.BAT_NO ?? "",
                    Rem = t.REM ?? ""
                })
                .ToListAsync();

            return Ok(new ApiResult<PickReturnDetailDto>
            {
                Success = true,
                Data = new PickReturnDetailDto
                {
                    Header = header,
                    Details = details
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取拣货退回单详情时发生错误");
            return StatusCode(500, new ApiResult<PickReturnDetailDto>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>拣货退回单列表行数据（对应前端表格）</summary>
public class PickReturnDto
{
    public int ItemNo { get; set; }
    public DateTime ReturnDate { get; set; }
    public string PickReturnNo { get; set; } = "";
    public string DeptCode { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string SalName { get; set; } = "";
    public string OthId { get; set; } = "";
    public string OthBilNo { get; set; } = "";
    // 表身明细字段
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdSpec { get; set; } = "";
    public decimal Qty { get; set; }
    public string Unit { get; set; } = "";
}

/// <summary>拣货退回单详情（含表头+表身）</summary>
public class PickReturnDetailDto
{
    public MfJt Header { get; set; } = new();
    public List<TfJtDto> Details { get; set; } = new();
}

/// <summary>表身明细DTO</summary>
public class TfJtDto
{
    public int ItemNo { get; set; }
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdSpec { get; set; } = "";
    public decimal Qty { get; set; }
    public string Unit { get; set; } = "";
    public string LotNo { get; set; } = "";
    public string Rem { get; set; } = "";
}
