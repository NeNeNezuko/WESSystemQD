using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OutboundNoticeDispatchController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<OutboundNoticeDispatchController> _logger;

    public OutboundNoticeDispatchController(WarehouseDbContext context, ILogger<OutboundNoticeDispatchController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询出库通知单派工列表（以表身TF_CKTZ为主，LEFT JOIN表头MF_CKTZ）
    /// 返回主表数据 + 明细数据
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<DispatchSearchResult>>> Search(
        [FromQuery] string? sourceTypes,
        [FromQuery] DateTime? estDateFrom,
        [FromQuery] DateTime? estDateTo,
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? documentNumber,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? businessType,
        [FromQuery] string? erpOrderNo,
        [FromQuery] string? bizOrderNo,
        [FromQuery] string? dispatchStatus,
        [FromQuery] string? pickCloseStatus)
    {
        try
        {
            var query = from t in _context.TfCktzs
                        join m in _context.MfCktzs on t.TZ_NO equals m.TZ_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

            // 来源单据类别筛选（根据BIL_TYPE区分：出库通知单/调拨通知单）
            // TODO: 具体的类别映射规则待确认，目前先按 BIL_TYPE 包含关系处理
            if (!string.IsNullOrWhiteSpace(sourceTypes))
            {
                var types = sourceTypes.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                if (types.Length > 0)
                {
                    // 先不做过滤，等确认映射规则后再补充
                    // query = query.Where(x => x.M != null && types.Contains(x.M.BIL_TYPE));
                }
            }

            // 预计出货日期范围筛选（使用表头的EST_DD）
            if (estDateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.EST_DD >= estDateFrom.Value);
            if (estDateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.EST_DD <= estDateTo.Value.AddDays(1).AddSeconds(-1));

            // 单据日期范围筛选（使用表头的TZ_DD）
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.TZ_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.TZ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 单据号码模糊匹配
            if (!string.IsNullOrWhiteSpace(documentNumber))
                query = query.Where(x => x.T.TZ_NO.Contains(documentNumber));

            // 仓库代号模糊匹配
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.M != null && (x.M.WH != null && x.M.WH.Contains(warehouseCode)));

            // 业务类型筛选
            if (!string.IsNullOrWhiteSpace(businessType))
                query = query.Where(x => x.M != null && (x.M.BIL_TYPE != null && x.M.BIL_TYPE.Contains(businessType)));

            // ERP申请单号模糊匹配
            if (!string.IsNullOrWhiteSpace(erpOrderNo))
            {
                // TODO: 确认ERP申请单号对应哪个字段，暂用REM备注字段匹配
                query = query.Where(x => x.M != null && (x.M.REM != null && x.M.REM.Contains(erpOrderNo)));
            }

            // 业务单号模糊匹配
            if (!string.IsNullOrWhiteSpace(bizOrderNo))
            {
                // TODO: 确认业务单号对应哪个字段
                // query = query.Where(...);
            }

            // 派工状态筛选（TODO: 确认派工状态存储在哪个字段）
            if (!string.IsNullOrWhiteSpace(dispatchStatus) && dispatchStatus != "全部")
            {
                // TODO: 待确认派工状态字段后补充过滤逻辑
                // var isDispatched = dispatchStatus == "已派工";
                // query = query.Where(...);
            }

            // 拣货结案标记筛选（TODO: 确认拣货结案标记存储在哪个字段）
            if (!string.IsNullOrWhiteSpace(pickCloseStatus) && pickCloseStatus != "全部")
            {
                // TODO: 待确认拣货结案标记字段后补充过滤逻辑
            }

            // 按单据号+项次排序
            query = query.OrderBy(x => x.T.TZ_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 投影为主表列表（按单据号去重，取第一条作为主表行）
            var mainList = rawList
                .GroupBy(x => x.T.TZ_NO)
                .Select(g => g.First())
                .Select(x => new DispatchMainDto
                {
                    ItemNo = x.T.ITM,
                    DocumentDate = x.M?.TZ_DD ?? DateTime.MinValue,
                    DocumentNumber = x.T.TZ_NO,
                    ErpOrderNo = "",       // TODO: 数据来源待确认
                    EstDeliveryDate = x.M?.EST_DD,
                    CustomerCode = x.M?.CUS_NO ?? "",
                    CustomerName = x.M?.CUS_NAME ?? "",
                    HandlerName = "",      // TODO: 经办人名称待确认来源（SAL_NO转名称？）
                    BillCategory = "",     // TODO: 单据类别待确认来源（BIL_TYPE映射？）
                    Priority = 0,          // TODO: 优先级数据来源待确认
                    IsClosed = (x.M?.CLS_ID ?? "N") == "Y",
                    DispatchStatus = "",   // TODO: 派工状态待确认来源
                    Remark = x.M?.REM ?? "",
                    PickerName = ""        // TODO: 拣货员名称待确认来源
                })
                .ToList();

            // 投影为明细列表
            var detailList = rawList.Select(x => new DispatchDetailDto
            {
                DocumentNumber = x.T.TZ_NO,
                ItemNo = x.T.ITM,
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                PrdSpec = x.T.PRD_MARK ?? "",
                WarehouseCode = x.T.WH ?? "",
                WarehouseName = "",   // TODO: 仓库名称需JOIN MY_WH表获取
                Unit = x.T.UNIT ?? "",
                Qty = x.T.QTY ?? 0,
                PlannedDeductionQty = 0,  // TODO: 已转计划出库量，数据来源待确认（TF_RKTZ_RCV？）
                PickedQty = 0,            // TODO: 已拣货量，数据来源待确认
                ReturnedQty = 0,          // TODO: 已退数量，数据来源待确认
                OutboundQty = 0,          // TODO: 已出库量，数据来源待确认
                ErpApplyItemNo = 0,       // TODO: ERP申请单项次，数据来源待确认
                BizOrderNo = "",          // TODO: 业务单号，数据来源待确认
                Summary = x.T.REM ?? ""   // 摘要取备注字段
            }).ToList();

            return Ok(new ApiResult<DispatchSearchResult>
            {
                Success = true,
                Data = new DispatchSearchResult
                {
                    MainList = mainList,
                    DetailList = detailList
                },
                Total = mainList.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询出库通知单派工时发生错误");
            return StatusCode(500, new ApiResult<DispatchSearchResult>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>出库通知单派工 — 主表格行数据</summary>
public class DispatchMainDto
{
    public int ItemNo { get; set; }
    public DateTime DocumentDate { get; set; }
    public string DocumentNumber { get; set; } = "";
    public string ErpOrderNo { get; set; } = "";
    public DateTime? EstDeliveryDate { get; set; }
    public string CustomerCode { get; set; } = "";
    public string CustomerName { get; set; } = "";
    public string HandlerName { get; set; } = "";
    public string BillCategory { get; set; } = "";
    public int Priority { get; set; }
    public bool IsClosed { get; set; }
    public string DispatchStatus { get; set; } = "";
    public string Remark { get; set; } = "";
    public string PickerName { get; set; } = "";          // 拣货员名称
}

/// <summary>出库通知单派工 — 明细表格行数据</summary>
public class DispatchDetailDto
{
    public string DocumentNumber { get; set; } = "";
    public int ItemNo { get; set; }
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdSpec { get; set; } = "";
    public string WarehouseCode { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string Unit { get; set; } = "";
    public decimal Qty { get; set; }
    public decimal PlannedDeductionQty { get; set; }
    public decimal PickedQty { get; set; }
    public decimal ReturnedQty { get; set; }               // 已退数量
    public decimal OutboundQty { get; set; }                // 已出库量
    public int ErpApplyItemNo { get; set; }                 // ERP申请单项次
    public string BizOrderNo { get; set; } = "";            // 业务单号
    public string Summary { get; set; } = "";               // 摘要
}

/// <summary>出库通知单派工 — 查询结果（含主表+明细）</summary>
public class DispatchSearchResult
{
    public List<DispatchMainDto> MainList { get; set; } = new();
    public List<DispatchDetailDto> DetailList { get; set; } = new();
}
