using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WavePickTaskController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<WavePickTaskController> _logger;

    public WavePickTaskController(WarehouseDbContext context, ILogger<WavePickTaskController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询波次拣货任务单列表（以表身TF_JHRW为主，LEFT JOIN表头MF_JHRW）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<WavePickTaskDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? jrNo,
        [FromQuery] string? tzNo,
        [FromQuery] string? orgBilNo,
        [FromQuery] string? applyNo,
        [FromQuery] string? depCode,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? areaSh,
        [FromQuery] string? pkFlowMark,
        [FromQuery] string? clsMark,
        [FromQuery] string? businessType)
    {
        try
        {
            var query = from t in _context.TfJhrws
                        join m in _context.MfJhrws on t.JR_NO equals m.JR_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 日期范围筛选（使用表头的JR_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.JR_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.JR_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 波次拣货任务单号模糊匹配
            if (!string.IsNullOrWhiteSpace(jrNo))
                query = query.Where(x => x.T.JR_NO.Contains(jrNo) || (x.M != null && x.M.JR_NO_NEW != null && x.M.JR_NO_NEW.Contains(jrNo)));

            // 出库通知单号模糊匹配（通过TZ_NO）
            if (!string.IsNullOrWhiteSpace(tzNo))
                query = query.Where(x => x.T.TZ_NO != null && x.T.TZ_NO.Contains(tzNo));

            // 业务单号模糊匹配（通过ORG_BIL_NO）
            if (!string.IsNullOrWhiteSpace(orgBilNo))
                query = query.Where(x => x.T.ORG_BIL_NO != null && x.T.ORG_BIL_NO.Contains(orgBilNo));

            // 申请单号/初圈任务单号模糊匹配（通过PR_NO）
            if (!string.IsNullOrWhiteSpace(applyNo))
                query = query.Where(x => x.M != null && (x.M.PR_NO != null && x.M.PR_NO.Contains(applyNo)));

            // 部门代号模糊匹配
            if (!string.IsNullOrWhiteSpace(depCode))
                query = query.Where(x => x.M != null && (x.M.DEP != null && x.M.DEP.Contains(depCode)));

            // 仓库代号模糊匹配
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.M != null && (x.M.WH != null && x.M.WH.Contains(warehouseCode)));

            // 收货点模糊匹配
            if (!string.IsNullOrWhiteSpace(areaSh))
                query = query.Where(x => x.M != null && (x.M.AREA_SH != null && x.M.AREA_SH.Contains(areaSh)));

            // 捡线策略标记筛选（PK_FLOW: 1全拣-分拣 2直接拣货）
            if (!string.IsNullOrWhiteSpace(pkFlowMark) && pkFlowMark != "全部")
            {
                query = query.Where(x => x.M != null && x.M.PK_FLOW == pkFlowMark);
            }

            // 拣货结案标记筛选（CLS_ID: Y已结案 N未结案）
            if (!string.IsNullOrWhiteSpace(clsMark) && clsMark != "全部")
            {
                query = query.Where(x => x.M != null && x.M.CLS_ID == clsMark);
            }

            // 业务类型筛选
            if (!string.IsNullOrWhiteSpace(businessType))
                query = query.Where(x => x.M != null && (x.M.TYPE_ID != null && x.M.TYPE_ID.Contains(businessType)));

            // 按任务单号+项次排序
            query = query.OrderBy(x => x.T.JR_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 关联仓库名称、部门名称
            var whCodes = rawList.Select(x => x.M?.WH).Where(w => !string.IsNullOrWhiteSpace(w)).Distinct().ToList();
            var depCodes = rawList.Select(x => x.M?.DEP).Where(d => !string.IsNullOrWhiteSpace(d)).Distinct().ToList();
            var whNames = await _context.MyWhs
                .Where(w => whCodes.Contains(w.WH))
                .ToDictionaryAsync(w => w.WH, w => w.NAME);
            var depNames = await _context.Depts
                .Where(d => depCodes.Contains(d.DEP))
                .ToDictionaryAsync(d => d.DEP, d => d.NAME);

            // 在内存中做投影
            var list = rawList.Select(x => new WavePickTaskDto
            {
                ItemNo = x.T.ITM,
                JrNo = x.T.JR_NO,
                JrDate = x.M?.JR_DD ?? DateTime.MinValue,
                WarehouseCode = x.M?.WH ?? "",
                WarehouseName = x.M?.WH != null && whNames.ContainsKey(x.M.WH) ? whNames[x.M.WH] : "",
                DepCode = x.M?.DEP ?? "",
                DepName = x.M?.DEP != null && depNames.ContainsKey(x.M.DEP) ? depNames[x.M.DEP] : "",
                UsrName = x.M?.USR ?? "",
                AreaSh = x.M?.AREA_SH ?? "",
                BcNo = x.M?.BC_NO ?? "",
                TzNo = x.T.TZ_NO ?? "",
                OrgBilNo = x.T.ORG_BIL_NO ?? "",
                StatusPg = x.M?.STATUS_PG ?? "0",
                ClsId = x.M?.CLS_ID ?? "N",
                PkFlow = x.M?.PK_FLOW ?? "",
                Priority = x.M?.PRIORITY ?? 0,
                UsrPk = x.M?.USR_PK ?? "",
                SalNo = x.M?.SAL_NO ?? "",
                TypeId = x.M?.TYPE_ID ?? "",
                // 表身明细字段
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                PrdMark = x.T.PRD_MARK ?? "",
                Qty = x.T.QTY ?? 0,
                QtyPk = x.T.QTY_PK ?? 0,
                Unit = x.T.UNIT ?? "",
                BatNo = x.T.BAT_NO ?? "",
                Chuw = x.T.CHUW ?? "",
                CarNo = x.T.CAR_NO ?? ""
            }).ToList();

            return Ok(new ApiResult<List<WavePickTaskDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询波次拣货任务单时发生错误");
            return StatusCode(500, new ApiResult<List<WavePickTaskDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据单据号获取波次拣货任务单详情（含表身明细）
    /// </summary>
    [HttpGet("{jrNo}")]
    public async Task<ActionResult<ApiResult<WavePickTaskDetailDto>>> GetDetail(string jrNo)
    {
        try
        {
            var header = await _context.MfJhrws.FindAsync(jrNo);
            if (header == null)
                return NotFound(new ApiResult<WavePickTaskDetailDto>
                {
                    Success = false,
                    Message = "未找到该波次拣货任务单"
                });

            var details = await _context.TfJhrws
                .Where(t => t.JR_NO == jrNo)
                .OrderBy(t => t.ITM)
                .Select(t => new TfJhrwDto
                {
                    ItemNo = t.ITM,
                    TzId = t.TZ_ID ?? "",
                    TzNo = t.TZ_NO ?? "",
                    TzItm = t.TZ_ITM ?? 0,
                    BcNo = t.BC_NO ?? "",
                    BcItm = t.BC_ITM ?? 0,
                    KeyItm = t.KEY_ITM ?? 0,
                    PrdNo = t.PRD_NO ?? "",
                    PrdName = t.PRD_NAME ?? "",
                    PrdMark = t.PRD_MARK ?? "",
                    BatNo = t.BAT_NO ?? "",
                    Wh = t.WH ?? "",
                    Chuw = t.CHUW ?? "",
                    Unit = t.UNIT ?? "",
                    Qty = t.QTY ?? 0,
                    QtyPk = t.QTY_PK ?? 0,
                    QtyMiss = t.QTY_MISS ?? 0,
                    QtyImperfect = t.QTY_IMPERFECT ?? 0,
                    ContainCode = t.CONTAIN_CODE ?? "",
                    CarNo = t.CAR_NO ?? "",
                    XjFlag = t.XJ_FLAG ?? "",
                    EstDhDd = t.EST_DH_DD,
                    OrgBilId = t.ORG_BIL_ID ?? "",
                    OrgBilNo = t.ORG_BIL_NO ?? "",
                    OrgBilItm = t.ORG_BIL_ITM ?? 0,
                    ErpBilId = t.ERP_BIL_ID ?? "",
                    ErpBilNo = t.ERP_BIL_NO ?? "",
                    ErpBilItm = t.ERP_BIL_ITM ?? 0,
                    Rem = t.REM ?? ""
                })
                .ToListAsync();

            return Ok(new ApiResult<WavePickTaskDetailDto>
            {
                Success = true,
                Data = new WavePickTaskDetailDto
                {
                    Header = header,
                    Details = details
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取波次拣货任务单详情时发生错误");
            return StatusCode(500, new ApiResult<WavePickTaskDetailDto>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>波次拣货任务单列表行数据（对应前端表格）</summary>
public class WavePickTaskDto
{
    public int ItemNo { get; set; }
    public string JrNo { get; set; } = "";
    public DateTime JrDate { get; set; }
    public string WarehouseCode { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string DepCode { get; set; } = "";
    public string DepName { get; set; } = "";
    public string UsrName { get; set; } = "";
    public string AreaSh { get; set; } = "";
    public string BcNo { get; set; } = "";
    public string TzNo { get; set; } = "";
    public string OrgBilNo { get; set; } = "";
    public string StatusPg { get; set; } = "0";
    public string ClsId { get; set; } = "N";
    public string PkFlow { get; set; } = "";
    public int Priority { get; set; }
    public string UsrPk { get; set; } = "";
    public string SalNo { get; set; } = "";
    public string TypeId { get; set; } = "";
    // 表身明细字段
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public decimal Qty { get; set; }
    public decimal QtyPk { get; set; }
    public string Unit { get; set; } = "";
    public string BatNo { get; set; } = "";
    public string Chuw { get; set; } = "";
    public string CarNo { get; set; } = "";
}

/// <summary>波次拣货任务单详情（含表头+表身）</summary>
public class WavePickTaskDetailDto
{
    public MfJhrw Header { get; set; } = new();
    public List<TfJhrwDto> Details { get; set; } = new();
}

/// <summary>表身明细DTO</summary>
public class TfJhrwDto
{
    public int ItemNo { get; set; }
    public string TzId { get; set; } = "";
    public string TzNo { get; set; } = "";
    public int TzItm { get; set; }
    public string BcNo { get; set; } = "";
    public int BcItm { get; set; }
    public int KeyItm { get; set; }
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public string BatNo { get; set; } = "";
    public string Wh { get; set; } = "";
    public string Chuw { get; set; } = "";
    public string Unit { get; set; } = "";
    public decimal Qty { get; set; }
    public decimal QtyPk { get; set; }
    public decimal QtyMiss { get; set; }
    public decimal QtyImperfect { get; set; }
    public string ContainCode { get; set; } = "";
    public string CarNo { get; set; } = "";
    public string XjFlag { get; set; } = "";
    public DateTime? EstDhDd { get; set; }
    public string OrgBilId { get; set; } = "";
    public string OrgBilNo { get; set; } = "";
    public int OrgBilItm { get; set; }
    public string ErpBilId { get; set; } = "";
    public string ErpBilNo { get; set; } = "";
    public int ErpBilItm { get; set; }
    public string Rem { get; set; } = "";
}
