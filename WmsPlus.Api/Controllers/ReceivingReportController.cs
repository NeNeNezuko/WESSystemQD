using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReceivingReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ReceivingReportController> _logger;

    public ReceivingReportController(WarehouseDbContext context, ILogger<ReceivingReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询收货报表数据（TF_SH + MF_SH）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<object>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? bilNoFrom,
        [FromQuery] string? bilNoTo,
        [FromQuery] string? shNoFrom,
        [FromQuery] string? shNoTo,
        [FromQuery] string? prdNoFrom,
        [FromQuery] string? prdNoTo,
        [FromQuery] string? prdName,
        [FromQuery] bool prdNameFuzzy,
        [FromQuery] string? wh,
        [FromQuery] bool includeOffShelf,
        [FromQuery] string tabType)
    {
        try
        {
            // 构建基础查询：TF_SH (t) LEFT JOIN MF_SH (m) ON t.SH_NO = m.SH_NO
            var query = from t in _context.TfShs
                        join m in _context.MfShs on t.SH_NO equals m.SH_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { t, m };

            // 收货日期范围筛选
            if (dateFrom.HasValue)
                query = query.Where(x => x.t.SH_DD >= dateFrom.Value || (x.m != null && x.m.SH_DD >= dateFrom.Value));
            if (dateTo.HasValue)
                query = query.Where(x => x.t.SH_DD <= dateTo.Value.AddDays(1).AddSeconds(-1) || (x.m != null && x.m.SH_DD <= dateTo.Value.AddDays(1).AddSeconds(-1)));

            // 转入单号范围匹配（使用字符串比较）
            if (!string.IsNullOrWhiteSpace(bilNoFrom))
                query = query.Where(x => x.t.BIL_NO != null && string.Compare(x.t.BIL_NO, bilNoFrom) >= 0 || (x.m != null && x.m.BIL_NO != null && string.Compare(x.m.BIL_NO, bilNoFrom) >= 0));
            if (!string.IsNullOrWhiteSpace(bilNoTo))
                query = query.Where(x => x.t.BIL_NO != null && string.Compare(x.t.BIL_NO, bilNoTo) <= 0 || (x.m != null && x.m.BIL_NO != null && string.Compare(x.m.BIL_NO, bilNoTo) <= 0));

            // 单据号码范围匹配（使用字符串比较）
            if (!string.IsNullOrWhiteSpace(shNoFrom))
                query = query.Where(x => x.t.SH_NO != null && string.Compare(x.t.SH_NO, shNoFrom) >= 0 || (x.m != null && x.m.SH_NO != null && string.Compare(x.m.SH_NO, shNoFrom) >= 0));
            if (!string.IsNullOrWhiteSpace(shNoTo))
                query = query.Where(x => x.t.SH_NO != null && string.Compare(x.t.SH_NO, shNoTo) <= 0 || (x.m != null && x.m.SH_NO != null && string.Compare(x.m.SH_NO, shNoTo) <= 0));

            // 货品代号范围匹配（使用字符串比较）
            if (!string.IsNullOrWhiteSpace(prdNoFrom))
                query = query.Where(x => x.t.PRD_NO != null && string.Compare(x.t.PRD_NO, prdNoFrom) >= 0);
            if (!string.IsNullOrWhiteSpace(prdNoTo))
                query = query.Where(x => x.t.PRD_NO != null && string.Compare(x.t.PRD_NO, prdNoTo) <= 0);

            // 货品名称筛选：根据模糊查询标志决定匹配方式
            if (!string.IsNullOrWhiteSpace(prdName))
            {
                if (prdNameFuzzy)
                    query = query.Where(x => x.t.PRD_NAME != null && x.t.PRD_NAME.Contains(prdName));
                else
                    query = query.Where(x => x.t.PRD_NAME == prdName);
            }

            // 仓库代号匹配
            if (!string.IsNullOrWhiteSpace(wh))
                query = query.Where(x => x.t.WH == wh || (x.m != null && x.m.WH == wh));

            if (tabType == "detail")
            {
                // 明细模式：关联 MY_WH(仓库名称)、DEPT(部门名称)，按 SH_NO, ITM 排序
                var detailQuery = from q in query
                                  join w in _context.MyWhs on q.t.WH equals w.WH into wj
                                  from w in wj.DefaultIfEmpty()
                                  join d in _context.Depts on q.m.DEP equals d.DEP into dj
                                  from d in dj.DefaultIfEmpty()
                                  orderby q.t.SH_NO, q.t.ITM
                                  select new ReceivingReportDetailDto
                                  {
                                      Itm = q.t.ITM,
                                      ShDate = q.t.SH_DD != null ? q.t.SH_DD : q.m.SH_DD,
                                      BilNo = q.t.BIL_NO != null ? q.t.BIL_NO : q.m.BIL_NO != null ? q.m.BIL_NO : "",
                                      ShNo = q.t.SH_NO,
                                      PrdNo = q.t.PRD_NO != null ? q.t.PRD_NO : "",
                                      PrdName = q.t.PRD_NAME != null ? q.t.PRD_NAME : "",
                                      BatNo = q.t.BAT_NO != null ? q.t.BAT_NO : "",
                                      WhName = w.NAME != null ? w.NAME : "",
                                      Qty = q.t.QTY,
                                      Qty1 = q.t.QTY1,
                                      Unit = q.t.UNIT != null ? q.t.UNIT : "",
                                      DepName = d.NAME != null ? d.NAME : ""
                                  };

                var rawList = await detailQuery.ToListAsync();
                var list = rawList.Select((x, i) => new ReceivingReportDetailDto
                {
                    RowNo = i + 1,
                    Itm = x.Itm,
                    ShDate = x.ShDate,
                    BilNo = x.BilNo,
                    ShNo = x.ShNo,
                    PrdNo = x.PrdNo,
                    PrdName = x.PrdName,
                    BatNo = x.BatNo,
                    WhName = x.WhName,
                    Qty = x.Qty,
                    Qty1 = x.Qty1,
                    Unit = x.Unit,
                    DepName = x.DepName
                }).ToList();

                return Ok(new ApiResult<List<ReceivingReportDetailDto>>
                {
                    Success = true,
                    Data = list,
                    Total = list.Count
                });
            }
            else
            {
                // 统计模式：按 PRD_NO 分组汇总 QTY，WH_ERP从MF_SH获取
                var summaryQuery = from q in query
                                   group q by new { q.t.PRD_NO, q.t.PRD_NAME, q.m.WH_ERP } into g
                                   orderby g.Key.PRD_NO
                                   select new ReceivingReportSummaryDto
                                   {
                                       PrdNo = g.Key.PRD_NO != null ? g.Key.PRD_NO : "",
                                       PrdName = g.Key.PRD_NAME != null ? g.Key.PRD_NAME : "",
                                       Spc = "",
                                       WhErp = g.Key.WH_ERP != null ? g.Key.WH_ERP : "",
                                       TotalQty = g.Sum(x => x.t.QTY)
                                   };

                var rawSummaryList = await summaryQuery.ToListAsync();
                var summaryList = rawSummaryList.Select((x, i) => new ReceivingReportSummaryDto
                {
                    RowNo = i + 1,
                    ShDateYear = x.ShDateYear,
                    ShDateQuarter = x.ShDateQuarter,
                    ShDateMonth = x.ShDateMonth,
                    ShDateWeek = x.ShDateWeek,
                    ShDateDay = x.ShDateDay,
                    PrdNo = x.PrdNo,
                    PrdName = x.PrdName,
                    Spc = x.Spc,
                    WhErp = x.WhErp,
                    WhName = x.WhName,
                    CusName = x.CusName,
                    TotalQty = x.TotalQty
                }).ToList();

                return Ok(new ApiResult<List<ReceivingReportSummaryDto>>
                {
                    Success = true,
                    Data = summaryList,
                    Total = summaryList.Count
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询收货报表时发生错误");
            return StatusCode(500, new ApiResult<object>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>收货报表明细行数据</summary>
public class ReceivingReportDetailDto
{
    public int RowNo { get; set; }
    public int? Itm { get; set; }
    public DateTime? ShDate { get; set; }
    public string BilNo { get; set; } = "";
    public string ShNo { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string BatNo { get; set; } = "";
    public string WhName { get; set; } = "";
    public decimal? Qty { get; set; }
    public decimal? Qty1 { get; set; }
    public string Unit { get; set; } = "";
    public string DepName { get; set; } = "";
}

/// <summary>收货报统计行数据</summary>
public class ReceivingReportSummaryDto
{
    public int RowNo { get; set; }
    public string? ShDateYear { get; set; }
    public string? ShDateQuarter { get; set; }
    public string? ShDateMonth { get; set; }
    public string? ShDateWeek { get; set; }
    public DateTime? ShDateDay { get; set; }
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string Spc { get; set; } = "";
    public string WhErp { get; set; } = "";
    public string WhName { get; set; } = "";
    public string CusName { get; set; } = "";
    public decimal? TotalQty { get; set; }
}
