using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SecondarySortReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<SecondarySortReportController> _logger;

    public SecondarySortReportController(WarehouseDbContext context, ILogger<SecondarySortReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询二次分拣单报表（明细表以TF_PKFJ表身为主，统计表以MF_PKFJ表头为主）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<SecondarySortReportDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? pkfjNo,
        [FromQuery] string? wh,
        [FromQuery] string? tabType = "detail")
    {
        try
        {
            if (tabType == "summary")
            {
                // 统计表：以MF_PKFJ表头为主
                var headerQuery = _context.MfPkfjs.AsQueryable();

                if (dateFrom.HasValue)
                    headerQuery = headerQuery.Where(m => m.PKFJ_DD >= dateFrom.Value);
                if (dateTo.HasValue)
                    headerQuery = headerQuery.Where(m => m.PKFJ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
                if (!string.IsNullOrWhiteSpace(pkfjNo))
                    headerQuery = headerQuery.Where(m => m.PKFJ_NO != null && m.PKFJ_NO.Contains(pkfjNo));

                headerQuery = headerQuery.OrderBy(m => m.PKFJ_NO);

                var headerList = await headerQuery.ToListAsync();
                var list = headerList.Select(m => new SecondarySortReportDto
                {
                    ItemNo = 0,
                    PkfjNo = m.PKFJ_NO ?? "",
                    PkfjDd = m.PKFJ_DD,
                    Wh = "",
                    Dep = m.DEP ?? "",
                    ClsId = m.CLS_ID ?? "N",
                    Usr = m.USR ?? "",
                    SysDate = m.SYS_DATE
                }).ToList();

                return Ok(new ApiResult<List<SecondarySortReportDto>> { Success = true, Data = list, Total = list.Count });
            }
            else
            {
                // 明细表：以TF_PKFJ表身为主，LEFT JOIN MF_PKFJ表头
                var query = from t in _context.TfPkfjs
                            join m in _context.MfPkfjs on t.PKFJ_NO equals m.PKFJ_NO into mj
                            from m in mj.DefaultIfEmpty()
                            select new { T = t, M = m };

                if (dateFrom.HasValue)
                    query = query.Where(x => x.M != null && x.M.PKFJ_DD >= dateFrom.Value);
                if (dateTo.HasValue)
                    query = query.Where(x => x.M != null && x.M.PKFJ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
                if (!string.IsNullOrWhiteSpace(pkfjNo))
                    query = query.Where(x => x.T.PKFJ_NO != null && x.T.PKFJ_NO.Contains(pkfjNo));

                query = query.OrderBy(x => x.T.PKFJ_NO).ThenBy(x => x.T.ITM);

                var rawList = await query.ToListAsync();
                var list = rawList.Select(x => new SecondarySortReportDto
                {
                    ItemNo = x.T.ITM,
                    PkfjNo = x.T.PKFJ_NO ?? "",
                    PkfjDd = x.M?.PKFJ_DD ?? DateTime.MinValue,
                    Wh = "",
                    PrdNo = x.T.PRD_NO ?? "",
                    PrdName = x.T.PRD_NAME ?? "",
                    Qty = x.T.QTY ?? 0,
                    Rem = x.T.REM ?? ""
                }).ToList();

                return Ok(new ApiResult<List<SecondarySortReportDto>> { Success = true, Data = list, Total = list.Count });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询二次分拣单报表时发生错误");
            return StatusCode(500, new ApiResult<List<SecondarySortReportDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>二次分拣单报表DTO</summary>
public class SecondarySortReportDto
{
    public int ItemNo { get; set; }
    public string PkfjNo { get; set; } = "";
    public DateTime? PkfjDd { get; set; }
    public string Wh { get; set; } = "";
    // 明细表字段
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public decimal Qty { get; set; }
    public string Rem { get; set; } = "";
    // 统计表字段
    public string Dep { get; set; } = "";
    public string ClsId { get; set; } = "";
    public string Usr { get; set; } = "";
    public DateTime? SysDate { get; set; }
}
