using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WorkshopInspectionAnalysisController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<WorkshopInspectionAnalysisController> _logger;

    public WorkshopInspectionAnalysisController(WarehouseDbContext context, ILogger<WorkshopInspectionAnalysisController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询车间入库检验时效分析表列表（以MF_CJ为主表，LEFT JOIN MF_TY、MF_RK）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<WorkshopInspectionAnalysisDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? cjNo,
        [FromQuery] string? wh,
        [FromQuery] string? salNo,
        [FromQuery] string? flagJy)
    {
        try
        {
            var query = from cj in _context.MfCjs.AsQueryable()
                        join ty in _context.MfTys on cj.TZ_NO equals ty.TZ_NO_UO into tyj
                        from ty in tyj.DefaultIfEmpty()
                        join rk in _context.MfRks on cj.RK_NO equals rk.RK_NO into rkj
                        from rk in rkj.DefaultIfEmpty()
                        select new { Cj = cj, Ty = ty, Rk = rk };

            if (dateFrom.HasValue)
                query = query.Where(x => x.Cj.CJ_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.Cj.CJ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            if (!string.IsNullOrWhiteSpace(cjNo))
                query = query.Where(x => x.Cj.CJ_NO != null && x.Cj.CJ_NO.Contains(cjNo));

            if (!string.IsNullOrWhiteSpace(wh))
                query = query.Where(x => x.Cj.WH != null && x.Cj.WH.Contains(wh));

            if (!string.IsNullOrWhiteSpace(salNo))
                query = query.Where(x => x.Cj.SAL_NO != null && x.Cj.SAL_NO.Contains(salNo));

            if (!string.IsNullOrWhiteSpace(flagJy) && flagJy != "全部")
                query = query.Where(x => x.Cj.FLAG_JY == flagJy);

            query = query.OrderByDescending(x => x.Cj.CJ_DD).ThenBy(x => x.Cj.CJ_NO);

            var rawList = await query.ToListAsync();

            var list = rawList.Select((x, index) => new WorkshopInspectionAnalysisDto
            {
                ItemNo = index + 1,
                CjNo = x.Cj.CJ_NO ?? "",
                CjDd = x.Cj.CJ_DD,
                Wh = x.Cj.WH ?? "",
                SalNo = x.Cj.SAL_NO ?? "",
                FlagJy = x.Cj.FLAG_JY ?? "",
                TyNo = x.Ty?.TY_NO ?? "",
                TyDd = x.Ty?.TY_DD,
                RkNo = x.Rk?.RK_NO ?? "",
                RkDd = x.Rk?.RK_DD,
                CjToTyHours = (x.Cj.CJ_DD.HasValue && x.Ty != null && x.Ty.TY_DD.HasValue)
                    ? (x.Ty.TY_DD.Value - x.Cj.CJ_DD.Value).TotalHours : (double?)null,
                TyToRkHours = (x.Ty != null && x.Ty.TY_DD.HasValue && x.Rk != null && x.Rk.RK_DD.HasValue)
                    ? (x.Rk.RK_DD.Value - x.Ty.TY_DD.Value).TotalHours : (double?)null,
                TotalHours = (x.Cj.CJ_DD.HasValue && x.Rk != null && x.Rk.RK_DD.HasValue)
                    ? (x.Rk.RK_DD.Value - x.Cj.CJ_DD.Value).TotalHours : (double?)null,
                Usr = x.Cj.USR ?? ""
            }).ToList();

            return Ok(new ApiResult<List<WorkshopInspectionAnalysisDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询车间入库检验时效分析表时发生错误");
            return StatusCode(500, new ApiResult<List<WorkshopInspectionAnalysisDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>车间入库检验时效分析表行数据（对应前端表格）</summary>
public class WorkshopInspectionAnalysisDto
{
    public int ItemNo { get; set; }
    public string CjNo { get; set; } = "";
    public DateTime? CjDd { get; set; }
    public string Wh { get; set; } = "";
    public string SalNo { get; set; } = "";
    public string FlagJy { get; set; } = "";
    public string TyNo { get; set; } = "";
    public DateTime? TyDd { get; set; }
    public string RkNo { get; set; } = "";
    public DateTime? RkDd { get; set; }
    public double? CjToTyHours { get; set; }
    public double? TyToRkHours { get; set; }
    public double? TotalHours { get; set; }
    public string Usr { get; set; } = "";
}
