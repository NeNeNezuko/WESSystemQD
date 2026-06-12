using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoryPermissionSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<CategoryPermissionSettingController> _logger;

    public CategoryPermissionSettingController(WarehouseDbContext context, ILogger<CategoryPermissionSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<CategoryPermissionSettingDto>>>> Search(
        [FromQuery] string? roleNo,
        [FromQuery] string? name)
    {
        try
        {
            var query = _context.Roles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(roleNo))
                query = query.Where(x => x.ROLENO != null && x.ROLENO.Contains(roleNo));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            query = query.OrderBy(x => x.ROLENO);

            var list = await query.Select(x => new CategoryPermissionSettingDto
            {
                RoleNo = x.ROLENO ?? "",
                Name = x.NAME ?? "",
                Dep = x.DEP ?? "",
                CompNo = x.COMPNO ?? "",
                TypeId = x.TYPE_ID ?? "",
                PublicId = x.PUBLIC_ID ?? "",
                Rem = x.REM ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<CategoryPermissionSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询类别权限设定时发生错误");
            return StatusCode(500, new ApiResult<List<CategoryPermissionSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>类别权限设定列表行数据（对应前端表格）</summary>
public class CategoryPermissionSettingDto
{
    public string RoleNo { get; set; } = "";
    public string Name { get; set; } = "";
    public string Dep { get; set; } = "";
    public string CompNo { get; set; } = "";
    public string TypeId { get; set; } = "";
    public string PublicId { get; set; } = "";
    public string Rem { get; set; } = "";
}
