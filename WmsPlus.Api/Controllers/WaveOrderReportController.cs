using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WaveOrderReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<WaveOrderReportController> _logger;

    public WaveOrderReportController(WarehouseDbContext context, ILogger<WaveOrderReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询波次单报表（明细表以TF_BC表身为主，统计表以MF_BC表头为主）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<WaveOrderReportDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? bcNo,
        [FromQuery] string? wh,
        [FromQuery] string? status,
        [FromQuery] string? tabType = "detail")
    {
        try
        {
            if (tabType == "summary")
            {
                // 统计表：以MF_BC表头为主
                var headerQuery = _context.MfBcs.AsQueryable();

                if (dateFrom.HasValue)
                    headerQuery = headerQuery.Where(m => m.BC_DD >= dateFrom.Value);
                if (dateTo.HasValue)
                    headerQuery = headerQuery.Where(m => m.BC_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
                if (!string.IsNullOrWhiteSpace(bcNo))
                    headerQuery = headerQuery.Where(m => m.BC_NO != null && m.BC_NO.Contains(bcNo));
                if (!string.IsNullOrWhiteSpace(wh))
                    headerQuery = headerQuery.Where(m => m.WH != null && m.WH.Contains(wh));
                if (!string.IsNullOrWhiteSpace(status) && status != "全部")
                {
                    var isClosed = status == "已结案" ? "Y" : "N";
                    headerQuery = headerQuery.Where(m => m.CLS_ID == isClosed);
                }

                headerQuery = headerQuery.OrderBy(m => m.BC_NO);

                var headerList = await headerQuery.ToListAsync();
                var list = headerList.Select(m => new WaveOrderReportDto
                {
                    ItemNo = 0,
                    BcNo = m.BC_NO ?? "",
                    BcDd = m.BC_DD,
                    Wh = m.WH ?? "",
                    Dep = m.DEP ?? "",
                    BilType = m.BIL_TYPE ?? "",
                    SalNo = m.SAL_NO ?? "",
                    ClsId = m.CLS_ID ?? "N",
                    Usr = m.USR ?? "",
                    SysDate = m.SYS_DATE
                }).ToList();

                return Ok(new ApiResult<List<WaveOrderReportDto>>
                {
                    Success = true,
                    Data = list,
                    Total = list.Count
                });
            }
            else
            {
                // 明细表：以TF_BC表身为主，LEFT JOIN MF_BC表头
                var query = from t in _context.TfBcs
                            join m in _context.MfBcs on t.BC_NO equals m.BC_NO into mj
                            from m in mj.DefaultIfEmpty()
                            select new { T = t, M = m };

                if (dateFrom.HasValue)
                    query = query.Where(x => x.M != null && x.M.BC_DD >= dateFrom.Value);
                if (dateTo.HasValue)
                    query = query.Where(x => x.M != null && x.M.BC_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
                if (!string.IsNullOrWhiteSpace(bcNo))
                    query = query.Where(x => x.T.BC_NO != null && x.T.BC_NO.Contains(bcNo));
                if (!string.IsNullOrWhiteSpace(wh))
                    query = query.Where(x => x.M != null && x.M.WH != null && x.M.WH.Contains(wh));
                if (!string.IsNullOrWhiteSpace(status) && status != "全部")
                {
                    var isClosed = status == "已结案" ? "Y" : "N";
                    query = query.Where(x => x.M != null && x.M.CLS_ID == isClosed);
                }

                query = query.OrderBy(x => x.T.BC_NO).ThenBy(x => x.T.ITM);

                var rawList = await query.ToListAsync();
                var list = rawList.Select(x => new WaveOrderReportDto
                {
                    ItemNo = x.T.ITM ?? 0,
                    BcNo = x.T.BC_NO ?? "",
                    BcDd = x.M?.BC_DD ?? DateTime.MinValue,
                    Wh = x.M?.WH ?? "",
                    PrdNo = x.T.PRD_NO ?? "",
                    PrdName = x.T.PRD_NAME ?? "",
                    PrdMark = x.T.PRD_MARK ?? "",
                    BatNo = x.T.BAT_NO ?? "",
                    Unit = x.T.UNIT ?? "",
                    Qty = x.T.QTY ?? 0,
                    PickQty = x.T.PICK_QTY ?? 0,
                    Rem = x.T.REM ?? ""
                }).ToList();

                return Ok(new ApiResult<List<WaveOrderReportDto>>
                {
                    Success = true,
                    Data = list,
                    Total = list.Count
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询波次单报表时发生错误");
            return StatusCode(500, new ApiResult<List<WaveOrderReportDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>波次单报表DTO</summary>
public class WaveOrderReportDto
{
    public int ItemNo { get; set; }
    public string BcNo { get; set; } = "";
    public DateTime? BcDd { get; set; }
    public string Wh { get; set; } = "";
    // 明细表字段
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public string BatNo { get; set; } = "";
    public string Unit { get; set; } = "";
    public decimal Qty { get; set; }
    public decimal PickQty { get; set; }
    public string Rem { get; set; } = "";
    // 统计表字段
    public string Dep { get; set; } = "";
    public string BilType { get; set; } = "";
    public string SalNo { get; set; } = "";
    public string ClsId { get; set; } = "";
    public string Usr { get; set; } = "";
    public DateTime? SysDate { get; set; }
}
