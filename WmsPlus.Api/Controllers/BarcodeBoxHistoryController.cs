using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BarcodeBoxHistoryController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<BarcodeBoxHistoryController> _logger;

    public BarcodeBoxHistoryController(WarehouseDbContext context, ILogger<BarcodeBoxHistoryController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<BarcodeBoxHistoryDto>>>> Search(
        [FromQuery] DateTime? changeDateFrom,
        [FromQuery] DateTime? changeDateTo,
        [FromQuery] string? prdNo,
        [FromQuery] string? boxBarcodeFrom,
        [FromQuery] string? boxBarcodeTo)
    {
        try
        {
            var query = _context.BarBoxChanges.AsQueryable();
            if (changeDateFrom.HasValue) query = query.Where(x => x.CHANGE_TIME >= changeDateFrom.Value);
            if (changeDateTo.HasValue) query = query.Where(x => x.CHANGE_TIME <= changeDateTo.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(prdNo)) query = query.Where(x => x.PRD_NO != null && x.PRD_NO.Contains(prdNo));
            if (!string.IsNullOrWhiteSpace(boxBarcodeFrom)) query = query.Where(x => x.BOX_BARCODE != null && string.Compare(x.BOX_BARCODE, boxBarcodeFrom) >= 0);
            if (!string.IsNullOrWhiteSpace(boxBarcodeTo)) query = query.Where(x => x.BOX_BARCODE != null && string.Compare(x.BOX_BARCODE, boxBarcodeTo) <= 0);

            var list = await query.Select(x => new BarcodeBoxHistoryDto{
                SeqNo = x.SEQ_NO ?? 0, ChangeTime = x.CHANGE_TIME, BoxBarcode = x.BOX_BARCODE ?? "",
                PrdNo = x.PRD_NO ?? "", PrdName = x.PRD_NAME ?? "", BatNo = x.BAT_NO ?? "",
                SourceDocType = x.SOURCE_DOC_TYPE ?? "", DocName = x.DOC_NAME ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<BarcodeBoxHistoryDto>>{ Success=true, Data=list, Total=list.Count });
        }
        catch (Exception ex){ _logger.LogError(ex,"查询箱条码变动历史时发生错误"); return StatusCode(500,new ApiResult<List<BarcodeBoxHistoryDto>>{Success=false,Message=$"服务器内部错误: {ex.Message}"});}
    }
}

public class BarcodeBoxHistoryDto{
    public int SeqNo{get;set;} public DateTime? ChangeTime{get;set;} public string BoxBarcode{get;set;}="";
    public string PrdNo{get;set;}=""; public string PrdName{get;set;}=""; public string BatNo{get;set;}="";
    public string SourceDocType{get;set;}=""; public string DocName{get;set;}="";
}
