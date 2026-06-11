using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SystemRegistrationController : ControllerBase
{
    private readonly ILogger<SystemRegistrationController> _logger;

    public SystemRegistrationController(ILogger<SystemRegistrationController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 获取系统注册信息
    /// </summary>
    [HttpGet("info")]
    public ActionResult<ApiResult<SystemRegistrationDto>> GetRegistrationInfo()
    {
        try
        {
            // TODO: 从数据库或配置文件读取实际注册信息
            var data = new SystemRegistrationDto
            {
                OriginalRegNo = "GZ001",
                RegNo = "GZ001",
                ProductName = "AI-WMS PLUS 产品注册 V2.0(Concurrency Version)",
                UserName = "",
                Domain = "",
                StableInfo = "",
                ProxyServer = "",
                ProxyPort = ""
            };

            return Ok(new ApiResult<SystemRegistrationDto>
            {
                Success = true,
                Data = data
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取系统注册信息时发生错误");
            return StatusCode(500, new ApiResult<SystemRegistrationDto>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 注册（占位）
    /// </summary>
    [HttpPost("register")]
    public ActionResult<ApiResult<object>> Register([FromBody] SystemRegistrationDto dto)
    {
        try
        {
            // TODO: 实现实际注册逻辑
            return Ok(new ApiResult<object>
            {
                Success = true,
                Message = "注册成功",
                Data = new { }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "系统注册时发生错误");
            return StatusCode(500, new ApiResult<object>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 清除注册记录（占位）
    /// </summary>
    [HttpPost("clear")]
    public ActionResult<ApiResult<object>> ClearRegistration()
    {
        try
        {
            // TODO: 实际清除逻辑
            return Ok(new ApiResult<object>
            {
                Success = true,
                Message = "清除成功",
                Data = new { }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "清除注册记录时发生错误");
            return StatusCode(500, new ApiResult<object>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 重新出厂（占位）
    /// </summary>
    [HttpPost("reset")]
    public ActionResult<ApiResult<object>> ResetFactory()
    {
        try
        {
            // TODO: 实际重置逻辑
            return Ok(new ApiResult<object>
            {
                Success = true,
                Message = "重置成功",
                Data = new { }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "重新出厂时发生错误");
            return StatusCode(500, new ApiResult<object>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

/// <summary>系统注册数据传输对象</summary>
public class SystemRegistrationDto
{
    /// <summary>产品名称</summary>
    public string ProductName { get; set; } = "AI-WMS PLUS 产品注册 V2.0(Concurrency Version)";

    /// <summary>原注册号（只读）</summary>
    public string OriginalRegNo { get; set; } = "GZ001";

    /// <summary>注册号</summary>
    public string RegNo { get; set; } = "GZ001";

    /// <summary>用户名</summary>
    public string UserName { get; set; } = "";

    /// <summary>域名</summary>
    public string Domain { get; set; } = "";

    /// <summary>稳模信息</summary>
    public string StableInfo { get; set; } = "";

    /// <summary>代理服务器地址</summary>
    public string ProxyServer { get; set; } = "";

    /// <summary>代理服务器端口</summary>
    public string ProxyPort { get; set; } = "";
}
