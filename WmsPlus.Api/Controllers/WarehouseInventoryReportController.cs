using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;
using WmsPlus.Api.Models.Entities;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WarehouseInventoryReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<WarehouseInventoryReportController> _logger;

    public WarehouseInventoryReportController(WarehouseDbContext context, ILogger<WarehouseInventoryReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<WarehouseInventoryReportDto>>>> Search(
        [FromQuery] string? warehouseCode,
        [FromQuery] string? prdNo,
        [FromQuery] string? batNo)
    {
        try
        {
            var query = from p in _context.Set<Prdt1>() select p;

            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.WH != null && x.WH.Contains(warehouseCode));
            if (!string.IsNullOrWhiteSpace(prdNo))
                query = query.Where(x => x.PRD_NO != null && x.PRD_NO.Contains(prdNo));
            if (!string.IsNullOrWhiteSpace(batNo))
                query = query.Where(x => x.BAT_NO != null && x.BAT_NO.Contains(batNo));

            var rawList = await query.OrderBy(x => x.WH).ThenBy(x => x.PRD_NO).ThenBy(x => x.BAT_NO).Take(2000).ToListAsync();

            var list = rawList.Select(x => new WarehouseInventoryReportDto
            {
                Wh = x.WH ?? "",
                PrdNo = x.PRD_NO ?? "",
                PrdMark = x.PRD_MARK ?? "",
                BatNo = x.BAT_NO ?? "",
                ValidDd = x.VALID_DD,
                QtyIn = x.QTY_IN,
                QtyOut = x.QTY_OUT,
                QtyPk = x.QTY_PK,
                QtyQc = x.QTY_QC,
                QtyTy = x.QTY_TY,
                QtyUo = x.QTY_UO,
                QtyUp = x.QTY_UP,
                QtyBc = x.QTY_BC,
                Qty1In = x.QTY1_IN,
                Qty1Out = x.QTY1_OUT,
                Qty1Pk = x.QTY1_PK,
                LockId = x.LOCK_ID ?? "",
                LstInd = x.LST_IND,
                LstOtd = x.LST_OTD,
                LstTyd = x.LST_TYD,
                InsertDd = x.INSERT_DD
            }).ToList();

            return Ok(new ApiResult<List<WarehouseInventoryReportDto>> { Success = true, Data = list, Total = list.Count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询仓库库存表时发生错误");
            return StatusCode(500, new ApiResult<List<WarehouseInventoryReportDto>> { Success = false, Message = $"服务器内部错误: {ex.Message}" });
        }
    }
}

public class WarehouseInventoryReportDto
{
    public string Wh { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public string BatNo { get; set; } = "";
    public DateTime? ValidDd { get; set; }
    public decimal? QtyIn { get; set; }
    public decimal? QtyOut { get; set; }
    public decimal? QtyPk { get; set; }
    public decimal? QtyQc { get; set; }
    public decimal? QtyTy { get; set; }
    public decimal? QtyUo { get; set; }
    public decimal? QtyUp { get; set; }
    public decimal? QtyBc { get; set; }
    public decimal? Qty1In { get; set; }
    public decimal? Qty1Out { get; set; }
    public decimal? Qty1Pk { get; set; }
    public string LockId { get; set; } = "";
    public DateTime? LstInd { get; set; }
    public DateTime? LstOtd { get; set; }
    public DateTime? LstTyd { get; set; }
    public DateTime? InsertDd { get; set; }
}
