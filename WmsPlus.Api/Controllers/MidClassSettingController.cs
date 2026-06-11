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
