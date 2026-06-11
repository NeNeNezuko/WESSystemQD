using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RegistrationInfoController : ControllerBase
{
    private readonly ILogger<RegistrationInfoController> _logger;

    public RegistrationInfoController(ILogger<RegistrationInfoController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 查询注册信息模块列表
    /// </summary>
    [HttpGet("search")]
    public ActionResult<ApiResult<List<RegistrationInfoDto>>> Search(
        [FromQuery] string? serialNo = null,
        [FromQuery] string? applyDate = null,
        [FromQuery] string? version = null,
        [FromQuery] string? contractExpireDate = null,
        [FromQuery] string? stopDate = null,
        [FromQuery] bool stopFlag = false,
        [FromQuery] string? moduleTab = "module-list")
    {
        try
        {
            // 根据内部标签页返回不同的模拟数据
            List<RegistrationInfoDto> data = moduleTab switch
            {
                "module-list-two" => GetModuleListTwo(),
                "module-list-three" => GetModuleListThree(),
                "other" => GetOtherModules(),
                _ => GetModuleList()
            };

            // TODO: 实际项目中从数据库查询

            return Ok(new ApiResult<List<RegistrationInfoDto>>
            {
                Success = true,
                Data = data,
                Total = data.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询注册信息时发生错误");
            return StatusCode(500, new ApiResult<List<RegistrationInfoDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    #region 模拟数据

    private static List<RegistrationInfoDto> GetModuleList() => new()
    {
        new() { ModuleCode = "AI_WMS", ModuleName = "AI-WMS PLUS", MaxUsers = 3, UserCount = 1 },
        new() { ModuleCode = "AI_MES2", ModuleName = "AI-MES", MaxUsers = 3, UserCount = 1 }
    };

    private static List<RegistrationInfoDto> GetModuleListTwo() => new()
    {
        new() { ModuleCode = "AI_PICK", ModuleName = "AI-PICK", MaxUsers = 5, UserCount = 2 },
        new() { ModuleCode = "AI_WAVE", ModuleName = "AI-WAVE", MaxUsers = 5, UserCount = 1 }
    };

    private static List<RegistrationInfoDto> GetModuleListThree() => new()
    {
        new() { ModuleCode = "AI_RFID", ModuleName = "AI-RFID", MaxUsers = 2, UserCount = 0 },
        new() { ModuleCode = "AI_AGV", ModuleName = "AI-AGV", MaxUsers = 2, UserCount = 0 }
    };

    private static List<RegistrationInfoDto> GetOtherModules() => new()
    {
        new() { ModuleCode = "AI_REPORT", ModuleName = "AI-REPORT", MaxUsers = 10, UserCount = 5 },
        new() { ModuleCode = "AI_MOBILE", ModuleName = "AI-MOBILE", MaxUsers = 10, UserCount = 8 }
    };

    #endregion
}

/// <summary>注册信息列表行数据</summary>
public class RegistrationInfoDto
{
    /// <summary>主模块代号</summary>
    public string ModuleCode { get; set; } = "";
    /// <summary>主模块名称</summary>
    public string ModuleName { get; set; } = "";
    /// <summary>最大用户数</summary>
    public int MaxUsers { get; set; }
    /// <summary>用户数</summary>
    public int UserCount { get; set; }
}
