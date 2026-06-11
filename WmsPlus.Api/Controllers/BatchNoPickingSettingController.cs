using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BatchNoPickingSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<BatchNoPickingSettingController> _logger;

    public BatchNoPickingSettingController(WarehouseDbContext context, ILogger<BatchNoPickingSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<BatchNoPickingSettingDto>>>> Search(
        [FromQuery] string? wh,
        [FromQuery] string? prdNo,
        [FromQuery] string? batNo)
    {
        try
        {
            var query = _context.BatNotPws.AsQueryable();

            if (!string.IsNullOrWhiteSpace(wh))
                query = query.Where(x => x.WH != null && x.WH.Contains(wh));

            if (!string.IsNullOrWhiteSpace(prdNo))
                query = query.Where(x => x.PRD_NO != null && x.PRD_NO.Contains(prdNo));

            if (!string.IsNullOrWhiteSpace(batNo))
                query = query.Where(x => x.BAT_NO != null && x.BAT_NO.Contains(batNo));

            query = query.OrderBy(x => x.WH).ThenBy(x => x.PRD_NO);

            var list = await query.Select(x => new BatchNoPickingSettingDto
            {
                Guid = x.GUID ?? "",
                Wh = x.WH ?? "",
                PrdNo = x.PRD_NO ?? "",
                PrdMark = x.PRD_MARK ?? "",
                BatNo = x.BAT_NO ?? "",
                Usr = x.USR ?? "",
                SysDate = x.SYS_DATE,
                Rem = x.REM ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<BatchNoPickingSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询不能参与配位的批号设置时发生错误");
            return StatusCode(500, new ApiResult<List<BatchNoPickingSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>不能参与配位的批号设置列表行数据（对应前端表格）</summary>
public class BatchNoPickingSettingDto
{
    public string Guid { get; set; } = "";
    public string Wh { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public string BatNo { get; set; } = "";
    public string Usr { get; set; } = "";
    public DateTime? SysDate { get; set; }
    public string Rem { get; set; } = "";
}
