using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ForkliftManagementController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ForkliftManagementController> _logger;

    public ForkliftManagementController(WarehouseDbContext context, ILogger<ForkliftManagementController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ForkliftManagementDto>>>> Search(
        [FromQuery] string? truckNo,
        [FromQuery] string? wh)
    {
        try
        {
            var query = _context.ForkTrucks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(truckNo))
                query = query.Where(x => x.TRUCK_NO != null && x.TRUCK_NO.Contains(truckNo));

            if (!string.IsNullOrWhiteSpace(wh))
                query = query.Where(x => x.WH != null && x.WH.Contains(wh));

            query = query.OrderBy(x => x.TRUCK_NO);

            var list = await query.Select(x => new ForkliftManagementDto
            {
                TruckNo = x.TRUCK_NO ?? "",
                Name = x.NAME ?? "",
                Wh = x.WH ?? "",
                Rem = x.REM ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<ForkliftManagementDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询叉车车号管理时发生错误");
            return StatusCode(500, new ApiResult<List<ForkliftManagementDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

/// <summary>叉车车号管理列表行数据（对应前端表格）</summary>
public class ForkliftManagementDto
{
    public string TruckNo { get; set; } = "";
    public string Name { get; set; } = "";
    public string Wh { get; set; } = "";
    public string Rem { get; set; } = "";
}
