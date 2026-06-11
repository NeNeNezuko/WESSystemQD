using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SystemSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<SystemSettingController> _logger;

    public SystemSettingController(WarehouseDbContext context, ILogger<SystemSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 获取所有系统设定参数
    /// </summary>
    [HttpGet("all")]
    public async Task<ActionResult<ApiResult<object>>> GetAllSettings()
    {
        try
        {
            var spcCompList = await _context.SpcComps
                .OrderBy(x => x.CTRL_ID)
                .Select(x => new { x.CTRL_ID, Value = x.SPC_ID, x.REM })
                .ToListAsync();

            var drpPropList = await _context.DrpProps
                .OrderBy(x => x.ITEM)
                .Select(x => new { x.ITEM, x.VALUE, x.REM })
                .ToListAsync();

            return Ok(new ApiResult<object>
            {
                Success = true,
                Data = new { Settings = spcCompList, PrefixItems = drpPropList },
                Total = spcCompList.Count + drpPropList.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询系统设定时发生错误");
            return StatusCode(500, new ApiResult<object>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}
