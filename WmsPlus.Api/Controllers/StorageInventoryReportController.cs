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
public class StorageInventoryReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<StorageInventoryReportController> _logger;

    public StorageInventoryReportController(WarehouseDbContext context, ILogger<StorageInventoryReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<StorageInventoryReportDto>>>> Search(
        [FromQuery] string? warehouseCode,
        [FromQuery] string? chuw,
        [FromQuery] string? prdNo,
        [FromQuery] string? batNo)
    {
        try
        {
            var query = from p in _context.Set<Prdt1Cw>() select p;

            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.WH != null && x.WH.Contains(warehouseCode));
            if (!string.IsNullOrWhiteSpace(chuw))
                query = query.Where(x => x.CHUW != null && x.CHUW.Contains(chuw));
            if (!string.IsNullOrWhiteSpace(prdNo))
                query = query.Where(x => x.PRD_NO != null && x.PRD_NO.Contains(prdNo));
            if (!string.IsNullOrWhiteSpace(batNo))
                query = query.Where(x => x.BAT_NO != null && x.BAT_NO.Contains(batNo));

            var rawList = await query.OrderBy(x => x.WH).ThenBy(x => x.CHUW).ThenBy(x => x.PRD_NO).Take(2000).ToListAsync();

            var list = rawList.Select(x => new StorageInventoryReportDto
            {
                Wh = x.WH ?? "",
                Chuw = x.CHUW ?? "",
                PrdNo = x.PRD_NO ?? "",
                PrdMark = x.PRD_MARK ?? "",
                BatNo = x.BAT_NO ?? "",
                ValidDd = x.VALID_DD,
                QtyIn = x.QTY_IN,
                QtyOut = x.QTY_OUT,
                QtyPk = x.QTY_PK,
                QtyTy = x.QTY_TY,
                QtyBc = x.QTY_BC,
                Qty1In = x.QTY1_IN,
                Qty1Out = x.QTY1_OUT,
                Qty1Pk = x.QTY1_PK,
                Qty1Ty = x.QTY1_TY,
                LstInd = x.LST_IND,
                LstOtd = x.LST_OTD,
                LstTyd = x.LST_TYD,
                InsertDd = x.INSERT_DD
            }).ToList();

            return Ok(new ApiResult<List<StorageInventoryReportDto>> { Success = true, Data = list, Total = list.Count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询储位库存表时发生错误");
            return StatusCode(500, new ApiResult<List<StorageInventoryReportDto>> { Success = false, Message = $"服务器内部错误: {ex.Message}" });
        }
    }
}

public class StorageInventoryReportDto
{
    public string Wh { get; set; } = "";
    public string Chuw { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public string BatNo { get; set; } = "";
    public DateTime? ValidDd { get; set; }
    public decimal? QtyIn { get; set; }
    public decimal? QtyOut { get; set; }
    public decimal? QtyPk { get; set; }
    public decimal? QtyTy { get; set; }
    public decimal? QtyBc { get; set; }
    public decimal? Qty1In { get; set; }
    public decimal? Qty1Out { get; set; }
    public decimal? Qty1Pk { get; set; }
    public decimal? Qty1Ty { get; set; }
    public DateTime? LstInd { get; set; }
    public DateTime? LstOtd { get; set; }
    public DateTime? LstTyd { get; set; }
    public DateTime? InsertDd { get; set; }
}
