using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductStorageByClassController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ProductStorageByClassController> _logger;

    public ProductStorageByClassController(WarehouseDbContext context, ILogger<ProductStorageByClassController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ProductStorageByClassDto>>>> Search(
        [FromQuery] string? idxNo,
        [FromQuery] string? wh)
    {
        try
        {
            var query = _context.PrdtCws.AsQueryable();

            if (!string.IsNullOrWhiteSpace(idxNo))
                query = query.Where(x => x.IDX_NO != null && x.IDX_NO.Contains(idxNo));

            if (!string.IsNullOrWhiteSpace(wh))
                query = query.Where(x => x.WH != null && x.WH.Contains(wh));

            query = query.OrderBy(x => x.IDX_NO).ThenBy(x => x.PRD_NO);

            var list = await query.Select(x => new ProductStorageByClassDto
            {
                Guid = x.GUID ?? "",
                PrdNo = x.PRD_NO ?? "",
                IdxNo = x.IDX_NO ?? "",
                Chuw = x.CHUW ?? "",
                Wh = x.WH ?? "",
                Gs = x.GS ?? "",
                Gl = x.GL ?? "",
                Layer = x.LAYER ?? "",
                ZoneId = x.ZONE_ID ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<ProductStorageByClassDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询依储类设定货品储位时发生错误");
            return StatusCode(500, new ApiResult<List<ProductStorageByClassDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>依储类设定货品储位列表行数据（对应前端表格）</summary>
public class ProductStorageByClassDto
{
    public string Guid { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string IdxNo { get; set; } = "";
    public string Chuw { get; set; } = "";
    public string Wh { get; set; } = "";
    public string Gs { get; set; } = "";
    public string Gl { get; set; } = "";
    public string Layer { get; set; } = "";
    public string ZoneId { get; set; } = "";
}
