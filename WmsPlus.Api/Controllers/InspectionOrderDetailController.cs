using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InspectionOrderDetailController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<InspectionOrderDetailController> _logger;

    public InspectionOrderDetailController(WarehouseDbContext context, ILogger<InspectionOrderDetailController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<InspectionOrderDetailDto>>>> Search(
        [FromQuery] string? tyNo,
        [FromQuery] string? usr,
        [FromQuery] string? bilKnd,
        [FromQuery] string? tywz,
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo)
    {
        try
        {
            var query = from t in _context.TfTys
                        join m in _context.MfTys on t.TY_NO equals m.TY_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.TY_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.TY_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(tyNo))
                query = query.Where(x => x.T.TY_NO.Contains(tyNo));
            if (!string.IsNullOrWhiteSpace(usr))
                query = query.Where(x => x.M != null && (x.M.USR != null && x.M.USR.Contains(usr)));
            if (!string.IsNullOrWhiteSpace(bilKnd) && bilKnd != "全部")
                query = query.Where(x => x.M != null && (x.M.BIL_KND != null && x.M.BIL_KND == bilKnd));
            if (!string.IsNullOrWhiteSpace(tywz) && tywz != "全部")
                query = query.Where(x => x.M != null && (x.M.TYWZ != null && x.M.TYWZ == tywz));

            query = query.OrderBy(x => x.T.TY_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            var list = rawList.Select((x, idx) => new InspectionOrderDetailDto
            {
                ItemNo = idx + 1,
                TyDd = x.M?.TY_DD,
                TyNo = x.T.TY_NO ?? "",
                BilKnd = x.M?.BIL_KND ?? "",
                Tywz = x.M?.TYWZ ?? "",
                CusNo = x.M?.CUS_NO ?? "",
                CusName = x.M?.CUS_NAME ?? "",
                Dep = x.M?.DEP ?? "",
                TypeId = x.M?.TYPE_ID ?? "",
                BilNo = x.T.BIL_NO ?? "",
                OthBilNo = x.M?.OTH_BIL_NO ?? "",
                ClsIdSpc = x.M?.CLS_ID_SPC ?? "",
                Rem = x.M?.REM ?? "",
                Usr = x.M?.USR ?? "",
                SysDate = x.M?.SYS_DATE
            }).ToList();

            return Ok(new ApiResult<List<InspectionOrderDetailDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询检验单明细时发生错误");
            return StatusCode(500, new ApiResult<List<InspectionOrderDetailDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

public class InspectionOrderDetailDto
{
    public int ItemNo { get; set; }
    public DateTime? TyDd { get; set; }
    public string TyNo { get; set; } = "";
    public string BilKnd { get; set; } = "";
    public string Tywz { get; set; } = "";
    public string CusNo { get; set; } = "";
    public string CusName { get; set; } = "";
    public string Dep { get; set; } = "";
    public string TypeId { get; set; } = "";
    public string BilNo { get; set; } = "";
    public string OthBilNo { get; set; } = "";
    public string ClsIdSpc { get; set; } = "";
    public string Rem { get; set; } = "";
    public string Usr { get; set; } = "";
    public DateTime? SysDate { get; set; }
}
