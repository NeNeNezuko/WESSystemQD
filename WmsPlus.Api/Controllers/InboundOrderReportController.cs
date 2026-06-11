using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InboundOrderReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<InboundOrderReportController> _logger;

    public InboundOrderReportController(WarehouseDbContext context, ILogger<InboundOrderReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询入库单报表 - 明细表数据
    /// 以TF_RK(表身)为主，LEFT JOIN MF_RK(表头)和MY_WH(仓库)
    /// </summary>
    [HttpGet("detailSearch")]
    public async Task<ActionResult<object>> DetailSearch([FromQuery] InboundOrderReportQuery query)
    {
        try
        {
            var pageIndex = query.PageIndex > 0 ? query.PageIndex : 1;
            var pageSize = query.PageSize > 0 ? query.PageSize : 20;

            var baseQuery = from t in _context.TfRks
                            join m in _context.MfRks on t.RK_NO equals m.RK_NO into mj
                            from m in mj.DefaultIfEmpty()
                            join w in _context.MyWhs on (m != null ? m.WH : null) equals w.WH into wj
                            from w in wj.DefaultIfEmpty()
                            select new { T = t, M = m, W = w };

            // 日期范围筛选（按MF_RK.SYS_DATE）
            if (!string.IsNullOrWhiteSpace(query.DateRange))
            {
                var parts = query.DateRange.Split('~');
                if (parts.Length == 2)
                {
                    if (DateTime.TryParse(parts[0].Trim(), out var dateFrom))
                        baseQuery = baseQuery.Where(x => x.M != null && x.M.SYS_DATE >= dateFrom);
                    if (DateTime.TryParse(parts[1].Trim(), out var dateTo))
                        baseQuery = baseQuery.Where(x => x.M != null && x.M.SYS_DATE <= dateTo.AddDays(1).AddSeconds(-1));
                }
            }

            // ERP申请单号筛选（按MF_RK.ERP_AP_NO Contains 模糊匹配）
            if (!string.IsNullOrWhiteSpace(query.ErpApNo))
                baseQuery = baseQuery.Where(x => x.M != null && x.M.ERP_AP_NO != null && x.M.ERP_AP_NO.Contains(query.ErpApNo));

            // 入库单号起止范围（按MF_RK.RK_NO 范围筛选）
            if (!string.IsNullOrWhiteSpace(query.BilNoFrom))
                baseQuery = baseQuery.Where(x => x.M != null && string.Compare(x.M.RK_NO ?? "", query.BilNoFrom, StringComparison.Ordinal) >= 0);
            if (!string.IsNullOrWhiteSpace(query.BilNoTo))
                baseQuery = baseQuery.Where(x => x.M != null && string.Compare(x.M.RK_NO ?? "", query.BilNoTo, StringComparison.Ordinal) <= 0);

            // 货品代号起止范围（按TF_RK.PRD_NO 范围筛选）
            if (!string.IsNullOrWhiteSpace(query.PrdNoFrom))
                baseQuery = baseQuery.Where(x => string.Compare(x.T.PRD_NO ?? "", query.PrdNoFrom, StringComparison.Ordinal) >= 0);
            if (!string.IsNullOrWhiteSpace(query.PrdNoTo))
                baseQuery = baseQuery.Where(x => string.Compare(x.T.PRD_NO ?? "", query.PrdNoTo, StringComparison.Ordinal) <= 0);

            // 货品名称筛选（按TF_RK.PRD_NAME Contains 模糊匹配，PrdNameFuzzy控制是否模糊）
            if (!string.IsNullOrWhiteSpace(query.PrdName))
            {
                if (query.PrdNameFuzzy)
                    baseQuery = baseQuery.Where(x => x.T.PRD_NAME != null && x.T.PRD_NAME.Contains(query.PrdName));
                else
                    baseQuery = baseQuery.Where(x => x.T.PRD_NAME == query.PrdName);
            }

            // 仓库名称筛选（按MY_WH.NAME Contains 模糊匹配，WhNameFuzzy控制是否模糊）
            if (!string.IsNullOrWhiteSpace(query.WhName))
            {
                if (query.WhNameFuzzy)
                    baseQuery = baseQuery.Where(x => x.W != null && x.W.NAME != null && x.W.NAME.Contains(query.WhName));
                else
                    baseQuery = baseQuery.Where(x => x.W != null && x.W.NAME == query.WhName);
            }

            // 储位代号起止范围（按TF_RK.CHUW 范围筛选）
            if (!string.IsNullOrWhiteSpace(query.ChuwFrom))
                baseQuery = baseQuery.Where(x => string.Compare(x.T.CHUW ?? "", query.ChuwFrom, StringComparison.Ordinal) >= 0);
            if (!string.IsNullOrWhiteSpace(query.ChuwTo))
                baseQuery = baseQuery.Where(x => string.Compare(x.T.CHUW ?? "", query.ChuwTo, StringComparison.Ordinal) <= 0);

            // 查询总数
            var totalCount = await baseQuery.CountAsync();

            // 排序与分页
            var items = await baseQuery
                .OrderBy(x => x.M == null ? (DateTime?)null : x.M.SYS_DATE)
                .ThenBy(x => x.T.RK_NO)
                .ThenBy(x => x.T.ITM)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new InboundOrderReportDetailItem
                {
                    RowIdx = 0,
                    SysDate = x.M == null ? null : x.M.SYS_DATE,
                    BusNo = x.T.BUS_NO ?? "",
                    ErpApNo = x.M == null ? "" : (x.M.ERP_AP_NO ?? ""),
                    RkNo = x.T.RK_NO ?? "",
                    PrdNo = x.T.PRD_NO ?? "",
                    PrdName = x.T.PRD_NAME ?? "",
                    BatNo = x.T.BAT_NO ?? "",
                    WhName = x.W == null ? "" : (x.W.NAME ?? ""),
                    Qty = x.T.QTY ?? 0,
                    Qty1 = x.T.QTY1 ?? 0
                })
                .ToListAsync();

            // 计算行号
            for (var i = 0; i < items.Count; i++)
                items[i].RowIdx = (pageIndex - 1) * pageSize + i + 1;

            return Ok(new { items, totalCount });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询入库单报表-明细表时发生错误");
            return StatusCode(500, new { message = $"服务器内部错误: {ex.Message}" });
        }
    }

    /// <summary>
    /// 查询入库单报表 - 统计表数据
    /// 以TF_RK LEFT JOIN MF_RK LEFT JOIN MY_WH，按维度分组聚合
    /// </summary>
    [HttpGet("statsSearch")]
    public async Task<ActionResult<object>> StatsSearch([FromQuery] InboundOrderReportQuery query)
    {
        try
        {
            var pageIndex = query.PageIndex > 0 ? query.PageIndex : 1;
            var pageSize = query.PageSize > 0 ? query.PageSize : 20;

            var baseQuery = from t in _context.TfRks
                            join m in _context.MfRks on t.RK_NO equals m.RK_NO into mj
                            from m in mj.DefaultIfEmpty()
                            join w in _context.MyWhs on (m != null ? m.WH : null) equals w.WH into wj
                            from w in wj.DefaultIfEmpty()
                            select new { T = t, M = m, W = w };

            // 与明细表相同的筛选条件
            if (!string.IsNullOrWhiteSpace(query.DateRange))
            {
                var parts = query.DateRange.Split('~');
                if (parts.Length == 2)
                {
                    if (DateTime.TryParse(parts[0].Trim(), out var dateFrom))
                        baseQuery = baseQuery.Where(x => x.M != null && x.M.SYS_DATE >= dateFrom);
                    if (DateTime.TryParse(parts[1].Trim(), out var dateTo))
                        baseQuery = baseQuery.Where(x => x.M != null && x.M.SYS_DATE <= dateTo.AddDays(1).AddSeconds(-1));
                }
            }

            if (!string.IsNullOrWhiteSpace(query.ErpApNo))
                baseQuery = baseQuery.Where(x => x.M != null && x.M.ERP_AP_NO != null && x.M.ERP_AP_NO.Contains(query.ErpApNo));

            if (!string.IsNullOrWhiteSpace(query.BilNoFrom))
                baseQuery = baseQuery.Where(x => x.M != null && string.Compare(x.M.RK_NO ?? "", query.BilNoFrom, StringComparison.Ordinal) >= 0);
            if (!string.IsNullOrWhiteSpace(query.BilNoTo))
                baseQuery = baseQuery.Where(x => x.M != null && string.Compare(x.M.RK_NO ?? "", query.BilNoTo, StringComparison.Ordinal) <= 0);

            if (!string.IsNullOrWhiteSpace(query.PrdNoFrom))
                baseQuery = baseQuery.Where(x => string.Compare(x.T.PRD_NO ?? "", query.PrdNoFrom, StringComparison.Ordinal) >= 0);
            if (!string.IsNullOrWhiteSpace(query.PrdNoTo))
                baseQuery = baseQuery.Where(x => string.Compare(x.T.PRD_NO ?? "", query.PrdNoTo, StringComparison.Ordinal) <= 0);

            if (!string.IsNullOrWhiteSpace(query.PrdName))
            {
                if (query.PrdNameFuzzy)
                    baseQuery = baseQuery.Where(x => x.T.PRD_NAME != null && x.T.PRD_NAME.Contains(query.PrdName));
                else
                    baseQuery = baseQuery.Where(x => x.T.PRD_NAME == query.PrdName);
            }

            if (!string.IsNullOrWhiteSpace(query.WhName))
            {
                if (query.WhNameFuzzy)
                    baseQuery = baseQuery.Where(x => x.W != null && x.W.NAME != null && x.W.NAME.Contains(query.WhName));
                else
                    baseQuery = baseQuery.Where(x => x.W != null && x.W.NAME == query.WhName);
            }

            if (!string.IsNullOrWhiteSpace(query.ChuwFrom))
                baseQuery = baseQuery.Where(x => string.Compare(x.T.CHUW ?? "", query.ChuwFrom, StringComparison.Ordinal) >= 0);
            if (!string.IsNullOrWhiteSpace(query.ChuwTo))
                baseQuery = baseQuery.Where(x => string.Compare(x.T.CHUW ?? "", query.ChuwTo, StringComparison.Ordinal) <= 0);

            // 分组聚合统计
            var groupedQuery = baseQuery
                .GroupBy(x => new
                {
                    Year = x.M != null && x.M.RK_DD.HasValue ? x.M.RK_DD.Value.Year : 0,
                    RkNo = x.T.RK_NO ?? "",
                    PrdNo = x.T.PRD_NO ?? "",
                    Wh = x.M != null ? (x.M.WH ?? "") : "",
                    WhName = x.W == null ? "" : (x.W.NAME ?? ""),
                    CusName = x.M != null ? (x.M.CUS_NAME ?? "") : ""
                })
                .Select(g => new InboundOrderReportStatsItem
                {
                    RowIdx = 0,
                    YearPart = g.Key.Year,
                    RkNo = g.Key.RkNo,
                    PrdNo = g.Key.PrdNo,
                    PrdName = g.FirstOrDefault().T != null ? (g.FirstOrDefault().T.PRD_NAME ?? "") : "",
                    Wh = g.Key.Wh,
                    WhName = g.Key.WhName,
                    CusName = g.Key.CusName,
                    TotalQty = g.Sum(x => x.T.QTY) ?? 0,
                    TotalQty1 = g.Sum(x => x.T.QTY1) ?? 0
                });

            var totalCount = await groupedQuery.CountAsync();

            var items = await groupedQuery
                .OrderBy(x => x.YearPart)
                .ThenBy(x => x.RkNo)
                .ThenBy(x => x.PrdNo)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            for (var i = 0; i < items.Count; i++)
                items[i].RowIdx = (pageIndex - 1) * pageSize + i + 1;

            return Ok(new { items, totalCount });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询入库单报表-统计表时发生错误");
            return StatusCode(500, new { message = $"服务器内部错误: {ex.Message}" });
        }
    }
}

// ====== 内部类型定义 ======

/// <summary>入库单报表查询参数</summary>
public class InboundOrderReportQuery
{
    public string? DateRange { get; set; }
    public string? ErpApNo { get; set; }
    public string? BilNoFrom { get; set; }
    public string? BilNoTo { get; set; }
    public string? PrdNoFrom { get; set; }
    public string? PrdNoTo { get; set; }
    public string? PrdName { get; set; }
    public bool PrdNameFuzzy { get; set; }
    public string? WhName { get; set; }
    public bool WhNameFuzzy { get; set; }
    public string? ChuwFrom { get; set; }
    public string? ChuwTo { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}

/// <summary>入库单报表明细行</summary>
public class InboundOrderReportDetailItem
{
    public int RowIdx { get; set; }
    public DateTime? SysDate { get; set; }
    public string BusNo { get; set; } = "";
    public string ErpApNo { get; set; } = "";
    public string RkNo { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string BatNo { get; set; } = "";
    public string WhName { get; set; } = "";
    public decimal Qty { get; set; }
    public decimal Qty1 { get; set; }
}

/// <summary>入库单报表统计行</summary>
public class InboundOrderReportStatsItem
{
    public int RowIdx { get; set; }
    public int YearPart { get; set; }
    public string RkNo { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string Wh { get; set; } = "";
    public string WhName { get; set; } = "";
    public string CusName { get; set; } = "";
    public decimal TotalQty { get; set; }
    public decimal TotalQty1 { get; set; }
}
