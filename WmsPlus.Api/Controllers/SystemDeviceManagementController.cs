using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SystemDeviceManagementController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<SystemDeviceManagementController> _logger;

    public SystemDeviceManagementController(WarehouseDbContext context, ILogger<SystemDeviceManagementController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<SystemDeviceManagementDto>>>> Search(
        [FromQuery] string? hwNo,
        [FromQuery] string? name)
    {
        try
        {
            var query = _context.HwSets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(hwNo))
                query = query.Where(x => x.HW_NO != null && x.HW_NO.Contains(hwNo));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            query = query.OrderBy(x => x.HW_NO);

            var list = await query.Select(x => new SystemDeviceManagementDto
            {
                HwNo = x.HW_NO ?? "",
                Name = x.NAME ?? "",
                Ip = x.IP ?? "",
                Port = x.PORT ?? "",
                ModelNo = x.MODEL_NO ?? "",
                TypeNo = x.TYPE_NO ?? "",
                Wh = x.WH ?? "",
                StopId = x.STOP_ID ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<SystemDeviceManagementDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询系统设备管理时发生错误");
            return StatusCode(500, new ApiResult<List<SystemDeviceManagementDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>系统设备管理列表行数据</summary>
public class SystemDeviceManagementDto
{
    public string HwNo { get; set; } = "";
    public string Name { get; set; } = "";
    public string Ip { get; set; } = "";
    public string Port { get; set; } = "";
    public string ModelNo { get; set; } = "";
    public string TypeNo { get; set; } = "";
    public string Wh { get; set; } = "";
    public string StopId { get; set; } = "";
}
