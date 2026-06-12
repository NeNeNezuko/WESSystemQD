using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ServiceExceptionQueryController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ServiceExceptionQueryController> _logger;

    public ServiceExceptionQueryController(WarehouseDbContext context, ILogger<ServiceExceptionQueryController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ServiceExceptionQueryDto>>>> Search(
        [FromQuery] string? svcNo,
        [FromQuery] string? ycNo,
        [FromQuery] string? startDate,
        [FromQuery] string? endDate)
    {
        try
        {
            var query = _context.SvcYcs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(svcNo))
                query = query.Where(x => x.SVC_NO != null && x.SVC_NO.Contains(svcNo));

            if (!string.IsNullOrWhiteSpace(ycNo))
                query = query.Where(x => x.YC_NO != null && x.YC_NO.Contains(ycNo));

            if (!string.IsNullOrWhiteSpace(startDate) && DateTime.TryParse(startDate, out var sd))
                query = query.Where(x => x.SYS_DATE >= sd);

            if (!string.IsNullOrWhiteSpace(endDate) && DateTime.TryParse(endDate, out var ed))
                query = query.Where(x => x.SYS_DATE <= ed);

            query = query.OrderByDescending(x => x.SYS_DATE);

            var list = await query.Select(x => new ServiceExceptionQueryDto
            {
                YcNo = x.YC_NO ?? "",
                SvcNo = x.SVC_NO ?? "",
                SvcNo1 = x.SVC_NO1 ?? "",
                Rem = x.REM ?? "",
                SysDate = x.SYS_DATE
            }).ToListAsync();

            return Ok(new ApiResult<List<ServiceExceptionQueryDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询服务异常时发生错误");
            return StatusCode(500, new ApiResult<List<ServiceExceptionQueryDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>服务异常查询行数据</summary>
public class ServiceExceptionQueryDto
{
    public string YcNo { get; set; } = "";
    public string SvcNo { get; set; } = "";
    public string SvcNo1 { get; set; } = "";
    public string Rem { get; set; } = "";
    public DateTime? SysDate { get; set; }
}
