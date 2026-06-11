using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PickReturnReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<PickReturnReportController> _logger;

    public PickReturnReportController(WarehouseDbContext context, ILogger<PickReturnReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询拣货退回单报表（明细表以TF_JT表身为主，统计表以MF_JT表头为主）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<PickReturnReportDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? jtNo,
        [FromQuery] string? wh,
        [FromQuery] string? tabType = "detail")
    {
        try
        {
            if (tabType == "summary")
            {
                // 统计表：以MF_JT表头为主
                var headerQuery = _context.MfJts.AsQueryable();

                if (dateFrom.HasValue)
                    headerQuery = headerQuery.Where(m => m.JT_DD >= dateFrom.Value);
                if (dateTo.HasValue)
                    headerQuery = headerQuery.Where(m => m.JT_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
                if (!string.IsNullOrWhiteSpace(jtNo))
                    headerQuery = headerQuery.Where(m => m.JT_NO != null && m.JT_NO.Contains(jtNo));
                if (!string.IsNullOrWhiteSpace(wh))
                    headerQuery = headerQuery.Where(m => m.WH != null && m.WH.Contains(wh));

                headerQuery = headerQuery.OrderBy(m => m.JT_NO);

                var headerList = await headerQuery.ToListAsync();
                var list = headerList.Select(m => new PickReturnReportDto
                {
                    ItemNo = 0,
                    JtNo = m.JT_NO ?? "",
                    JtDd = m.JT_DD,
                    Wh = m.WH ?? "",
                    Dep = m.DEP ?? "",
                    SalNo = m.SAL_NO ?? "",
                    ClsId = m.CLS_ID ?? "N",
                    Usr = m.USR ?? "",
                    SysDate = m.SYS_DATE
                }).ToList();

                return Ok(new ApiResult<List<PickReturnReportDto>> { Success = true, Data = list, Total = list.Count });
            }
            else
            {
                // 明细表：以TF_JT表身为主，LEFT JOIN MF_JT表头
                var query = from t in _context.TfJts
                            join m in _context.MfJts on t.JT_NO equals m.JT_NO into mj
                            from m in mj.DefaultIfEmpty()
                            select new { T = t, M = m };

                if (dateFrom.HasValue)
                    query = query.Where(x => x.M != null && x.M.JT_DD >= dateFrom.Value);
                if (dateTo.HasValue)
                    query = query.Where(x => x.M != null && x.M.JT_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
                if (!string.IsNullOrWhiteSpace(jtNo))
                    query = query.Where(x => x.T.JT_NO != null && x.T.JT_NO.Contains(jtNo));
                if (!string.IsNullOrWhiteSpace(wh))
                    query = query.Where(x => x.M != null && x.M.WH != null && x.M.WH.Contains(wh));

                query = query.OrderBy(x => x.T.JT_NO).ThenBy(x => x.T.ITM);

                var rawList = await query.ToListAsync();
                var list = rawList.Select(x => new PickReturnReportDto
                {
                    ItemNo = x.T.ITM,
                    JtNo = x.T.JT_NO ?? "",
                    JtDd = x.M?.JT_DD ?? DateTime.MinValue,
                    Wh = x.M?.WH ?? "",
                    PrdNo = x.T.PRD_NO ?? "",
                    PrdName = x.T.PRD_NAME ?? "",
                    Qty = x.T.QTY ?? 0,
                    Rem = x.T.REM ?? ""
                }).ToList();

                return Ok(new ApiResult<List<PickReturnReportDto>> { Success = true, Data = list, Total = list.Count });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询拣货退回单报表时发生错误");
            return StatusCode(500, new ApiResult<List<PickReturnReportDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>拣货退回单报表DTO</summary>
public class PickReturnReportDto
{
    public int ItemNo { get; set; }
    public string JtNo { get; set; } = "";
    public DateTime? JtDd { get; set; }
    public string Wh { get; set; } = "";
    // 明细表字段
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public decimal Qty { get; set; }
    public string Rem { get; set; } = "";
    // 统计表字段
    public string Dep { get; set; } = "";
    public string SalNo { get; set; } = "";
    public string ClsId { get; set; } = "";
    public string Usr { get; set; } = "";
    public DateTime? SysDate { get; set; }
}
