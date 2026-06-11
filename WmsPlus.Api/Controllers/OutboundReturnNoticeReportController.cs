using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OutboundReturnNoticeReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<OutboundReturnNoticeReportController> _logger;

    public OutboundReturnNoticeReportController(WarehouseDbContext context, ILogger<OutboundReturnNoticeReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询出库退回通知单报表（支持明细表/统计表两种视图）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<object>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? returnNumber,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? closeStatus,
        [FromQuery] string? viewType = "detail")
    {
        try
        {
            if (viewType == "summary")
            {
                return await SearchSummary(dateFrom, dateTo, returnNumber, warehouseCode, closeStatus);
            }
            else
            {
                return await SearchDetail(dateFrom, dateTo, returnNumber, warehouseCode, closeStatus);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询出库退回通知单报表时发生错误");
            return StatusCode(500, new ApiResult<List<object>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    private async Task<ActionResult<ApiResult<List<object>>>> SearchDetail(
        DateTime? dateFrom, DateTime? dateTo,
        string? returnNumber, string? warehouseCode, string? closeStatus)
    {
        var query = from t in _context.TfCktbs
                    join m in _context.MfCktbs on t.TB_NO equals m.TB_NO into mj
                    from m in mj.DefaultIfEmpty()
                    select new { T = t, M = m };

        if (dateFrom.HasValue)
            query = query.Where(x => x.M != null && x.M.TB_DD >= dateFrom.Value);
        if (dateTo.HasValue)
            query = query.Where(x => x.M != null && x.M.TB_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
        if (!string.IsNullOrWhiteSpace(returnNumber))
            query = query.Where(x => x.T.TB_NO.Contains(returnNumber));
        if (!string.IsNullOrWhiteSpace(warehouseCode))
            query = query.Where(x => x.M != null && (x.M.WH != null && x.M.WH.Contains(warehouseCode)));
        if (!string.IsNullOrWhiteSpace(closeStatus) && closeStatus != "全部")
        {
            var isClosed = closeStatus == "已结案" ? "Y" : "N";
            query = query.Where(x => x.M != null && x.M.CLS_ID == isClosed);
        }

        query = query.OrderBy(x => x.T.TB_NO).ThenBy(x => x.T.ITM);

        var rawList = await query.ToListAsync();

        var list = rawList.Select(x => new OutboundReturnDetailDto
        {
            ItemNo = x.T.ITM,
            TB_NO = x.T.TB_NO ?? "",
            TB_DD = x.M?.TB_DD,
            WH = x.M?.WH ?? "",
            DEP = x.M?.DEP ?? "",
            PRD_NO = x.T.PRD_NO ?? "",
            PRD_NAME = x.T.PRD_NAME ?? "",
            UNIT = x.T.UNIT ?? "",
            QTY = x.T.QTY ?? 0,
            BAT_NO = x.T.BAT_NO ?? "",
            REM = x.T.REM ?? ""
        }).Cast<object>().ToList();

        return Ok(new ApiResult<List<object>> { Success = true, Data = list, Total = list.Count });
    }

    private async Task<ActionResult<ApiResult<List<object>>>> SearchSummary(
        DateTime? dateFrom, DateTime? dateTo,
        string? returnNumber, string? warehouseCode, string? closeStatus)
    {
        var query = _context.MfCktbs.AsQueryable();

        if (dateFrom.HasValue)
            query = query.Where(m => m.TB_DD >= dateFrom.Value);
        if (dateTo.HasValue)
            query = query.Where(m => m.TB_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
        if (!string.IsNullOrWhiteSpace(returnNumber))
            query = query.Where(m => m.TB_NO.Contains(returnNumber));
        if (!string.IsNullOrWhiteSpace(warehouseCode))
            query = query.Where(m => m.WH != null && m.WH.Contains(warehouseCode));
        if (!string.IsNullOrWhiteSpace(closeStatus) && closeStatus != "全部")
        {
            var isClosed = closeStatus == "已结案" ? "Y" : "N";
            query = query.Where(m => m.CLS_ID == isClosed);
        }

        query = query.OrderBy(m => m.TB_NO);

        var rawList = await query.ToListAsync();

        var list = rawList.Select(m => new OutboundReturnSummaryDto
        {
            TB_NO = m.TB_NO,
            TB_DD = m.TB_DD,
            WH = m.WH ?? "",
            DEP = m.DEP ?? "",
            CUS_NO = m.CUS_NO ?? "",
            BIL_TYPE = m.BIL_TYPE ?? "",
            SAL_NO = m.SAL_NO ?? "",
            CLS_ID = m.CLS_ID ?? "",
            USR = m.USR ?? "",
            SYS_DATE = m.SYS_DATE
        }).Cast<object>().ToList();

        return Ok(new ApiResult<List<object>> { Success = true, Data = list, Total = list.Count });
    }
}

// ====== DTO 定义 ======

public class OutboundReturnDetailDto
{
    public int ItemNo { get; set; }
    public string TB_NO { get; set; } = "";
    public DateTime? TB_DD { get; set; }
    public string WH { get; set; } = "";
    public string DEP { get; set; } = "";
    public string PRD_NO { get; set; } = "";
    public string PRD_NAME { get; set; } = "";
    public string UNIT { get; set; } = "";
    public decimal QTY { get; set; }
    public string BAT_NO { get; set; } = "";
    public string REM { get; set; } = "";
}

public class OutboundReturnSummaryDto
{
    public string TB_NO { get; set; } = "";
    public DateTime? TB_DD { get; set; }
    public string WH { get; set; } = "";
    public string DEP { get; set; } = "";
    public string CUS_NO { get; set; } = "";
    public string BIL_TYPE { get; set; } = "";
    public string SAL_NO { get; set; } = "";
    public string CLS_ID { get; set; } = "";
    public string USR { get; set; } = "";
    public DateTime? SYS_DATE { get; set; }
}
