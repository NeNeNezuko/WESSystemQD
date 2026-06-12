using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CustomReportController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<CustomReportController> _logger;

    public CustomReportController(WarehouseDbContext context, ILogger<CustomReportController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<CustomReportDto>>>> Search(
        [FromQuery] string? name,
        [FromQuery] string? usr)
    {
        try
        {
            var query = _context.MfRpts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME_GB != null && x.NAME_GB.Contains(name));

            if (!string.IsNullOrWhiteSpace(usr))
                query = query.Where(x => x.USR != null && x.USR.Contains(usr));

            query = query.OrderBy(x => x.PGM).ThenBy(x => x.ITM);

            var list = await query.Select(x => new CustomReportDto
            {
                Pgm = x.PGM ?? "",
                NameGb = x.NAME_GB ?? "",
                NameBig5 = x.NAME_BIG5 ?? "",
                NameEng = x.NAME_ENG ?? "",
                Itm = x.ITM.GetValueOrDefault(),
                ChkMenu = x.CHK_MENU ?? "",
                ChkUsrs = x.CHK_USRS ?? "",
                Usr = x.USR ?? "",
                SysDate = x.SYS_DATE
            }).ToListAsync();

            return Ok(new ApiResult<List<CustomReportDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询自定义报表时发生错误");
            return StatusCode(500, new ApiResult<List<CustomReportDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>自定义报表列表行数据（对应前端表格）</summary>
public class CustomReportDto
{
    public string Pgm { get; set; } = "";
    public string NameGb { get; set; } = "";
    public string NameBig5 { get; set; } = "";
    public string NameEng { get; set; } = "";
    public int Itm { get; set; }
    public string ChkMenu { get; set; } = "";
    public string ChkUsrs { get; set; } = "";
    public string Usr { get; set; } = "";
    public DateTime? SysDate { get; set; }
}
