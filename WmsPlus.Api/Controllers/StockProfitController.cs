using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StockProfitController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<StockProfitController> _logger;

    public StockProfitController(WarehouseDbContext context, ILogger<StockProfitController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询盘盈单列表（以表身TF_YN为主，LEFT JOIN表头MF_YN）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<StockProfitDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? documentNumber,
        [FromQuery] string? salNo,
        [FromQuery] string? warehouseCode)
    {
        try
        {
            var query = from t in _context.TfYns
                        join m in _context.MfYns on t.YN_NO equals m.YN_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 单据日期范围筛选（使用表头的YN_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.YN_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.YN_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 单据号码模糊匹配
            if (!string.IsNullOrWhiteSpace(documentNumber))
                query = query.Where(x => x.T.YN_NO.Contains(documentNumber));

            // 经办人模糊匹配
            if (!string.IsNullOrWhiteSpace(salNo))
                query = query.Where(x => x.M != null && (x.M.SAL_NO != null && x.M.SAL_NO.Contains(salNo)));

            // 仓库代号模糊匹配
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.M != null && (x.M.WH != null && x.M.WH.Contains(warehouseCode)));

            // 按单据号+项次排序
            query = query.OrderBy(x => x.T.YN_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new StockProfitDto
            {
                ItemNo = x.T.ITM,
                DocumentDate = x.M?.YN_DD ?? DateTime.MinValue,
                DocumentNumber = x.T.YN_NO,
                PdNo = x.M?.PD_NO ?? "",
                DepName = "",  // 需要关联DEPT表获取，先留空
                WarehouseName = "",  // 需要关联MY_WH获取，先留空
                BilTypeName = x.M?.BIL_TYPE ?? "",
                SalName = x.M?.SAL_NO ?? ""
            }).ToList();

            return Ok(new ApiResult<List<StockProfitDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询盘盈单时发生错误");
            return StatusCode(500, new ApiResult<List<StockProfitDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>盘盈单列表行数据（对应前端表格）</summary>
public class StockProfitDto
{
    public int ItemNo { get; set; }
    public DateTime DocumentDate { get; set; }
    public string DocumentNumber { get; set; } = "";
    public string PdNo { get; set; } = "";
    public string DepName { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string BilTypeName { get; set; } = "";
    public string SalName { get; set; } = "";
}
