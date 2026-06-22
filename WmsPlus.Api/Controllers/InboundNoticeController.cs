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
public class InboundNoticeController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<InboundNoticeController> _logger;

    public InboundNoticeController(WarehouseDbContext context, ILogger<InboundNoticeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询入库通知单列表（以表身TF_RKTZ为主，LEFT JOIN表头MF_RKTZ）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<InboundNoticeDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? documentNumber,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? vendorCode,
        [FromQuery] string? closeStatus,
        [FromQuery] string? applyOrderNumber,
        [FromQuery] string? businessType,
        [FromQuery] string? creatorName,
        [FromQuery] bool fuzzyCreatorName = false)
    {
        try
        {
            var query = from t in _context.TfRktzs
                        join m in _context.MfRktzs on t.TZ_NO equals m.TZ_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 单据日期范围筛选（使用表头的TZ_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.TZ_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.TZ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 单据号码模糊匹配
            if (!string.IsNullOrWhiteSpace(documentNumber))
                query = query.Where(x => x.T.TZ_NO.Contains(documentNumber));

            // 仓库代号模糊匹配
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.M != null && (x.M.WH != null && x.M.WH.Contains(warehouseCode)));

            // 厂商/客户代号模糊匹配
            if (!string.IsNullOrWhiteSpace(vendorCode))
                query = query.Where(x => x.M != null && (x.M.CUS_NO != null && x.M.CUS_NO.Contains(vendorCode)));

            // 结案状态筛选
            if (!string.IsNullOrWhiteSpace(closeStatus) && closeStatus != "全部")
            {
                var isClosed = closeStatus == "已结案" ? "Y" : "N";
                query = query.Where(x => x.M != null && x.M.CLS_ID == isClosed);
            }

            // 业务类型筛选
            if (!string.IsNullOrWhiteSpace(businessType))
                query = query.Where(x => x.M != null && (x.M.BIL_TYPE != null && x.M.BIL_TYPE.Contains(businessType)));

            // 制单人名称筛选（实际按制单人代号USR筛选）
            if (!string.IsNullOrWhiteSpace(creatorName))
            {
                if (fuzzyCreatorName)
                    query = query.Where(x => x.M != null && (x.M.USR != null && x.M.USR.Contains(creatorName)));
                else
                    query = query.Where(x => x.M != null && x.M.USR == creatorName);
            }

            // 按单据号+项次排序
            query = query.OrderBy(x => x.T.TZ_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new InboundNoticeDto
            {
                ItemNo = x.T.ITM,
                DocumentDate = x.M?.TZ_DD ?? DateTime.MinValue,
                DocumentNumber = x.T.TZ_NO,
                WarehouseCode = x.M?.WH ?? "",
                WarehouseName = "",  // MF_RKTZ表头无仓库名称字段
                VendorCode = x.M?.CUS_NO ?? "",
                VendorName = x.M?.CUS_NAME ?? "",
                ApplyOrderNumber = "",
                ApplyDocType = x.M?.BIL_TYPE ?? "",
                OrderNumber = x.T.TZ_NO,
                IsClosed = (x.M?.CLS_ID ?? "N") == "Y",
                // 表身明细字段
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                PrdSpec = x.T.PRD_MARK ?? "",
                Qty = x.T.QTY ?? 0,
                Unit = x.T.UNIT ?? ""
            }).ToList();

            return Ok(new ApiResult<List<InboundNoticeDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询入库通知单时发生错误");
            return StatusCode(500, new ApiResult<List<InboundNoticeDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据单据号获取入库通知单详情（含表身明细）
    /// </summary>
    [HttpGet("{tzNo}")]
    public async Task<ActionResult<ApiResult<InboundNoticeDetailDto>>> GetDetail(string tzNo)
    {
        try
        {
            var header = await _context.MfRktzs.FindAsync(tzNo);
            if (header == null)
                return NotFound(new ApiResult<InboundNoticeDetailDto>
                {
                    Success = false,
                    Message = "未找到该入库通知单"
                });

            var details = await _context.TfRktzs
                .Where(t => t.TZ_NO == tzNo)
                .OrderBy(t => t.ITM)
                .Select(t => new TfRktzDto
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

            return Ok(new ApiResult<InboundNoticeDetailDto>
            {
                Success = true,
                Data = new InboundNoticeDetailDto
                {
                    Header = MySqlDateHelper.SafeConvert(header),
                    Details = details
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取入库通知单详情时发生错误");
            return StatusCode(500, new ApiResult<InboundNoticeDetailDto>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>入库通知单列表行数据（对应前端表格）</summary>
public class InboundNoticeDto
{
    public int ItemNo { get; set; }
    public DateTime DocumentDate { get; set; }
    public string DocumentNumber { get; set; } = "";
    public string WarehouseCode { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string VendorCode { get; set; } = "";
    public string VendorName { get; set; } = "";
    public string ApplyOrderNumber { get; set; } = "";
    public string ApplyDocType { get; set; } = "";
    public string OrderNumber { get; set; } = "";
    public bool IsClosed { get; set; }
    // 表身明细字段
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdSpec { get; set; } = "";
    public decimal Qty { get; set; }
    public string Unit { get; set; } = "";
}

/// <summary>入库通知单详情（含表头+表身）</summary>
public class InboundNoticeDetailDto
{
    public MfRktz Header { get; set; } = new();
    public List<TfRktzDto> Details { get; set; } = new();
}

/// <summary>表身明细DTO</summary>
public class TfRktzDto
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
