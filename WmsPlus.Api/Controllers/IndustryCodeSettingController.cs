using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class IndustryCodeSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<IndustryCodeSettingController> _logger;

    public IndustryCodeSettingController(WarehouseDbContext context, ILogger<IndustryCodeSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<IndustryCodeSettingDto>>>> Search(
        [FromQuery] string? nsNo,
        [FromQuery] string? name)
    {
        try
        {
            var query = _context.DefNss.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nsNo))
                query = query.Where(x => x.NS_NO != null && x.NS_NO.Contains(nsNo));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            query = query.OrderBy(x => x.NS_NO);

            var list = await query.Select(x => new IndustryCodeSettingDto
            {
                NsNo = x.NS_NO ?? "",
                Name = x.NAME ?? "",
                Rem = x.REM ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<IndustryCodeSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询行业代号设定时发生错误");
            return StatusCode(500, new ApiResult<List<IndustryCodeSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

/// <summary>行业代号设定列表行数据（对应前端表格）</summary>
public class IndustryCodeSettingDto
{
    public string NsNo { get; set; } = "";
    public string Name { get; set; } = "";
    public string Rem { get; set; } = "";
}
