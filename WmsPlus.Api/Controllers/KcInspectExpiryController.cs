using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

/// <summary>
/// 库存货品检验到期查询控制器
/// 以TF_QJRW（请检任务单身）为主表，LEFT JOIN MF_QJRW（请检任务单表头）
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class KcInspectExpiryController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<KcInspectExpiryController> _logger;

    public KcInspectExpiryController(WarehouseDbContext context, ILogger<KcInspectExpiryController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询库存检验到期数据列表
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<KcInspectExpiryDto>>>> Search(
        [FromQuery] string? warehouseCode,
        [FromQuery] string? prdNo,
        [FromQuery] string? prdName,
        [FromQuery] int exceedDays = 0)
    {
        try
        {
            // 以TF_QJRW为主表 LEFT JOIN MF_QJRW表头
            var query = from t in _context.TfQjrws
                        join m in _context.MfQjrws on t.QJ_NO equals m.QJ_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 仓库代号筛选（匹配表头检验仓库或表身仓库）
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.M != null && ((x.M.WH_TY != null && x.M.WH_TY.Contains(warehouseCode)) || (x.T.WH != null && x.T.WH.Contains(warehouseCode))));

            // 货品代号模糊匹配
            if (!string.IsNullOrWhiteSpace(prdNo))
                query = query.Where(x => x.T.PRD_NO != null && x.T.PRD_NO.Contains(prdNo));

            // 货品名称模糊匹配
            if (!string.IsNullOrWhiteSpace(prdName))
                query = query.Where(x => x.T.PRD_NAME != null && x.T.PRD_NAME.Contains(prdName));

            // 超验天数筛选：最近验日期为空或早于目标日期
            if (exceedDays > 0)
            {
                var targetDate = DateTime.Today.AddDays(-exceedDays);
                query = query.Where(x => x.T.LST_TYD == null || x.T.LST_TYD < targetDate);
            }

            // 按请检任务单号+项次排序
            query = query.OrderBy(x => x.T.QJ_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 投影为DTO
            var list = rawList.Select(x => new KcInspectExpiryDto
            {
                WarehouseName = "",
                Chuw = x.T.CHUW ?? "",
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                BatNo = x.T.BAT_NO ?? "",
                UnitName = x.T.UNIT ?? "",
                Qty = x.T.QTY ?? 0,
                Qty1 = x.T.QTY1 ?? 0,
                LstTyd = x.T.LST_TYD
            }).ToList();

            return Ok(new ApiResult<List<KcInspectExpiryDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询库存检验到期数据时发生错误");
            return StatusCode(500, new ApiResult<List<KcInspectExpiryDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>库存检验到期查询行数据（对应前端表格）</summary>
public class KcInspectExpiryDto
{
    /// <summary>仓库名称</summary>
    public string WarehouseName { get; set; } = "";

    /// <summary>储位</summary>
    public string Chuw { get; set; } = "";

    /// <summary>货品代号</summary>
    public string PrdNo { get; set; } = "";

    /// <summary>货品名称</summary>
    public string PrdName { get; set; } = "";

    /// <summary>批号</summary>
    public string BatNo { get; set; } = "";

    /// <summary>单位名称</summary>
    public string UnitName { get; set; } = "";

    /// <summary>可验数量</summary>
    public decimal Qty { get; set; }

    /// <summary>可验数量(副)</summary>
    public decimal Qty1 { get; set; }

    /// <summary>最近验日期</summary>
    public DateTime? LstTyd { get; set; }
}


