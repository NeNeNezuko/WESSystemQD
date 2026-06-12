using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ThirdPartyCallDetailController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ThirdPartyCallDetailController> _logger;

    public ThirdPartyCallDetailController(WarehouseDbContext context, ILogger<ThirdPartyCallDetailController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ThirdPartyCallDetailDto>>>> Search(
        [FromQuery] string? actNo,
        [FromQuery] string? bilNo,
        [FromQuery] string? supNo,
        [FromQuery] string? status,
        [FromQuery] string? startDate,
        [FromQuery] string? endDate)
    {
        try
        {
            var query = _context.ApiActionIs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(actNo))
                query = query.Where(x => x.ACT_NO != null && x.ACT_NO.Contains(actNo));

            if (!string.IsNullOrWhiteSpace(bilNo))
                query = query.Where(x => x.BIL_NO != null && x.BIL_NO.Contains(bilNo));

            if (!string.IsNullOrWhiteSpace(supNo))
                query = query.Where(x => x.SUP_NO != null && x.SUP_NO.Contains(supNo));

            if (!string.IsNullOrWhiteSpace(status) && status != "全部")
                query = query.Where(x => x.STATUS_ID == status);

            if (!string.IsNullOrWhiteSpace(startDate) && DateTime.TryParse(startDate, out var sd))
                query = query.Where(x => x.SYS_DATE >= sd);

            if (!string.IsNullOrWhiteSpace(endDate) && DateTime.TryParse(endDate, out var ed))
                query = query.Where(x => x.SYS_DATE <= ed);

            query = query.OrderByDescending(x => x.SYS_DATE).Take(500);

            var list = await query.Select(x => new ThirdPartyCallDetailDto
            {
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
                RequestSize = x.REQUEST_SIZE ?? 0,
                SysDate = x.SYS_DATE,
                StartDate = x.START_DATE,
                EndDate = x.END_DATE
            }).ToListAsync();

            return Ok(new ApiResult<List<ThirdPartyCallDetailDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询第三方调用明细时发生错误");
            return StatusCode(500, new ApiResult<List<ThirdPartyCallDetailDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>第三方调用明细列表行数据</summary>
public class ThirdPartyCallDetailDto
{
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
    public int RequestSize { get; set; }
    public DateTime? SysDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
