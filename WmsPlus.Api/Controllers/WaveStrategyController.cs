using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WaveStrategyController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<WaveStrategyController> _logger;

    public WaveStrategyController(WarehouseDbContext context, ILogger<WaveStrategyController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<StrategyDto>>>> Search(
        [FromQuery] string? ruleId,
        [FromQuery] string? name)
    {
        try
        {
            var query = _context.BcRules.AsQueryable();

            if (!string.IsNullOrWhiteSpace(ruleId))
                query = query.Where(x => x.RULE_ID != null && x.RULE_ID.Contains(ruleId));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            query = query.OrderBy(x => x.RULE_ID);

            var list = await query.Select(x => new StrategyDto
            {
                RuleId = x.RULE_ID ?? "",
                Name = x.NAME ?? "",
                StopId = x.STOP_ID ?? "",
                WhType = x.WH_TYPE ?? "",
                Usr = x.USR ?? "",
                SysDate = x.SYS_DATE,
                ModifyMan = x.MODIFY_MAN ?? "",
                ModifyDd = x.MODIFY_DD
            }).ToListAsync();

            return Ok(new ApiResult<List<StrategyDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询波次策略时发生错误");
            return StatusCode(500, new ApiResult<List<StrategyDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}
