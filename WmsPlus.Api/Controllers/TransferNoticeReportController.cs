using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransferNoticeReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<TransferNoticeReportController> _logger;

    public TransferNoticeReportController(WarehouseDbContext context, ILogger<TransferNoticeReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询调拨通知单报表（支持明细表/统计表两种视图）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<object>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? noticeNumber,
        [FromQuery] string? whOut,
        [FromQuery] string? whIn,
        [FromQuery] string? viewType = "detail")
    {
        try
        {
            if (viewType == "summary")
                return await SearchSummary(dateFrom, dateTo, noticeNumber, whOut, whIn);
            else
                return await SearchDetail(dateFrom, dateTo, noticeNumber, whOut, whIn);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询调拨通知单报表时发生错误");
            return StatusCode(500, new ApiResult<List<object>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    private async Task<ActionResult<ApiResult<List<object>>>> SearchDetail(
        DateTime? dateFrom, DateTime? dateTo,
        string? noticeNumber, string? whOut, string? whIn)
    {
        var query = from t in _context.TfIctzs
                    join m in _context.MfIctzs on t.TZ_NO equals m.TZ_NO into mj
                    from m in mj.DefaultIfEmpty()
                    select new { T = t, M = m };

        if (dateFrom.HasValue)
            query = query.Where(x => x.M != null && x.M.TZ_DD >= dateFrom.Value);
        if (dateTo.HasValue)
            query = query.Where(x => x.M != null && x.M.TZ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
        if (!string.IsNullOrWhiteSpace(noticeNumber))
            query = query.Where(x => x.T.TZ_NO.Contains(noticeNumber));
        if (!string.IsNullOrWhiteSpace(whOut))
            query = query.Where(x => x.M != null && (x.M.WH1 != null && x.M.WH1.Contains(whOut)));
        if (!string.IsNullOrWhiteSpace(whIn))
            query = query.Where(x => x.M != null && (x.M.WH2 != null && x.M.WH2.Contains(whIn)));

        query = query.OrderBy(x => x.T.TZ_NO).ThenBy(x => x.T.ITM);

        var rawList = await query.ToListAsync();

        var list = rawList.Select(x => new TransferNoticeDetailDto
        {
            ItemNo = x.T.ITM,
            TZ_NO = x.T.TZ_NO ?? "",
            TZ_DD = x.M?.TZ_DD,
            WH1 = x.T.WH1 ?? "",
            WH2 = x.T.WH2 ?? "",
            PRD_NO = x.T.PRD_NO ?? "",
            PRD_NAME = x.T.PRD_NAME ?? "",
            QTY = x.T.QTY ?? 0,
            REM = x.T.REM ?? ""
        }).Cast<object>().ToList();

        return Ok(new ApiResult<List<object>> { Success = true, Data = list, Total = list.Count });
    }

    private async Task<ActionResult<ApiResult<List<object>>>> SearchSummary(
        DateTime? dateFrom, DateTime? dateTo,
        string? noticeNumber, string? whOut, string? whIn)
    {
        var query = _context.MfIctzs.AsQueryable();

        if (dateFrom.HasValue)
            query = query.Where(m => m.TZ_DD >= dateFrom.Value);
        if (dateTo.HasValue)
            query = query.Where(m => m.TZ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
        if (!string.IsNullOrWhiteSpace(noticeNumber))
            query = query.Where(m => m.TZ_NO.Contains(noticeNumber));
        if (!string.IsNullOrWhiteSpace(whOut))
            query = query.Where(m => m.WH1 != null && m.WH1.Contains(whOut));
        if (!string.IsNullOrWhiteSpace(whIn))
            query = query.Where(m => m.WH2 != null && m.WH2.Contains(whIn));

        query = query.OrderBy(m => m.TZ_NO);

        var rawList = await query.ToListAsync();

        var list = rawList.Select(m => new TransferNoticeSummaryDto
        {
            TZ_NO = m.TZ_NO,
            TZ_DD = m.TZ_DD,
            DEP = m.DEP ?? "",
            WH1 = m.WH1 ?? "",
            WH2 = m.WH2 ?? "",
            SAL_NO = m.SAL_NO ?? "",
            EST_DD = m.EST_DD,
            AREA_SH = m.AREA_SH ?? "",
            CLS_ID_BC = m.CLS_ID_BC ?? "",
            CLS_ID_CK = m.CLS_ID_CK ?? "",
            TYPE_ID = m.TYPE_ID ?? "",
            SYS_DATE = m.SYS_DATE
        }).Cast<object>().ToList();

        return Ok(new ApiResult<List<object>> { Success = true, Data = list, Total = list.Count });
    }
}

// ====== DTO 定义 ======

public class TransferNoticeDetailDto
{
    public int ItemNo { get; set; }
    public string TZ_NO { get; set; } = "";
    public DateTime? TZ_DD { get; set; }
    public string WH1 { get; set; } = "";
    public string WH2 { get; set; } = "";
    public string PRD_NO { get; set; } = "";
    public string PRD_NAME { get; set; } = "";
    public decimal QTY { get; set; }
    public string REM { get; set; } = "";
}

public class TransferNoticeSummaryDto
{
    public string TZ_NO { get; set; } = "";
    public DateTime? TZ_DD { get; set; }
    public string DEP { get; set; } = "";
    public string WH1 { get; set; } = "";
    public string WH2 { get; set; } = "";
    public string SAL_NO { get; set; } = "";
    public DateTime? EST_DD { get; set; }
    public string AREA_SH { get; set; } = "";
    public string CLS_ID_BC { get; set; } = "";
    public string CLS_ID_CK { get; set; } = "";
    public string TYPE_ID { get; set; } = "";
    public DateTime? SYS_DATE { get; set; }
}
