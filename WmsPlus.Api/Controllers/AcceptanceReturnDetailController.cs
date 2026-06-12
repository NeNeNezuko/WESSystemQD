using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AcceptanceReturnDetailController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<AcceptanceReturnDetailController> _logger;

    public AcceptanceReturnDetailController(WarehouseDbContext context, ILogger<AcceptanceReturnDetailController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<AcceptanceReturnDetailDto>>>> Search(
        [FromQuery] string? ybNo,
        [FromQuery] string? cusNo,
        [FromQuery] string? tyNo,
        [FromQuery] string? usr,
        [FromQuery] string? bilKnd,
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo)
    {
        try
        {
            var query = from t in _context.TfYbs
                        join m in _context.MfYbs on t.YB_NO equals m.YB_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.YB_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.YB_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(ybNo))
                query = query.Where(x => x.T.YB_NO.Contains(ybNo));
            if (!string.IsNullOrWhiteSpace(cusNo))
                query = query.Where(x => x.M != null && (x.M.CUS_NO != null && x.M.CUS_NO.Contains(cusNo)));
            if (!string.IsNullOrWhiteSpace(tyNo))
                query = query.Where(x => x.T.TY_NO.Contains(tyNo));
            if (!string.IsNullOrWhiteSpace(usr))
                query = query.Where(x => x.M != null && (x.M.USR != null && x.M.USR.Contains(usr)));
            if (!string.IsNullOrWhiteSpace(bilKnd) && bilKnd != "全部")
                query = query.Where(x => x.M != null && (x.M.BIL_KND != null && x.M.BIL_KND == bilKnd));

            query = query.OrderBy(x => x.T.YB_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            var list = rawList.Select((x, idx) => new AcceptanceReturnDetailDto
            {
                ItemNo = idx + 1,
                YbDd = x.M?.YB_DD,
                YbNo = x.T.YB_NO ?? "",
                CusNo = x.M?.CUS_NO ?? "",
                CusName = x.M?.CUS_NAME ?? "",
                TyNo = x.T.TY_NO ?? "",
                BilKnd = x.M?.BIL_KND ?? "",
                Dep = x.M?.DEP ?? "",
                TypeId = x.M?.TYPE_ID ?? "",
                Rem = x.M?.REM ?? "",
                Usr = x.M?.USR ?? "",
                SysDate = x.M?.SYS_DATE
            }).ToList();

            return Ok(new ApiResult<List<AcceptanceReturnDetailDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询验收退回单明细时发生错误");
            return StatusCode(500, new ApiResult<List<AcceptanceReturnDetailDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

public class AcceptanceReturnDetailDto
{
    public int ItemNo { get; set; }
    public DateTime? YbDd { get; set; }
    public string YbNo { get; set; } = "";
    public string CusNo { get; set; } = "";
    public string CusName { get; set; } = "";
    public string TyNo { get; set; } = "";
    public string BilKnd { get; set; } = "";
    public string Dep { get; set; } = "";
    public string TypeId { get; set; } = "";
    public string Rem { get; set; } = "";
    public string Usr { get; set; } = "";
    public DateTime? SysDate { get; set; }
}
