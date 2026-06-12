using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CategoryMemberSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<CategoryMemberSettingController> _logger;

    public CategoryMemberSettingController(WarehouseDbContext context, ILogger<CategoryMemberSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<CategoryMemberSettingDto>>>> Search(
        [FromQuery] string? usr,
        [FromQuery] string? roleNo)
    {
        try
        {
            var query = _context.PswdRoles.AsQueryable();

            if (!string.IsNullOrWhiteSpace(usr))
                query = query.Where(x => x.USR != null && x.USR.Contains(usr));

            if (!string.IsNullOrWhiteSpace(roleNo))
                query = query.Where(x => x.ROLENO != null && x.ROLENO.Contains(roleNo));

            query = query.OrderBy(x => x.ROLENO).ThenBy(x => x.USR);

            var list = await query.Select(x => new CategoryMemberSettingDto
            {
                CompNo = x.COMPNO ?? "",
                RoleNo = x.ROLENO ?? "",
                Usr = x.USR ?? "",
                TypeId = x.TYPE_ID ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<CategoryMemberSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询类别成员设定时发生错误");
            return StatusCode(500, new ApiResult<List<CategoryMemberSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>类别成员设定列表行数据（对应前端表格）</summary>
public class CategoryMemberSettingDto
{
    public string CompNo { get; set; } = "";
    public string RoleNo { get; set; } = "";
    public string Usr { get; set; } = "";
    public string TypeId { get; set; } = "";
}
