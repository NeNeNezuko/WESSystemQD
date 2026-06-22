using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;
using WmsPlus.Api.Utils;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OutboundNoticeController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<OutboundNoticeController> _logger;

    public OutboundNoticeController(WarehouseDbContext context, ILogger<OutboundNoticeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询出库通知单列表（以表身TF_CKTZ为主，LEFT JOIN表头MF_CKTZ）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<OutboundNoticeDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? documentNumber,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? customerCode,
        [FromQuery] string? closeStatus,
        [FromQuery] string? businessType,
        [FromQuery] DateTime? deliveryDateFrom,
        [FromQuery] DateTime? deliveryDateTo,
        [FromQuery] string? receivePoint,
        [FromQuery] string? closeCaseType)
    {
        try
        {
            var query = from t in _context.TfCktzs
                        join m in _context.MfCktzs on t.TZ_NO equals m.TZ_NO into mj
                        from m in mj.DefaultIfEmpty()
                        select new { T = t, M = m };

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

            // 客户代号模糊匹配
            if (!string.IsNullOrWhiteSpace(customerCode))
                query = query.Where(x => x.M != null && (x.M.CUS_NO != null && x.M.CUS_NO.Contains(customerCode)));

            // 结案状态筛选
            if (!string.IsNullOrWhiteSpace(closeStatus) && closeStatus != "全部")
            {
                var isClosed = closeStatus == "已结案" ? "Y" : "N";
                query = query.Where(x => x.M != null && x.M.CLS_ID == isClosed);
            }

            // 业务类型筛选
            if (!string.IsNullOrWhiteSpace(businessType))
                query = query.Where(x => x.M != null && (x.M.BIL_TYPE != null && x.M.BIL_TYPE.Contains(businessType)));

            // 预计到货时间范围筛选（使用表头的EST_DD）
            if (deliveryDateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.EST_DD >= deliveryDateFrom.Value);
            if (deliveryDateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.EST_DD <= deliveryDateTo.Value.AddDays(1).AddSeconds(-1));

            // 收货点筛选（TODO: 确认收货点对应字段）
            if (!string.IsNullOrWhiteSpace(receivePoint))
            {
                // TODO: 待确认收货点字段后补充过滤逻辑
            }

            // 波次/拣货/出库结案筛选（TODO: 确认结案类型对应字段）
            if (!string.IsNullOrWhiteSpace(closeCaseType) && closeCaseType != "全部")
            {
                // TODO: 待确认波次/拣货/出库结案字段后补充过滤逻辑
            }

            // 按单据号+项次排序
            query = query.OrderBy(x => x.T.TZ_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new OutboundNoticeDto
            {
                ItemNo = x.T.ITM,
                DocumentDate = x.M?.TZ_DD ?? DateTime.MinValue,
                DocumentNumber = x.T.TZ_NO,
                WarehouseCode = x.M?.WH ?? "",
                WarehouseName = "",
                CustomerCode = x.M?.CUS_NO ?? "",
                CustomerName = x.M?.CUS_NAME ?? "",
                IsClosed = (x.M?.CLS_ID ?? "N") == "Y",
                // 表身明细字段
                PrdNo = x.T.PRD_NO ?? "",
                PrdName = x.T.PRD_NAME ?? "",
                PrdSpec = x.T.PRD_MARK ?? "",
                Qty = x.T.QTY ?? 0,
                Unit = x.T.UNIT ?? "",
                // 扩展字段
                ErpApplyId = "",       // TODO: ERP申请单ID数据来源待确认
                DispatchStatus = "",   // TODO: 派工状态数据来源待确认
                PickerName = ""        // TODO: 拣货员名称数据来源待确认
            }).ToList();

            return Ok(new ApiResult<List<OutboundNoticeDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询出库通知单时发生错误");
            return StatusCode(500, new ApiResult<List<OutboundNoticeDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据单据号获取出库通知单详情（含表身明细）
    /// </summary>
    [HttpGet("{tzNo}")]
    public async Task<ActionResult<ApiResult<OutboundNoticeDetailDto>>> GetDetail(string tzNo)
    {
        try
        {
            var header = await _context.MfCktzs.FindAsync(tzNo);
            if (header == null)
                return NotFound(new ApiResult<OutboundNoticeDetailDto>
                {
                    Success = false,
                    Message = "未找到该出库通知单"
                });

            var details = await _context.TfCktzs
                .Where(t => t.TZ_NO == tzNo)
                .OrderBy(t => t.ITM)
                .Select(t => new TfCktzDto
                {
                    ItemNo = t.ITM,
                    PrdNo = t.PRD_NO ?? "",
                    PrdName = t.PRD_NAME ?? "",
                    PrdSpec = t.PRD_MARK ?? "",
                    Qty = t.QTY ?? 0,
                    Unit = t.UNIT ?? "",
                    LotNo = t.BAT_NO ?? "",
                    Rem = t.REM ?? ""
                })
                .ToListAsync();

            return Ok(new ApiResult<OutboundNoticeDetailDto>
            {
                Success = true,
                Data = new OutboundNoticeDetailDto
                {
                    Header = MySqlDateHelper.SafeConvert(header),
                    Details = details
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取出库通知单详情时发生错误");
            return StatusCode(500, new ApiResult<OutboundNoticeDetailDto>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>出库通知单列表行数据（对应前端表格）</summary>
public class OutboundNoticeDto
{
    public int ItemNo { get; set; }
    public DateTime DocumentDate { get; set; }
    public string DocumentNumber { get; set; } = "";
    public string WarehouseCode { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string CustomerCode { get; set; } = "";
    public string CustomerName { get; set; } = "";
    public bool IsClosed { get; set; }
    // 表身明细字段
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdSpec { get; set; } = "";
    public decimal Qty { get; set; }
    public string Unit { get; set; } = "";

    // 出库任务分配作业扩展字段
    public int PriorityLevel { get; set; }
    public string BizTypeName { get; set; } = "";
    public string NoticeNo { get; set; } = "";
    public string ApplyNo { get; set; } = "";
    public DateTime? ExpectedOutDate { get; set; }
    public string OperatorName { get; set; } = "";

    // 新增扩展字段
    public string ErpApplyId { get; set; } = "";          // ERP申请单ID
    public string DispatchStatus { get; set; } = "";      // 派工状态
    public string PickerName { get; set; } = "";          // 拣货员名称
}

/// <summary>出库通知单详情（含表头+表身）</summary>
public class OutboundNoticeDetailDto
{
    public MfCktz Header { get; set; } = new();
    public List<TfCktzDto> Details { get; set; } = new();
}

/// <summary>表身明细DTO</summary>
public class TfCktzDto
{
    public int ItemNo { get; set; }
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string PrdSpec { get; set; } = "";
    public decimal Qty { get; set; }
    public string Unit { get; set; } = "";
    public string LotNo { get; set; } = "";
    public string Rem { get; set; } = "";
}
