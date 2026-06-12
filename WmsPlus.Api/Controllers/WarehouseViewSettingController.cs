using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WarehouseViewSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<WarehouseViewSettingController> _logger;

    public WarehouseViewSettingController(WarehouseDbContext context, ILogger<WarehouseViewSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<WarehouseViewSettingDto>>>> Search(
        [FromQuery] string? vwNo,
        [FromQuery] string? name)
    {
        try
        {
            var query = _context.MyWhViews.AsQueryable();

            if (!string.IsNullOrWhiteSpace(vwNo))
                query = query.Where(x => x.VW_NO != null && x.VW_NO.Contains(vwNo));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            query = query.OrderBy(x => x.VW_NO);

            var list = await query.Select(x => new WarehouseViewSettingDto
            {
                VwNo = x.VW_NO ?? "",
                Name = x.NAME ?? "",
                StopId = x.STOP_ID ?? "",
                SysId = x.SYS_ID ?? "",
                ChkUsrs = x.CHK_USRS ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<WarehouseViewSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询可视化仓储仿真布局设定时发生错误");
            return StatusCode(500, new ApiResult<List<WarehouseViewSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>可视化仓储仿真布局设定列表行数据</summary>
public class WarehouseViewSettingDto
{
    public string VwNo { get; set; } = "";
    public string Name { get; set; } = "";
    public string StopId { get; set; } = "";
    public string SysId { get; set; } = "";
    public string ChkUsrs { get; set; } = "";
}
