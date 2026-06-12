using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ReceivingInspectionAnalysisController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ReceivingInspectionAnalysisController> _logger;

    public ReceivingInspectionAnalysisController(WarehouseDbContext context, ILogger<ReceivingInspectionAnalysisController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询收货检验时效分析表列表（以MF_SH为主表，LEFT JOIN MF_TY、MF_RK、MF_YB）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ReceivingInspectionAnalysisDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? shNo,
        [FromQuery] string? wh,
        [FromQuery] string? cusNo,
        [FromQuery] string? jyFlag)
    {
        try
        {
            var query = from sh in _context.MfShs
                        join ty in _context.MfTys on sh.SH_NO equals ty.TZ_NO_UO into tyj
                        from ty in tyj.DefaultIfEmpty()
                        join rk in _context.MfRks on sh.SH_NO equals rk.TZ_NO_UO into rkj
                        from rk in rkj.DefaultIfEmpty()
                        select new { Sh = sh, Ty = ty, Rk = rk };

            // 收货日期范围筛选
            if (dateFrom.HasValue)
                query = query.Where(x => x.Sh.SH_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.Sh.SH_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 收货单号模糊匹配
            if (!string.IsNullOrWhiteSpace(shNo))
                query = query.Where(x => x.Sh.SH_NO.Contains(shNo));

            // 仓库代号模糊匹配
            if (!string.IsNullOrWhiteSpace(wh))
                query = query.Where(x => x.Sh.WH != null && x.Sh.WH.Contains(wh));

            // 厂商代号模糊匹配
            if (!string.IsNullOrWhiteSpace(cusNo))
                query = query.Where(x => x.Sh.CUS_NO != null && x.Sh.CUS_NO.Contains(cusNo));

            // 检验标志筛选
            if (!string.IsNullOrWhiteSpace(jyFlag) && jyFlag != "全部")
                query = query.Where(x => x.Sh.FLAG_JY == jyFlag);

            // 按收货日期降序排序
            query = query.OrderByDescending(x => x.Sh.SH_DD).ThenBy(x => x.Sh.SH_NO);

            var rawList = await query.ToListAsync();

            // 在内存中做投影并计算时效
            var list = rawList.Select((x, index) => new ReceivingInspectionAnalysisDto
            {
                ItemNo = index + 1,
                ShNo = x.Sh.SH_NO ?? "",
                ShDd = x.Sh.SH_DD,
                Wh = x.Sh.WH ?? "",
                CusNo = x.Sh.CUS_NO ?? "",
                CusName = "",  // 可通过LEFT JOIN MF_YB获取，此处预留
                JyFlag = x.Sh.FLAG_JY ?? "",
                TyNo = x.Ty?.TY_NO ?? "",
                TyDd = x.Ty?.TY_DD,
                RkNo = x.Rk?.RK_NO ?? "",
                RkDd = x.Rk?.RK_DD,
                ShToTyHours = (x.Sh.SH_DD.HasValue && x.Ty != null && x.Ty.TY_DD.HasValue)
                    ? (x.Ty.TY_DD.Value - x.Sh.SH_DD.Value).TotalHours : (double?)null,
                TyToRkHours = (x.Ty != null && x.Ty.TY_DD.HasValue && x.Rk != null && x.Rk.RK_DD.HasValue)
                    ? (x.Rk.RK_DD.Value - x.Ty.TY_DD.Value).TotalHours : (double?)null,
                TotalHours = (x.Sh.SH_DD.HasValue && x.Rk != null && x.Rk.RK_DD.HasValue)
                    ? (x.Rk.RK_DD.Value - x.Sh.SH_DD.Value).TotalHours : (double?)null,
                Usr = x.Sh.USR ?? ""
            }).ToList();

            return Ok(new ApiResult<List<ReceivingInspectionAnalysisDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询收货检验时效分析表时发生错误");
            return StatusCode(500, new ApiResult<List<ReceivingInspectionAnalysisDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>收货检验时效分析表行数据（对应前端表格）</summary>
public class ReceivingInspectionAnalysisDto
{
    public int ItemNo { get; set; }
    public string ShNo { get; set; } = "";
    public DateTime? ShDd { get; set; }
    public string Wh { get; set; } = "";
    public string CusNo { get; set; } = "";
    public string CusName { get; set; } = "";
    public string JyFlag { get; set; } = "";
    public string TyNo { get; set; } = "";
    public DateTime? TyDd { get; set; }
    public string RkNo { get; set; } = "";
    public DateTime? RkDd { get; set; }
    public double? ShToTyHours { get; set; }
    public double? TyToRkHours { get; set; }
    public double? TotalHours { get; set; }
    public string Usr { get; set; } = "";
}
