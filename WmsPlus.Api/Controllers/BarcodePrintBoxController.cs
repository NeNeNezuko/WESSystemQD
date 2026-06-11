using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BarcodePrintBoxController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<BarcodePrintBoxController> _logger;

    public BarcodePrintBoxController(WarehouseDbContext context, ILogger<BarcodePrintBoxController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<BarcodePrintBoxDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] DateTime? lastPrintTimeFrom,
        [FromQuery] DateTime? lastPrintTimeTo,
        [FromQuery] string? sourceNo,
        [FromQuery] string? prdNo,
        [FromQuery] string? batNo,
        [FromQuery] string? inputUser,
        [FromQuery] string? cusNo,
        [FromQuery] string? boxBarcodeFrom,
        [FromQuery] string? boxBarcodeTo,
        [FromQuery] bool showEmptyOnly,
        [FromQuery] bool showSpecialInspect,
        [FromQuery] string? outerBoxFlag)
    {
        try
        {
            var query = _context.PrdtBarcodeBoxes.AsQueryable();

            if (dateFrom.HasValue) query = query.Where(x => x.CREATE_DD >= dateFrom.Value);
            if (dateTo.HasValue) query = query.Where(x => x.CREATE_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
            if (lastPrintTimeFrom.HasValue) query = query.Where(x => x.LAST_PRINT_TIME >= lastPrintTimeFrom.Value);
            if (lastPrintTimeTo.HasValue) query = query.Where(x => x.LAST_PRINT_TIME <= lastPrintTimeTo.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(sourceNo)) query = query.Where(x => x.SOURCE_NO != null && x.SOURCE_NO.Contains(sourceNo));
            if (!string.IsNullOrWhiteSpace(prdNo)) query = query.Where(x => x.PRD_NO != null && x.PRD_NO.Contains(prdNo));
            if (!string.IsNullOrWhiteSpace(batNo)) query = query.Where(x => x.BAT_NO != null && x.BAT_NO.Contains(batNo));
            if (!string.IsNullOrWhiteSpace(inputUser)) query = query.Where(x => x.INPUT_USR != null && x.INPUT_USR.Contains(inputUser));
            if (!string.IsNullOrWhiteSpace(cusNo)) query = query.Where(x => x.CUS_NO != null && x.CUS_NO.Contains(cusNo));
            if (!string.IsNullOrWhiteSpace(boxBarcodeFrom)) query = query.Where(x => x.BOX_BARCODE != null && string.Compare(x.BOX_BARCODE, boxBarcodeFrom) >= 0);
            if (!string.IsNullOrWhiteSpace(boxBarcodeTo)) query = query.Where(x => x.BOX_BARCODE != null && string.Compare(x.BOX_BARCODE, boxBarcodeTo) <= 0);
            if (!string.IsNullOrWhiteSpace(outerBoxFlag)) query = query.Where(x => x.OUTER_BOX_FLAG == outerBoxFlag);

            var list = await query.Select(x => new BarcodePrintBoxDto{
                ScanCode = x.SCAN_CODE ?? "", BoxBarcode = x.BOX_BARCODE ?? "", PrdNo = x.PRD_NO ?? "", PrdName = x.PRD_NAME ?? "",
                BatNo = x.BAT_NO ?? "", QTY = x.QTY, SourceNo = x.SOURCE_NO ?? "", SourceItm = x.SOURCE_ITM, ValidDate = x.VALID_DATE,
                ChangeHistory = x.CHANGE_HISTORY ?? "", LastPrintTime = x.LAST_PRINT_TIME
            }).ToListAsync();

            return Ok(new ApiResult<List<BarcodePrintBoxDto>>{ Success=true, Data=list, Total=list.Count });
        }
        catch (Exception ex){ _logger.LogError(ex,"查询箱条码标签打印时发生错误"); return StatusCode(500,new ApiResult<List<BarcodePrintBoxDto>>{ Success=false, Message=$"服务器内部错误: {ex.Message}" });}
    }
}

public class BarcodePrintBoxDto{
    public string ScanCode{get;set;}=""; public string BoxBarcode{get;set;}=""; public string PrdNo{get;set;}=""; public string PrdName{get;set;}="";
    public string BatNo{get;set;}=""; public decimal? QTY{get;set;} public string SourceNo{get;set;}=""; public int? SourceItm{get;set;}
    public DateTime? ValidDate{get;set;} public string ChangeHistory{get;set;}=""; public DateTime? LastPrintTime{get;set;}
}
