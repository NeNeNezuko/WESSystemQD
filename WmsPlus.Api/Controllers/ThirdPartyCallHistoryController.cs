using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ThirdPartyCallHistoryController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ThirdPartyCallHistoryController> _logger;

    public ThirdPartyCallHistoryController(WarehouseDbContext context, ILogger<ThirdPartyCallHistoryController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ThirdPartyCallHistoryDto>>>> Search(
        [FromQuery] string? actNo,
        [FromQuery] string? actId,
        [FromQuery] string? startDate,
        [FromQuery] string? endDate)
    {
        try
        {
            var query = _context.ApiActionHisIs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(actNo))
                query = query.Where(x => x.ACT_NO != null && x.ACT_NO.Contains(actNo));

            if (!string.IsNullOrWhiteSpace(actId) && actId != "全部")
                query = query.Where(x => x.ACT_ID == actId);

            if (!string.IsNullOrWhiteSpace(startDate) && DateTime.TryParse(startDate, out var sd))
                query = query.Where(x => x.START_DATE >= sd);

            if (!string.IsNullOrWhiteSpace(endDate) && DateTime.TryParse(endDate, out var ed))
                query = query.Where(x => x.END_DATE <= ed);

            query = query.OrderByDescending(x => x.START_DATE).Take(500);

            var list = await query.Select(x => new ThirdPartyCallHistoryDto
            {
                HisNo = x.HIS_NO,
                ActId = x.ACT_ID ?? "",
                ActNo = x.ACT_NO ?? "",
                ActNoMain = x.ACT_NO_MAIN ?? "",
                MethodNo = x.METHOD_NO ?? "",
                HttpMethod = x.HTTP_METHOD ?? "",
                BilId = x.BIL_ID ?? "",
                BilNo = x.BIL_NO ?? "",
                OthBilId = x.OTH_BIL_ID ?? "",
                Wh = x.WH ?? "",
                StatusId = x.STATUS_ID ?? "",
                ErrCode = x.ERR_CODE ?? "",
                StartDate = x.START_DATE,
                EndDate = x.END_DATE
            }).ToListAsync();

            return Ok(new ApiResult<List<ThirdPartyCallHistoryDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询第三方调用历史时发生错误");
            return StatusCode(500, new ApiResult<List<ThirdPartyCallHistoryDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>第三方调用历史列表行数据</summary>
public class ThirdPartyCallHistoryDto
{
    public long? HisNo { get; set; }
    public string ActId { get; set; } = "";
    public string ActNo { get; set; } = "";
    public string ActNoMain { get; set; } = "";
    public string MethodNo { get; set; } = "";
    public string HttpMethod { get; set; } = "";
    public string BilId { get; set; } = "";
    public string BilNo { get; set; } = "";
    public string OthBilId { get; set; } = "";
    public string Wh { get; set; } = "";
    public string StatusId { get; set; } = "";
    public string ErrCode { get; set; } = "";
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
