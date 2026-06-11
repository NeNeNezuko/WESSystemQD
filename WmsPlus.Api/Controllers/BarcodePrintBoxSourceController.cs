using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BarcodePrintBoxSourceController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<BarcodePrintBoxSourceController> _logger;

    public BarcodePrintBoxSourceController(WarehouseDbContext context, ILogger<BarcodePrintBoxSourceController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<BarcodePrintBoxSourceDto>>>> Search(
        [FromQuery] string? sourceDoc,
        [FromQuery] string? sourceNo,
        [FromQuery] string? prdNo,
        [FromQuery] string? cusName)
    {
        try
        {
            var query = _context.PrdtBarcodeBoxes.AsQueryable();
            if (!string.IsNullOrWhiteSpace(sourceDoc)) query = query.Where(x => x.SOURCE_DOC != null && x.SOURCE_DOC.Contains(sourceDoc));
            if (!string.IsNullOrWhiteSpace(sourceNo)) query = query.Where(x => x.SOURCE_NO != null && x.SOURCE_NO.Contains(sourceNo));
            if (!string.IsNullOrWhiteSpace(prdNo)) query = query.Where(x => x.PRD_NO != null && x.PRD_NO.Contains(prdNo));
            if (!string.IsNullOrWhiteSpace(cusName)) query = query.Where(x => x.CUS_NAME != null && x.CUS_NAME.Contains(cusName));

            var list = await query.Select(x => new BarcodePrintBoxSourceDto
            {
                SeqNo = x.SEQ_NO ?? 0, PrdNo = x.PRD_NO ?? "", PrdName = x.PRD_NAME ?? "", BatNo = x.BAT_NO ?? "",
                OrigQty = x.ORIG_QTY.HasValue ? (int?)x.ORIG_QTY.Value : null, PrintedQty = x.PRINTED_QTY.HasValue ? (int?)x.PRINTED_QTY.Value : null, ThisPrintQty = x.THIS_PRINT_QTY.HasValue ? (int?)x.THIS_PRINT_QTY.Value : null,
                StandardBoxQty = x.STANDARD_BOX_QTY.HasValue ? (int?)x.STANDARD_BOX_QTY.Value : null, TailBoxQty = x.TAIL_BOX_QTY.HasValue ? (int?)x.TAIL_BOX_QTY.Value : null, LabelCount = x.LABEL_COUNT
            }).ToListAsync();

            return Ok(new ApiResult<List<BarcodePrintBoxSourceDto>> { Success = true, Data = list, Total = list.Count });
        }
        catch (Exception ex) { _logger.LogError(ex, "查询依来源单打印箱条码时发生错误"); return StatusCode(500, new ApiResult<List<BarcodePrintBoxSourceDto>> { Success = false, Message = $"服务器内部错误: {ex.Message}" }); }
    }
}

public class BarcodePrintBoxSourceDto
{
    public int SeqNo { get; set; }
    public string PrdNo { get; set; } = ""; public string PrdName { get; set; } = ""; public string BatNo { get; set; } = "";
    public int? OrigQty { get; set; } public int? PrintedQty { get; set; } public int? ThisPrintQty { get; set; }
    public int? StandardBoxQty { get; set; } public int? TailBoxQty { get; set; } public int? LabelCount { get; set; }
}
