using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StockInspectionAnalysisController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<StockInspectionAnalysisController> _logger;

    public StockInspectionAnalysisController(WarehouseDbContext context, ILogger<StockInspectionAnalysisController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询库存检验时效分析表列表（以MF_QJRW为主表，LEFT JOIN MF_TY、MF_IC）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<StockInspectionAnalysisDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? qjNo,
        [FromQuery] string? whTy,
        [FromQuery] string? bilKnd)
    {
        try
        {
            // 以MF_QJRW为主表，LEFT JOIN MF_TY(检验)、MF_IC(调拨)
            var query = from qj in _context.MfQjrws
                        join ty in _context.MfTys on qj.QJ_NO equals ty.TZ_NO_UO into tyj
                        from ty in tyj.DefaultIfEmpty()
                        join ic in _context.MfIcs on qj.QJ_NO equals ic.TZ_NO_UO into icj
                        from ic in icj.DefaultIfEmpty()
                        select new { Qj = qj, Ty = ty, Ic = ic };

            // 请检日期范围筛选
            if (dateFrom.HasValue)
                query = query.Where(x => x.Qj.QJ_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.Qj.QJ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 请检任务单号模糊匹配
            if (!string.IsNullOrWhiteSpace(qjNo))
                query = query.Where(x => x.Qj.QJ_NO.Contains(qjNo));

            // 检验仓库模糊匹配
            if (!string.IsNullOrWhiteSpace(whTy))
                query = query.Where(x => x.Qj.WH_TY != null && x.Qj.WH_TY.Contains(whTy));

            // 单据类型筛选
            if (!string.IsNullOrWhiteSpace(bilKnd) && bilKnd != "全部")
                query = query.Where(x => x.Qj.BIL_KND == bilKnd);

            // 按请检日期降序排序
            query = query.OrderByDescending(x => x.Qj.QJ_DD).ThenBy(x => x.Qj.QJ_NO);

            var rawList = await query.ToListAsync();

            // 在内存中做投影并计算时效
            var list = rawList.Select((x, index) => new StockInspectionAnalysisDto
            {
                ItemNo = index + 1,
                QjNo = x.Qj.QJ_NO ?? "",
                QjDd = x.Qj.QJ_DD,
                WhTy = x.Qj.WH_TY ?? "",
                BilKnd = x.Qj.BIL_KND ?? "",
                Tywz = "",  // MfQjrw 无此字段，留空
                TyNo = x.Ty?.TY_NO ?? "",
                TyDd = x.Ty?.TY_DD,
                IcNo = x.Ic?.IC_NO ?? "",
                IcDd = x.Ic?.IC_DD,
                QjToTyHours = (x.Qj.QJ_DD.HasValue && x.Ty != null && x.Ty.TY_DD.HasValue)
                    ? (x.Ty.TY_DD.Value - x.Qj.QJ_DD.Value).TotalHours : (double?)null,
                TotalHours = (x.Qj.QJ_DD.HasValue && x.Ic != null && x.Ic.IC_DD.HasValue)
                    ? (x.Ic.IC_DD.Value - x.Qj.QJ_DD.Value).TotalHours : (double?)null,
                Usr = x.Qj.USR ?? ""
            }).ToList();

            return Ok(new ApiResult<List<StockInspectionAnalysisDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询库存检验时效分析表时发生错误");
            return StatusCode(500, new ApiResult<List<StockInspectionAnalysisDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>库存检验时效分析表行数据（对应前端表格）</summary>
public class StockInspectionAnalysisDto
{
    public int ItemNo { get; set; }
    public string QjNo { get; set; } = "";
    public DateTime? QjDd { get; set; }
    public string WhTy { get; set; } = "";
    public string BilKnd { get; set; } = "";
    public string Tywz { get; set; } = "";
    public string TyNo { get; set; } = "";
    public DateTime? TyDd { get; set; }
    public string IcNo { get; set; } = "";
    public DateTime? IcDd { get; set; }
    public double? QjToTyHours { get; set; }
    public double? TotalHours { get; set; }
    public string Usr { get; set; } = "";
}
