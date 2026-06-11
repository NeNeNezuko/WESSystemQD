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
public class MonthlyWhInventoryReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<MonthlyWhInventoryReportController> _logger;

    public MonthlyWhInventoryReportController(WarehouseDbContext context, ILogger<MonthlyWhInventoryReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<MonthlyWhInventoryReportDto>>>> Search(
        [FromQuery] string? year,
        [FromQuery] string? month,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? prdNo)
    {
        try
        {
            var query = from s in _context.Set<Sprd>() select s;

            if (!string.IsNullOrWhiteSpace(year) && int.TryParse(year, out var y))
                query = query.Where(x => x.YY == y);
            if (!string.IsNullOrWhiteSpace(month) && int.TryParse(month, out var m))
                query = query.Where(x => x.MM == m);
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.WH != null && x.WH.Contains(warehouseCode));
            if (!string.IsNullOrWhiteSpace(prdNo))
                query = query.Where(x => x.PRD_NO != null && x.PRD_NO.Contains(prdNo));

            var rawList = await query.OrderBy(x => x.WH).ThenBy(x => x.PRD_NO).ToListAsync();

            var list = rawList.Select(x => new MonthlyWhInventoryReportDto
            {
                Wh = x.WH ?? "",
                Yy = x.YY,
                Mm = x.MM,
                PrdNo = x.PRD_NO ?? "",
                PrdMark = x.PRD_MARK ?? "",
                BatNo = x.BAT_NO ?? "",
                ValidDd = x.VALID_DD,
                QtyIn = x.QTY_IN,
                QtyOut = x.QTY_OUT,
                Qty1In = x.QTY1_IN,
                Qty1Out = x.QTY1_OUT,
                LstInd = x.LST_IND,
                LstOtd = x.LST_OTD
            }).ToList();

            return Ok(new ApiResult<List<MonthlyWhInventoryReportDto>> { Success = true, Data = list, Total = list.Count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询每月仓库库存表时发生错误");
            return StatusCode(500, new ApiResult<List<MonthlyWhInventoryReportDto>> { Success = false, Message = $"服务器内部错误: {ex.Message}" });
        }
    }
}

public class MonthlyWhInventoryReportDto
{
    public string Wh { get; set; } = "";
    public int? Yy { get; set; }
    public int? Mm { get; set; }
    public string PrdNo { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public string BatNo { get; set; } = "";
    public DateTime? ValidDd { get; set; }
    public decimal? QtyIn { get; set; }
    public decimal? QtyOut { get; set; }
    public decimal? Qty1In { get; set; }
    public decimal? Qty1Out { get; set; }
    public DateTime? LstInd { get; set; }
    public DateTime? LstOtd { get; set; }
}
