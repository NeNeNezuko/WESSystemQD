using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InventoryTransferReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<InventoryTransferReportController> _logger;

    public InventoryTransferReportController(WarehouseDbContext context, ILogger<InventoryTransferReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<InventoryTransferReportDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? icNo,
        [FromQuery] string? warehouseCode)
    {
        try
        {
            var query = from t in _context.Set<TfIc>()
                        join m in _context.Set<MfIc>() on t.IC_NO equals m.IC_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.IC_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.IC_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(icNo))
                query = query.Where(x => x.T.IC_NO != null && x.T.IC_NO.Contains(icNo));
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.M != null && x.M.WH != null && x.M.WH.Contains(warehouseCode));

            query = query.OrderBy(x => x.T.IC_NO).ThenBy(x => x.T.ITM);
            var rawList = await query.ToListAsync();

            var list = rawList.Select(x => new InventoryTransferReportDto
            {
                IcNo = x.T.IC_NO ?? "",
                IcDate = x.M?.IC_DD,
                Wh = x.M?.WH ?? "",
                Dep = x.M?.DEP ?? "",
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                Qty = x.T.QTY,
                BilType = x.M?.BIL_TYPE ?? "",
                BilNo = x.M?.BIL_NO ?? "",
                SalNo = x.M?.SAL_NO ?? "",
                Usr = x.M?.USR ?? "",
                SysDate = x.M?.SYS_DATE,
                Rem = x.T.REM ?? ""
            }).ToList();

            return Ok(new ApiResult<List<InventoryTransferReportDto>> { Success = true, Data = list, Total = list.Count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询库存调拨单报表时发生错误");
            return StatusCode(500, new ApiResult<List<InventoryTransferReportDto>> { Success = false, Message = $"服务器内部错误: {ex.Message}" });
        }
    }
}

public class InventoryTransferReportDto
{
    public string IcNo { get; set; } = "";
    public DateTime? IcDate { get; set; }
    public string Wh { get; set; } = "";
    public string Dep { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public decimal? Qty { get; set; }
    public string BilType { get; set; } = "";
    public string BilNo { get; set; } = "";
    public string SalNo { get; set; } = "";
    public string Usr { get; set; } = "";
    public DateTime? SysDate { get; set; }
    public string Rem { get; set; } = "";
}
