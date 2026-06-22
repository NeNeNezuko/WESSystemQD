using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class MidClassSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<MidClassSettingController> _logger;

    public MidClassSettingController(WarehouseDbContext context, ILogger<MidClassSettingController> logger)
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
    public async Task<ActionResult<ApiResult<List<MidClassSettingDto>>>> Search(
        [FromQuery] string? idxNo,
        [FromQuery] string? name)
    {
        try
        {
            var query = _context.Indxes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(idxNo))
                query = query.Where(x => x.IDX_NO != null && x.IDX_NO.Contains(idxNo));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            query = query.OrderBy(x => x.IDX_NO);

            var list = await query.Select(x => new MidClassSettingDto
            {
                IdxNo = x.IDX_NO ?? "",
                Name = x.NAME ?? "",
                IdxUp = x.IDX_UP ?? "",
                StopDd = x.STOP_DD,
                Rem = x.REM ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<MidClassSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询中类代号设定时发生错误");
            return StatusCode(500, new ApiResult<List<MidClassSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据中类代号获取详情（编辑回填用）
    /// </summary>
    [HttpGet("getByIdxNo")]
    public async Task<ActionResult<ApiResult<MidClassSettingCreateRequest>>> GetByIdxNo([FromQuery] string idxNo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(idxNo))
                return BadRequest(new ApiResult<MidClassSettingCreateRequest> { Success = false, Message = "中类代号不能为空" });

            var entity = await _context.Indxes.FirstOrDefaultAsync(x => x.IDX_NO == idxNo);
            if (entity == null)
            {
                return Ok(new ApiResult<MidClassSettingCreateRequest> { Success = false, Message = $"中类代号 [{idxNo}] 不存在" });
            }

            var result = new MidClassSettingCreateRequest
            {
                IdxNo = entity.IDX_NO ?? "",
                Name = entity.NAME ?? "",
                IdxUp = entity.IDX_UP ?? "",
                StopDd = SafeDateTime(entity.STOP_DD),
                Rem = entity.REM ?? ""
            };

            return Ok(new ApiResult<MidClassSettingCreateRequest>
            {
                Success = true,
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取中类详情时发生错误: {IdxNo}", idxNo);
            return StatusCode(500, new ApiResult<MidClassSettingCreateRequest>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 新增中类代号设定
    /// </summary>
    [HttpPost("create")]
    public async Task<ActionResult<ApiResult<object>>> Create([FromBody] MidClassSettingCreateRequest request)
    {
        try
        {
            // 校验中类代号是否重复
            var existing = await _context.Indxes.FirstOrDefaultAsync(x => x.IDX_NO == request.IdxNo);
            if (existing != null)
            {
                return Ok(new ApiResult<object>
                {
                    Success = false,
                    Message = $"中类代号 [{request.IdxNo}] 已存在，请使用其他代号"
                });
            }

            var entity = new Indx
            {
                IDX_NO = request.IdxNo,
                NAME = request.Name,
                IDX_UP = request.IdxUp,
                STOP_DD = request.StopDd,
                REM = request.Rem,
                USR = "ADMIN"
            };

            _context.Indxes.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(new ApiResult<object>
            {
                Success = true,
                Message = "保存成功",
                Data = new { IdxNo = entity.IDX_NO }
            });
        }
        catch (Exception ex)
        {
            var innerMsg = ex.InnerException?.Message ?? "";
            var deepInner = ex.InnerException?.InnerException?.Message ?? "";
            _logger.LogError(ex, "新增中类代号设定时发生错误: {Msg} | Inner: {Inner} | DeepInner: {DeepInner}",
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
    /// 更新中类代号设定
    /// </summary>
    [HttpPut("update")]
    public async Task<ActionResult<ApiResult<object>>> Update([FromBody] MidClassSettingCreateRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.IdxNo))
                return BadRequest(new ApiResult<object> { Success = false, Message = "中类代号不能为空" });

            var entity = await _context.Indxes.FirstOrDefaultAsync(x => x.IDX_NO == request.IdxNo);
            if (entity == null)
            {
                return Ok(new ApiResult<object> { Success = false, Message = $"中类代号 [{request.IdxNo}] 不存在，无法更新" });
            }

            entity.NAME = request.Name;
            entity.IDX_UP = request.IdxUp;
            entity.STOP_DD = request.StopDd;
            entity.REM = request.Rem;
            entity.UP_DD = DateTime.Now;

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
            _logger.LogError(ex, "更新中类代号设定时发生错误: {IdxNo} | Msg: {Msg} | Inner: {Inner} | DeepInner: {DeepInner}",
                request.IdxNo, ex.Message, innerMsg, deepInner);
            return StatusCode(500, new ApiResult<object>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}" +
                          (!string.IsNullOrEmpty(innerMsg) ? $" | 内部异常: {innerMsg}" : "") +
                          (!string.IsNullOrEmpty(deepInner) ? $" | 深层异常: {deepInner}" : "")
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>中类代号设定列表行数据（对应前端表格）</summary>
public class MidClassSettingDto
{
    public string IdxNo { get; set; } = "";
    public string Name { get; set; } = "";
    public string IdxUp { get; set; } = "";
    public DateTime? StopDd { get; set; }
    public string Rem { get; set; } = "";
}

/// <summary>中类代号设定新增/编辑请求体</summary>
public class MidClassSettingCreateRequest
{
    public string IdxNo { get; set; } = "";
    public string Name { get; set; } = "";
    public string IdxUp { get; set; } = "";
    public DateTime? StopDd { get; set; }
    public string Rem { get; set; } = "";
}
