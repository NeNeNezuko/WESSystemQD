using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ServiceLogQueryController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ServiceLogQueryController> _logger;

    public ServiceLogQueryController(WarehouseDbContext context, ILogger<ServiceLogQueryController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ServiceLogQueryDto>>>> Search(
        [FromQuery] string? svcNo,
        [FromQuery] string? name,
        [FromQuery] string? startDate,
        [FromQuery] string? endDate)
    {
        try
        {
            var query = _context.SvcLogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(svcNo))
                query = query.Where(x => x.SVC_NO != null && x.SVC_NO.Contains(svcNo));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            if (!string.IsNullOrWhiteSpace(startDate) && DateTime.TryParse(startDate, out var sd))
                query = query.Where(x => x.START_TIME >= sd);

            if (!string.IsNullOrWhiteSpace(endDate) && DateTime.TryParse(endDate, out var ed))
                query = query.Where(x => x.END_TIME <= ed);

            query = query.OrderByDescending(x => x.START_TIME);

            var list = await query.Select(x => new ServiceLogQueryDto
            {
                SvcNo = x.SVC_NO ?? "",
                SvcNo1 = x.SVC_NO1 ?? "",
                Name = x.NAME ?? "",
                Name1 = x.NAME1 ?? "",
                Path = x.PATH ?? "",
                IntervalTime = x.INTERVAL_TIME,
                StartTime = x.START_TIME,
                EndTime = x.END_TIME,
                Rem = x.REM ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<ServiceLogQueryDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询服务日志时发生错误");
            return StatusCode(500, new ApiResult<List<ServiceLogQueryDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>服务日志查询行数据</summary>
public class ServiceLogQueryDto
{
    public string SvcNo { get; set; } = "";
    public string SvcNo1 { get; set; } = "";
    public string Name { get; set; } = "";
    public string Name1 { get; set; } = "";
    public string Path { get; set; } = "";
    public int? IntervalTime { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string Rem { get; set; } = "";
}
