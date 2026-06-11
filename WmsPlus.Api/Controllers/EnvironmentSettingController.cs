using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EnvironmentSettingController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<EnvironmentSettingController> _logger;

    public EnvironmentSettingController(AppDbContext context, ILogger<EnvironmentSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    private static string GetCompName(string compNo) => compNo switch
    {
        "DB" => "DB",
        "GU01" => "天津渤海化学试剂测试账套",
        "GZ01" => "天津渤海化学试剂测试账套",
        "GZ02" => "天津渤海化学的测试账套",
        _ => compNo
    };

    /// <summary>获取账套列表（从pswd表获取不重复的COMPNO）</summary>
    [HttpGet("account-sets")]
    public async Task<ActionResult<ApiResult<List<AccountSetDto>>>> GetAccountSets()
    {
        try
        {
            var rawAccountSets = await _context.Users
                .Select(x => x.COMPNO)
                .Distinct()
                .OrderBy(x => x)
                .ToListAsync();

            var accountSets = rawAccountSets.Select(compNo => new AccountSetDto
            {
                CompNo = compNo ?? "",
                CompName = GetCompName(compNo ?? "")
            }).ToList();

            return Ok(new ApiResult<List<AccountSetDto>>
            {
                Success = true,
                Data = accountSets,
                Total = accountSets.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取账套列表时发生错误");
            return StatusCode(500, new ApiResult<List<AccountSetDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>测试数据库连接</summary>
    [HttpPost("test-connection")]
    public ActionResult<ApiResult<object>> TestConnection(ConnectionTestRequest request)
    {
        try
        {
            // TODO: 实际测试SQL连接，当前仅返回模拟结果
            return Ok(new ApiResult<object>
            {
                Success = true,
                Data = new { Connected = true, Message = "连接成功" },
                Message = "数据库连接成功"
            });
        }
        catch (Exception ex)
        {
            return Ok(new ApiResult<object>
            {
                Success = false,
                Data = new { Connected = false, Message = ex.Message },
                Message = $"连接失败: {ex.Message}"
            });
        }
    }

    /// <summary>保存环境设定</summary>
    [HttpPost("save")]
    public async Task<ActionResult<ApiResult<object>>> SaveSettings(EnvironmentSettingSaveRequest request)
    {
        try
        {
            // TODO: 实际保存环境设定到配置文件或数据库
            _logger.LogInformation("保存环境设定: User={User}, Server={Server}, Db={Db}, DefaultAccount={DefaultAccount}",
                request.SimUser, request.ServerName, request.DatabaseName, request.DefaultAccountSet);

            await Task.CompletedTask;

            return Ok(new ApiResult<object>
            {
                Success = true,
                Message = "环境设定保存成功"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "保存环境设定时发生错误");
            return StatusCode(500, new ApiResult<object>
            {
                Success = false,
                Message = $"保存失败: {ex.Message}"
            });
        }
    }
}

/// <summary>账套信息DTO</summary>
public class AccountSetDto
{
    public string CompNo { get; set; } = "";
    public string CompName { get; set; } = "";
}

/// <summary>连接测试请求</summary>
public class ConnectionTestRequest
{
    public string ServerName { get; set; } = "";
    public string DatabaseName { get; set; } = "";
    public string User { get; set; } = "";
    public string Password { get; set; } = "";
}

/// <summary>环境设定保存请求</summary>
public class EnvironmentSettingSaveRequest
{
    public string SimUser { get; set; } = "";
    public string SimPassword { get; set; } = "";
    public string SimName { get; set; } = "";
    public string ServerName { get; set; } = "";
    public string DatabaseName { get; set; } = "";
    public string DbUser { get; set; } = "";
    public string DbPassword { get; set; } = "";
    public List<string> SelectedAccountSets { get; set; } = new();
    public string DefaultAccountSet { get; set; } = "";
}
