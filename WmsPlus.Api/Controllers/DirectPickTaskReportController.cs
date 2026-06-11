using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DirectPickTaskReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<DirectPickTaskReportController> _logger;

    public DirectPickTaskReportController(WarehouseDbContext context, ILogger<DirectPickTaskReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询直接拣货任务单报表（明细表以TF_JHRW表身为主，统计表以MF_JHRW表头为主）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<DirectPickTaskReportDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? jrNo,
        [FromQuery] string? wh,
        [FromQuery] string? tabType = "detail")
    {
        try
        {
            if (tabType == "summary")
            {
                // 统计表：以MF_JHRW表头为主（筛选类型为直接拣货）
                var headerQuery = _context.MfJhrws.AsQueryable();

                if (dateFrom.HasValue)
                    headerQuery = headerQuery.Where(m => m.JR_DD >= dateFrom.Value);
                if (dateTo.HasValue)
                    headerQuery = headerQuery.Where(m => m.JR_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
                if (!string.IsNullOrWhiteSpace(jrNo))
                    headerQuery = headerQuery.Where(m => m.JR_NO != null && m.JR_NO.Contains(jrNo));
                if (!string.IsNullOrWhiteSpace(wh))
                    headerQuery = headerQuery.Where(m => m.WH != null && m.WH.Contains(wh));

                headerQuery = headerQuery.OrderBy(m => m.JR_NO);

                var headerList = await headerQuery.ToListAsync();
                var list = headerList.Select(m => new DirectPickTaskReportDto
                {
                    ItemNo = 0,
                    JrNo = m.JR_NO ?? "",
                    JrDd = m.JR_DD,
                    Wh = m.WH ?? "",
                    Dep = m.DEP ?? "",
                    SalNo = m.SAL_NO ?? "",
                    TypeId = m.TYPE_ID ?? "",
                    ClsId = m.CLS_ID ?? "N",
                    Usr = m.USR ?? "",
                    SysDate = m.SYS_DATE
                }).ToList();

                return Ok(new ApiResult<List<DirectPickTaskReportDto>>
                {
                    Success = true,
                    Data = list,
                    Total = list.Count
                });
            }
            else
            {
                // 明细表：以TF_JHRW表身为主，LEFT JOIN MF_JHRW表头
                var query = from t in _context.TfJhrws
                            join m in _context.MfJhrws on t.JR_NO equals m.JR_NO into mj
                            from m in mj.DefaultIfEmpty()
                            select new { T = t, M = m };

                if (dateFrom.HasValue)
                    query = query.Where(x => x.M != null && x.M.JR_DD >= dateFrom.Value);
                if (dateTo.HasValue)
                    query = query.Where(x => x.M != null && x.M.JR_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
                if (!string.IsNullOrWhiteSpace(jrNo))
                    query = query.Where(x => x.T.JR_NO != null && x.T.JR_NO.Contains(jrNo));
                if (!string.IsNullOrWhiteSpace(wh))
                    query = query.Where(x => x.M != null && x.M.WH != null && x.M.WH.Contains(wh));

                query = query.OrderBy(x => x.T.JR_NO).ThenBy(x => x.T.ITM);

                var rawList = await query.ToListAsync();
                var list = rawList.Select(x => new DirectPickTaskReportDto
                {
                    ItemNo = x.T.ITM,
                    JrNo = x.T.JR_NO ?? "",
                    JrDd = x.M?.JR_DD ?? DateTime.MinValue,
                    Wh = x.M?.WH ?? "",
                    PrdNo = x.T.PRD_NO ?? "",
                    PrdName = x.T.PRD_NAME ?? "",
                    PrdMark = x.T.PRD_MARK ?? "",
                    BatNo = x.T.BAT_NO ?? "",
                    Chuw = x.T.CHUW ?? "",
                    Unit = x.T.UNIT ?? "",
                    Qty = x.T.QTY ?? 0,
                    QtyPk = x.T.QTY_PK ?? 0,
                    Rem = x.T.REM ?? ""
                }).ToList();

                return Ok(new ApiResult<List<DirectPickTaskReportDto>>
                {
                    Success = true,
                    Data = list,
                    Total = list.Count
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询直接拣货任务单报表时发生错误");
            return StatusCode(500, new ApiResult<List<DirectPickTaskReportDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>直接拣货任务单报表DTO</summary>
public class DirectPickTaskReportDto
{
    public int ItemNo { get; set; }
    public string JrNo { get; set; } = "";
    public DateTime? JrDd { get; set; }
    public string Wh { get; set; } = "";
    // 明细表字段
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public string BatNo { get; set; } = "";
    public string Chuw { get; set; } = "";
    public string Unit { get; set; } = "";
    public decimal Qty { get; set; }
    public decimal QtyPk { get; set; }
    public string Rem { get; set; } = "";
    // 统计表字段
    public string Dep { get; set; } = "";
    public string SalNo { get; set; } = "";
    public string TypeId { get; set; } = "";
    public string ClsId { get; set; } = "";
    public string Usr { get; set; } = "";
    public DateTime? SysDate { get; set; }
}
