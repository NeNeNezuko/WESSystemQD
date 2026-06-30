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

    /// <summary>
    /// 生成下一个出库通知单单据号码（UP + yyyyMMdd + 6位序号）
    /// </summary>
    [HttpGet("next-no")]
    public async Task<ActionResult<ApiResult<string>>> GetNextDocumentNo()
    {
        try
        {
            var prefix = $"UP{DateTime.Now:yyyyMMdd}";
            var todayMax = await _context.MfCktzs
                .Where(m => m.TZ_NO.StartsWith(prefix))
                .Select(m => m.TZ_NO)
                .ToListAsync();

            var maxSeq = 0;
            foreach (var no in todayMax)
            {
                if (no.Length >= 14 && int.TryParse(no.Substring(8), out var seq))
                {
                    if (seq > maxSeq) maxSeq = seq;
                }
            }
            var nextNo = $"{prefix}{(maxSeq + 1):D6}";
            return Ok(new ApiResult<string> { Success = true, Data = nextNo });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "生成出库通知单单号时发生错误");
            return StatusCode(500, new ApiResult<string> { Success = false, Message = $"服务器内部错误: {ex.Message}" });
        }
    }

    /// <summary>
    /// 保存出库通知单（表头+表身事务保存）
    /// </summary>
    [HttpPost("save")]
    public async Task<ActionResult<ApiResult<string>>> Save([FromBody] OutboundNoticeSaveRequest request)
    {
        if (request?.Header == null)
            return BadRequest(new ApiResult<string> { Success = false, Message = "请求数据无效" });

        try
        {
            var existing = await _context.MfCktzs.FindAsync(request.Header.TZ_NO);
            if (existing != null)
                return Conflict(new ApiResult<string> { Success = false, Message = $"单据号码 {request.Header.TZ_NO} 已存在，请勿重复保存" });

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var header = new MfCktz
                {
                    TZ_NO = request.Header.TZ_NO,
                    TZ_DD = request.Header.TZ_DD,
                    DEP = request.Header.DEP,
                    BIL_TYPE = request.Header.BIL_TYPE,
                    CUS_NO = request.Header.CUS_NO,
                    CUS_NAME = request.Header.CUS_NAME,
                    WH = request.Header.WH,
                    APPLY_NO = request.Header.APPLY_NO,
                    EST_DD = request.Header.EST_DD,
                    EXPECT_DD = request.Header.EXPECT_DD,
                    PRIORITY = request.Header.PRIORITY,
                    CLS_ID = "N",
                    REM = request.Header.REM,
                    USR = request.Header.USR,
                    SYS_DATE = DateTime.Now
                };
                _context.MfCktzs.Add(header);

                if (request.Details != null && request.Details.Count > 0)
                {
                    foreach (var d in request.Details)
                    {
                        _context.TfCktzs.Add(new TfCktz
                        {
                            TZ_NO = request.Header.TZ_NO,
                            ITM = d.ITM,
                            TZ_DD = request.Header.TZ_DD,
                            PRD_NO = d.PRD_NO,
                            PRD_NAME = d.PRD_NAME,
                            BAT_NO = d.BAT_NO,
                            WH = d.WH,
                            UNIT = d.UNIT,
                            QTY = d.QTY,
                            REM = d.REM
                        });
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new ApiResult<string> { Success = true, Data = request.Header.TZ_NO, Message = "保存成功" });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "保存出库通知单事务失败");
                return StatusCode(500, new ApiResult<string> { Success = false, Message = $"保存失败: {ex.Message}" });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "保存出库通知单时发生错误");
            return StatusCode(500, new ApiResult<string> { Success = false, Message = $"服务器内部错误: {ex.Message}" });
        }
    }
}

// ====== 保存请求 DTO ======

public class OutboundNoticeSaveRequest
{
    public OutboundNoticeSaveHeader Header { get; set; } = new();
    public List<OutboundNoticeSaveDetail>? Details { get; set; }
}

public class OutboundNoticeSaveHeader
{
    public string TZ_NO { get; set; } = "";
    public DateTime? TZ_DD { get; set; }
    public string? DEP { get; set; }
    public string? BIL_TYPE { get; set; }
    public string? CUS_NO { get; set; }
    public string? CUS_NAME { get; set; }
    public string? WH { get; set; }
    public string? APPLY_NO { get; set; }
    public DateTime? EST_DD { get; set; }
    public DateTime? EXPECT_DD { get; set; }
    public int? PRIORITY { get; set; }
    public string? REM { get; set; }
    public string? USR { get; set; }
}

public class OutboundNoticeSaveDetail
{
    public int ITM { get; set; }
    public string? PRD_NO { get; set; }
    public string? PRD_NAME { get; set; }
    public string? BAT_NO { get; set; }
    public string? WH { get; set; }
    public string? UNIT { get; set; }
    public decimal QTY { get; set; }
    public string? REM { get; set; }
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
