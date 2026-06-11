using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PickStrategyController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<PickStrategyController> _logger;

    public PickStrategyController(WarehouseDbContext context, ILogger<PickStrategyController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<PickStrategyDto>>>> Search(
        [FromQuery] string? ruleId,
        [FromQuery] string? name)
    {
        try
        {
            var query = _context.PkRules.AsQueryable();

            if (!string.IsNullOrWhiteSpace(ruleId))
                query = query.Where(x => x.RULE_ID != null && x.RULE_ID.Contains(ruleId));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            query = query.OrderBy(x => x.RULE_ID);

            var list = await query.Select(x => new PickStrategyDto
            {
                RuleId = x.RULE_ID ?? "",
                Name = x.NAME ?? "",
                WhType = x.WH_TYPE ?? "",
                StopId = x.STOP_ID ?? "",
                Usr = x.USR ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<PickStrategyDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询拣货策略时发生错误");
            return StatusCode(500, new ApiResult<List<PickStrategyDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>拣货策略列表行数据（对应前端表格）</summary>
public class PickStrategyDto
{
    public string RuleId { get; set; } = "";
    public string Name { get; set; } = "";
    public string WhType { get; set; } = "";
    public string StopId { get; set; } = "";
    public string Usr { get; set; } = "";
}
