using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InspectionTaskDetailController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<InspectionTaskDetailController> _logger;

    public InspectionTaskDetailController(WarehouseDbContext context, ILogger<InspectionTaskDetailController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<InspectionTaskDetailDto>>>> Search(
        [FromQuery] string? qjNo,
        [FromQuery] string? whTy,
        [FromQuery] string? usr,
        [FromQuery] string? tywz,
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo)
    {
        try
        {
            var query = from t in _context.TfQjrws
                        join m in _context.MfQjrws on t.QJ_NO equals m.QJ_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.QJ_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.QJ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(qjNo))
                query = query.Where(x => x.T.QJ_NO.Contains(qjNo));
            if (!string.IsNullOrWhiteSpace(whTy))
                query = query.Where(x => x.M != null && (x.M.WH_TY != null && x.M.WH_TY.Contains(whTy)));
            if (!string.IsNullOrWhiteSpace(usr))
                query = query.Where(x => x.M != null && (x.M.USR != null && x.M.USR.Contains(usr)));
            if (!string.IsNullOrWhiteSpace(tywz) && tywz != "全部")
                query = query.Where(x => x.M != null && false); // MfQjrw无TYWZ字段，此条件始终不匹配

            query = query.OrderBy(x => x.T.QJ_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            var list = rawList.Select((x, idx) => new InspectionTaskDetailDto
            {
                ItemNo = idx + 1,
                QjDd = x.M?.QJ_DD,
                QjNo = x.T.QJ_NO ?? "",
                WhTy = x.M?.WH_TY ?? "",
                ConNo = x.T.CON_NO ?? "",
                Dep = x.M?.DEP ?? "",
                BilKnd = x.M?.BIL_KND ?? "",
                Tywz = "",  // MfQjrw 无此字段，留空
                TnNo = x.T.TN_NO ?? "",
                XjFlag = x.M?.XJ_FLAG ?? "",
                Rem = x.M?.REM ?? "",
                Usr = x.M?.USR ?? "",
                SysDate = x.M?.SYS_DATE
            }).ToList();

            return Ok(new ApiResult<List<InspectionTaskDetailDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询请检任务单明细时发生错误");
            return StatusCode(500, new ApiResult<List<InspectionTaskDetailDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

public class InspectionTaskDetailDto
{
    public int ItemNo { get; set; }
    public DateTime? QjDd { get; set; }
    public string QjNo { get; set; } = "";
    public string WhTy { get; set; } = "";
    public string ConNo { get; set; } = "";
    public string Dep { get; set; } = "";
    public string BilKnd { get; set; } = "";
    public string Tywz { get; set; } = "";
    public string TnNo { get; set; } = "";
    public string XjFlag { get; set; } = "";
    public string Rem { get; set; } = "";
    public string Usr { get; set; } = "";
    public DateTime? SysDate { get; set; }
}
