using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StorageLocationDetailController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<StorageLocationDetailController> _logger;

    public StorageLocationDetailController(WarehouseDbContext context, ILogger<StorageLocationDetailController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<StorageLocationDetailDto>>>> Search(
        [FromQuery] string? chuw,
        [FromQuery] string? wh,
        [FromQuery] string? cwStatus)
    {
        try
        {
            var query = _context.CwWhs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(chuw))
                query = query.Where(x => x.CHUW != null && x.CHUW.Contains(chuw));

            if (!string.IsNullOrWhiteSpace(wh))
                query = query.Where(x => x.WH != null && x.WH.Contains(wh));

            if (!string.IsNullOrWhiteSpace(cwStatus) && cwStatus != "全部")
                query = query.Where(x => x.CW_STATUS != null && x.CW_STATUS == cwStatus);

            query = query.OrderBy(x => x.CHUW);

            var list = await query.Select(x => new StorageLocationDetailDto
            {
                Chuw = x.CHUW ?? "",
                Name = x.NAME ?? "",
                Wh = x.WH ?? "",
                Gs = x.GS ?? "",
                Gl = x.GL ?? "",
                Layer = x.LAYER ?? "",
                CwStatus = x.CW_STATUS ?? "",
                LockCw = x.LOCK_CW ?? "",
                AreaId = x.AREA_ID ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<StorageLocationDetailDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询储位明细时发生错误");
            return StatusCode(500, new ApiResult<List<StorageLocationDetailDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>储位明细列表行数据（对应前端表格）</summary>
public class StorageLocationDetailDto
{
    public string Chuw { get; set; } = "";
    public string Name { get; set; } = "";
    public string Wh { get; set; } = "";
    public string Gs { get; set; } = "";
    public string Gl { get; set; } = "";
    public string Layer { get; set; } = "";
    public string CwStatus { get; set; } = "";
    public string LockCw { get; set; } = "";
    public string AreaId { get; set; } = "";
}

// ApiResult<T> 已在 InboundNoticeController.cs 等处全局定义，此处移除避免 CS0101 冲突
