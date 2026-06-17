using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WaveOrderController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<WaveOrderController> _logger;

    public WaveOrderController(WarehouseDbContext context, ILogger<WaveOrderController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询波次单列表（以表身TF_BC为主，LEFT JOIN表头MF_BC）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<WaveOrderDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? bcNo,
        [FromQuery] string? outboundNoticeNo,
        [FromQuery] string? businessNo,
        [FromQuery] string? applyNo,
        [FromQuery] string? deptCode,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? receivingPoint,
        [FromQuery] string? businessType)
    {
        try
        {
            var query = from t in _context.TfBcs
                        join m in _context.MfBcs on t.BC_NO equals m.BC_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 波次日期范围筛选
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.BC_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.BC_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 波次单号模糊匹配
            if (!string.IsNullOrWhiteSpace(bcNo))
                query = query.Where(x => x.T.BC_NO.Contains(bcNo));

            // 出库通知单号模糊匹配
            if (!string.IsNullOrWhiteSpace(outboundNoticeNo))
                query = query.Where(x => x.M != null && (x.M.CK_TZ_NO != null && x.M.CK_TZ_NO.Contains(outboundNoticeNo)));

            // 业务单号模糊匹配
            if (!string.IsNullOrWhiteSpace(businessNo))
                query = query.Where(x => x.M != null && (x.M.BIL_NO != null && x.M.BIL_NO.Contains(businessNo)));

            // 申请单号模糊匹配
            if (!string.IsNullOrWhiteSpace(applyNo))
                query = query.Where(x => x.M != null && (x.M.APPLY_NO != null && x.M.APPLY_NO.Contains(applyNo)));

            // 部门代号模糊匹配
            if (!string.IsNullOrWhiteSpace(deptCode))
                query = query.Where(x => x.M != null && (x.M.DEP != null && x.M.DEP.Contains(deptCode)));

            // 仓库代号模糊匹配
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.M != null && (x.M.WH != null && x.M.WH.Contains(warehouseCode)));

            // 收货点模糊匹配
            if (!string.IsNullOrWhiteSpace(receivingPoint))
                query = query.Where(x => x.M != null && (x.M.RCV_POINT != null && x.M.RCV_POINT.Contains(receivingPoint)));

            // 业务类型筛选
            if (!string.IsNullOrWhiteSpace(businessType))
                query = query.Where(x => x.M != null && (x.M.BIL_TYPE != null && x.M.BIL_TYPE.Contains(businessType)));

            // 按波次单号+项次排序
            query = query.OrderBy(x => x.T.BC_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new WaveOrderDto
            {
                ItemNo = x.T.ITM ?? 0,
                WaveDate = x.M?.BC_DD ?? DateTime.MinValue,
                BcNo = x.T.BC_NO,
                WarehouseCode = x.M?.WH ?? "",
                WarehouseName = "",
                DeptCode = x.M?.DEP ?? "",
                DeptName = "",
                HandlerName = x.M?.USR ?? "",
                TicketRemark = x.M?.REM ?? "",
                Priority = x.M?.PRIORITY ?? 0
            }).ToList();

            return Ok(new ApiResult<List<WaveOrderDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询波次单时发生错误");
            return StatusCode(500, new ApiResult<List<WaveOrderDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据波次单号获取波次单详情（含表身明细）
    /// </summary>
    [HttpGet("{bcNo}")]
    public async Task<ActionResult<ApiResult<WaveOrderDetailDto>>> GetDetail(string bcNo)
    {
        try
        {
            var header = await _context.MfBcs.FindAsync(bcNo);
            if (header == null)
                return NotFound(new ApiResult<WaveOrderDetailDto>
                {
                    Success = false,
                    Message = "未找到该波次单"
                });

            var details = await _context.TfBcs
                .Where(t => t.BC_NO == bcNo)
                .OrderBy(t => t.ITM)
                .Select(t => new TfBcDto
                {
                    ItemNo = t.ITM ?? 0,
                    PrdNo = t.PRD_NO ?? "",
                    PrdName = t.PRD_NAME ?? "",
                    PrdSpec = t.PRD_MARK ?? "",
                    Qty = t.QTY ?? 0,
                    PickQty = t.PICK_QTY ?? 0,
                    Unit = t.UNIT ?? "",
                    LotNo = t.BAT_NO ?? "",
                    Rem = t.REM ?? ""
                })
                .ToListAsync();

            return Ok(new ApiResult<WaveOrderDetailDto>
            {
                Success = true,
                Data = new WaveOrderDetailDto
                {
                    Header = header,
                    Details = details
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取波次单详情时发生错误");
            return StatusCode(500, new ApiResult<WaveOrderDetailDto>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>波次单列表行数据（对应前端表格）</summary>
public class WaveOrderDto
{
    public int ItemNo { get; set; }
    public DateTime WaveDate { get; set; }
    public string BcNo { get; set; } = "";
    public string WarehouseCode { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string DeptCode { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string HandlerName { get; set; } = "";
    public string TicketRemark { get; set; } = "";
    public int Priority { get; set; }
}

/// <summary>波次单详情（含表头+表身）</summary>
public class WaveOrderDetailDto
{
    public MfBc Header { get; set; } = new();
    public List<TfBcDto> Details { get; set; } = new();
}

/// <summary>表身明细DTO</summary>
public class TfBcDto
{
    public int ItemNo { get; set; }
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdSpec { get; set; } = "";
    public decimal Qty { get; set; }
    public decimal PickQty { get; set; }
    public string Unit { get; set; } = "";
    public string LotNo { get; set; } = "";
    public string Rem { get; set; } = "";
}

/// <summary>通用API返回结果</summary>
public class WaveOrderApiResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
    public T? Data { get; set; }
    public int Total { get; set; }
}
