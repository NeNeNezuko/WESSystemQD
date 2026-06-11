using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NoticeSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<NoticeSettingController> _logger;

    public NoticeSettingController(WarehouseDbContext context, ILogger<NoticeSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<NoticeSettingDto>>>> Search(
        [FromQuery] string? setNo,
        [FromQuery] string? wh)
    {
        try
        {
            var query = _context.NoticeSets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(setNo))
                query = query.Where(x => x.SET_NO != null && x.SET_NO.Contains(setNo));

            if (!string.IsNullOrWhiteSpace(wh))
                query = query.Where(x => x.WH != null && x.WH.Contains(wh));

            query = query.OrderBy(x => x.SET_NO);

            var list = await query.Select(x => new NoticeSettingDto
            {
                SetNo = x.SET_NO ?? "",
                TypeId = x.TYPE_ID ?? "",
                Wh = x.WH ?? "",
                SendObj = x.SEND_OBJ ?? "",
                SendType = x.SEND_TYPE ?? "",
                StopId = x.STOP_ID ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<NoticeSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询即时消息通知设定时发生错误");
            return StatusCode(500, new ApiResult<List<NoticeSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>即时消息通知设定列表行数据（对应前端表格）</summary>
public class NoticeSettingDto
{
    public string SetNo { get; set; } = "";
    public string TypeId { get; set; } = "";
    public string Wh { get; set; } = "";
    public string SendObj { get; set; } = "";
    public string SendType { get; set; } = "";
    public string StopId { get; set; } = "";
}
