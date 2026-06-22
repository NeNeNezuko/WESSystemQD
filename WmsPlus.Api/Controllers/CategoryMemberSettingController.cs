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
    private readonly AppDbContext _context;
    private readonly ILogger<CategoryMemberSettingController> _logger;

    public CategoryMemberSettingController(AppDbContext context, ILogger<CategoryMemberSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>查询角色列表（ROLE表）</summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<CategoryMemberSettingDto>>>> Search(
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

            var list = await query.Select(x => new CategoryMemberSettingDto
            {
                RoleNo = x.ROLENO ?? "",
                Name = x.NAME ?? "",
                TypeId = x.TYPE_ID ?? "",
                CompNo = x.COMPNO ?? ""
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

    /// <summary>查询指定角色下的成员列表（PSWD_ROLE + 关联 pswd 获取用户名称）</summary>
    [HttpGet("members")]
    public async Task<ActionResult<ApiResult<List<RoleMemberDto>>>> GetMembers([FromQuery] string roleNo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(roleNo))
                return Ok(new ApiResult<List<RoleMemberDto>> { Success = true, Data = new(), Total = 0 });

            // 先获取角色的 TYPE_ID 和 COMPNO
            var role = await _context.Roles.FirstOrDefaultAsync(x => x.ROLENO == roleNo);
            if (role == null)
                return Ok(new ApiResult<List<RoleMemberDto>> { Success = true, Data = new(), Total = 0 });

            var typeId = role.TYPE_ID ?? "";
            var compNo = role.COMPNO ?? "";

            var list = await _context.PswdRoles
                .Where(x => x.ROLENO == roleNo && x.TYPE_ID == typeId && x.COMPNO == compNo)
                .OrderBy(x => x.USR)
                .Select(x => new RoleMemberDto
                {
                    Usr = x.USR ?? ""
                }).ToListAsync();

            // 关联 pswd 表补充用户名称
            foreach (var item in list)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.USR == item.Usr);
                item.Name = user?.NAME ?? "";
            }

            return Ok(new ApiResult<List<RoleMemberDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询角色成员时发生错误");
            return StatusCode(500, new ApiResult<List<RoleMemberDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>保存角色成员（先删后插）</summary>
    [HttpPut("members")]
    public async Task<ActionResult<ApiResult<bool>>> SaveMembers([FromBody] SaveMembersRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.RoleNo))
                return BadRequest(new ApiResult<bool> { Success = false, Message = "角色代号不能为空" });

            var role = await _context.Roles.FirstOrDefaultAsync(x => x.ROLENO == request.RoleNo);
            if (role == null)
                return NotFound(new ApiResult<bool> { Success = false, Message = $"角色 {request.RoleNo} 不存在" });

            var typeId = role.TYPE_ID ?? "";
            var compNo = role.COMPNO ?? "";

            // 删除该角色下原有成员
            var oldMembers = await _context.PswdRoles
                .Where(x => x.ROLENO == request.RoleNo && x.TYPE_ID == typeId && x.COMPNO == compNo)
                .ToListAsync();
            _context.PswdRoles.RemoveRange(oldMembers);

            // 批量插入新成员
            if (request.Members != null)
            {
                foreach (var usr in request.Members)
                {
                    _context.PswdRoles.Add(new PswdRole
                    {
                        COMPNO = compNo,
                        ROLENO = request.RoleNo,
                        TYPE_ID = typeId,
                        USR = usr
                    });
                }
            }

            await _context.SaveChangesAsync();
            return Ok(new ApiResult<bool> { Success = true, Data = true });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "保存角色成员时发生错误");
            return StatusCode(500, new ApiResult<bool>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>查询用户列表（pswd表，供新增用户选择器使用）</summary>
    [HttpGet("users")]
    public async Task<ActionResult<ApiResult<List<UserSelectDto>>>> GetUsers(
        [FromQuery] string? keyword,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
                query = query.Where(x =>
                    (x.USR != null && x.USR.Contains(keyword)) ||
                    (x.NAME != null && x.NAME.Contains(keyword)));

            var total = await query.CountAsync();

            var list = await query
                .OrderBy(x => x.USR)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new UserSelectDto
                {
                    Usr = x.USR ?? "",
                    Name = x.NAME ?? ""
                }).ToListAsync();

            return Ok(new ApiResult<List<UserSelectDto>>
            {
                Success = true,
                Data = list,
                Total = total
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询用户列表时发生错误");
            return StatusCode(500, new ApiResult<List<UserSelectDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>类别成员设定列表行数据（对应前端表格 - 角色列表）</summary>
public class CategoryMemberSettingDto
{
    public string RoleNo { get; set; } = "";
    public string Name { get; set; } = "";
    public string TypeId { get; set; } = "";
    public string CompNo { get; set; } = "";
}

/// <summary>角色成员行数据</summary>
public class RoleMemberDto
{
    public string Usr { get; set; } = "";
    public string Name { get; set; } = "";
}

/// <summary>保存角色成员请求体</summary>
public class SaveMembersRequest
{
    public string RoleNo { get; set; } = "";
    public List<string>? Members { get; set; }
}

/// <summary>用户选择器行数据</summary>
public class UserSelectDto
{
    public string Usr { get; set; } = "";
    public string Name { get; set; } = "";
}
