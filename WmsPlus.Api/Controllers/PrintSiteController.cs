using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PrintSiteController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<PrintSiteController> _logger;

    public PrintSiteController(WarehouseDbContext context, ILogger<PrintSiteController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询打印网点列表（PRINT_SET）
    /// </summary>
    [HttpGet("list")]
    public async Task<ActionResult<ApiResult<List<PrintSiteListDto>>>> SearchList(
        [FromQuery] string? siteName,
        [FromQuery] string? machineIp,
        [FromQuery] bool? stopFlag,
        [FromQuery] string? tabType)
    {
        try
        {
            var query = _context.PrintSets.AsQueryable();

            // 网点名称模糊匹配
            if (!string.IsNullOrWhiteSpace(siteName))
                query = query.Where(x => x.SITE_NAME != null && x.SITE_NAME.Contains(siteName));

            // 机器IP模糊匹配
            if (!string.IsNullOrWhiteSpace(machineIp))
                query = query.Where(x => x.MACHINE_IP != null && x.MACHINE_IP.Contains(machineIp));

            // 停用标记筛选
            if (stopFlag.HasValue)
            {
                query = stopFlag.Value
                    ? query.Where(x => x.STOP_FLAG == "Y")
                    : query.Where(x => x.STOP_FLAG == null || x.STOP_FLAG != "Y");
            }

            // 按序号排序
            query = query.OrderBy(x => x.SEQ_NO);

            var rawList = await query.ToListAsync();

            var list = rawList.Select(x => new PrintSiteListDto
            {
                SeqNo = x.SEQ_NO,
                SiteName = x.SITE_NAME ?? "",
                MachineIp = x.MACHINE_IP ?? "",
                MachineName = x.MACHINE_NAME ?? "",
                StopFlag = x.STOP_FLAG ?? "",
                StopDate = x.STOP_DATE
            }).ToList();

            return Ok(new ApiResult<List<PrintSiteListDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询打印网点列表时发生错误");
            return StatusCode(500, new ApiResult<List<PrintSiteListDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 查询打印网点资料（PRINT_SER_TASK）
    /// </summary>
    [HttpGet("query")]
    public async Task<ActionResult<ApiResult<List<PrintSiteQueryDto>>>> SearchQuery(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? versionCode,
        [FromQuery] string? printerUser,
        [FromQuery] string? siteName,
        [FromQuery] string? tabType)
    {
        try
        {
            var query = _context.PrintSerTasks.AsQueryable();

            // 打印时间范围筛选
            if (dateFrom.HasValue)
                query = query.Where(x => x.PRINT_TIME >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.PRINT_TIME <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 版套代号模糊匹配
            if (!string.IsNullOrWhiteSpace(versionCode))
                query = query.Where(x => x.VERSION_CODE != null && x.VERSION_CODE.Contains(versionCode));

            // 打印人员模糊匹配
            if (!string.IsNullOrWhiteSpace(printerUser))
                query = query.Where(x => x.PRINTER_USER != null && x.PRINTER_USER.Contains(printerUser));

            // 网点名称模糊匹配
            if (!string.IsNullOrWhiteSpace(siteName))
                query = query.Where(x => x.SITE_NAME != null && x.SITE_NAME.Contains(siteName));

            // 按序号排序
            query = query.OrderBy(x => x.SEQ_NO);

            var rawList = await query.ToListAsync();

            var list = rawList.Select(x => new PrintSiteQueryDto
            {
                SeqNo = x.SEQ_NO,
                PrintTime = x.PRINT_TIME,
                VersionCode = x.VERSION_CODE ?? "",
                PrinterUser = x.PRINTER_USER ?? "",
                SiteName = x.SITE_NAME ?? "",
                ProgramCode = x.PROGRAM_CODE ?? "",
                TemplateCode = x.TEMPLATE_CODE ?? "",
                PrintStatus = x.PRINT_STATUS ?? "",
                FailCount = x.FAIL_COUNT,
                FailReason = x.FAIL_REASON ?? "",
                PrintNo = x.PRINT_NO ?? ""
            }).ToList();

            return Ok(new ApiResult<List<PrintSiteQueryDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询打印网点资料时发生错误");
            return StatusCode(500, new ApiResult<List<PrintSiteQueryDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>打印网点列表行数据（对应前端表格）</summary>
public class PrintSiteListDto
{
    public int SeqNo { get; set; }
    public string SiteName { get; set; } = "";
    public string MachineIp { get; set; } = "";
    public string MachineName { get; set; } = "";
    public string StopFlag { get; set; } = "";
    public DateTime? StopDate { get; set; }
}

/// <summary>打印网点资料行数据（对应前端表格）</summary>
public class PrintSiteQueryDto
{
    public int SeqNo { get; set; }
    public DateTime? PrintTime { get; set; }
    public string VersionCode { get; set; } = "";
    public string PrinterUser { get; set; } = "";
    public string SiteName { get; set; } = "";
    public string ProgramCode { get; set; } = "";
    public string TemplateCode { get; set; } = "";
    public string PrintStatus { get; set; } = "";
    public int? FailCount { get; set; }
    public string FailReason { get; set; } = "";
    public string PrintNo { get; set; } = "";
}
