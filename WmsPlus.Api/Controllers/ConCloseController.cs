using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ConCloseController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ConCloseController> _logger;

    public ConCloseController(WarehouseDbContext context, ILogger<ConCloseController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询关账作业列表
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ConCloseDto>>>> Search(
        [FromQuery] string? conNo = null,
        [FromQuery] string? closeDateFrom = null,
        [FromQuery] string? closeDateTo = null)
    {
        try
        {
            var query = _context.ConCloses.AsQueryable();

            // 货主编码模糊匹配
            if (!string.IsNullOrWhiteSpace(conNo))
                query = query.Where(x => x.CON_NO != null && x.CON_NO.Contains(conNo));

            // 关账日期范围筛选
            if (DateTime.TryParse(closeDateFrom, out var dateFrom))
                query = query.Where(x => x.CLOSE_DD >= dateFrom);
            if (DateTime.TryParse(closeDateTo, out var dateTo))
                query = query.Where(x => x.CLOSE_DD <= dateTo.AddDays(1).AddSeconds(-1));

            var list = await query
                .OrderByDescending(x => x.MODIFY_DD)
                .ToListAsync();

            var result = list.Select(x => new ConCloseDto
            {
                Guid = x.GUID ?? "",
                ActNo = x.ACT_NO ?? "",
                ConNo = x.CON_NO ?? "",
                CloseDate = x.CLOSE_DD,
                ModifyMan = x.MODIFY_MAN ?? "",
                ModifyDate = x.MODIFY_DD
            }).ToList();

            return Ok(new ApiResult<List<ConCloseDto>>
            {
                Success = true,
                Data = result,
                Total = result.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询关账作业时发生错误");
            return StatusCode(500, new ApiResult<List<ConCloseDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

/// <summary>关账作业列表行数据</summary>
public class ConCloseDto
{
    /// <summary>唯一号</summary>
    public string Guid { get; set; } = "";
    /// <summary>任务编号</summary>
    public string ActNo { get; set; } = "";
    /// <summary>货主编码</summary>
    public string ConNo { get; set; } = "";
    /// <summary>关帐日期</summary>
    public DateTime? CloseDate { get; set; }
    /// <summary>最近修改人</summary>
    public string ModifyMan { get; set; } = "";
    /// <summary>最近修改日期</summary>
    public DateTime? ModifyDate { get; set; }
}
