using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BarcodePrintSerialController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<BarcodePrintSerialController> _logger;

    public BarcodePrintSerialController(WarehouseDbContext context, ILogger<BarcodePrintSerialController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<BarcodePrintSerialDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] DateTime? lastPrintTimeFrom,
        [FromQuery] DateTime? lastPrintTimeTo,
        [FromQuery] string? sourceNo,
        [FromQuery] string? prdNo,
        [FromQuery] string? batNo,
        [FromQuery] string? inputUser,
        [FromQuery] string? cusNo,
        [FromQuery] string? serialFrom,
        [FromQuery] string? serialTo,
        [FromQuery] bool showEmptyOnly)
    {
        try
        {
            var query = _context.BarRecs.AsQueryable();
            if (dateFrom.HasValue) query = query.Where(x => x.CREATE_DD >= dateFrom.Value);
            if (dateTo.HasValue) query = query.Where(x => x.CREATE_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
            if (lastPrintTimeFrom.HasValue) query = query.Where(x => x.LAST_PRINT_TIME >= lastPrintTimeFrom.Value);
            if (lastPrintTimeTo.HasValue) query = query.Where(x => x.LAST_PRINT_TIME <= lastPrintTimeTo.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(sourceNo)) query = query.Where(x => x.SOURCE_NO != null && x.SOURCE_NO.Contains(sourceNo));
            if (!string.IsNullOrWhiteSpace(prdNo)) query = query.Where(x => x.PRD_NO != null && x.PRD_NO.Contains(prdNo));
            if (!string.IsNullOrWhiteSpace(batNo)) query = query.Where(x => x.BAT_NO != null && x.BAT_NO.Contains(batNo));
            if (!string.IsNullOrWhiteSpace(inputUser)) query = query.Where(x => x.INPUT_USR != null && x.INPUT_USR.Contains(inputUser));
            if (!string.IsNullOrWhiteSpace(cusNo)) query = query.Where(x => x.CUS_NO != null && x.CUS_NO.Contains(cusNo));
            if (!string.IsNullOrWhiteSpace(serialFrom)) query = query.Where(x => x.SERIAL_NO != null && string.Compare(x.SERIAL_NO, serialFrom) >= 0);
            if (!string.IsNullOrWhiteSpace(serialTo)) query = query.Where(x => x.SERIAL_NO != null && string.Compare(x.SERIAL_NO, serialTo) <= 0);

            var list = await query.Select(x => new BarcodePrintSerialDto{
                ScanCode = x.SCAN_CODE ?? "", SerialNo = x.SERIAL_NO ?? "", PrdNo = x.PRD_NO ?? "", PrdName = x.PRD_NAME ?? "",
                BatNo = x.BAT_NO ?? "", SourceNo = x.SOURCE_NO ?? "", SourceItm = x.SOURCE_ITM, ValidDate = x.VALID_DATE, LastPrintTime = x.LAST_PRINT_TIME
            }).ToListAsync();

            return Ok(new ApiResult<List<BarcodePrintSerialDto>>{ Success=true, Data=list, Total=list.Count });
        }
        catch (Exception ex){ _logger.LogError(ex,"查询序列号标签打印时发生错误"); return StatusCode(500,new ApiResult<List<BarcodePrintSerialDto>>{Success=false,Message=$"服务器内部错误: {ex.Message}"});}
    }
}

public class BarcodePrintSerialDto{
    public string ScanCode{get;set;}=""; public string SerialNo{get;set;}=""; public string PrdNo{get;set;}=""; public string PrdName{get;set;}="";
    public string BatNo{get;set;}=""; public string SourceNo{get;set;}=""; public int? SourceItm{get;set;}
    public DateTime? ValidDate{get;set;} public DateTime? LastPrintTime{get;set;}
}
