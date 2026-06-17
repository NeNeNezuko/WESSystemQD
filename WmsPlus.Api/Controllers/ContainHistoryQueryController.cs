using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ContainHistoryQueryController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ContainHistoryQueryController> _logger;

    public ContainHistoryQueryController(WarehouseDbContext context, ILogger<ContainHistoryQueryController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询物流容器变动历史列表（查询MF_CONTAIN_HIS表）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ContainHistoryQueryDto>>>> Search(
        [FromQuery] DateTime? changeTimeFrom,
        [FromQuery] DateTime? changeTimeTo,
        [FromQuery] string? containCode)
    {
        try
        {
            var query = from h in _context.MfContainHis
                        select h;

            // 变动时间范围筛选
            if (changeTimeFrom.HasValue)
                query = query.Where(x => x.CHANGE_TIME >= changeTimeFrom.Value);
            if (changeTimeTo.HasValue)
                query = query.Where(x => x.CHANGE_TIME <= changeTimeTo.Value.AddDays(1).AddSeconds(-1));

            // 容器条码模糊匹配
            if (!string.IsNullOrWhiteSpace(containCode))
                query = query.Where(x => x.CONTAIN_CODE != null && x.CONTAIN_CODE.Contains(containCode));

            // 按变动时间倒序排序（最新的在前）
            query = query.OrderByDescending(x => x.CHANGE_TIME);

            var rawList = await query.ToListAsync();

            var list = rawList.Select(x => new ContainHistoryQueryDto
            {
                ContainCode = x.CONTAIN_CODE ?? "",
                ContainStatus = x.CONTAIN_STATUS ?? "",
                ContainType = x.CONTAIN_TYPE ?? "",
                WarehouseName = "",
                TransitFlag = x.TRANSIT_FLAG ?? "",
                InspectFlag = x.INSPECT_FLAG ?? "",
                ChangeDocName = x.CHANGE_DOC_NAME ?? "",
                ChangeNo = x.CHANGE_NO ?? "",
                ChangeMan = x.CHANGE_MAN ?? "",
                ChangeTime = x.CHANGE_TIME,
                ScatterItm = x.SCATTER_ITM ?? "",
                BarcodeType = x.BARCODE_TYPE ?? "",
                Barcode = x.BARCODE ?? "",
                PieceCount = x.PIECE_COUNT ?? 0,
                ScatterQty = x.SCATTER_QTY ?? 0,
                OuterBoxCode = x.OUTER_BOX_CODE ?? "",
                CwCode = x.CW_CODE ?? "",
                CwName = x.CW_NAME ?? "",
                CwPosition = x.CW_POSITION ?? "",
                PickFlag = x.PICK_FLAG ?? "",
                IsScatter = x.IS_SCATTER ?? ""
            }).ToList();

            return Ok(new ApiResult<List<ContainHistoryQueryDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询物流容器变动历史时发生错误");
            return StatusCode(500, new ApiResult<List<ContainHistoryQueryDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>物流容器变动历史列表行数据</summary>
public class ContainHistoryQueryDto
{
    public string ContainCode { get; set; } = "";
    public string ContainStatus { get; set; } = "";
    public string ContainType { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string TransitFlag { get; set; } = "";
    public string InspectFlag { get; set; } = "";
    public string ChangeDocName { get; set; } = "";
    public string ChangeNo { get; set; } = "";
    public string ChangeMan { get; set; } = "";
    public DateTime? ChangeTime { get; set; }
    public string ScatterItm { get; set; } = "";
    public string BarcodeType { get; set; } = "";
    public string Barcode { get; set; } = "";
    public decimal PieceCount { get; set; }
    public decimal ScatterQty { get; set; }
    public string OuterBoxCode { get; set; } = "";
    public string CwCode { get; set; } = "";
    public string CwName { get; set; } = "";
    public string CwPosition { get; set; } = "";
    public string PickFlag { get; set; } = "";
    public string IsScatter { get; set; } = "";
}
