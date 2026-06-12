using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ApiExceptionTableController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ApiExceptionTableController> _logger;

    public ApiExceptionTableController(WarehouseDbContext context, ILogger<ApiExceptionTableController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ApiExceptionTableDto>>>> Search(
        [FromQuery] string? actNo,
        [FromQuery] string? bilNo,
        [FromQuery] string? method,
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

            if (!string.IsNullOrWhiteSpace(method))
                query = query.Where(x => x.METHOD_NO != null && x.METHOD_NO.Contains(method));

            if (!string.IsNullOrWhiteSpace(status) && status != "全部")
                query = query.Where(x => x.STATUS_ID == status);

            if (!string.IsNullOrWhiteSpace(startDate) && DateTime.TryParse(startDate, out var sd))
                query = query.Where(x => x.START_DATE >= sd);

            if (!string.IsNullOrWhiteSpace(endDate) && DateTime.TryParse(endDate, out var ed))
                query = query.Where(x => x.END_DATE <= ed);

            query = query.OrderByDescending(x => x.START_DATE).Take(500);

            var list = await query.Select(x => new ApiExceptionTableDto
            {
                ActNo = x.ACT_NO ?? "",
                MethodNo = x.METHOD_NO ?? "",
                HttpMethod = x.HTTP_METHOD ?? "",
                Path = x.PATH ?? "",
                BilId = x.BIL_ID ?? "",
                BilNo = x.BIL_NO ?? "",
                Wh = x.WH ?? "",
                ConNo = x.CON_NO ?? "",
                StatusId = x.STATUS_ID ?? "",
                ErrCode = x.ERR_CODE ?? "",
                StartDate = x.START_DATE,
                EndDate = x.END_DATE
            }).ToListAsync();

            return Ok(new ApiResult<List<ApiExceptionTableDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询API接口异常时发生错误");
            return StatusCode(500, new ApiResult<List<ApiExceptionTableDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>API接口异常列表行数据</summary>
public class ApiExceptionTableDto
{
    public string ActNo { get; set; } = "";
    public string MethodNo { get; set; } = "";
    public string HttpMethod { get; set; } = "";
    public string Path { get; set; } = "";
    public string BilId { get; set; } = "";
    public string BilNo { get; set; } = "";
    public string Wh { get; set; } = "";
    public string ConNo { get; set; } = "";
    public string StatusId { get; set; } = "";
    public string ErrCode { get; set; } = "";
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
