using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BarcodeTemplateProductController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<BarcodeTemplateProductController> _logger;

    public BarcodeTemplateProductController(WarehouseDbContext context, ILogger<BarcodeTemplateProductController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询依货品设置条码打印套版列表
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<BarcodeTemplateProductDto>>>> Search(
        [FromQuery] string? barType)
    {
        try
        {
            var query = _context.PrdtBarRpts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(barType))
                query = query.Where(x => x.BAR_TYPE != null && x.BAR_TYPE.Contains(barType));

            var list = await query
                .Select(x => new BarcodeTemplateProductDto
                {
                    BarType = x.BAR_TYPE ?? "",
                    PrdNo = x.PRD_NO ?? "",
                    PrdName = x.PRD_NAME ?? "",
                    MidClassNo = x.MID_CLASS_NO ?? "",
                    MidClassName = x.MID_CLASS_NAME ?? "",
                    TemplateCode = x.TEMPLATE_CODE ?? "",
                    TemplateName = x.TEMPLATE_NAME ?? ""
                })
                .ToListAsync();

            return Ok(new ApiResult<List<BarcodeTemplateProductDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询依货品设置条码打印套版时发生错误");
            return StatusCode(500, new ApiResult<List<BarcodeTemplateProductDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

/// <summary>依货品设置条码打印套版DTO</summary>
public class BarcodeTemplateProductDto
{
    public string BarType { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string MidClassNo { get; set; } = "";
    public string MidClassName { get; set; } = "";
    public string TemplateCode { get; set; } = "";
    public string TemplateName { get; set; } = "";
}
