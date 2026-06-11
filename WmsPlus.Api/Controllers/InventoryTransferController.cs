using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InventoryTransferController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<InventoryTransferController> _logger;

    public InventoryTransferController(WarehouseDbContext context, ILogger<InventoryTransferController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询库存调拨单列表（以表身TF_IC为主，LEFT JOIN表头MF_IC）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<InventoryTransferDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? documentNumber,
        [FromQuery] string? deptCode,
        [FromQuery] string? salNo,
        [FromQuery] string? bilType)
    {
        try
        {
            var query = from t in _context.TfIcs
                        join m in _context.MfIcs on t.IC_NO equals m.IC_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 单据日期范围筛选（使用表头的IC_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.IC_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.IC_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 单据号码模糊匹配（总单号码IC_NO）
            if (!string.IsNullOrWhiteSpace(documentNumber))
                query = query.Where(x => x.M != null && (x.M.IC_NO != null && x.M.IC_NO.Contains(documentNumber)));

            // 部门代号模糊匹配
            if (!string.IsNullOrWhiteSpace(deptCode))
                query = query.Where(x => x.M != null && (x.M.DEP != null && x.M.DEP.Contains(deptCode)));

            // 经办人代号模糊匹配
            if (!string.IsNullOrWhiteSpace(salNo))
                query = query.Where(x => x.M != null && (x.M.SAL_NO != null && x.M.SAL_NO.Contains(salNo)));

            // 来源单据别筛选
            if (!string.IsNullOrWhiteSpace(bilType))
                query = query.Where(x => x.M != null && (x.M.BIL_TYPE != null && x.M.BIL_TYPE.Contains(bilType)));

            // 按总单号+项次排序
            query = query.OrderBy(x => x.M!.IC_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new InventoryTransferDto
            {
                ItemNo = x.T.ITM,
                IcNo = x.M?.IC_NO ?? "",
                DocumentDate = x.M?.IC_DD ?? DateTime.MinValue,
                DeptCode = x.M?.DEP ?? "",
                DeptName = "",
                SalNo = x.M?.SAL_NO ?? "",
                SalName = "",
                BilType = x.M?.BIL_TYPE ?? "",
                BilNo = x.M?.BIL_NO ?? "",
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                PrdMark = x.T.PRD_MARK ?? "",
                QTY = x.T.QTY ?? 0,
                Unit = x.T.UNIT ?? "",
                WhOut = x.T.WH1 ?? "",
                WhIn = x.T.WH2 ?? "",
                Rem = x.T.REM ?? ""
            }).ToList();

            return Ok(new ApiResult<List<InventoryTransferDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询库存调拨单时发生错误");
            return StatusCode(500, new ApiResult<List<InventoryTransferDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>库存调拨单列表行数据（对应前端表格）</summary>
public class InventoryTransferDto
{
    public int ItemNo { get; set; }
    public string IcNo { get; set; } = "";
    public DateTime DocumentDate { get; set; }
    public string DeptCode { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string SalNo { get; set; } = "";
    public string SalName { get; set; } = "";
    public string BilType { get; set; } = "";
    public string BilNo { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public decimal QTY { get; set; }
    public string Unit { get; set; } = "";
    public string WhOut { get; set; } = "";
    public string WhIn { get; set; } = "";
    public string Rem { get; set; } = "";
}
