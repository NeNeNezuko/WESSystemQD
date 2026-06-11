using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TransferNoticeController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<TransferNoticeController> _logger;

    public TransferNoticeController(WarehouseDbContext context, ILogger<TransferNoticeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询调拨通知单列表（以表身TF_ICTZ为主，LEFT JOIN表头MF_ICTZ）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<TransferNoticeDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] DateTime? estDateFrom,
        [FromQuery] DateTime? estDateTo,
        [FromQuery] string? documentNumber,
        [FromQuery] string? whOut,
        [FromQuery] string? whIn,
        [FromQuery] string? areaSh,
        [FromQuery] string? clsIdBc,
        [FromQuery] string? clsIdCk,
        [FromQuery] string? typeId)
    {
        try
        {
            var query = from t in _context.TfIctzs
                        join m in _context.MfIctzs on t.TZ_NO equals m.TZ_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 单据日期范围筛选（使用表头的TZ_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.TZ_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.TZ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 稽催日期范围筛选（使用表头的EST_DD）
            if (estDateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.EST_DD >= estDateFrom.Value);
            if (estDateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.EST_DD <= estDateTo.Value.AddDays(1).AddSeconds(-1));

            // 单据号码模糊匹配
            if (!string.IsNullOrWhiteSpace(documentNumber))
                query = query.Where(x => x.M != null && x.M.TZ_NO != null && x.M.TZ_NO.Contains(documentNumber));

            // 出库仓库代号模糊匹配
            if (!string.IsNullOrWhiteSpace(whOut))
                query = query.Where(x => x.M != null && x.M.WH1 != null && x.M.WH1.Contains(whOut));

            // 入库仓库代号模糊匹配
            if (!string.IsNullOrWhiteSpace(whIn))
                query = query.Where(x => x.M != null && x.M.WH2 != null && x.M.WH2.Contains(whIn));

            // 收货点模糊匹配
            if (!string.IsNullOrWhiteSpace(areaSh))
                query = query.Where(x => x.M != null && x.M.AREA_SH != null && x.M.AREA_SH.Contains(areaSh));

            // 洽谈结案筛选
            if (!string.IsNullOrWhiteSpace(clsIdBc) && clsIdBc != "全部")
            {
                var bcValue = clsIdBc == "是" ? "Y" : "N";
                query = query.Where(x => x.M != null && x.M.CLS_ID_BC == bcValue);
            }

            // 转采结案筛选
            if (!string.IsNullOrWhiteSpace(clsIdCk) && clsIdCk != "全部")
            {
                var ckValue = clsIdCk == "是" ? "Y" : "N";
                query = query.Where(x => x.M != null && x.M.CLS_ID_CK == ckValue);
            }

            // 业务类别筛选
            if (!string.IsNullOrWhiteSpace(typeId))
                query = query.Where(x => x.M != null && x.M.TYPE_ID != null && x.M.TYPE_ID.Contains(typeId));

            // 按单据号+项次排序
            query = query.OrderBy(x => x.M!.TZ_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new TransferNoticeDto
            {
                ItemNo = x.T.ITM,
                DocumentDate = x.M?.TZ_DD ?? DateTime.MinValue,
                DocumentNumber = x.M?.TZ_NO ?? "",
                WhOut = x.M?.WH1 ?? "",
                WhOutName = "",
                WhIn = x.M?.WH2 ?? "",
                WhInName = "",
                SalNo = x.M?.SAL_NO ?? "",
                SalName = "",
                EstDate = x.M?.EST_DD,
                UpDate = x.M?.UP_DD,
                AreaSh = x.M?.AREA_SH ?? "",
                Rem = x.M?.REM ?? "",
                StatusPg = x.M?.STATUS_PG ?? "",
                ClsIdBc = x.M?.CLS_ID_BC ?? "",
                ClsIdCk = x.M?.CLS_ID_CK ?? "",
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                Qty = x.T.QTY ?? 0,
                Unit = x.T.UNIT ?? ""
            }).ToList();

            return Ok(new ApiResult<List<TransferNoticeDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询调拨通知单时发生错误");
            return StatusCode(500, new ApiResult<List<TransferNoticeDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>调拨通知单列表行数据（对应前端表格）</summary>
public class TransferNoticeDto
{
    public int ItemNo { get; set; }
    public DateTime DocumentDate { get; set; }
    public string DocumentNumber { get; set; } = "";
    public string WhOut { get; set; } = "";
    public string WhOutName { get; set; } = "";
    public string WhIn { get; set; } = "";
    public string WhInName { get; set; } = "";
    public string SalNo { get; set; } = "";
    public string SalName { get; set; } = "";
    public DateTime? EstDate { get; set; }
    public DateTime? UpDate { get; set; }
    public string AreaSh { get; set; } = "";
    public string Rem { get; set; } = "";
    public string StatusPg { get; set; } = "";
    public string ClsIdBc { get; set; } = "";
    public string ClsIdCk { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public decimal Qty { get; set; }
    public string Unit { get; set; } = "";
}
