using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StorageNatureSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<StorageNatureSettingController> _logger;

    public StorageNatureSettingController(WarehouseDbContext context, ILogger<StorageNatureSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<StorageNatureSettingDto>>>> Search(
        [FromQuery] string? cwxzNo,
        [FromQuery] string? name)
    {
        try
        {
            var query = _context.CwXzs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(cwxzNo))
                query = query.Where(x => x.CWXZ_NO != null && x.CWXZ_NO.Contains(cwxzNo));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            query = query.OrderBy(x => x.CWXZ_NO);

            var list = await query.Select(x => new StorageNatureSettingDto
            {
                CwxzNo = x.CWXZ_NO ?? "",
                Name = x.NAME ?? "",
                UpDd = x.UP_DD
            }).ToListAsync();

            return Ok(new ApiResult<List<StorageNatureSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询储存性质设定时发生错误");
            return StatusCode(500, new ApiResult<List<StorageNatureSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>储存性质设定列表行数据（对应前端表格）</summary>
public class StorageNatureSettingDto
{
    public string CwxzNo { get; set; } = "";
    public string Name { get; set; } = "";
    public DateTime? UpDd { get; set; }
}
