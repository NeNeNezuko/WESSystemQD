using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StorageTransferReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<StorageTransferReportController> _logger;

    public StorageTransferReportController(WarehouseDbContext context, ILogger<StorageTransferReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<StorageTransferReportDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? dbNo,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? tabType = "detail")
    {
        try
        {
            if (tabType == "summary")
                return await SearchSummary(dateFrom, dateTo, dbNo, warehouseCode);

            var query = from t in _context.Set<TfCwdb>()
                        join m in _context.Set<MfCwdb>() on t.DB_NO equals m.DB_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.DB_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.DB_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(dbNo))
                query = query.Where(x => x.T.DB_NO != null && x.T.DB_NO.Contains(dbNo));
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.M != null && x.M.WH != null && x.M.WH.Contains(warehouseCode));

            query = query.OrderBy(x => x.T.DB_NO).ThenBy(x => x.T.ITM);
            var rawList = await query.ToListAsync();

            var list = rawList.Select(x => new StorageTransferReportDto
            {
                ItemNo = x.T.ITM,
                DbNo = x.T.DB_NO ?? "",
                DbDate = x.M?.DB_DD,
                Wh = x.M?.WH ?? "",
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                Qty = x.T.QTY,
                Rem = x.T.REM ?? ""
            }).ToList();

            return Ok(new ApiResult<List<StorageTransferReportDto>> { Success = true, Data = list, Total = list.Count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询储位调拨单报告时发生错误");
            return StatusCode(500, new ApiResult<List<StorageTransferReportDto>> { Success = false, Message = $"服务器内部错误: {ex.Message}" });
        }
    }

    private async Task<ActionResult<ApiResult<List<StorageTransferReportDto>>>> SearchSummary(DateTime? dateFrom, DateTime? dateTo, string? dbNo, string? warehouseCode)
    {
        var query = from m in _context.Set<MfCwdb>() select m;
        if (dateFrom.HasValue) query = query.Where(x => x.DB_DD >= dateFrom.Value);
        if (dateTo.HasValue) query = query.Where(x => x.DB_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
        if (!string.IsNullOrWhiteSpace(dbNo)) query = query.Where(x => x.DB_NO != null && x.DB_NO.Contains(dbNo));
        if (!string.IsNullOrWhiteSpace(warehouseCode)) query = query.Where(x => x.WH != null && x.WH.Contains(warehouseCode));

        var rawList = await query.OrderBy(x => x.DB_NO).ToListAsync();
        var list = rawList.Select(x => new StorageTransferReportDto
        {
            DbNo = x.DB_NO ?? "",
            DbDate = x.DB_DD,
            Wh = x.WH ?? "",
            Dep = x.DEP ?? "",
            SalNo = x.SAL_NO ?? "",
            BilType = x.BIL_TYPE ?? "",
            Usr = x.USR ?? "",
            SysDate = x.SYS_DATE
        }).ToList();
        return Ok(new ApiResult<List<StorageTransferReportDto>> { Success = true, Data = list, Total = list.Count });
    }
}

public class StorageTransferReportDto
{
    public int ItemNo { get; set; }
    public string DbNo { get; set; } = "";
    public DateTime? DbDate { get; set; }
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
