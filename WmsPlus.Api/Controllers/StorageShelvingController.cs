using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StorageShelvingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<StorageShelvingController> _logger;

    public StorageShelvingController(WarehouseDbContext context, ILogger<StorageShelvingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询储位上架单列表（以表身TF_CWSJ为主，LEFT JOIN表头MF_CWSJ）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<StorageShelvingDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? documentNumber,
        [FromQuery] string? deptCode,
        [FromQuery] string? salNo,
        [FromQuery] string? bilType)
    {
        try
        {
            var query = from t in _context.TfCwsjs
                        join m in _context.MfCwsjs on t.SJ_NO equals m.SJ_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 单据日期范围筛选（使用表头的SJ_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.SJ_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.SJ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 单据号码模糊匹配（表身或表头）
            if (!string.IsNullOrWhiteSpace(documentNumber))
                query = query.Where(x => (x.T.SJ_NO != null && x.T.SJ_NO.Contains(documentNumber)) ||
                                          (x.M != null && x.M.SJ_NO != null && x.M.SJ_NO.Contains(documentNumber)));

            // 部门代号筛选
            if (!string.IsNullOrWhiteSpace(deptCode))
                query = query.Where(x => x.M != null && (x.M.DEP != null && x.M.DEP.Contains(deptCode)));

            // 经办人筛选
            if (!string.IsNullOrWhiteSpace(salNo))
                query = query.Where(x => x.M != null && (x.M.SAL_NO != null && x.M.SAL_NO.Contains(salNo)));

            // 单据类别筛选
            if (!string.IsNullOrWhiteSpace(bilType))
                query = query.Where(x => x.M != null && (x.M.BIL_TYPE != null && x.M.BIL_TYPE.Contains(bilType)));

            // 按单据号+项次排序
            query = query.OrderBy(x => x.M!.SJ_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new StorageShelvingDto
            {
                ItemNo = x.T.ITM,
                DocumentDate = x.M?.SJ_DD ?? DateTime.MinValue,
                DocumentNumber = x.M?.SJ_NO ?? x.T.SJ_NO,
                DeptCode = x.M?.DEP ?? "",
                SalNo = x.M?.SAL_NO ?? "",
                BilType = x.M?.BIL_TYPE ?? "",
                BilId = x.M?.BIL_ID ?? "",
                BilNo = x.M?.BIL_NO ?? "",
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                PrdMark = x.T.PRD_MARK ?? "",
                QTY = x.T.QTY ?? 0,
                Unit = x.T.UNIT ?? "",
                WH = x.T.WH1 ?? "",
                CHUW = x.T.CHUW1 ?? ""
            }).ToList();

            return Ok(new ApiResult<List<StorageShelvingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询储位上架单时发生错误");
            return StatusCode(500, new ApiResult<List<StorageShelvingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>储位上架单列表行数据（对应前端表格）</summary>
public class StorageShelvingDto
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
