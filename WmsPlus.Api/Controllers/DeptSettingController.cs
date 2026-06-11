using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DeptSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<DeptSettingController> _logger;

    public DeptSettingController(WarehouseDbContext context, ILogger<DeptSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<DeptSettingDto>>>> Search(
        [FromQuery] string? dep,
        [FromQuery] string? name)
    {
        try
        {
            var query = _context.Depts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(dep))
                query = query.Where(x => x.DEP != null && x.DEP.Contains(dep));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            query = query.OrderBy(x => x.DEP);

            var list = await query.Select(x => new DeptSettingDto
            {
                Dep = x.DEP ?? "",
                Name = x.NAME ?? "",
                Up = x.UP ?? "",
                MakeId = x.MAKE_ID ?? "",
                StopDd = x.STOP_DD
            }).ToListAsync();

            return Ok(new ApiResult<List<DeptSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询部门设定时发生错误");
            return StatusCode(500, new ApiResult<List<DeptSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>部门设定列表行数据（对应前端表格）</summary>
public class DeptSettingDto
{
    public string Dep { get; set; } = "";
    public string Name { get; set; } = "";
    public string Up { get; set; } = "";
    public string MakeId { get; set; } = "";
    public DateTime? StopDd { get; set; }
}
