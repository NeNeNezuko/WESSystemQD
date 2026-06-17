using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BatchValidityOperationController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<BatchValidityOperationController> _logger;

    public BatchValidityOperationController(WarehouseDbContext context, ILogger<BatchValidityOperationController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询批号有效期修改历史列表（LEFT JOIN MY_WH 取仓库名称、PRDT 取货品名称、INDX 取中类名称）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<BatchValidityDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? wh,
        [FromQuery] string? prdNoFrom,
        [FromQuery] string? prdNoTo,
        [FromQuery] string? batNo,
        [FromQuery] string? idxNo,
        [FromQuery] bool emptyValidDateOnly)
    {
        try
        {
            var query = from v in _context.ValidddUpdHises
                        join w in _context.MyWhs on v.WH equals w.WH into wj
                        from w in wj.DefaultIfEmpty()
                        join p in _context.Prdts on v.PRD_NO equals p.PRD_NO into pj
                        from p in pj.DefaultIfEmpty()
                        join i in _context.Indxes on p.IDX1 equals i.IDX_NO into ij
                        from i in ij.DefaultIfEmpty()
                        select new { V = v, W = w, P = p, I = i };

            // 修改日期范围筛选
            if (dateFrom.HasValue)
                query = query.Where(x => x.V.UPD_DATE >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.V.UPD_DATE <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 仓库代号模糊匹配
            if (!string.IsNullOrWhiteSpace(wh))
                query = query.Where(x => x.V.WH != null && x.V.WH.Contains(wh));

            // 货品代号范围筛选
            if (!string.IsNullOrWhiteSpace(prdNoFrom))
                query = query.Where(x => x.V.PRD_NO != null && x.V.PRD_NO.CompareTo(prdNoFrom) >= 0);
            if (!string.IsNullOrWhiteSpace(prdNoTo))
                query = query.Where(x => x.V.PRD_NO != null && x.V.PRD_NO.CompareTo(prdNoTo) <= 0);

            // 批号模糊匹配
            if (!string.IsNullOrWhiteSpace(batNo))
                query = query.Where(x => x.V.BAT_NO != null && x.V.BAT_NO.Contains(batNo));

            // 中类代号筛选（通过 PRDT.IDX1）
            if (!string.IsNullOrWhiteSpace(idxNo))
                query = query.Where(x => x.P != null && x.P.IDX1 != null && x.P.IDX1.Contains(idxNo));

            // 仅显示有效日期为空的记录
            if (emptyValidDateOnly)
                query = query.Where(x => x.V.VALID_DD_CUR == null);

            // 按项次排序
            query = query.OrderBy(x => x.V.HIS_NO);

            var rawList = await query.ToListAsync();

            var list = rawList.Select(x => new BatchValidityDto
            {
                HisNo = x.V.HIS_NO ?? 0,
                Wh = x.V.WH ?? "",
                WhName = x.W?.NAME ?? "",
                PrdNo = x.V.PRD_NO ?? "",
                PrdName = x.P?.NAME ?? "",
                BatNo = x.V.BAT_NO ?? "",
                ValidDDCur = x.V.VALID_DD_CUR,
                ValidDDOrg = x.V.VALID_DD_ORG,
                LastOutDate = x.V.LAST_OUT_DD,
                LastInDate = x.V.LAST_IN_DD,
                LastInspectDate = x.V.LAST_INSPECT_DD,
                ProduceDate = x.V.PRODUCE_DD,
                IdxNo = x.P?.IDX1 ?? "",
                IdxName = x.I?.NAME ?? "",
                Qty = x.V.QTY ?? 0
            }).ToList();

            return Ok(new ApiResult<List<BatchValidityDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询批号有效期修改历史时发生错误");
            return StatusCode(500, new ApiResult<List<BatchValidityDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>批号有效期修改历史列表行数据（对应前端表格）</summary>
public class BatchValidityDto
{
    public int HisNo { get; set; }
    public string Wh { get; set; } = "";
    public string WhName { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string BatNo { get; set; } = "";
    public DateTime? ValidDDCur { get; set; }
    public DateTime? ValidDDOrg { get; set; }
    public DateTime? LastOutDate { get; set; }
    public DateTime? LastInDate { get; set; }
    public DateTime? LastInspectDate { get; set; }
    public DateTime? ProduceDate { get; set; }
    public string IdxNo { get; set; } = "";
    public string IdxName { get; set; } = "";
    public decimal Qty { get; set; }
}
