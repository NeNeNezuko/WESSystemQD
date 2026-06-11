using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DirectPickTaskController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<DirectPickTaskController> _logger;

    public DirectPickTaskController(WarehouseDbContext context, ILogger<DirectPickTaskController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询直接拣货任务单列表（以表身TF_XJRW为主，LEFT JOIN表头MF_XJRW）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<DirectPickTaskDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? documentNumber,
        [FromQuery] string? noticeNumber,
        [FromQuery] string? deptCode,
        [FromQuery] string? warehouseCode)
    {
        try
        {
            var query = from t in _context.TfXjrws
                        join m in _context.MfXjrws on t.JR_NO equals m.JR_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 单据日期范围筛选（使用表头的JR_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.JR_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.JR_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 单据号码模糊匹配
            if (!string.IsNullOrWhiteSpace(documentNumber))
                query = query.Where(x => x.M != null && (x.M.JR_NO != null && x.M.JR_NO.Contains(documentNumber)));

            // 通知单号模糊匹配
            if (!string.IsNullOrWhiteSpace(noticeNumber))
                query = query.Where(x => x.T.TZ_NO != null && x.T.TZ_NO.Contains(noticeNumber));

            // 部门代号模糊匹配
            if (!string.IsNullOrWhiteSpace(deptCode))
                query = query.Where(x => x.M != null && (x.M.DEP != null && x.M.DEP.Contains(deptCode)));

            // 仓库代号模糊匹配
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.M != null && (x.M.WH != null && x.M.WH.Contains(warehouseCode)));

            // 按单据号+项次排序
            query = query.OrderBy(x => x.T.JR_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 关联查询部门名称和仓库名称
            var depCodes = rawList.Where(x => x.M?.DEP != null).Select(x => x.M!.DEP!).Distinct().ToList();
            var whCodes = rawList.Where(x => x.M?.WH != null).Select(x => x.M!.WH!).Distinct().ToList();

            var deptDict = await _context.Depts
                .Where(d => depCodes.Contains(d.DEP))
                .ToDictionaryAsync(d => d.DEP, d => d.NAME);

            var whDict = await _context.MyWhs
                .Where(w => whCodes.Contains(w.WH))
                .ToDictionaryAsync(w => w.WH, w => w.NAME);

            // 在内存中做投影
            var list = rawList.Select(x => new DirectPickTaskDto
            {
                ItemNo = x.T.ITM,
                DocumentDate = x.M?.JR_DD ?? DateTime.MinValue,
                DocumentNumber = x.M?.JR_NO ?? "",
                DeptName = x.M?.DEP != null && deptDict.ContainsKey(x.M.DEP) ? deptDict[x.M.DEP] : "",
                Priority = x.M?.PRIORITY,
                WarehouseCode = x.M?.WH ?? "",
                WarehouseName = x.M?.WH != null && whDict.ContainsKey(x.M.WH) ? whDict[x.M.WH] : "",
                ContainCode = x.T.CONTAIN_CODE ?? "",
                ClsId = (x.M?.CLS_ID ?? "N") == "Y",
                XjFlag = x.T.XJ_FLAG ?? ""
            }).ToList();

            return Ok(new ApiResult<List<DirectPickTaskDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询直接拣货任务单时发生错误");
            return StatusCode(500, new ApiResult<List<DirectPickTaskDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据单据号获取直接拣货任务单详情（含表身明细）
    /// </summary>
    [HttpGet("{jrNo}")]
    public async Task<ActionResult<ApiResult<DirectPickTaskDetailDto>>> GetDetail(string jrNo)
    {
        try
        {
            var header = await _context.MfXjrws.FindAsync(jrNo);
            if (header == null)
                return NotFound(new ApiResult<DirectPickTaskDetailDto>
                {
                    Success = false,
                    Message = "未找到该直接拣货任务单"
                });

            var details = await _context.TfXjrws
                .Where(t => t.JR_NO == jrNo)
                .OrderBy(t => t.ITM)
                .Select(t => new TfXjrwDto
                {
                    ItemNo = t.ITM,
                    TzId = t.TZ_ID ?? "",
                    TzNo = t.TZ_NO ?? "",
                    TzItm = t.TZ_ITM ?? 0,
                    PrdNo = t.PRD_NO ?? "",
                    PrdName = t.PRD_NAME ?? "",
                    PrdMark = t.PRD_MARK ?? "",
                    BatNo = t.BAT_NO ?? "",
                    Wh = t.WH ?? "",
                    Chuw = t.CHUW ?? "",
                    Unit = t.UNIT ?? "",
                    Qty = t.QTY ?? 0,
                    QtyPk = t.QTY_PK ?? 0,
                    ContainCode = t.CONTAIN_CODE ?? "",
                    XjFlag = t.XJ_FLAG ?? "",
                    Rem = t.REM ?? ""
                })
                .ToListAsync();

            return Ok(new ApiResult<DirectPickTaskDetailDto>
            {
                Success = true,
                Data = new DirectPickTaskDetailDto
                {
                    Header = header,
                    Details = details
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取直接拣货任务单详情时发生错误");
            return StatusCode(500, new ApiResult<DirectPickTaskDetailDto>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>直接拣货任务单列表行数据（对应前端表格）</summary>
public class DirectPickTaskDto
{
    public int ItemNo { get; set; }
    public DateTime DocumentDate { get; set; }
    public string DocumentNumber { get; set; } = "";
    public string DeptName { get; set; } = "";
    public int? Priority { get; set; }
    public string WarehouseCode { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string ContainCode { get; set; } = "";
    public bool ClsId { get; set; }
    public string XjFlag { get; set; } = "";
}

/// <summary>直接拣货任务单详情（含表头+表身）</summary>
public class DirectPickTaskDetailDto
{
    public MfXjrw Header { get; set; } = new();
    public List<TfXjrwDto> Details { get; set; } = new();
}

/// <summary>表身明细DTO</summary>
public class TfXjrwDto
{
    public int ItemNo { get; set; }
    public string TzId { get; set; } = "";
    public string TzNo { get; set; } = "";
    public int TzItm { get; set; }
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public string BatNo { get; set; } = "";
    public string Wh { get; set; } = "";
    public string Chuw { get; set; } = "";
    public string Unit { get; set; } = "";
    public decimal Qty { get; set; }
    public decimal QtyPk { get; set; }
    public string ContainCode { get; set; } = "";
    public string XjFlag { get; set; } = "";
    public string Rem { get; set; } = "";
}
