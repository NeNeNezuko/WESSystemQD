using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BarcodePrintProductSourceController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<BarcodePrintProductSourceController> _logger;

    public BarcodePrintProductSourceController(WarehouseDbContext context, ILogger<BarcodePrintProductSourceController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<BarcodePrintProductSourceDto>>>> Search(
        [FromQuery] string? sourceDoc,
        [FromQuery] string? sourceNo,
        [FromQuery] string? prdNo,
        [FromQuery] string? cusName)
    {
        try
        {
            var query = _context.PrdtBarcodes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(sourceDoc))
                query = query.Where(x => x.SOURCE_DOC != null && x.SOURCE_DOC.Contains(sourceDoc));
            if (!string.IsNullOrWhiteSpace(sourceNo))
                query = query.Where(x => x.SOURCE_NO != null && x.SOURCE_NO.Contains(sourceNo));
            if (!string.IsNullOrWhiteSpace(prdNo))
                query = query.Where(x => x.PRD_NO != null && x.PRD_NO.Contains(prdNo));
            if (!string.IsNullOrWhiteSpace(cusName))
                query = query.Where(x => x.CUS_NAME != null && x.CUS_NAME.Contains(cusName));

            var list = await query
                .Select(x => new BarcodePrintProductSourceDto
                {
                    SeqNo = x.SEQ_NO ?? 0,
                    PrdNo = x.PRD_NO ?? "",
                    PrdName = x.PRD_NAME ?? "",
                    BatNo = x.BAT_NO ?? "",
                    OrigQty = x.ORIG_QTY.HasValue ? (int?)x.ORIG_QTY.Value : null,
                    PrintedQty = x.PRINTED_QTY.HasValue ? (int?)x.PRINTED_QTY.Value : null,
                    LabelCount = x.LABEL_COUNT,
                    PrintBarcode = x.PRINT_BARCODE ?? "",
                    CusNo = x.CUS_NO ?? "",
                    SoNo = x.SO_NO ?? ""
                })
                .ToListAsync();

            return Ok(new ApiResult<List<BarcodePrintProductSourceDto>> { Success = true, Data = list, Total = list.Count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询依来源单打印货品条码时发生错误");
            return StatusCode(500, new ApiResult<List<BarcodePrintProductSourceDto>> { Success = false, Message = $"服务器内部错误: {ex.Message}" });
        }
    }
}

public class BarcodePrintProductSourceDto
{
    public int SeqNo { get; set; }
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string BatNo { get; set; } = "";
    public int? OrigQty { get; set; }
    public int? PrintedQty { get; set; }
    public int? LabelCount { get; set; }
    public string PrintBarcode { get; set; } = "";
    public string CusNo { get; set; } = "";
    public string SoNo { get; set; } = "";
}
