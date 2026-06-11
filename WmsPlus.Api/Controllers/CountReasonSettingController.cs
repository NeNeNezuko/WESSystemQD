using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CountReasonSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<CountReasonSettingController> _logger;

    public CountReasonSettingController(WarehouseDbContext context, ILogger<CountReasonSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<CountReasonSettingDto>>>> Search(
        [FromQuery] string? bilId,
        [FromQuery] string? ijReason)
    {
        try
        {
            var query = _context.IjReasonSets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(bilId) && bilId != "全部")
                query = query.Where(x => x.BIL_ID == bilId);

            if (!string.IsNullOrWhiteSpace(ijReason))
                query = query.Where(x => x.IJ_REASON != null && x.IJ_REASON.Contains(ijReason));

            query = query.OrderBy(x => x.BIL_ID).ThenBy(x => x.IJ_REASON);

            var list = await query.Select(x => new CountReasonSettingDto
            {
                BilId = x.BIL_ID ?? "",
                IjReason = x.IJ_REASON ?? "",
                ReasonRem = x.REASON_REM ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<CountReasonSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询查盘/与原因设定时发生错误");
            return StatusCode(500, new ApiResult<List<CountReasonSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>查盘/与原因设定列表行数据（对应前端表格）</summary>
public class CountReasonSettingDto
{
    public string BilId { get; set; } = "";
    public string IjReason { get; set; } = "";
    public string ReasonRem { get; set; } = "";
}
