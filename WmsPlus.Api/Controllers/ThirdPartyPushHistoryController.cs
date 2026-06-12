using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ThirdPartyPushHistoryController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ThirdPartyPushHistoryController> _logger;

    public ThirdPartyPushHistoryController(WarehouseDbContext context, ILogger<ThirdPartyPushHistoryController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ThirdPartyPushHistoryDto>>>> Search(
        [FromQuery] string? actNo,
        [FromQuery] string? actId,
        [FromQuery] string? startDate,
        [FromQuery] string? endDate)
    {
        try
        {
            var query = _context.ApiActionHisOs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(actNo))
                query = query.Where(x => x.ACT_NO != null && x.ACT_NO.Contains(actNo));

            if (!string.IsNullOrWhiteSpace(actId))
                query = query.Where(x => x.ACT_ID != null && x.ACT_ID == actId);

            if (!string.IsNullOrWhiteSpace(startDate) && DateTime.TryParse(startDate, out var sd))
                query = query.Where(x => x.START_DATE >= sd);

            if (!string.IsNullOrWhiteSpace(endDate) && DateTime.TryParse(endDate, out var ed))
                query = query.Where(x => x.START_DATE <= ed.AddDays(1));

            query = query.OrderByDescending(x => x.HIS_NO);

            var list = await query.Select(x => new ThirdPartyPushHistoryDto
            {
                HisNo = x.HIS_NO,
                ActId = x.ACT_ID ?? "",
                ActNo = x.ACT_NO ?? "",
                MethodNo = x.METHOD_NO ?? "",
                HttpMethod = x.HTTP_METHOD ?? "",
                BilId = x.BIL_ID ?? "",
                BilNo = x.BIL_NO ?? "",
                OthBilId = x.OTH_BIL_ID ?? "",
                StatusId = x.STATUS_ID ?? "",
                ErrMsg = x.ERR_MSG ?? "",
                HttpCode = x.HTTP_CODE ?? "",
                PushSize = x.PUSH_SIZE,
                StartDate = x.START_DATE,
                EndDate = x.END_DATE
            }).ToListAsync();

            return Ok(new ApiResult<List<ThirdPartyPushHistoryDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询推送第三方历史表时发生错误");
            return StatusCode(500, new ApiResult<List<ThirdPartyPushHistoryDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>推送第三方历史表列表行数据（对应前端表格）</summary>
public class ThirdPartyPushHistoryDto
{
    public long? HisNo { get; set; }
    public string ActId { get; set; } = "";
    public string ActNo { get; set; } = "";
    public string MethodNo { get; set; } = "";
    public string HttpMethod { get; set; } = "";
    public string BilId { get; set; } = "";
    public string BilNo { get; set; } = "";
    public string OthBilId { get; set; } = "";
    public string StatusId { get; set; } = "";
    public string ErrMsg { get; set; } = "";
    public string HttpCode { get; set; } = "";
    public int? PushSize { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
