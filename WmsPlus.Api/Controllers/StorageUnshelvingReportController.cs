using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StorageUnshelvingReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<StorageUnshelvingReportController> _logger;

    public StorageUnshelvingReportController(WarehouseDbContext context, ILogger<StorageUnshelvingReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<StorageUnshelvingReportDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? xjNo,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? tabType = "detail")
    {
        try
        {
            if (tabType == "summary")
                return await SearchSummary(dateFrom, dateTo, xjNo, warehouseCode);

            var query = from t in _context.Set<TfCwxj>()
                        join m in _context.Set<MfCwxj>() on t.XJ_NO equals m.XJ_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.XJ_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.XJ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(xjNo))
                query = query.Where(x => x.T.XJ_NO != null && x.T.XJ_NO.Contains(xjNo));
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.M != null && x.M.WH != null && x.M.WH.Contains(warehouseCode));

            query = query.OrderBy(x => x.T.XJ_NO).ThenBy(x => x.T.ITM);
            var rawList = await query.ToListAsync();

            var list = rawList.Select(x => new StorageUnshelvingReportDto
            {
                ItemNo = x.T.ITM,
                XjNo = x.T.XJ_NO ?? "",
                XjDate = x.M?.XJ_DD,
                Wh = x.M?.WH ?? "",
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                Qty = x.T.QTY,
                Rem = x.T.REM ?? ""
            }).ToList();

            return Ok(new ApiResult<List<StorageUnshelvingReportDto>> { Success = true, Data = list, Total = list.Count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询储位下架单报告时发生错误");
            return StatusCode(500, new ApiResult<List<StorageUnshelvingReportDto>> { Success = false, Message = $"服务器内部错误: {ex.Message}" });
        }
    }

    private async Task<ActionResult<ApiResult<List<StorageUnshelvingReportDto>>>> SearchSummary(DateTime? dateFrom, DateTime? dateTo, string? xjNo, string? warehouseCode)
    {
        var query = from m in _context.Set<MfCwxj>() select m;
        if (dateFrom.HasValue) query = query.Where(x => x.XJ_DD >= dateFrom.Value);
        if (dateTo.HasValue) query = query.Where(x => x.XJ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
        if (!string.IsNullOrWhiteSpace(xjNo)) query = query.Where(x => x.XJ_NO != null && x.XJ_NO.Contains(xjNo));
        if (!string.IsNullOrWhiteSpace(warehouseCode)) query = query.Where(x => x.WH != null && x.WH.Contains(warehouseCode));

        var rawList = await query.OrderBy(x => x.XJ_NO).ToListAsync();
        var list = rawList.Select(x => new StorageUnshelvingReportDto
        {
            XjNo = x.XJ_NO ?? "",
            XjDate = x.XJ_DD,
            Wh = x.WH ?? "",
            Dep = x.DEP ?? "",
            SalNo = x.SAL_NO ?? "",
            BilType = x.BIL_TYPE ?? "",
            Usr = x.USR ?? "",
            SysDate = x.SYS_DATE
        }).ToList();
        return Ok(new ApiResult<List<StorageUnshelvingReportDto>> { Success = true, Data = list, Total = list.Count });
    }
}

public class StorageUnshelvingReportDto
{
    public int ItemNo { get; set; }
    public string XjNo { get; set; } = "";
    public DateTime? XjDate { get; set; }
    public string Wh { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public decimal? Qty { get; set; }
    public string Rem { get; set; } = "";
    public string Dep { get; set; } = "";
    public string SalNo { get; set; } = "";
    public string BilType { get; set; } = "";
    public string Usr { get; set; } = "";
    public DateTime? SysDate { get; set; }
}
