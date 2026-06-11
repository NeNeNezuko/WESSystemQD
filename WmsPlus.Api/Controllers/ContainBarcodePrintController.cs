using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ContainBarcodePrintController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ContainBarcodePrintController> _logger;

    public ContainBarcodePrintController(WarehouseDbContext context, ILogger<ContainBarcodePrintController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询物流容器条码标签列表（查询MF_CONTAIN表）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ContainBarcodePrintDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? combineNo,
        [FromQuery] string? chuw,
        [FromQuery] string? containType,
        [FromQuery] string? containStatus,
        [FromQuery] string? transitFlag,
        [FromQuery] string? inspectFlag,
        [FromQuery] string? containCodeFrom,
        [FromQuery] string? containCodeTo,
        [FromQuery] DateTime? inventoryDateFrom,
        [FromQuery] DateTime? inventoryDateTo,
        [FromQuery] bool showEmptyOnly = false)
    {
        try
        {
            var query = from c in _context.MfContains
                        select c;

            // 打印日期范围筛选
            if (dateFrom.HasValue)
                query = query.Where(x => x.PRT_DATE >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.PRT_DATE <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 合单代号模糊匹配
            if (!string.IsNullOrWhiteSpace(combineNo))
                query = query.Where(x => x.COMBINE_NO != null && x.COMBINE_NO.Contains(combineNo));

            // 储位代号模糊匹配
            if (!string.IsNullOrWhiteSpace(chuw))
                query = query.Where(x => x.CHUW != null && x.CHUW.Contains(chuw));

            // 容器类型代号模糊匹配
            if (!string.IsNullOrWhiteSpace(containType))
                query = query.Where(x => x.CONTAIN_TYPE != null && x.CONTAIN_TYPE.Contains(containType));

            // 容器状态筛选
            if (!string.IsNullOrWhiteSpace(containStatus))
                query = query.Where(x => x.CONTAIN_STATUS == containStatus);

            // 在途标记筛选
            if (!string.IsNullOrWhiteSpace(transitFlag))
                query = query.Where(x => x.TRANSIT_FLAG == transitFlag);

            // 述检标记筛选
            if (!string.IsNullOrWhiteSpace(inspectFlag))
                query = query.Where(x => x.INSPECT_FLAG == inspectFlag);

            // 容器条码起止范围筛选
            if (!string.IsNullOrWhiteSpace(containCodeFrom))
                query = query.Where(x => x.CONTAIN_CODE != null && x.CONTAIN_CODE.CompareTo(containCodeFrom) >= 0);
            if (!string.IsNullOrWhiteSpace(containCodeTo))
                query = query.Where(x => x.CONTAIN_CODE != null && x.CONTAIN_CODE.CompareTo(containCodeTo) <= 0);

            // 盘点日期范围筛选
            if (inventoryDateFrom.HasValue)
                query = query.Where(x => x.INVENTORY_DATE >= inventoryDateFrom.Value);
            if (inventoryDateTo.HasValue)
                query = query.Where(x => x.INVENTORY_DATE <= inventoryDateTo.Value.AddDays(1).AddSeconds(-1));

            // 盘点为空列示
            if (showEmptyOnly)
                query = query.Where(x => string.IsNullOrEmpty(x.INVENTORY_QTY) || x.INVENTORY_QTY == "0");

            // 按容器条码排序
            query = query.OrderBy(x => x.CONTAIN_CODE);

            var rawList = await query.ToListAsync();

            var list = rawList.Select(x => new ContainBarcodePrintDto
            {
                ScanCode = x.SCAN_CODE ?? "",
                ContainCode = x.CONTAIN_CODE ?? "",
                ContainType = x.CONTAIN_TYPE ?? "",
                ContainStatus = x.CONTAIN_STATUS ?? "",
                WarehouseName = "",  // 需要关联MY_WH获取
                ChuwName = "",       // 需要关联储位表获取
                ChuwPos = x.CHUW_POS ?? "",
                TransitFlag = x.TRANSIT_FLAG ?? "",
                InspectFlag = x.INSPECT_FLAG ?? "",
                ContainDetail = x.CONTAIN_DETAIL ?? "",
                ModifyHistory = x.MODIFY_HISTORY ?? ""
            }).ToList();

            return Ok(new ApiResult<List<ContainBarcodePrintDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询物流容器条码标签时发生错误");
            return StatusCode(500, new ApiResult<List<ContainBarcodePrintDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>物流容器条码标签列表行数据</summary>
public class ContainBarcodePrintDto
{
    public string ScanCode { get; set; } = "";
    public string ContainCode { get; set; } = "";
    public string ContainType { get; set; } = "";
    public string ContainStatus { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string ChuwName { get; set; } = "";
    public string ChuwPos { get; set; } = "";
    public string TransitFlag { get; set; } = "";
    public string InspectFlag { get; set; } = "";
    public string ContainDetail { get; set; } = "";
    public string ModifyHistory { get; set; } = "";
}
