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
    private readonly AppDbContext _context;
    private readonly ILogger<CategoryPermissionSettingController> _logger;

    public CategoryPermissionSettingController(AppDbContext context, ILogger<CategoryPermissionSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>查询角色列表</summary>
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
                SubId = x.SUB_ID ?? "",
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

    /// <summary>根据角色代号查询详情（含权限明细）</summary>
    [HttpGet("{roleNo}")]
    public async Task<ActionResult<ApiResult<CategoryPermissionDetailDto>>> GetByRoleNo(string roleNo)
    {
        try
        {
            var role = await _context.Roles.FindAsync(roleNo);
            if (role == null)
                return NotFound(new ApiResult<CategoryPermissionDetailDto> { Success = false, Message = "角色不存在" });

            var permissions = await _context.FxPswds
                .Where(x => x.ROLENO == roleNo)
                .OrderBy(x => x.PGM)
                .Select(x => new PermissionRowDto
                {
                    Pgm = x.PGM ?? "",
                    TypeId = x.TYPE_ID ?? "",
                    CompNo = x.COMPNO ?? "",
                    DeproNo = x.DEPRO_NO ?? "",
                    Qry = x.QRY ?? "",
                    Ins = x.INS ?? "",
                    Upd = x.UPD ?? "",
                    Del = x.DEL ?? "",
                    Prn = x.PRN ?? "",
                    Qty = x.QTY ?? "",
                    Fld = x.FLD ?? "",
                    Property = x.PROPERTY ?? "",
                    AllowId = x.ALLOW_ID ?? "",
                    Ept = x.EPT ?? ""
                }).ToListAsync();

            var dto = new CategoryPermissionDetailDto
            {
                RoleInfo = new CategoryPermissionSettingDto
                {
                    RoleNo = role.ROLENO ?? "",
                    Name = role.NAME ?? "",
                    Dep = role.DEP ?? "",
                    CompNo = role.COMPNO ?? "",
                    TypeId = role.TYPE_ID ?? "",
                    PublicId = role.PUBLIC_ID ?? "",
                    SubId = role.SUB_ID ?? "",
                    Rem = role.REM ?? ""
                },
                Permissions = permissions
            };

            return Ok(new ApiResult<CategoryPermissionDetailDto> { Success = true, Data = dto });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取角色详情时发生错误");
            return StatusCode(500, new ApiResult<CategoryPermissionDetailDto>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>新增角色及权限</summary>
    [HttpPost]
    public async Task<ActionResult<ApiResult<string>>> Create([FromBody] CategoryPermissionCreateRequest request)
    {
        try
        {
            // 检查角色代号是否已存在
            var existing = await _context.Roles.FindAsync(request.RoleInfo.RoleNo);
            if (existing != null)
                return BadRequest(new ApiResult<string> { Success = false, Message = "角色代号已存在" });

            var now = DateTime.Now;

            // 新增 ROLE 记录
            var role = new Role
            {
                ROLENO = request.RoleInfo.RoleNo,
                NAME = request.RoleInfo.Name,
                DEP = request.RoleInfo.Dep,
                DEPRO_NO = request.RoleInfo.DeproNo,
                COMPNO = request.RoleInfo.CompNo,
                TYPE_ID = request.RoleInfo.TypeId,
                PUBLIC_ID = request.RoleInfo.PublicId,
                SUB_ID = request.RoleInfo.SubId,
                REM = request.RoleInfo.Rem,
                USR = request.Usr
            };
            _context.Roles.Add(role);

            // 批量新增 FX_PSWD 权限记录
            if (request.Permissions != null && request.Permissions.Count > 0)
            {
                foreach (var p in request.Permissions)
                {
                    _context.FxPswds.Add(new FxPswd
                    {
                        ROLENO = request.RoleInfo.RoleNo,
                        PGM = p.Pgm,
                        TYPE_ID = p.TypeId,
                        COMPNO = p.CompNo,
                        DEPRO_NO = p.DeproNo,
                        QRY = p.Qry,
                        INS = p.Ins,
                        UPD = p.Upd,
                        DEL = p.Del,
                        PRN = p.Prn,
                        QTY = p.Qty,
                        FLD = p.Fld,
                        PROPERTY = p.Property,
                        ALLOW_ID = p.AllowId,
                        EPT = p.Ept
                    });
                }
            }

            await _context.SaveChangesAsync();
            return Ok(new ApiResult<string> { Success = true, Message = "新增成功" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "新增角色时发生错误");
            return StatusCode(500, new ApiResult<string> { Success = false, Message = $"服务器内部错误: {ex.Message}" });
        }
    }

    /// <summary>更新角色及权限</summary>
    [HttpPut("{roleNo}")]
    public async Task<ActionResult<ApiResult<string>>> Update(string roleNo, [FromBody] CategoryPermissionCreateRequest request)
    {
        try
        {
            var role = await _context.Roles.FindAsync(roleNo);
            if (role == null)
                return NotFound(new ApiResult<string> { Success = false, Message = "角色不存在" });

            // 更新 ROLE 字段
            role.NAME = request.RoleInfo.Name;
            role.DEP = request.RoleInfo.Dep;
            role.DEPRO_NO = request.RoleInfo.DeproNo;
            role.COMPNO = request.RoleInfo.CompNo;
            role.TYPE_ID = request.RoleInfo.TypeId;
            role.PUBLIC_ID = request.RoleInfo.PublicId;
            role.SUB_ID = request.RoleInfo.SubId;
            role.REM = request.RoleInfo.Rem;

            // 删除旧权限，重新插入
            var oldPermissions = await _context.FxPswds.Where(x => x.ROLENO == roleNo).ToListAsync();
            _context.FxPswds.RemoveRange(oldPermissions);

            if (request.Permissions != null && request.Permissions.Count > 0)
            {
                foreach (var p in request.Permissions)
                {
                    _context.FxPswds.Add(new FxPswd
                    {
                        ROLENO = roleNo,
                        PGM = p.Pgm,
                        TYPE_ID = p.TypeId,
                        COMPNO = p.CompNo,
                        DEPRO_NO = p.DeproNo,
                        QRY = p.Qry,
                        INS = p.Ins,
                        UPD = p.Upd,
                        DEL = p.Del,
                        PRN = p.Prn,
                        QTY = p.Qty,
                        FLD = p.Fld,
                        PROPERTY = p.Property,
                        ALLOW_ID = p.AllowId,
                        EPT = p.Ept
                    });
                }
            }

            await _context.SaveChangesAsync();
            return Ok(new ApiResult<string> { Success = true, Message = "更新成功" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "更新角色时发生错误");
            return StatusCode(500, new ApiResult<string> { Success = false, Message = $"服务器内部错误: {ex.Message}" });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>列表行数据</summary>
public class CategoryPermissionSettingDto
{
    public string RoleNo { get; set; } = "";
    public string Name { get; set; } = "";
    public string Dep { get; set; } = "";
    public string CompNo { get; set; } = "";
    public string TypeId { get; set; } = "";
    public string PublicId { get; set; } = "";
    public string SubId { get; set; } = "";
    public string Rem { get; set; } = "";
    public string DeproNo { get; set; } = "";
}

/// <summary>角色详情（含权限明细）</summary>
public class CategoryPermissionDetailDto
{
    public CategoryPermissionSettingDto RoleInfo { get; set; } = new();
    public List<PermissionRowDto> Permissions { get; set; } = new();
}

/// <summary>权限明细行</summary>
public class PermissionRowDto
{
    public string Pgm { get; set; } = "";
    public string TypeId { get; set; } = "";
    public string CompNo { get; set; } = "";
    public string DeproNo { get; set; } = "";
    public string Qry { get; set; } = "";
    public string Ins { get; set; } = "";
    public string Upd { get; set; } = "";
    public string Del { get; set; } = "";
    public string Prn { get; set; } = "";
    public string Qty { get; set; } = "";
    public string Fld { get; set; } = "";
    public string Property { get; set; } = "";
    public string AllowId { get; set; } = "";
    public string Ept { get; set; } = "";
}

/// <summary>新增/更新请求体</summary>
public class CategoryPermissionCreateRequest
{
    public CategoryPermissionSettingDto RoleInfo { get; set; } = new();
    public List<PermissionRowDto> Permissions { get; set; } = new();
    public string Usr { get; set; } = "ADMIN";
}
