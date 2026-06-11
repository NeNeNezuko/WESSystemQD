using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StorageUnshelvingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<StorageUnshelvingController> _logger;

    public StorageUnshelvingController(WarehouseDbContext context, ILogger<StorageUnshelvingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询储位下架单列表（以表身TF_CWXJ为主，JOIN表头MF_CWXJ）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<StorageUnshelvingDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? documentNumber,
        [FromQuery] string? deptCode,
        [FromQuery] string? salNo,
        [FromQuery] string? bilType)
    {
        try
        {
            var query = from t in _context.TfCwxjs
                        join m in _context.MfCwxjs on t.XJ_NO equals m.XJ_NO
                        select new { T = t, M = m };

            // 单据日期范围筛选（使用表头的XJ_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M.XJ_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M.XJ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 单据号码模糊匹配
            if (!string.IsNullOrWhiteSpace(documentNumber))
                query = query.Where(x => x.T.XJ_NO.Contains(documentNumber) || x.M.XJ_NO.Contains(documentNumber));

            // 部门代号模糊匹配
            if (!string.IsNullOrWhiteSpace(deptCode))
                query = query.Where(x => x.M.DEP != null && x.M.DEP.Contains(deptCode));

            // 经办人模糊匹配
            if (!string.IsNullOrWhiteSpace(salNo))
                query = query.Where(x => x.M.SAL_NO != null && x.M.SAL_NO.Contains(salNo));

            // 单据类别模糊匹配
            if (!string.IsNullOrWhiteSpace(bilType))
                query = query.Where(x => x.M.BIL_TYPE != null && x.M.BIL_TYPE.Contains(bilType));

            // 按单据号+项次排序
            query = query.OrderBy(x => x.M.XJ_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new StorageUnshelvingDto
            {
                ItemNo = x.T.ITM,
                DocumentDate = x.M.XJ_DD ?? DateTime.MinValue,
                DocumentNumber = x.M.XJ_NO,
                DeptCode = x.M.DEP ?? "",
                SalNo = x.M.SAL_NO ?? "",
                BilType = x.M.BIL_TYPE ?? "",
                BilId = x.M.BIL_ID ?? "",
                BilNo = x.M.BIL_NO ?? "",
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                PrdMark = x.T.PRD_MARK ?? "",
                QTY = x.T.QTY ?? 0,
                Unit = x.T.UNIT ?? "",
                WH = x.T.WH ?? "",
                CHUW = x.T.CHUW1 ?? ""
            }).ToList();

            return Ok(new ApiResult<List<StorageUnshelvingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询储位下架单时发生错误");
            return StatusCode(500, new ApiResult<List<StorageUnshelvingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>储位下架单列表行数据（对应前端表格）</summary>
public class StorageUnshelvingDto
{
    public int ItemNo { get; set; }
    public DateTime DocumentDate { get; set; }
    public string DocumentNumber { get; set; } = "";
    public string DeptCode { get; set; } = "";
    public string SalNo { get; set; } = "";
    public string BilType { get; set; } = "";
    public string BilId { get; set; } = "";
    public string BilNo { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdMark { get; set; } = "";
    public decimal QTY { get; set; }
    public string Unit { get; set; } = "";
    public string WH { get; set; } = "";
    public string CHUW { get; set; } = "";
}
