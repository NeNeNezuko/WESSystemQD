using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DocumentConfirmController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<DocumentConfirmController> _logger;

    public DocumentConfirmController(WarehouseDbContext context, ILogger<DocumentConfirmController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询单据确认作业列表（根据单据别查询不同表头，按确认状态过滤）
    /// docType: PD(盘点单), YN(盘盈单), KU(盘亏单)
    /// confirmStatus: pending(待确认), confirmed(已确认)
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<DocumentConfirmDto>>>> Search(
        [FromQuery] string docType = "PD",
        [FromQuery] DateTime? dateFrom = null,
        [FromQuery] DateTime? dateTo = null,
        [FromQuery] string? warehouseCode = null,
        [FromQuery] string? prdNoFrom = null,
        [FromQuery] string? prdNoTo = null,
        [FromQuery] string confirmStatus = "pending")
    {
        try
        {
            var isConfirmed = confirmStatus == "confirmed";

            List<DocumentConfirmDto> list = docType.ToUpper() switch
            {
                "YN" => await QueryYnList(isConfirmed, dateFrom, dateTo, warehouseCode, prdNoFrom, prdNoTo),
                "KU" => await QueryKuList(isConfirmed, dateFrom, dateTo, warehouseCode, prdNoFrom, prdNoTo),
                _   => await QueryPdList(isConfirmed, dateFrom, dateTo, warehouseCode, prdNoFrom, prdNoTo)
            };

            return Ok(new ApiResult<List<DocumentConfirmDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询单据确认作业时发生错误");
            return StatusCode(500, new ApiResult<List<DocumentConfirmDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    #region 盘点单(MF_PD) 查询

    private async Task<List<DocumentConfirmDto>> QueryPdList(
        bool isConfirmed, DateTime? dateFrom, DateTime? dateTo,
        string? warehouseCode, string? prdNoFrom, string? prdNoTo)
    {
        var query = _context.MfPds.AsQueryable();

        // 确认状态筛选：待确认(CFM_SW!='T') / 已确认(CFM_SW=='T')
        query = isConfirmed
            ? query.Where(x => x.CFM_SW == "T")
            : query.Where(x => x.CFM_SW == null || x.CFM_SW != "T");

        if (dateFrom.HasValue)
            query = query.Where(x => x.PD_DD >= dateFrom.Value);
        if (dateTo.HasValue)
            query = query.Where(x => x.PD_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
        if (!string.IsNullOrWhiteSpace(warehouseCode))
            query = query.Where(x => x.WH != null && x.WH.Contains(warehouseCode));

        var rawList = await query.OrderBy(x => x.PD_DD).ThenBy(x => x.PD_NO).ToListAsync();

        // 如果有货品范围条件，需要关联表身过滤
        if (!string.IsNullOrWhiteSpace(prdNoFrom) || !string.IsNullOrWhiteSpace(prdNoTo))
        {
            var pdNos = await ApplyProductRangeFilter<PdProdKey>(prdNoFrom, prdNoTo,
                _context.TfPds.Select(t => new PdProdKey { DocNo = t.PD_NO, PrdNo = t.PRD_NO }));
            rawList = rawList.Where(x => pdNos.Contains(x.PD_NO)).ToList();
        }

        return rawList.Select(x => new DocumentConfirmDto
        {
            DocType = "PD",
            DocTypeName = "盘点单",
            DocumentDate = x.PD_DD ?? DateTime.MinValue,
            DocumentNumber = x.PD_NO,
            WarehouseCode = x.WH ?? "",
            WarehouseName = "",
            CreatorCode = x.USR ?? "",
            CreatorName = "",
            CreateTime = x.SYS_DATE ?? DateTime.MinValue,
            ConfirmStatus = (x.CFM_SW == "T") ? "已确认" : "待确认",
            ConfirmUser = x.CFM_USR ?? "",
            ConfirmTime = x.CFM_DATE
        }).ToList();
    }

    #endregion

    #region 盘盈单(MF_YN) 查询

    private async Task<List<DocumentConfirmDto>> QueryYnList(
        bool isConfirmed, DateTime? dateFrom, DateTime? dateTo,
        string? warehouseCode, string? prdNoFrom, string? prdNoTo)
    {
        var query = _context.MfYns.AsQueryable();

        query = isConfirmed
            ? query.Where(x => x.CFM_SW == "T")
            : query.Where(x => x.CFM_SW == null || x.CFM_SW != "T");

        if (dateFrom.HasValue)
            query = query.Where(x => x.YN_DD >= dateFrom.Value);
        if (dateTo.HasValue)
            query = query.Where(x => x.YN_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
        if (!string.IsNullOrWhiteSpace(warehouseCode))
            query = query.Where(x => x.WH != null && x.WH.Contains(warehouseCode));

        var rawList = await query.OrderBy(x => x.YN_DD).ThenBy(x => x.YN_NO).ToListAsync();

        if (!string.IsNullOrWhiteSpace(prdNoFrom) || !string.IsNullOrWhiteSpace(prdNoTo))
        {
            var ynNos = await ApplyProductRangeFilter<YnProdKey>(prdNoFrom, prdNoTo,
                _context.TfYns.Select(t => new YnProdKey { DocNo = t.YN_NO, PrdNo = t.PRD_NO }));
            rawList = rawList.Where(x => ynNos.Contains(x.YN_NO)).ToList();
        }

        return rawList.Select(x => new DocumentConfirmDto
        {
            DocType = "YN",
            DocTypeName = "盘盈单",
            DocumentDate = x.YN_DD ?? DateTime.MinValue,
            DocumentNumber = x.YN_NO,
            WarehouseCode = x.WH ?? "",
            WarehouseName = "",
            CreatorCode = x.USR ?? "",
            CreatorName = "",
            CreateTime = x.SYS_DATE ?? DateTime.MinValue,
            ConfirmStatus = (x.CFM_SW == "T") ? "已确认" : "待确认",
            ConfirmUser = x.CFM_USR ?? "",
            ConfirmTime = x.CFM_DATE
        }).ToList();
    }

    #endregion

    #region 盘亏单(MF_KU) 查询

    private async Task<List<DocumentConfirmDto>> QueryKuList(
        bool isConfirmed, DateTime? dateFrom, DateTime? dateTo,
        string? warehouseCode, string? prdNoFrom, string? prdNoTo)
    {
        var query = _context.MfKus.AsQueryable();

        query = isConfirmed
            ? query.Where(x => x.CFM_SW == "T")
            : query.Where(x => x.CFM_SW == null || x.CFM_SW != "T");

        if (dateFrom.HasValue)
            query = query.Where(x => x.KU_DD >= dateFrom.Value);
        if (dateTo.HasValue)
            query = query.Where(x => x.KU_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
        if (!string.IsNullOrWhiteSpace(warehouseCode))
            query = query.Where(x => x.WH != null && x.WH.Contains(warehouseCode));

        var rawList = await query.OrderBy(x => x.KU_DD).ThenBy(x => x.KU_NO).ToListAsync();

        if (!string.IsNullOrWhiteSpace(prdNoFrom) || !string.IsNullOrWhiteSpace(prdNoTo))
        {
            var kuNos = await ApplyProductRangeFilter<KuProdKey>(prdNoFrom, prdNoTo,
                _context.TfKus.Select(t => new KuProdKey { DocNo = t.KU_NO, PrdNo = t.PRD_NO }));
            rawList = rawList.Where(x => kuNos.Contains(x.KU_NO)).ToList();
        }

        return rawList.Select(x => new DocumentConfirmDto
        {
            DocType = "KU",
            DocTypeName = "盘亏单",
            DocumentDate = x.KU_DD ?? DateTime.MinValue,
            DocumentNumber = x.KU_NO,
            WarehouseCode = x.WH ?? "",
            WarehouseName = "",
            CreatorCode = x.USR ?? "",
            CreatorName = "",
            CreateTime = x.SYS_DATE ?? DateTime.MinValue,
            ConfirmStatus = (x.CFM_SW == "T") ? "已确认" : "待确认",
            ConfirmUser = x.CFM_USR ?? "",
            ConfirmTime = x.CFM_DATE
        }).ToList();
    }

    #endregion

    #region 通用方法

    /// <summary>
    /// 根据货品代号范围过滤出符合条件的单据号集合
    /// </summary>
    private async Task<List<string>> ApplyProductRangeFilter<T>(
        string? prdNoFrom, string? prdNoTo, IQueryable<T> tableBodyQuery) where T : IProdKey
    {
        var filtered = tableBodyQuery.AsQueryable();
        if (!string.IsNullOrWhiteSpace(prdNoFrom))
            filtered = filtered.Where(x => x.PrdNo != null && x.PrdNo.CompareTo(prdNoFrom) >= 0);
        if (!string.IsNullOrWhiteSpace(prdNoTo))
            filtered = filtered.Where(x => x.PrdNo != null && x.PrdNo.CompareTo(prdNoTo) <= 0);

        return await filtered.Select(x => x.DocNo).Distinct().ToListAsync();
    }

    #endregion
}

// ====== DTO 定义 ======

/// <summary>单据确认作业列表行数据</summary>
public class DocumentConfirmDto
{
    /// <summary>单据别代码（PD/YN/KU）</summary>
    public string DocType { get; set; } = "";
    /// <summary>单据别名称（盘点单/盘盈单/盘亏单）</summary>
    public string DocTypeName { get; set; } = "";
    /// <summary>单据日期</summary>
    public DateTime DocumentDate { get; set; }
    /// <summary>单据号码</summary>
    public string DocumentNumber { get; set; } = "";
    /// <summary>仓库代号</summary>
    public string WarehouseCode { get; set; } = "";
    /// <summary>仓库名称</summary>
    public string WarehouseName { get; set; } = "";
    /// <summary>制单人代号</summary>
    public string CreatorCode { get; set; } = "";
    /// <summary>制单人名称</summary>
    public string CreatorName { get; set; } = "";
    /// <summary>制单时间</summary>
    public DateTime CreateTime { get; set; }
    /// <summary>确认状态（待确认/已确认）</summary>
    public string ConfirmStatus { get; set; } = "待确认";
    /// <summary>确认人</summary>
    public string ConfirmUser { get; set; } = "";
    /// <summary>确认时间</summary>
    public DateTime? ConfirmTime { get; set; }
}

// ====== 货品范围过滤辅助类型 ======

/// <summary>表身货品键接口</summary>
public interface IProdKey
{
    string DocNo { get; set; }
    string? PrdNo { get; set; }
}

/// <summary>盘点单表身货品键</summary>
public class PdProdKey : IProdKey
{
    public string DocNo { get; set; } = "";
    public string? PrdNo { get; set; }
}

/// <summary>盘盈单表身货品键</summary>
public class YnProdKey : IProdKey
{
    public string DocNo { get; set; } = "";
    public string? PrdNo { get; set; }
}

/// <summary>盘亏单表身货品键</summary>
public class KuProdKey : IProdKey
{
    public string DocNo { get; set; } = "";
    public string? PrdNo { get; set; }
}
