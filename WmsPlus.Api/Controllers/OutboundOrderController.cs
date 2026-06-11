using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OutboundOrderController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<OutboundOrderController> _logger;

    public OutboundOrderController(WarehouseDbContext context, ILogger<OutboundOrderController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询出库单列表（以表身TF_CK为主，LEFT JOIN表头MF_CK）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<OutboundOrderDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? documentNumber,
        [FromQuery] string? ckTzNo,
        [FromQuery] string? bilNo,
        [FromQuery] string? applyNo,
        [FromQuery] string? depCode,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? rcvPoint,
        [FromQuery] string? customerCode)
    {
        try
        {
            var query = from t in _context.TfCks
                        join m in _context.MfCks on t.CK_ID equals m.CK_ID into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 单据日期范围筛选（使用表头的CK_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.CK_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.CK_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 单据号码模糊匹配
            if (!string.IsNullOrWhiteSpace(documentNumber))
                query = query.Where(x => x.T.CK_ID.Contains(documentNumber));

            // 出库通知单单号模糊匹配
            if (!string.IsNullOrWhiteSpace(ckTzNo))
                query = query.Where(x => x.M != null && (x.M.CK_TZ_NO != null && x.M.CK_TZ_NO.Contains(ckTzNo)));

            // 业务单号模糊匹配
            if (!string.IsNullOrWhiteSpace(bilNo))
                query = query.Where(x => x.M != null && (x.M.BIL_NO != null && x.M.BIL_NO.Contains(bilNo)));

            // 申请单号模糊匹配
            if (!string.IsNullOrWhiteSpace(applyNo))
                query = query.Where(x => x.M != null && (x.M.APPLY_NO != null && x.M.APPLY_NO.Contains(applyNo)));

            // 部门代号模糊匹配
            if (!string.IsNullOrWhiteSpace(depCode))
                query = query.Where(x => x.M != null && (x.M.DEP != null && x.M.DEP.Contains(depCode)));

            // 仓库代号模糊匹配
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.M != null && (x.M.WH != null && x.M.WH.Contains(warehouseCode)));

            // 收货点模糊匹配
            if (!string.IsNullOrWhiteSpace(rcvPoint))
                query = query.Where(x => x.M != null && (x.M.RCV_POINT != null && x.M.RCV_POINT.Contains(rcvPoint)));

            // 客户代号模糊匹配
            if (!string.IsNullOrWhiteSpace(customerCode))
                query = query.Where(x => x.M != null && (x.M.CUS_NO != null && x.M.CUS_NO.Contains(customerCode)));

            // 按单据号+项次排序
            query = query.OrderBy(x => x.T.CK_ID).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new OutboundOrderDto
            {
                ItemNo = x.T.ITM,
                DocumentDate = x.M?.CK_DD ?? DateTime.MinValue,
                DocumentNumber = x.T.CK_ID,
                DepCode = x.M?.DEP ?? "",
                DepName = "",
                WarehouseCode = x.M?.WH ?? "",
                WarehouseName = "",
                RcvPoint = x.M?.RCV_POINT ?? "",
                CustomerCode = x.M?.CUS_NO ?? "",
                CustomerName = x.M?.CUS_NAME ?? "",
                BatId = x.M?.BAT_ID ?? "",
                IsClosed = (x.M?.CLS_ID ?? "N") == "Y",
                // 表身明细字段
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                PrdSpec = x.T.PRD_MARK ?? "",
                Qty = x.T.QTY ?? 0,
                Unit = x.T.UNIT ?? ""
            }).ToList();

            return Ok(new ApiResult<List<OutboundOrderDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询出库单时发生错误");
            return StatusCode(500, new ApiResult<List<OutboundOrderDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据单据号获取出库单详情（含表身明细）
    /// </summary>
    [HttpGet("{ckId}")]
    public async Task<ActionResult<ApiResult<OutboundOrderDetailDto>>> GetDetail(string ckId)
    {
        try
        {
            var header = await _context.MfCks.FindAsync(ckId);
            if (header == null)
                return NotFound(new ApiResult<OutboundOrderDetailDto>
                {
                    Success = false,
                    Message = "未找到该出库单"
                });

            var details = await _context.TfCks
                .Where(t => t.CK_ID == ckId)
                .OrderBy(t => t.ITM)
                .Select(t => new TfCkDto
                {
                    ItemNo = t.ITM,
                    PrdNo = t.PRD_NO ?? "",
                    PrdName = t.PRD_NAME ?? "",
                    PrdSpec = t.PRD_MARK ?? "",
                    Qty = t.QTY ?? 0,
                    Unit = t.UNIT ?? "",
                    BatNo = t.BAT_NO ?? "",
                    Rem = t.REM ?? ""
                })
                .ToListAsync();

            return Ok(new ApiResult<OutboundOrderDetailDto>
            {
                Success = true,
                Data = new OutboundOrderDetailDto
                {
                    Header = header,
                    Details = details
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取出库单详情时发生错误");
            return StatusCode(500, new ApiResult<OutboundOrderDetailDto>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>出库单列表行数据（对应前端表格）</summary>
public class OutboundOrderDto
{
    public int ItemNo { get; set; }
    public DateTime DocumentDate { get; set; }
    public string DocumentNumber { get; set; } = "";
    public string DepCode { get; set; } = "";
    public string DepName { get; set; } = "";
    public string WarehouseCode { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string RcvPoint { get; set; } = "";
    public string CustomerCode { get; set; } = "";
    public string CustomerName { get; set; } = "";
    public string BatId { get; set; } = "";
    public bool IsClosed { get; set; }
    // 表身明细字段
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdSpec { get; set; } = "";
    public decimal Qty { get; set; }
    public string Unit { get; set; } = "";
}

/// <summary>出库单详情（含表头+表身）</summary>
public class OutboundOrderDetailDto
{
    public MfCk Header { get; set; } = new();
    public List<TfCkDto> Details { get; set; } = new();
}

/// <summary>表身明细DTO</summary>
public class TfCkDto
{
    public int ItemNo { get; set; }
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdSpec { get; set; } = "";
    public decimal Qty { get; set; }
    public string Unit { get; set; } = "";
    public string BatNo { get; set; } = "";
    public string Rem { get; set; } = "";
}
