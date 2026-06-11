using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductStorageByNatureController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ProductStorageByNatureController> _logger;

    public ProductStorageByNatureController(WarehouseDbContext context, ILogger<ProductStorageByNatureController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ProductStorageByNatureDto>>>> Search(
        [FromQuery] string? cwxzNo,
        [FromQuery] string? wh)
    {
        try
        {
            var query = _context.PrdtCwXzs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(cwxzNo))
                query = query.Where(x => x.CWXZ_NO != null && x.CWXZ_NO.Contains(cwxzNo));

            if (!string.IsNullOrWhiteSpace(wh))
                query = query.Where(x => x.WH != null && x.WH.Contains(wh));

            query = query.OrderBy(x => x.CWXZ_NO);

            var list = await query.Select(x => new ProductStorageByNatureDto
            {
                Guid = x.GUID ?? "",
                PrdNo = x.PRD_NO ?? "",
                CwxzNo = x.CWXZ_NO ?? "",
                Chuw = x.CHUW ?? "",
                Wh = x.WH ?? "",
                Gs = x.GS ?? "",
                Gl = x.GL ?? "",
                Layer = x.LAYER ?? "",
                ZoneId = x.ZONE_ID ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<ProductStorageByNatureDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询依储存性设定货品储位时发生错误");
            return StatusCode(500, new ApiResult<List<ProductStorageByNatureDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>依储存性设定货品储位列表行数据（对应前端表格）</summary>
public class ProductStorageByNatureDto
{
    public string Guid { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string CwxzNo { get; set; } = "";
    public string Chuw { get; set; } = "";
    public string Wh { get; set; } = "";
    public string Gs { get; set; } = "";
    public string Gl { get; set; } = "";
    public string Layer { get; set; } = "";
    public string ZoneId { get; set; } = "";
}
