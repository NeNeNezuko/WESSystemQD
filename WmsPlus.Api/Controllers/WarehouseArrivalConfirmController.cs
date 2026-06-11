using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WarehouseArrivalConfirmController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<WarehouseArrivalConfirmController> _logger;

    public WarehouseArrivalConfirmController(WarehouseDbContext context, ILogger<WarehouseArrivalConfirmController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<WarehouseArrivalConfirmDto>>>> Search(
        [FromQuery] string? whOut,
        [FromQuery] string? whIn)
    {
        try
        {
            var query = _context.IzwhConfirms.AsQueryable();

            if (!string.IsNullOrWhiteSpace(whOut))
                query = query.Where(x => x.WH_OUT.Contains(whOut));

            if (!string.IsNullOrWhiteSpace(whIn))
                query = query.Where(x => x.WH_IN.Contains(whIn));

            query = query.OrderBy(x => x.WH_OUT).ThenBy(x => x.WH_IN);

            var list = await query.Select(x => new WarehouseArrivalConfirmDto
            {
                WhOut = x.WH_OUT ?? "",
                WhIn = x.WH_IN ?? "",
                Usr = x.USR ?? "",
                ModifyMan = x.MODIFY_MAN ?? "",
                SysDate = x.SYS_DATE,
                ModifyDd = x.MODIFY_DD
            }).ToListAsync();

            return Ok(new ApiResult<List<WarehouseArrivalConfirmDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询依仓库启用到货确认时发生错误");
            return StatusCode(500, new ApiResult<List<WarehouseArrivalConfirmDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>依仓库启用到货确认列表行数据（对应前端表格）</summary>
public class WarehouseArrivalConfirmDto
{
    public string WhOut { get; set; } = "";
    public string WhIn { get; set; } = "";
    public string Usr { get; set; } = "";
    public string ModifyMan { get; set; } = "";
    public DateTime? SysDate { get; set; }
    public DateTime? ModifyDd { get; set; }
}
