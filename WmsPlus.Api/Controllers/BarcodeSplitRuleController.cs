using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BarcodeSplitRuleController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<BarcodeSplitRuleController> _logger;

    public BarcodeSplitRuleController(WarehouseDbContext context, ILogger<BarcodeSplitRuleController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询入库条码拆码规则列表（MF_REMVOE_RULE表）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<BarcodeSplitRuleDto>>>> Search(
        [FromQuery] string? ruleCode,
        [FromQuery] string? ruleName)
    {
        try
        {
            var query = _context.MfRemoveRules.AsQueryable();

            // 规则代号模糊匹配
            if (!string.IsNullOrWhiteSpace(ruleCode))
                query = query.Where(x => x.RULE_CODE != null && x.RULE_CODE.Contains(ruleCode));

            // 规则名称模糊匹配
            if (!string.IsNullOrWhiteSpace(ruleName))
                query = query.Where(x => x.RULE_NAME != null && x.RULE_NAME.Contains(ruleName));

            // 按规则代号排序
            query = query.OrderBy(x => x.RULE_CODE);

            var rawList = await query.ToListAsync();

            var list = rawList.Select(x => new BarcodeSplitRuleDto
            {
                RuleCode = x.RULE_CODE ?? "",
                RuleName = x.RULE_NAME ?? "",
                BaseBarcode = x.BASE_BARCODE ?? "",
                EncodingMethod = x.ENCODING_METHOD ?? "",
                Separator = x.SEPARATOR ?? "",
                TotalLength = x.TOTAL_LENGTH,
                DefaultFlag = x.DEFAULT_FLAG ?? ""
            }).ToList();

            return Ok(new ApiResult<List<BarcodeSplitRuleDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询入库条码拆码规则时发生错误");
            return StatusCode(500, new ApiResult<List<BarcodeSplitRuleDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>入库条码拆码规则列表行数据</summary>
public class BarcodeSplitRuleDto
{
    public string RuleCode { get; set; } = "";
    public string RuleName { get; set; } = "";
    public string BaseBarcode { get; set; } = "";
    public string EncodingMethod { get; set; } = "";
    public string Separator { get; set; } = "";
    public int? TotalLength { get; set; }
    public string DefaultFlag { get; set; } = "";
}
