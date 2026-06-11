using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BarcodeRuleController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<BarcodeRuleController> _logger;

    public BarcodeRuleController(WarehouseDbContext context, ILogger<BarcodeRuleController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<BarcodeRuleDto>>>> Search(
        [FromQuery] string? ruleCode,
        [FromQuery] string? ruleName,
        [FromQuery] string? ruleType)
    {
        try
        {
            var query = _context.BarcodeRules.AsQueryable();

            if (!string.IsNullOrWhiteSpace(ruleType))
                query = query.Where(x => x.RuleType == ruleType);

            if (!string.IsNullOrWhiteSpace(ruleCode))
                query = query.Where(x => x.RuleCode != null && x.RuleCode.Contains(ruleCode));

            if (!string.IsNullOrWhiteSpace(ruleName))
                query = query.Where(x => x.RuleName != null && x.RuleName.Contains(ruleName));

            query = query.OrderBy(x => x.RuleCode);

            var list = await query.Select(x => new BarcodeRuleDto
            {
                RuleCode = x.RuleCode ?? "",
                RuleName = x.RuleName ?? "",
                FlowLength = x.FlowLength,
                Separator = x.Separator ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<BarcodeRuleDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询条码编码规则时发生错误");
            return StatusCode(500, new ApiResult<List<BarcodeRuleDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>条码编码规则列表行数据（对应前端表格）</summary>
public class BarcodeRuleDto
{
    public string RuleCode { get; set; } = "";
    public string RuleName { get; set; } = "";
    public int? FlowLength { get; set; }
    public string Separator { get; set; } = "";
}
