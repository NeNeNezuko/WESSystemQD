using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ThirdPartyPushDetailController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ThirdPartyPushDetailController> _logger;

    public ThirdPartyPushDetailController(WarehouseDbContext context, ILogger<ThirdPartyPushDetailController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ThirdPartyPushDetailDto>>>> Search(
        [FromQuery] string? actNo,
        [FromQuery] string? bilNo,
        [FromQuery] string? supNo,
        [FromQuery] string? status,
        [FromQuery] string? startDate,
        [FromQuery] string? endDate)
    {
        try
        {
            var query = _context.ApiActionOs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(actNo))
                query = query.Where(x => x.ACT_NO != null && x.ACT_NO.Contains(actNo));

            if (!string.IsNullOrWhiteSpace(bilNo))
                query = query.Where(x => x.BIL_NO != null && x.BIL_NO.Contains(bilNo));

            if (!string.IsNullOrWhiteSpace(supNo))
                query = query.Where(x => x.SUP_NO != null && x.SUP_NO.Contains(supNo));

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(x => x.STATUS_ID == status);

            if (!string.IsNullOrWhiteSpace(startDate) && DateTime.TryParse(startDate, out var sd))
                query = query.Where(x => x.SYS_DATE >= sd);

            if (!string.IsNullOrWhiteSpace(endDate) && DateTime.TryParse(endDate, out var ed))
                query = query.Where(x => x.SYS_DATE <= ed.AddDays(1));

            query = query.OrderByDescending(x => x.SYS_DATE);

            var list = await query.Select(x => new ThirdPartyPushDetailDto
            {
                ActNo = x.ACT_NO ?? "",
                MethodNo = x.METHOD_NO ?? "",
                HttpMethod = x.HTTP_METHOD ?? "",
                Path = x.PATH ?? "",
                BilId = x.BIL_ID ?? "",
                BilNo = x.BIL_NO ?? "",
                OthBilId = x.OTH_BIL_ID ?? "",
                OthBilNo = x.OTH_BIL_NO ?? "",
                RefId = x.REF_ID ?? "",
                StatusId = x.STATUS_ID ?? "",
                ErrMsg = x.ERR_MSG ?? "",
                HttpCode = x.HTTP_CODE ?? "",
                RunCount = x.RUN_COUNT,
                StartDate = x.START_DATE,
                EndDate = x.END_DATE,
                SysDate = x.SYS_DATE
            }).ToListAsync();

            return Ok(new ApiResult<List<ThirdPartyPushDetailDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询推送第三方明细表时发生错误");
            return StatusCode(500, new ApiResult<List<ThirdPartyPushDetailDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>推送第三方明细表列表行数据（对应前端表格）</summary>
public class ThirdPartyPushDetailDto
{
    public string ActNo { get; set; } = "";
    public string MethodNo { get; set; } = "";
    public string HttpMethod { get; set; } = "";
    public string Path { get; set; } = "";
    public string BilId { get; set; } = "";
    public string BilNo { get; set; } = "";
    public string OthBilId { get; set; } = "";
    public string OthBilNo { get; set; } = "";
    public string RefId { get; set; } = "";
    public string StatusId { get; set; } = "";
    public string ErrMsg { get; set; } = "";
    public string HttpCode { get; set; } = "";
    public int? RunCount { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime? SysDate { get; set; }
}
