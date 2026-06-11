using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InspectionOrderController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<InspectionOrderController> _logger;

    public InspectionOrderController(WarehouseDbContext context, ILogger<InspectionOrderController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询检验单列表（以表身TF_TY为主，LEFT JOIN表头MF_TY）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<InspectionOrderDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? documentNumber,
        [FromQuery] string? usr)
    {
        try
        {
            var query = from t in _context.TfTys
                        join m in _context.MfTys on t.TY_NO equals m.TY_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 单据日期范围筛选（使用表头的TY_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.TY_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.TY_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 单据号码模糊匹配
            if (!string.IsNullOrWhiteSpace(documentNumber))
                query = query.Where(x => x.T.TY_NO.Contains(documentNumber));

            // 制单人模糊匹配
            if (!string.IsNullOrWhiteSpace(usr))
                query = query.Where(x => x.M != null && (x.M.USR != null && x.M.USR.Contains(usr)));

            // 按单据号+项次排序
            query = query.OrderBy(x => x.T.TY_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new InspectionOrderDto
            {
                ItemNo = x.T.ITM,
                DocumentNumber = x.T.TY_NO,
                DocumentDate = x.M?.TY_DD ?? DateTime.MinValue,
                BilKnd = x.M?.BIL_KND ?? "",
                Tywz = x.M?.TYWZ ?? "",
                Dep = x.M?.DEP ?? "",
                BilType = x.M?.BIL_TYPE ?? "",
                TypeId = x.M?.TYPE_ID ?? "",
                BilNo = x.M?.BIL_NO ?? "",
                Rem = x.M?.REM ?? ""
            }).ToList();

            return Ok(new ApiResult<List<InspectionOrderDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询检验单时发生错误");
            return StatusCode(500, new ApiResult<List<InspectionOrderDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>检验单列表行数据（对应前端表格）</summary>
public class InspectionOrderDto
{
    public int ItemNo { get; set; }
    public string DocumentNumber { get; set; } = "";
    public DateTime DocumentDate { get; set; }
    public string BilKnd { get; set; } = "";
    public string Tywz { get; set; } = "";
    public string Dep { get; set; } = "";
    public string BilType { get; set; } = "";
    public string TypeId { get; set; } = "";
    public string BilNo { get; set; } = "";
    public string Rem { get; set; } = "";
}
