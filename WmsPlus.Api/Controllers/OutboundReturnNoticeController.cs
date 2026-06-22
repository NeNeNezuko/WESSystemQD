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
public class OutboundReturnNoticeController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<OutboundReturnNoticeController> _logger;

    public OutboundReturnNoticeController(WarehouseDbContext context, ILogger<OutboundReturnNoticeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询出库退回通知单列表（以表身TF_CKTB为主，LEFT JOIN表头MF_CKTB）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<OutboundReturnNoticeDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? documentNumber,
        [FromQuery] string? outboundNoticeNumber,
        [FromQuery] string? businessOrderNumber,
        [FromQuery] string? applyOrderNumber,
        [FromQuery] string? deptCode,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? customerCode)
    {
        try
        {
            var query = from t in _context.TfCktbs
                        join m in _context.MfCktbs on t.TB_NO equals m.TB_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 单据日期范围筛选（使用表头的TB_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.TB_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.TB_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 单据号码模糊匹配
            if (!string.IsNullOrWhiteSpace(documentNumber))
                query = query.Where(x => x.T.TB_NO.Contains(documentNumber));

            // 出库通知单号模糊匹配（外部系统单号）
            if (!string.IsNullOrWhiteSpace(outboundNoticeNumber))
                query = query.Where(x => x.M != null && (x.M.OTH_BIL_NO != null && x.M.OTH_BIL_NO.Contains(outboundNoticeNumber)));

            // 业务单号模糊匹配
            if (!string.IsNullOrWhiteSpace(businessOrderNumber))
                query = query.Where(x => x.M != null && (x.M.ORG_BIL_NO != null && x.M.ORG_BIL_NO.Contains(businessOrderNumber)));

            // 申请单号模糊匹配（已转单号）
            if (!string.IsNullOrWhiteSpace(applyOrderNumber))
                query = query.Where(x => x.M != null && (x.M.RTN_BIL_NO != null && x.M.RTN_BIL_NO.Contains(applyOrderNumber)));

            // 部门代号模糊匹配
            if (!string.IsNullOrWhiteSpace(deptCode))
                query = query.Where(x => x.M != null && (x.M.DEP != null && x.M.DEP.Contains(deptCode)));

            // 仓库代号模糊匹配
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.M != null && (x.M.WH != null && x.M.WH.Contains(warehouseCode)));

            // 客户代号模糊匹配
            if (!string.IsNullOrWhiteSpace(customerCode))
                query = query.Where(x => x.M != null && (x.M.CUS_NO != null && x.M.CUS_NO.Contains(customerCode)));

            // 按单据号+项次排序
            query = query.OrderBy(x => x.T.TB_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new OutboundReturnNoticeDto
            {
                ItemNo = x.T.ITM,
                DocumentDate = x.M?.TB_DD ?? DateTime.MinValue,
                DocumentNumber = x.T.TB_NO,
                DeptCode = x.M?.DEP ?? "",
                DeptName = "",  // 关联DEPT表查询
                WarehouseCode = x.M?.WH ?? "",
                WarehouseName = "",  // 关联MY_WH表查询
                CustomerCode = x.M?.CUS_NO ?? "",
                CustomerName = x.M?.CUS_NAME ?? "",
                ErpBillId = x.M?.ERP_BIL_ID ?? "",
                ErpBillNo = x.M?.ERP_BIL_NO ?? "",
                ErpApplyId = x.M?.ERP_BIL_ID ?? "",
                ErpApplyNo = x.M?.ERP_BIL_NO ?? "",
                EntryDate = x.M?.SYS_DATE ?? DateTime.MinValue,
                // 表身明细字段
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                PrdMark = x.T.PRD_MARK ?? "",
                Qty = x.T.QTY ?? 0,
                Unit = x.T.UNIT ?? ""
            }).ToList();

            return Ok(new ApiResult<List<OutboundReturnNoticeDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询出库退回通知单时发生错误");
            return StatusCode(500, new ApiResult<List<OutboundReturnNoticeDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据单据号获取出库退回通知单详情（含表身明细）
    /// </summary>
    [HttpGet("{tbNo}")]
    public async Task<ActionResult<ApiResult<OutboundReturnNoticeDetailDto>>> GetDetail(string tbNo)
    {
        try
        {
            var header = await _context.MfCktbs.FindAsync(tbNo);
            if (header == null)
                return NotFound(new ApiResult<OutboundReturnNoticeDetailDto>
                {
                    Success = false,
                    Message = "未找到该出库退回通知单"
                });

            var details = await _context.TfCktbs
                .Where(t => t.TB_NO == tbNo)
                .OrderBy(t => t.ITM)
                .Select(t => new TfCktbDto
                {
                    ItemNo = t.ITM,
                    PrdNo = t.PRD_NO ?? "",
                    PrdName = t.PRD_NAME ?? "",
                    PrdMark = t.PRD_MARK ?? "",
                    Qty = t.QTY ?? 0,
                    Unit = t.UNIT ?? "",
                    LotNo = t.BAT_NO ?? "",
                    Rem = t.REM ?? ""
                })
                .ToListAsync();

            return Ok(new ApiResult<OutboundReturnNoticeDetailDto>
            {
                Success = true,
                Data = new OutboundReturnNoticeDetailDto
                {
                    Header = MySqlDateHelper.SafeConvert(header),
                    Details = details
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取出库退回通知单详情时发生错误");
            return StatusCode(500, new ApiResult<OutboundReturnNoticeDetailDto>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>出库退回通知单列表行数据（对应前端表格）</summary>
public class OutboundReturnNoticeDto
{
    public int ItemNo { get; set; }
    public DateTime DocumentDate { get; set; }
    public string DocumentNumber { get; set; } = "";
    public string DeptCode { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string WarehouseCode { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string CustomerCode { get; set; } = "";
    public string CustomerName { get; set; } = "";
    public string ErpBillId { get; set; } = "";
    public string ErpBillNo { get; set; } = "";
    public string ErpApplyId { get; set; } = "";
    public string ErpApplyNo { get; set; } = "";
    public DateTime EntryDate { get; set; }
    // 表身明细字段
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public decimal Qty { get; set; }
    public string Unit { get; set; } = "";
}

/// <summary>出库退回通知单详情（含表头+表身）</summary>
public class OutboundReturnNoticeDetailDto
{
    public MfCktb Header { get; set; } = new();
    public List<TfCktbDto> Details { get; set; } = new();
}

/// <summary>表身明细DTO</summary>
public class TfCktbDto
{
    public int ItemNo { get; set; }
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public decimal Qty { get; set; }
    public string Unit { get; set; } = "";
    public string LotNo { get; set; } = "";
    public string Rem { get; set; } = "";
}
