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

    /// <summary>
    /// 安全转换日期字段（处理 MySqlConnector.MySqlDateTime 类型）
    /// </summary>
    private static DateTime? SafeDateTime(object? value)
    {
        if (value == null) return null;
        if (value is DateTime dt) return dt;
        try
        {
            var type = value.GetType();
            if (type.FullName == "MySqlConnector.MySqlDateTime" || type.Name == "MySqlDateTime")
            {
                var method = type.GetMethod("GetDateTime");
                if (method != null)
                    return (DateTime)method.Invoke(value, null)!;
                var prop = type.GetProperty("Value");
                if (prop != null)
                    return (DateTime)prop.GetValue(value)!;
            }
            return Convert.ToDateTime(value);
        }
        catch { return null; }
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

            // 查询上层部门名称
            var upCodes = list.Where(x => !string.IsNullOrWhiteSpace(x.Up)).Select(x => x.Up).Distinct().ToList();
            var upDict = new Dictionary<string, string>();
            if (upCodes.Count > 0)
            {
                upDict = await _context.Depts
                    .Where(d => upCodes.Contains(d.DEP))
                    .ToDictionaryAsync(d => d.DEP, d => d.NAME ?? "");
            }

            var result = list.Select(x => new DeptSettingDto
            {
                Dep = x.Dep,
                Name = x.Name,
                Up = x.Up,
                UpName = upDict.GetValueOrDefault(x.Up, ""),
                MakeId = x.MakeId,
                StopDd = x.StopDd
            }).ToList();

            return Ok(new ApiResult<List<DeptSettingDto>>
            {
                Success = true,
                Data = result,
                Total = result.Count
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
    /// <summary>
    /// 根据部门代号获取详情（编辑回填用）
    /// </summary>
    [HttpGet("getByDep")]
    public async Task<ActionResult<ApiResult<DeptSettingAddModel>>> GetByDep([FromQuery] string dep)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dep))
                return BadRequest(new ApiResult<DeptSettingAddModel> { Success = false, Message = "部门代号不能为空" });

            var entity = await _context.Depts.FirstOrDefaultAsync(x => x.DEP == dep);
            if (entity == null)
            {
                return Ok(new ApiResult<DeptSettingAddModel> { Success = false, Message = $"部门 [{dep}] 不存在" });
            }

            var result = new DeptSettingAddModel
            {
                Dep = entity.DEP ?? "",
                Name = entity.NAME ?? "",
                Up = entity.UP ?? "",
                EngName = entity.ENG_NAME ?? "",
                StopDd = SafeDateTime(entity.STOP_DD),
                MakeId = entity.MAKE_ID ?? "",
                GroupId = entity.GROUP_ID ?? "",
                NamePy = entity.NAME_PY ?? "",
                TpId = entity.TP_ID ?? ""
            };

            return Ok(new ApiResult<DeptSettingAddModel>
            {
                Success = true,
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取部门详情时发生错误: {Dep}", dep);
            return StatusCode(500, new ApiResult<DeptSettingAddModel>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 新增部门设定
    /// </summary>
    [HttpPost("create")]
    public async Task<ActionResult<ApiResult<object>>> Create([FromBody] DeptSettingAddModel request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Dep))
                return BadRequest(new ApiResult<object> { Success = false, Message = "部门代号不能为空" });

            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new ApiResult<object> { Success = false, Message = "部门名称不能为空" });

            // 校验是否重复
            var existing = await _context.Depts.FirstOrDefaultAsync(x => x.DEP == request.Dep);
            if (existing != null)
            {
                return Ok(new ApiResult<object>
                {
                    Success = false,
                    Message = $"部门代号 [{request.Dep}] 已存在，请使用其他代号"
                });
            }

            var entity = new Dept
            {
                DEP = request.Dep,
                NAME = request.Name,
                UP = request.Up,
                ENG_NAME = request.EngName,
                STOP_DD = request.StopDd,
                MAKE_ID = request.MakeId,
                GROUP_ID = request.GroupId,
                NAME_PY = request.NamePy,
                TP_ID = request.TpId,
                USR = "ADMIN",
                SYS_DATE = DateTime.Now
            };

            _context.Depts.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(new ApiResult<object>
            {
                Success = true,
                Message = "保存成功",
                Data = new { Dep = entity.DEP }
            });
        }
        catch (Exception ex)
        {
            var innerMsg = ex.InnerException?.Message ?? "";
            var deepInner = ex.InnerException?.InnerException?.Message ?? "";
            _logger.LogError(ex, "新增部门设定时发生错误: {Msg} | Inner: {Inner} | DeepInner: {DeepInner}",
                ex.Message, innerMsg, deepInner);
            return StatusCode(500, new ApiResult<object>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}" +
                          (!string.IsNullOrEmpty(innerMsg) ? $" | 内部异常: {innerMsg}" : "") +
                          (!string.IsNullOrEmpty(deepInner) ? $" | 深层异常: {deepInner}" : "")
            });
        }
    }

    /// <summary>
    /// 更新部门设定
    /// </summary>
    [HttpPut("update")]
    public async Task<ActionResult<ApiResult<object>>> Update([FromBody] DeptSettingAddModel request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Dep))
                return BadRequest(new ApiResult<object> { Success = false, Message = "部门代号不能为空" });

            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new ApiResult<object> { Success = false, Message = "部门名称不能为空" });

            var entity = await _context.Depts.FirstOrDefaultAsync(x => x.DEP == request.Dep);
            if (entity == null)
            {
                return Ok(new ApiResult<object> { Success = false, Message = $"部门 [{request.Dep}] 不存在，无法更新" });
            }

            entity.NAME = request.Name;
            entity.UP = request.Up;
            entity.ENG_NAME = request.EngName;
            entity.STOP_DD = request.StopDd;
            entity.MAKE_ID = request.MakeId;
            entity.GROUP_ID = request.GroupId;
            entity.NAME_PY = request.NamePy;
            entity.TP_ID = request.TpId;

            await _context.SaveChangesAsync();

            return Ok(new ApiResult<object>
            {
                Success = true,
                Message = "修改成功"
            });
        }
        catch (Exception ex)
        {
            var innerMsg = ex.InnerException?.Message ?? "";
            var deepInner = ex.InnerException?.InnerException?.Message ?? "";
            _logger.LogError(ex, "更新部门设定时发生错误: {Dep} | Msg: {Msg} | Inner: {Inner} | DeepInner: {DeepInner}",
                request.Dep, ex.Message, innerMsg, deepInner);
            return StatusCode(500, new ApiResult<object>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}" +
                          (!string.IsNullOrEmpty(innerMsg) ? $" | 内部异常: {innerMsg}" : "") +
                          (!string.IsNullOrEmpty(deepInner) ? $" | 深层异常: {deepInner}" : "")
            });
        }
    }

    /// <summary>
    /// 删除部门设定
    /// </summary>
    [HttpDelete("{dep}")]
    public async Task<ActionResult<ApiResult<object>>> Delete(string dep)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(dep))
                return BadRequest(new ApiResult<object> { Success = false, Message = "部门代号不能为空" });

            var entity = await _context.Depts.FirstOrDefaultAsync(x => x.DEP == dep);
            if (entity == null)
            {
                return Ok(new ApiResult<object> { Success = false, Message = $"部门 [{dep}] 不存在" });
            }

            _context.Depts.Remove(entity);
            await _context.SaveChangesAsync();

            return Ok(new ApiResult<object> { Success = true, Message = "删除成功" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除部门设定时发生错误: {Dep}", dep);
            return StatusCode(500, new ApiResult<object>
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
    public string UpName { get; set; } = "";
    public string MakeId { get; set; } = "";
    public DateTime? StopDd { get; set; }
}

/// <summary>部门设定新增/编辑请求体</summary>
public class DeptSettingAddModel
{
    /// <summary>部门代号(主键)</summary>
    public string Dep { get; set; } = "";

    /// <summary>部门名称</summary>
    public string Name { get; set; } = "";

    /// <summary>上层部门</summary>
    public string Up { get; set; } = "";

    /// <summary>英文名称</summary>
    public string EngName { get; set; } = "";

    /// <summary>停用日期</summary>
    public DateTime? StopDd { get; set; }

    /// <summary>部门性质</summary>
    public string MakeId { get; set; } = "";

    /// <summary>群组代号</summary>
    public string GroupId { get; set; } = "";

    /// <summary>助记码</summary>
    public string NamePy { get; set; } = "";

    /// <summary>类型代号</summary>
    public string TpId { get; set; } = "";
}
