using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class InspectionTaskController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<InspectionTaskController> _logger;

    public InspectionTaskController(WarehouseDbContext context, ILogger<InspectionTaskController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询请检任务单列表（以表身TF_QJRW为主，LEFT JOIN表头MF_QJRW）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<InspectionTaskDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? documentNumber,
        [FromQuery] string? usr,
        [FromQuery] string? warehouseTy)
    {
        try
        {
            var query = from t in _context.TfQjrws
                        join m in _context.MfQjrws on t.QJ_NO equals m.QJ_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 单据日期范围筛选（使用表头的QJ_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.QJ_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.QJ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 单据号码模糊匹配
            if (!string.IsNullOrWhiteSpace(documentNumber))
                query = query.Where(x => x.T.QJ_NO.Contains(documentNumber));

            // 制单人模糊匹配
            if (!string.IsNullOrWhiteSpace(usr))
                query = query.Where(x => x.M != null && (x.M.USR != null && x.M.USR.Contains(usr)));

            // 检验仓库模糊匹配
            if (!string.IsNullOrWhiteSpace(warehouseTy))
                query = query.Where(x => x.M != null && (x.M.WH_TY != null && x.M.WH_TY.Contains(warehouseTy)));

            // 按单据号+项次排序
            query = query.OrderBy(x => x.T.QJ_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new InspectionTaskDto
            {
                ItemNo = x.T.ITM,
                DocumentDate = x.M?.QJ_DD ?? DateTime.MinValue,
                DocumentNumber = x.T.QJ_NO,
                WarehouseTy = x.M?.WH_TY ?? "",
                WarehouseName = "",  // MF_QJRW表头无仓库名称字段
                Rem = x.M?.REM ?? "",
                UsrName = x.M?.USR ?? ""
            }).ToList();

            return Ok(new ApiResult<List<InspectionTaskDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询请检任务单时发生错误");
            return StatusCode(500, new ApiResult<List<InspectionTaskDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>请检任务单列表行数据（对应前端表格）</summary>
public class InspectionTaskDto
{
    public int ItemNo { get; set; }
    public DateTime DocumentDate { get; set; }
    public string DocumentNumber { get; set; } = "";
    public string WarehouseTy { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string Rem { get; set; } = "";
    public string UsrName { get; set; } = "";
}


