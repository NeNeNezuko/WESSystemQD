using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InventoryDetailLedgerController : ControllerBase
{
    private readonly ILogger<InventoryDetailLedgerController> _logger;

    public InventoryDetailLedgerController(ILogger<InventoryDetailLedgerController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 查询存货明细帐列表（REP_INV表无字段说明，仅返回空数据）
    /// </summary>
    [HttpGet("search")]
    public ActionResult<ApiResult<List<InventoryDetailLedgerDto>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? prdNo,
        [FromQuery] string? warehouseCode)
    {
        try
        {
            // REP_INV表无字段说明，仅做UI壳子，返回空列表
            return Ok(new ApiResult<List<InventoryDetailLedgerDto>>
            {
                Success = true,
                Data = new List<InventoryDetailLedgerDto>(),
                Total = 0
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询存货明细帐时发生错误");
            return StatusCode(500, new ApiResult<List<InventoryDetailLedgerDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

/// <summary>存货明细帐行数据</summary>
public class InventoryDetailLedgerDto
{
    public DateTime Date { get; set; }
    public string DocumentType { get; set; } = "";
    public string DocumentNumber { get; set; } = "";
    public string Summary { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string Unit { get; set; } = "";
    public decimal InQty { get; set; }
    public decimal OutQty { get; set; }
    public decimal BalanceQty { get; set; }
    public string BatNo { get; set; } = "";
    public string Chuw { get; set; } = "";
    public string WhCode { get; set; } = "";
    public string Rem { get; set; } = "";
}
