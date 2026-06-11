using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StorageShelvingReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<StorageShelvingReportController> _logger;

    public StorageShelvingReportController(WarehouseDbContext context, ILogger<StorageShelvingReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询储位上架单报告列表
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<StorageShelvingReportDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? sjNo,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? tabType = "detail")
    {
        try
        {
            if (tabType == "summary")
            {
                return await SearchSummary(dateFrom, dateTo, sjNo, warehouseCode);
            }

            // 明细表查询 - 以表身TF_CWSJ为主，LEFT JOIN表头MF_CWSJ
            var query = from t in _context.Set<TfCwsj>()
                        join m in _context.Set<MfCwsj>() on t.SJ_NO equals m.SJ_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.SJ_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.SJ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(sjNo))
                query = query.Where(x => x.T.SJ_NO != null && x.T.SJ_NO.Contains(sjNo));
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.M != null && x.M.WH != null && x.M.WH.Contains(warehouseCode));

            query = query.OrderBy(x => x.T.SJ_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            var list = rawList.Select(x => new StorageShelvingReportDto
            {
                ItemNo = x.T.ITM,
                SjNo = x.T.SJ_NO ?? "",
                SjDate = x.M?.SJ_DD,
                Wh = x.T.WH1 ?? x.M?.WH ?? "",
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                Qty = x.T.QTY,
                Rem = x.T.REM ?? ""
            }).ToList();

            return Ok(new ApiResult<List<StorageShelvingReportDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询储位上架单报告时发生错误");
            return StatusCode(500, new ApiResult<List<StorageShelvingReportDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    private async Task<ActionResult<ApiResult<List<StorageShelvingReportDto>>>> SearchSummary(
        DateTime? dateFrom, DateTime? dateTo, string? sjNo, string? warehouseCode)
    {
        var query = from m in _context.Set<MfCwsj>()
                    select m;

        if (dateFrom.HasValue)
            query = query.Where(x => x.SJ_DD >= dateFrom.Value);
        if (dateTo.HasValue)
            query = query.Where(x => x.SJ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
        if (!string.IsNullOrWhiteSpace(sjNo))
            query = query.Where(x => x.SJ_NO != null && x.SJ_NO.Contains(sjNo));
        if (!string.IsNullOrWhiteSpace(warehouseCode))
            query = query.Where(x => x.WH != null && x.WH.Contains(warehouseCode));

        query = query.OrderBy(x => x.SJ_NO);

        var rawList = await query.ToListAsync();

        var list = rawList.Select(x => new StorageShelvingReportDto
        {
            SjNo = x.SJ_NO ?? "",
            SjDate = x.SJ_DD,
            Wh = x.WH ?? "",
            Dep = x.DEP ?? "",
            SalNo = x.SAL_NO ?? "",
            BilType = x.BIL_TYPE ?? "",
            BilId = x.BIL_ID ?? "",
            BilNo = x.BIL_NO ?? "",
            Usr = x.USR ?? "",
            SysDate = x.SYS_DATE
        }).ToList();

        return Ok(new ApiResult<List<StorageShelvingReportDto>>
        {
            Success = true,
            Data = list,
            Total = list.Count
        });
    }
}

public class StorageShelvingReportDto
{
    public int ItemNo { get; set; }
    public string SjNo { get; set; } = "";
    public DateTime? SjDate { get; set; }
    public string Wh { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public decimal? Qty { get; set; }
    public string Rem { get; set; } = "";
    // 统计表字段
    public string Dep { get; set; } = "";
    public string SalNo { get; set; } = "";
    public string BilType { get; set; } = "";
    public string BilId { get; set; } = "";
    public string BilNo { get; set; } = "";
    public string Usr { get; set; } = "";
    public DateTime? SysDate { get; set; }
}
