using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BarcodeSplitSupplierController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<BarcodeSplitSupplierController> _logger;

    public BarcodeSplitSupplierController(WarehouseDbContext context, ILogger<BarcodeSplitSupplierController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询依供应商拆码规则列表（CUS_REMVOE_RULE表）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<BarcodeSplitSupplierDto>>>> Search(
        [FromQuery] string? cusNo)
    {
        try
        {
            var query = _context.CusRemoveRules.AsQueryable();

            // 供应商代号模糊匹配
            if (!string.IsNullOrWhiteSpace(cusNo))
                query = query.Where(x => x.CUS_NO != null && x.CUS_NO.Contains(cusNo));

            // 按序号排序
            query = query.OrderBy(x => x.SEQ_NO);

            var rawList = await query.ToListAsync();

            var list = rawList.Select(x => new BarcodeSplitSupplierDto
            {
                SeqNo = x.SEQ_NO ?? 0,
                CusNo = x.CUS_NO ?? "",
                CusName = x.CUS_NAME ?? "",
                RuleCode = x.RULE_CODE ?? "",
                RuleName = x.RULE_NAME ?? ""
            }).ToList();

            return Ok(new ApiResult<List<BarcodeSplitSupplierDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询依供应商拆码规则时发生错误");
            return StatusCode(500, new ApiResult<List<BarcodeSplitSupplierDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>依供应商拆码规则列表行数据</summary>
public class BarcodeSplitSupplierDto
{
    public int SeqNo { get; set; }
    public string CusNo { get; set; } = "";
    public string CusName { get; set; } = "";
    public string RuleCode { get; set; } = "";
    public string RuleName { get; set; } = "";
}
