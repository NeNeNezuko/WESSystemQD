using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AcceptanceReturnController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<AcceptanceReturnController> _logger;

    public AcceptanceReturnController(WarehouseDbContext context, ILogger<AcceptanceReturnController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询验收退回单列表（以表身TF_YB为主，LEFT JOIN表头MF_YB）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<AcceptanceReturnDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? documentNumber,
        [FromQuery] string? usr)
    {
        try
        {
            var query = from t in _context.TfYbs
                        join m in _context.MfYbs on t.YB_NO equals m.YB_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 单据日期范围筛选（使用表头的YB_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.YB_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.YB_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 单据号码模糊匹配
            if (!string.IsNullOrWhiteSpace(documentNumber))
                query = query.Where(x => x.T.YB_NO.Contains(documentNumber));

            // 制单人模糊匹配
            if (!string.IsNullOrWhiteSpace(usr))
                query = query.Where(x => x.M != null && (x.M.USR != null && x.M.USR.Contains(usr)));

            // 按单据号+项次排序
            query = query.OrderBy(x => x.T.YB_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new AcceptanceReturnDto
            {
                ItemNo = x.T.ITM ?? 0,
                DocumentNumber = x.T.YB_NO,
                DocumentDate = x.M?.YB_DD ?? DateTime.MinValue,
                BilKnd = x.M?.BIL_KND ?? "",
                DepName = "",  // DEPT关联待做
                BilType = x.M?.BIL_TYPE ?? "",
                TyNo = x.M?.TY_NO ?? "",
                Rem = x.M?.REM ?? "",
                CusNo = x.M?.CUS_NO ?? "",
                CusName = x.M?.CUS_NAME ?? ""
            }).ToList();

            return Ok(new ApiResult<List<AcceptanceReturnDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询验收退回单时发生错误");
            return StatusCode(500, new ApiResult<List<AcceptanceReturnDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>验收退回单列表行数据（对应前端表格）</summary>
public class AcceptanceReturnDto
{
    public int ItemNo { get; set; }
    public string DocumentNumber { get; set; } = "";
    public DateTime DocumentDate { get; set; }
    public string BilKnd { get; set; } = "";
    public string DepName { get; set; } = "";
    public string BilType { get; set; } = "";
    public string TyNo { get; set; } = "";
    public string Rem { get; set; } = "";
    public string CusNo { get; set; } = "";
    public string CusName { get; set; } = "";
}
