using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ContainTypeSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ContainTypeSettingController> _logger;

    public ContainTypeSettingController(WarehouseDbContext context, ILogger<ContainTypeSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询物流容器类型设定列表（查询CONTAIN_SET表）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ContainTypeSettingDto>>>> Search(
        [FromQuery] string? typeCode,
        [FromQuery] string? typeName)
    {
        try
        {
            var query = from c in _context.ContainSets
                        select c;

            // 类型代号模糊匹配
            if (!string.IsNullOrWhiteSpace(typeCode))
                query = query.Where(x => x.TYPE_CODE != null && x.TYPE_CODE.Contains(typeCode));

            // 类型名称模糊匹配
            if (!string.IsNullOrWhiteSpace(typeName))
                query = query.Where(x => x.TYPE_NAME != null && x.TYPE_NAME.Contains(typeName));

            // 按类型代号排序
            query = query.OrderBy(x => x.TYPE_CODE);

            var rawList = await query.ToListAsync();

            var list = rawList.Select((x, idx) => new ContainTypeSettingDto
            {
                RowNo = idx + 1,
                TypeCode = x.TYPE_CODE ?? "",
                TypeName = x.TYPE_NAME ?? "",
                CodePrefix = x.CODE_PREFIX ?? "",
                StopFlag = x.STOP_FLAG ?? "",
                IsSystem = x.IS_SYSTEM ?? "",
                RcsType = x.RCS_TYPE ?? ""
            }).ToList();

            return Ok(new ApiResult<List<ContainTypeSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询物流容器类型设定时发生错误");
            return StatusCode(500, new ApiResult<List<ContainTypeSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>物流容器类型设定列表行数据</summary>
public class ContainTypeSettingDto
{
    public int RowNo { get; set; }
    public string TypeCode { get; set; } = "";
    public string TypeName { get; set; } = "";
    public string CodePrefix { get; set; } = "";
    public string StopFlag { get; set; } = "";
    public string IsSystem { get; set; } = "";
    public string RcsType { get; set; } = "";
}
