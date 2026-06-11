using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductAttrSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ProductAttrSettingController> _logger;

    public ProductAttrSettingController(WarehouseDbContext context, ILogger<ProductAttrSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ProductAttrSettingDto>>>> Search(
        [FromQuery] string? prdNo)
    {
        try
        {
            var query = _context.PrdtPdaRns.AsQueryable();

            if (!string.IsNullOrWhiteSpace(prdNo))
                query = query.Where(x => x.PRD_NO != null && x.PRD_NO.Contains(prdNo));

            query = query.OrderBy(x => x.PRD_NO);

            var list = await query.Select(x => new ProductAttrSettingDto
            {
                PrdNo = x.PRD_NO ?? "",
                QtyCollect = x.QTY_COLLECT ?? "",
                BarcodeType = x.BARCODE_TYPE ?? "",
                NeedScale = x.NEED_SCALE ?? "",
                QtyQzMode = x.QTY_QZ_MODE ?? "",
                UtPoint = x.UT_POINT,
                Ut1Point = x.UT1_POINT,
                Ut1Disp = x.UT1_DISP ?? "",
                QtyType = x.QTY_TYPE ?? "",
                ShowType = x.SHOW_TYPE ?? "",
                ScalePoint = x.SCALE_POINT,
                ScaleQz = x.SCALE_QZ ?? "",
                ShowPak = x.SHOW_PAK ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<ProductAttrSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询货品属性信息设定时发生错误");
            return StatusCode(500, new ApiResult<List<ProductAttrSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>货品属性信息设定列表行数据（对应前端表格）</summary>
public class ProductAttrSettingDto
{
    public string PrdNo { get; set; } = "";
    public string QtyCollect { get; set; } = "";
    public string BarcodeType { get; set; } = "";
    public string NeedScale { get; set; } = "";
    public string QtyQzMode { get; set; } = "";
    public int? UtPoint { get; set; }
    public int? Ut1Point { get; set; }
    public string Ut1Disp { get; set; } = "";
    public string QtyType { get; set; } = "";
    public string ShowType { get; set; } = "";
    public int? ScalePoint { get; set; }
    public string ScaleQz { get; set; } = "";
    public string ShowPak { get; set; } = "";
}
