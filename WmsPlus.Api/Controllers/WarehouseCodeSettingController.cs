using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WarehouseCodeSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<WarehouseCodeSettingController> _logger;

    public WarehouseCodeSettingController(WarehouseDbContext context, ILogger<WarehouseCodeSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<WarehouseCodeSettingDto>>>> Search(
        [FromQuery] string? wh,
        [FromQuery] string? name)
    {
        try
        {
            var query = _context.MyWhs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(wh))
                query = query.Where(x => x.WH.Contains(wh));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            query = query.OrderBy(x => x.WH);

            var list = await query.Select(x => new WarehouseCodeSettingDto
            {
                Wh = x.WH,
                Name = x.NAME ?? "",
                Attrib = x.ATTRIB ?? "",
                Dep = x.DEP ?? "",
                CwFlag = x.CW_FLAG ?? "",
                WhType = x.WH_TYPE ?? "",
                StopDd = x.STOP_DD
            }).ToListAsync();

            return Ok(new ApiResult<List<WarehouseCodeSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询仓库代号设定时发生错误");
            return StatusCode(500, new ApiResult<List<WarehouseCodeSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>仓库代号设定列表行数据（对应前端表格）</summary>
public class WarehouseCodeSettingDto
{
    public string Wh { get; set; } = "";
    public string Name { get; set; } = "";
    public string Attrib { get; set; } = "";
    public string Dep { get; set; } = "";
    public string CwFlag { get; set; } = "";
    public string WhType { get; set; } = "";
    public DateTime? StopDd { get; set; }
}
