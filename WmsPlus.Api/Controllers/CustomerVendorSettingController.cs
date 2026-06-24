using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomerVendorSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<CustomerVendorSettingController> _logger;

    public CustomerVendorSettingController(WarehouseDbContext context, ILogger<CustomerVendorSettingController> logger)
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
    public async Task<ActionResult<ApiResult<List<CustomerVendorSettingDto>>>> Search(
        [FromQuery] string? cusNo,
        [FromQuery] string? name)
    {
        try
        {
            // 从 MF_CKTB 表中按 CUS_NO 去重查询客户/厂商资料
            var baseQuery = _context.MfCktbs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(cusNo))
                baseQuery = baseQuery.Where(x => x.CUS_NO != null && x.CUS_NO.Contains(cusNo));

            if (!string.IsNullOrWhiteSpace(name))
                baseQuery = baseQuery.Where(x => x.CUS_NAME != null && x.CUS_NAME.Contains(name));

            // 按 CUS_NO 去重后返回
            var list = await baseQuery
                .Where(x => x.CUS_NO != null && x.CUS_NO != "")
                .Select(x => new { x.CUS_NO, x.CUS_NAME })
                .Distinct()
                .OrderBy(x => x.CUS_NO)
                .Select(x => new CustomerVendorSettingDto
                {
                    CusNo = x.CUS_NO ?? "",
                    CusName = x.CUS_NAME ?? "",
                    Rem = ""
                }).ToListAsync();

            return Ok(new ApiResult<List<CustomerVendorSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询客户/厂商资料设定时发生错误");
            return StatusCode(500, new ApiResult<List<CustomerVendorSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据厂商代号获取详情（编辑回填用）
    /// </summary>
    [HttpGet("getByCusNo")]
    public async Task<ActionResult<ApiResult<CustomerVendorSettingCreateRequest>>> GetByCusNo([FromQuery] string cusNo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(cusNo))
                return BadRequest(new ApiResult<CustomerVendorSettingCreateRequest> { Success = false, Message = "厂商代号不能为空" });

            // 从 MF_CKTB 中查找第一条匹配的记录获取名称
            var entity = await _context.MfCktbs.FirstOrDefaultAsync(x => x.CUS_NO == cusNo);
            if (entity == null || string.IsNullOrWhiteSpace(entity.CUS_NO))
            {
                return Ok(new ApiResult<CustomerVendorSettingCreateRequest> { Success = false, Message = $"厂商代号 [{cusNo}] 不存在" });
            }

            var result = new CustomerVendorSettingCreateRequest
            {
                CusNo = entity.CUS_NO ?? "",
                CusName = entity.CUS_NAME ?? "",
                Rem = entity.REM ?? ""
            };

            return Ok(new ApiResult<CustomerVendorSettingCreateRequest>
            {
                Success = true,
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取客户/厂商详情时发生错误: {CusNo}", cusNo);
            return StatusCode(500, new ApiResult<CustomerVendorSettingCreateRequest>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 新增客户/厂商资料设定
    /// </summary>
    [HttpPost("create")]
    public async Task<ActionResult<ApiResult<object>>> Create([FromBody] CustomerVendorSettingCreateRequest request)
    {
        try
        {
            // 校验厂商代号是否重复
            var existing = await _context.MfCktbs.AnyAsync(x => x.CUS_NO == request.CusNo);
            if (existing)
            {
                return Ok(new ApiResult<object>
                {
                    Success = false,
                    Message = $"厂商代号 [{request.CusNo}] 已存在，请使用其他代号"
                });
            }

            // MF_CKTB 是单据表头，此处仅做占位记录（实际客户/厂商信息通常来自主数据表）
            var entity = new MfCktb
            {
                TB_NO = $"CUS_{request.CusNo}_{DateTime.Now:yyyyMMddHHmmss}",
                TB_DD = DateTime.Now,
                CUS_NO = request.CusNo,
                CUS_NAME = request.CusName,
                REM = request.Rem,
                USR = "ADMIN",
                SYS_DATE = DateTime.Now
            };

            _context.MfCktbs.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(new ApiResult<object>
            {
                Success = true,
                Message = "保存成功",
                Data = new { CusNo = request.CusNo }
            });
        }
        catch (Exception ex)
        {
            var innerMsg = ex.InnerException?.Message ?? "";
            var deepInner = ex.InnerException?.InnerException?.Message ?? "";
            _logger.LogError(ex, "新增客户/厂商资料设定时发生错误: {Msg} | Inner: {Inner} | DeepInner: {DeepInner}",
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
    /// 更新客户/厂商资料设定
    /// </summary>
    [HttpPut("update")]
    public async Task<ActionResult<ApiResult<object>>> Update([FromBody] CustomerVendorSettingCreateRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.CusNo))
                return BadRequest(new ApiResult<object> { Success = false, Message = "厂商代号不能为空" });

            // 查找所有包含该客户代号的记录并更新名称
            var entities = await _context.MfCktbs.Where(x => x.CUS_NO == request.CusNo).ToListAsync();
            if (entities.Count == 0)
            {
                return Ok(new ApiResult<object> { Success = false, Message = $"厂商代号 [{request.CusNo}] 不存在，无法更新" });
            }

            foreach (var entity in entities)
            {
                entity.CUS_NAME = request.CusName;
                entity.REM = request.Rem;
            }

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
            _logger.LogError(ex, "更新客户/厂商资料设定时发生错误: {CusNo} | Msg: {Msg} | Inner: {Inner} | DeepInner: {DeepInner}",
                request.CusNo, ex.Message, innerMsg, deepInner);
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

/// <summary>客户/厂商资料设定列表行数据（对应前端表格）</summary>
public class CustomerVendorSettingDto
{
    public string CusNo { get; set; } = "";
    public string CusName { get; set; } = "";
    public string StopDd { get; set; } = "";
    public string Rem { get; set; } = "";
}

/// <summary>客户/厂商资料设定新增/编辑请求体</summary>
public class CustomerVendorSettingCreateRequest
{
    public string CusNo { get; set; } = "";
    public string CusName { get; set; } = "";
    public DateTime? StopDd { get; set; }
    public string Rem { get; set; } = "";
}
