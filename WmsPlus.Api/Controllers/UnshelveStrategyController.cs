using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UnshelveStrategyController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<UnshelveStrategyController> _logger;

    public UnshelveStrategyController(WarehouseDbContext context, ILogger<UnshelveStrategyController> logger)
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
            var query = _context.XjRules.AsQueryable();

            if (!string.IsNullOrWhiteSpace(ruleId))
                query = query.Where(x => x.RULE_ID != null && x.RULE_ID.Contains(ruleId));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            query = query.OrderBy(x => x.RULE_ID);

            var list = await query.Select(x => new StrategyDto
            {
                RuleId = x.RULE_ID ?? "",
                Name = x.NAME ?? "",
                WhType = x.WH_TYPE ?? "",
                StopId = x.STOP_ID ?? "",
                Usr = x.USR ?? ""
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
            _logger.LogError(ex, "查询下架策略时发生错误");
            return StatusCode(500, new ApiResult<List<StrategyDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

public class StrategyDto
{
    public string RuleId { get; set; } = "";
    public string Name { get; set; } = "";
    public string WhType { get; set; } = "";
    public string StopId { get; set; } = "";
    public string Usr { get; set; } = "";
    public DateTime? SysDate { get; set; }
    public string ModifyMan { get; set; } = "";
    public DateTime? ModifyDd { get; set; }
}
