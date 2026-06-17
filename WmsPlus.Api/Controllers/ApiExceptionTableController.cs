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
        [FromQuery] string? dateFrom,
        [FromQuery] string? dateTo,
        [FromQuery] string? wh,
        [FromQuery] string? sourceNo,
        [FromQuery] bool taskTypeLk = false,
        [FromQuery] bool taskTypeLkPush = false,
        [FromQuery] bool taskTypeErp = false,
        [FromQuery] bool taskTypeErpPush = false)
    {
        try
        {
            var query = _context.ApiActionIs.AsQueryable();

            // 日期范围筛选
            if (!string.IsNullOrWhiteSpace(dateFrom) && DateTime.TryParse(dateFrom, out var sd))
                query = query.Where(x => x.START_DATE >= sd);
            if (!string.IsNullOrWhiteSpace(dateTo) && DateTime.TryParse(dateTo, out var ed))
                query = query.Where(x => x.END_DATE <= ed.AddDays(1).AddSeconds(-1));

            // 仓库代号模糊匹配
            if (!string.IsNullOrWhiteSpace(wh))
                query = query.Where(x => x.WH != null && x.WH.Contains(wh));

            // 来源单号模糊匹配
            if (!string.IsNullOrWhiteSpace(sourceNo))
                query = query.Where(x => x.BIL_NO != null && x.BIL_NO.Contains(sourceNo));

            // 任务类型筛选（通过ACT_NO前缀匹配）
            var taskTypes = new List<string>();
            if (taskTypeLk) taskTypes.Add("LK");
            if (taskTypeLkPush) taskTypes.Add("LK_PUSH");
            if (taskTypeErp) taskTypes.Add("ERP");
            if (taskTypeErpPush) taskTypes.Add("ERP_PUSH");
            if (taskTypes.Count > 0)
                query = query.Where(x => x.ACT_NO != null && taskTypes.Any(t => x.ACT_NO.StartsWith(t)));

            query = query.OrderByDescending(x => x.START_DATE).Take(500);

            var rawList = await query.ToListAsync();

            var list = rawList.Select((x, idx) => new ApiExceptionTableDto
            {
                Itm = (idx + 1).ToString(),
                TaskType = x.ACT_NO ?? "",
                ProcessNo = x.METHOD_NO ?? "",
                ApiCode = x.PATH ?? "",
                SourceDocName = x.BIL_ID ?? "",
                SourceNo = x.BIL_NO ?? "",
                Wh = x.WH ?? "",
                ContainCode = x.CONTAIN_CODE ?? x.CON_NO ?? "",
                ExecCount = 0,
                ErrMsg = x.ERR_MSG ?? x.ERR_CODE ?? "",
                HttpErrMsg = x.STATUS_ID ?? ""
            }).ToList();

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
    public string Itm { get; set; } = "";
    public string TaskType { get; set; } = "";
    public string ProcessNo { get; set; } = "";
    public string ApiCode { get; set; } = "";
    public string SourceDocName { get; set; } = "";
    public string SourceNo { get; set; } = "";
    public string Wh { get; set; } = "";
    public string ContainCode { get; set; } = "";
    public int ExecCount { get; set; }
    public string ErrMsg { get; set; } = "";
    public string HttpErrMsg { get; set; } = "";
}
