using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BarcodePrintProductController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<BarcodePrintProductController> _logger;

    public BarcodePrintProductController(WarehouseDbContext context, ILogger<BarcodePrintProductController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询货品条码标签打印列表
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<BarcodePrintProductDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] DateTime? lastPrintTimeFrom,
        [FromQuery] DateTime? lastPrintTimeTo,
        [FromQuery] string? sourceNo,
        [FromQuery] string? prdNo,
        [FromQuery] string? batNo,
        [FromQuery] string? inputUser,
        [FromQuery] string? inputBatch,
        [FromQuery] string? cusNo,
        [FromQuery] string? barcodeFrom,
        [FromQuery] string? barcodeTo)
    {
        try
        {
            var query = _context.PrdtBarcodes.AsQueryable();

            if (dateFrom.HasValue)
                query = query.Where(x => x.CREATE_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.CREATE_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
            if (lastPrintTimeFrom.HasValue)
                query = query.Where(x => x.LAST_PRINT_TIME >= lastPrintTimeFrom.Value);
            if (lastPrintTimeTo.HasValue)
                query = query.Where(x => x.LAST_PRINT_TIME <= lastPrintTimeTo.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(sourceNo))
                query = query.Where(x => x.SOURCE_NO != null && x.SOURCE_NO.Contains(sourceNo));
            if (!string.IsNullOrWhiteSpace(prdNo))
                query = query.Where(x => x.PRD_NO != null && x.PRD_NO.Contains(prdNo));
            if (!string.IsNullOrWhiteSpace(batNo))
                query = query.Where(x => x.BAT_NO != null && x.BAT_NO.Contains(batNo));
            if (!string.IsNullOrWhiteSpace(inputUser))
                query = query.Where(x => x.INPUT_USR != null && x.INPUT_USR.Contains(inputUser));
            if (!string.IsNullOrWhiteSpace(inputBatch))
                query = query.Where(x => x.INPUT_BATCH != null && x.INPUT_BATCH.Contains(inputBatch));
            if (!string.IsNullOrWhiteSpace(cusNo))
                query = query.Where(x => x.CUS_NO != null && x.CUS_NO.Contains(cusNo));
            if (!string.IsNullOrWhiteSpace(barcodeFrom))
                query = query.Where(x => x.BARCODE != null && string.Compare(x.BARCODE, barcodeFrom) >= 0);
            if (!string.IsNullOrWhiteSpace(barcodeTo))
                query = query.Where(x => x.BARCODE != null && string.Compare(x.BARCODE, barcodeTo) <= 0);

            var list = await query
                .Select(x => new BarcodePrintProductDto
                {
                    ScanCode = x.SCAN_CODE ?? "",
                    Barcode = x.BARCODE ?? "",
                    PrdNo = x.PRD_NO ?? "",
                    PrdName = x.PRD_NAME ?? "",
                    BatNo = x.BAT_NO ?? "",
                    SourceNo = x.SOURCE_NO ?? "",
                    SourceDoc = x.SOURCE_DOC ?? "",
                    ValidDate = x.VALID_DATE,
                    LastPrintTime = x.LAST_PRINT_TIME
                })
                .ToListAsync();

            return Ok(new ApiResult<List<BarcodePrintProductDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询货品条码标签打印时发生错误");
            return StatusCode(500, new ApiResult<List<BarcodePrintProductDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

/// <summary>货品条码标签打印DTO</summary>
public class BarcodePrintProductDto
{
    public string ScanCode { get; set; } = "";
    public string Barcode { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string BatNo { get; set; } = "";
    public string SourceNo { get; set; } = "";
    public string SourceDoc { get; set; } = "";
    public DateTime? ValidDate { get; set; }
    public DateTime? LastPrintTime { get; set; }
}
