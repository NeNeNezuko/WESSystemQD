using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductCodeSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ProductCodeSettingController> _logger;

    public ProductCodeSettingController(WarehouseDbContext context, ILogger<ProductCodeSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询货品代号设定列表
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ProductCodeSettingDto>>>> Search(
        [FromQuery] string? prdNo,
        [FromQuery] string? name)
    {
        try
        {
            var query = _context.Prdts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(prdNo))
                query = query.Where(x => x.PRD_NO != null && x.PRD_NO.Contains(prdNo));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            query = query.OrderBy(x => x.PRD_NO);

            var list = await query.Select(x => new ProductCodeSettingDto
            {
                PrdNo = x.PRD_NO ?? "",
                Name = x.NAME ?? "",
                Snm = x.SNM ?? "",
                Idx1 = x.IDX1 ?? "",
                Ut = x.UT ?? "",
                Spc = x.SPC ?? "",
                CwxzNo = x.CWXZ_NO ?? "",
                NouseDd = x.NOUSE_DD
            }).ToListAsync();

            return Ok(new ApiResult<List<ProductCodeSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询货品代号设定时发生错误");
            return StatusCode(500, new ApiResult<List<ProductCodeSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

/// <summary>货品代号设定DTO</summary>
public class ProductCodeSettingDto
{
    public string PrdNo { get; set; } = "";
    public string Name { get; set; } = "";
    public string Snm { get; set; } = "";
    public string Idx1 { get; set; } = "";
    public string Ut { get; set; } = "";
    public string Spc { get; set; } = "";
    public string CwxzNo { get; set; } = "";
    public DateTime? NouseDd { get; set; }
}
