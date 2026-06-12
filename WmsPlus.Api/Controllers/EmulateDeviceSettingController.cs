using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EmulateDeviceSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<EmulateDeviceSettingController> _logger;

    public EmulateDeviceSettingController(WarehouseDbContext context, ILogger<EmulateDeviceSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<EmulateDeviceSettingDto>>>> Search(
        [FromQuery] string? deviceId,
        [FromQuery] string? typeId)
    {
        try
        {
            var query = _context.EmulateSets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(deviceId))
                query = query.Where(x => x.DEVICE_ID != null && x.DEVICE_ID.Contains(deviceId));

            if (!string.IsNullOrWhiteSpace(typeId) && typeId != "全部")
                query = query.Where(x => x.TYPE_ID != null && x.TYPE_ID == typeId);

            query = query.OrderBy(x => x.DEVICE_ID);

            var list = await query.Select(x => new EmulateDeviceSettingDto
            {
                EmulateId = x.EMULATE_ID,
                DeviceId = x.DEVICE_ID ?? "",
                TypeId = x.TYPE_ID ?? "",
                StatusId = x.STATUS_ID ?? "",
                ModifyDd = x.MODIFY_DD
            }).ToListAsync();

            return Ok(new ApiResult<List<EmulateDeviceSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询仿真布局设备设定时发生错误");
            return StatusCode(500, new ApiResult<List<EmulateDeviceSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>仿真布局设备设定列表行数据</summary>
public class EmulateDeviceSettingDto
{
    public int? EmulateId { get; set; }
    public string DeviceId { get; set; } = "";
    public string TypeId { get; set; } = "";
    public string StatusId { get; set; } = "";
    public DateTime? ModifyDd { get; set; }
}
