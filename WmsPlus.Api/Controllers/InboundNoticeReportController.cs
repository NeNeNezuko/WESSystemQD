using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InboundNoticeReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<InboundNoticeReportController> _logger;

    public InboundNoticeReportController(WarehouseDbContext context, ILogger<InboundNoticeReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询入库通知单报表 - 明细表数据
    /// 以TF_RKTZ(表身)为主，LEFT JOIN MF_RKTZ(表头)和MY_WH(仓库)
    /// </summary>
    [HttpGet("detail")]
    public async Task<ActionResult<ApiResult<List<InboundNoticeReportDetailDto>>>> QueryDetail(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? bizOrderNo,
        [FromQuery] string? erpApplyNo,
        [FromQuery] string? docNoFrom,
        [FromQuery] string? docNoTo,
        [FromQuery] string? prdNoFrom,
        [FromQuery] string? prdNoTo,
        [FromQuery] string? prdName,
        [FromQuery] string? warehouseCode,
        [FromQuery] bool fuzzyBizOrderNo = false,
        [FromQuery] bool fuzzyErpApplyNo = false,
        [FromQuery] bool fuzzyPrdName = false,
        [FromQuery] bool includeChildWh = false)
    {
        try
        {
            var query = from t in _context.TfRktzs
                        join m in _context.MfRktzs on t.TZ_NO equals m.TZ_NO into mj
                        from m in mj.DefaultIfEmpty()
                        join w in _context.MyWhs on (m != null ? m.WH : null) equals w.WH into wj
                        from w in wj.DefaultIfEmpty()
                        select new { T = t, M = m, W = w };

            // 制表日期范围筛选
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.SYS_DATE >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.SYS_DATE <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 业务单号筛选（表头暂无此字段，预留接口）
            if (!string.IsNullOrWhiteSpace(bizOrderNo))
            {
                // 预留：当实体类补充BIL_NO字段后启用
                // if (fuzzyBizOrderNo)
                //     query = query.Where(x => x.M != null && (x.M.BIL_NO != null && x.M.BIL_NO.Contains(bizOrderNo)));
                // else
                //     query = query.Where(x => x.M != null && x.M.BIL_NO == bizOrderNo);
            }

            // ERP申请单据号筛选（表头暂无此字段，预留接口）
            if (!string.IsNullOrWhiteSpace(erpApplyNo))
            {
                // 预留：当实体类补充ERP_BIL_NO字段后启用
                // if (fuzzyErpApplyNo)
                //     query = query.Where(x => x.M != null && (x.M.ERP_BIL_NO != null && x.M.ERP_BIL_NO.Contains(erpApplyNo)));
                // else
                //     query = query.Where(x => x.M != null && x.M.ERP_BIL_NO == erpApplyNo);
            }

            // 单据号码起止范围
            if (!string.IsNullOrWhiteSpace(docNoFrom))
                query = query.Where(x => string.Compare(x.T.TZ_NO ?? "", docNoFrom, StringComparison.Ordinal) >= 0);
            if (!string.IsNullOrWhiteSpace(docNoTo))
                query = query.Where(x => string.Compare(x.T.TZ_NO ?? "", docNoTo, StringComparison.Ordinal) <= 0);

            // 货品代号起止范围
            if (!string.IsNullOrWhiteSpace(prdNoFrom))
                query = query.Where(x => string.Compare(x.T.PRD_NO ?? "", prdNoFrom, StringComparison.Ordinal) >= 0);
            if (!string.IsNullOrWhiteSpace(prdNoTo))
                query = query.Where(x => string.Compare(x.T.PRD_NO ?? "", prdNoTo, StringComparison.Ordinal) <= 0);

            // 货品名称筛选
            if (!string.IsNullOrWhiteSpace(prdName))
            {
                if (fuzzyPrdName)
                    query = query.Where(x => x.T.PRD_NAME != null && x.T.PRD_NAME.Contains(prdName));
                else
                    query = query.Where(x => x.T.PRD_NAME == prdName);
            }

            // 仓库代号筛选
            if (!string.IsNullOrWhiteSpace(warehouseCode))
            {
                if (includeChildWh)
                    query = query.Where(x => x.M != null && (x.M.WH != null && x.M.WH.StartsWith(warehouseCode)));
                else
                    query = query.Where(x => x.M != null && x.M.WH == warehouseCode);
            }

            // 排序：按单据号+项次
            query = query.OrderBy(x => x.T.TZ_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            var list = rawList.Select((x, idx) => new InboundNoticeReportDetailDto
            {
                ItemNo = idx + 1,
                MakeTime = x.M?.SYS_DATE,
                SourceNo = "", // 来源单号（MF_RKTZ表头暂无此字段）
                ErpApplyNo = "", // ERP申请单据号（MF_RKTZ表头暂无此字段）
                DocNo = x.T.TZ_NO ?? "",
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                BatNo = x.T.BAT_NO ?? "",
                Qty = x.T.QTY ?? 0,
                QtyRk = 0, // 已入库量（TF_RKTZ表身暂无此字段）
                WhName = x.W?.NAME ?? ""
            }).ToList();

            return Ok(new ApiResult<List<InboundNoticeReportDetailDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询入库通知单报表-明细表时发生错误");
            return StatusCode(500, new ApiResult<List<InboundNoticeReportDetailDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 查询入库通知单报表 - 统计表数据
    /// 以TF_RKTZ(表身)为主，LEFT JOIN MF_RKTZ(表头)，展示统计维度字段
    /// </summary>
    [HttpGet("summary")]
    public async Task<ActionResult<ApiResult<List<InboundNoticeReportSummaryDto>>>> QuerySummary(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? bizOrderNo,
        [FromQuery] string? erpApplyNo,
        [FromQuery] string? docNoFrom,
        [FromQuery] string? docNoTo,
        [FromQuery] string? prdNoFrom,
        [FromQuery] string? prdNoTo,
        [FromQuery] string? prdName,
        [FromQuery] string? warehouseCode,
        [FromQuery] bool fuzzyBizOrderNo = false,
        [FromQuery] bool fuzzyErpApplyNo = false,
        [FromQuery] bool fuzzyPrdName = false,
        [FromQuery] bool includeChildWh = false)
    {
        try
        {
            var query = from t in _context.TfRktzs
                        join m in _context.MfRktzs on t.TZ_NO equals m.TZ_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 与明细表相同的筛选条件（业务单号和ERP申请单号预留）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.SYS_DATE >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.SYS_DATE <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 业务单号和ERP申请单号筛选（表头暂无此字段，预留接口）
            // if (!string.IsNullOrWhiteSpace(bizOrderNo))
            // {
            //     if (fuzzyBizOrderNo)
            //         query = query.Where(x => x.M != null && (x.M.BIL_NO != null && x.M.BIL_NO.Contains(bizOrderNo)));
            //     else
            //         query = query.Where(x => x.M != null && x.M.BIL_NO == bizOrderNo);
            // }
            //
            // if (!string.IsNullOrWhiteSpace(erpApplyNo))
            // {
            //     if (fuzzyErpApplyNo)
            //         query = query.Where(x => x.M != null && (x.M.ERP_BIL_NO != null && x.M.ERP_BIL_NO.Contains(erpApplyNo)));
            //     else
            //         query = query.Where(x => x.M != null && x.M.ERP_BIL_NO == erpApplyNo);
            // }

            if (!string.IsNullOrWhiteSpace(docNoFrom))
                query = query.Where(x => string.Compare(x.T.TZ_NO ?? "", docNoFrom, StringComparison.Ordinal) >= 0);
            if (!string.IsNullOrWhiteSpace(docNoTo))
                query = query.Where(x => string.Compare(x.T.TZ_NO ?? "", docNoTo, StringComparison.Ordinal) <= 0);

            if (!string.IsNullOrWhiteSpace(prdNoFrom))
                query = query.Where(x => string.Compare(x.T.PRD_NO ?? "", prdNoFrom, StringComparison.Ordinal) >= 0);
            if (!string.IsNullOrWhiteSpace(prdNoTo))
                query = query.Where(x => string.Compare(x.T.PRD_NO ?? "", prdNoTo, StringComparison.Ordinal) <= 0);

            if (!string.IsNullOrWhiteSpace(prdName))
            {
                if (fuzzyPrdName)
                    query = query.Where(x => x.T.PRD_NAME != null && x.T.PRD_NAME.Contains(prdName));
                else
                    query = query.Where(x => x.T.PRD_NAME == prdName);
            }

            if (!string.IsNullOrWhiteSpace(warehouseCode))
            {
                if (includeChildWh)
                    query = query.Where(x => x.M != null && (x.M.WH != null && x.M.WH.StartsWith(warehouseCode)));
                else
                    query = query.Where(x => x.M != null && x.M.WH == warehouseCode);
            }

            query = query.OrderBy(x => x.T.TZ_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 计算日期维度
            var calendar = System.Globalization.CultureInfo.CurrentCulture.Calendar;
            var list = rawList.Select((x, idx) =>
            {
                var tzDate = x.M?.TZ_DD;
                return new InboundNoticeReportSummaryDto
                {
                    ItemNo = idx + 1,
                    MakeTime = x.M?.SYS_DATE,
                    DocYear = tzDate.HasValue ? tzDate.Value.Year : 0,
                    DocQuarter = tzDate.HasValue ? (tzDate.Value.Month - 1) / 3 + 1 : 0,
                    DocMonth = tzDate.HasValue ? tzDate.Value.Month : 0,
                    DocWeek = tzDate.HasValue ? calendar.GetWeekOfYear(tzDate.Value, System.Globalization.CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday) : 0,
                    BizOrderNo = "", // 业务单号（MF_RKTZ表头暂无此字段）
                    ErpApplyNo = "", // ERP申请单据号（MF_RKTZ表头暂无此字段）
                    DocNo = x.T.TZ_NO ?? "",
                    PrdNo = x.T.PRD_NO ?? "",
                    PrdName = x.T.PRD_NAME ?? ""
                };
            }).ToList();

            return Ok(new ApiResult<List<InboundNoticeReportSummaryDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询入库通知单报表-统计表时发生错误");
            return StatusCode(500, new ApiResult<List<InboundNoticeReportSummaryDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>明细表行数据</summary>
public class InboundNoticeReportDetailDto
{
    public int ItemNo { get; set; }
    public DateTime? MakeTime { get; set; }
    public string SourceNo { get; set; } = "";
    public string ErpApplyNo { get; set; } = "";
    public string DocNo { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string BatNo { get; set; } = "";
    public decimal Qty { get; set; }
    public decimal QtyRk { get; set; }
    public string WhName { get; set; } = "";
}

/// <summary>统计表行数据</summary>
public class InboundNoticeReportSummaryDto
{
    public int ItemNo { get; set; }
    public DateTime? MakeTime { get; set; }
    public int DocYear { get; set; }
    public int DocQuarter { get; set; }
    public int DocMonth { get; set; }
    public int DocWeek { get; set; }
    public string BizOrderNo { get; set; } = "";
    public string ErpApplyNo { get; set; } = "";
    public string DocNo { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
}
