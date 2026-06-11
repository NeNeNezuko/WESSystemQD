using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;
using WmsPlus.Api.Models.Entities;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductLockReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ProductLockReportController> _logger;

    public ProductLockReportController(WarehouseDbContext context, ILogger<ProductLockReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ProductLockReportDto>>>> Search(
        [FromQuery] string? warehouseCode,
        [FromQuery] string? prdNo,
        [FromQuery] string? batNo)
    {
        try
        {
            var query = from l in _context.Set<Prdt1Lock>() select l;

            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.WH != null && x.WH.Contains(warehouseCode));
            if (!string.IsNullOrWhiteSpace(prdNo))
                query = query.Where(x => x.PRD_NO != null && x.PRD_NO.Contains(prdNo));
            if (!string.IsNullOrWhiteSpace(batNo))
                query = query.Where(x => x.BAT_NO != null && x.BAT_NO.Contains(batNo));

            var rawList = await query.OrderByDescending(x => x.LOCK_DD).Take(2000).ToListAsync();

            var list = rawList.Select(x => new ProductLockReportDto
            {
                Guid = x.GUID ?? "",
                Wh = x.WH ?? "",
                PrdNo = x.PRD_NO ?? "",
                PrdMark = x.PRD_MARK ?? "",
                BatNo = x.BAT_NO ?? "",
                ActNo = x.ACT_NO ?? "",
                LockDd = x.LOCK_DD
            }).ToList();

            return Ok(new ApiResult<List<ProductLockReportDto>> { Success = true, Data = list, Total = list.Count });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询货品批号库存锁定表时发生错误");
            return StatusCode(500, new ApiResult<List<ProductLockReportDto>> { Success = false, Message = $"服务器内部错误: {ex.Message}" });
        }
    }
}

public class ProductLockReportDto
{
    public string Guid { get; set; } = "";
    public string Wh { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public string BatNo { get; set; } = "";
    public string ActNo { get; set; } = "";
    public DateTime? LockDd { get; set; }
}
