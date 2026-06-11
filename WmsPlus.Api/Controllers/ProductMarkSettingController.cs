using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductMarkSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ProductMarkSettingController> _logger;

    public ProductMarkSettingController(WarehouseDbContext context, ILogger<ProductMarkSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ProductMarkSettingDto>>>> Search(
        [FromQuery] string? mobId,
        [FromQuery] string? mobName)
    {
        try
        {
            var query = _context.PrdMarks.AsQueryable();

            if (!string.IsNullOrWhiteSpace(mobId))
                query = query.Where(x => x.MOB_ID != null && x.MOB_ID.Contains(mobId));

            if (!string.IsNullOrWhiteSpace(mobName))
                query = query.Where(x => x.MOB_NAME != null && x.MOB_NAME.Contains(mobName));

            query = query.OrderBy(x => x.MOB_ID);

            var list = await query.Select(x => new ProductMarkSettingDto
            {
                MobId = x.MOB_ID ?? "",
                MobName = x.MOB_NAME ?? "",
                PrdMark = x.PRD_MARK ?? "",
                Rem = x.REM ?? "",
                EndDd = x.END_DD
            }).ToListAsync();

            return Ok(new ApiResult<List<ProductMarkSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询货品特征码段设定时发生错误");
            return StatusCode(500, new ApiResult<List<ProductMarkSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>货品特征码段设定列表行数据（对应前端表格）</summary>
public class ProductMarkSettingDto
{
    public string MobId { get; set; } = "";
    public string MobName { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public string Rem { get; set; } = "";
    public DateTime? EndDd { get; set; }
}
