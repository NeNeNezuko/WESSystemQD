using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OutboundTaskAssignmentController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<OutboundTaskAssignmentController> _logger;

    public OutboundTaskAssignmentController(WarehouseDbContext context, ILogger<OutboundTaskAssignmentController> logger)
    {
        _context = context;
        _logger = logger;
    }

    #region Tab1: 出库通知

    /// <summary>
    /// 查询出库通知列表（查询单主表）
    /// </summary>
    [HttpGet("outbound-notice/search")]
    public async Task<ActionResult<ApiResult<List<OutboundNoticeDto>>>> SearchOutboundNotice(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? sourceType,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? businessType,
        [FromQuery] string? applyOrderNumber,
        [FromQuery] string? noticeNumber,
        [FromQuery] string? businessOrderNumber,
        [FromQuery] string? processStatus,
        [FromQuery] string? customerCode)
    {
        try
        {
            // 以出库通知单MF_CKTZ为主表查询
            var query = from m in _context.MfCktzs
                        select m;

            if (dateFrom.HasValue)
                query = query.Where(x => x.TZ_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.TZ_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(noticeNumber))
                query = query.Where(x => x.TZ_NO.Contains(noticeNumber));
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.WH != null && x.WH.Contains(warehouseCode));
            if (!string.IsNullOrWhiteSpace(customerCode))
                query = query.Where(x => x.CUS_NO != null && x.CUS_NO.Contains(customerCode));

            var rawList = await query
                .OrderByDescending(x => x.TZ_DD)
                .ThenBy(x => x.TZ_NO)
                .ToListAsync();

            var list = rawList.Select((x, idx) => new OutboundNoticeDto
            {
                ItemNo = idx + 1,
                PriorityLevel = x.PRIORITY ?? 0,
                BizTypeName = x.BIL_TYPE ?? "",
                DocumentDate = x.TZ_DD ?? DateTime.MinValue,
                NoticeNo = x.TZ_NO ?? "",
                ApplyNo = x.APPLY_NO ?? "",
                ExpectedOutDate = x.EXPECT_DD,
                CustomerCode = x.CUS_NO ?? "",
                CustomerName = x.CUS_NAME ?? "",
                OperatorName = x.USR ?? ""
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
            _logger.LogError(ex, "查询出库通知列表时发生错误");
            return StatusCode(500, new ApiResult<List<OutboundNoticeDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据通知单号查询出库通知明细（通知单明细从表）
    /// </summary>
    [HttpGet("outbound-notice/detail/{noticeNo}")]
    public async Task<ActionResult<ApiResult<List<OutboundNoticeItemDto>>>> GetOutboundNoticeDetail(string noticeNo)
    {
        try
        {
            var details = await _context.TfCktzs
                .Where(t => t.TZ_NO == noticeNo)
                .OrderBy(t => t.ITM)
                .Select(t => new OutboundNoticeItemDto
                {
                    NoticeNo = t.TZ_NO ?? "",
                    WaveNo = t.BC_NO ?? "",
                    PrdNo = t.PRD_NO ?? "",
                    PrdName = t.PRD_NAME ?? "",
                    BatchNo = t.BAT_NO ?? "",
                    WarehouseName = "",
                    Unit = t.UNIT ?? "",
                    OrigQty = t.QTY,
                    ConvertedWaveQty = t.CONVERT_QTY,
                    ReturnQty = t.RETURN_QTY,
                    PickedQty = t.PICK_QTY
                })
                .ToListAsync();

            return Ok(new ApiResult<List<OutboundNoticeItemDto>>
            {
                Success = true,
                Data = details,
                Total = details.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询出库通知明细时发生错误");
            return StatusCode(500, new ApiResult<List<OutboundNoticeItemDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    #endregion

    #region Tab2: 波次管理

    /// <summary>
    /// 查询波次管理列表（波次单主表 MF_BC）
    /// </summary>
    [HttpGet("wave-manage/search")]
    public async Task<ActionResult<ApiResult<List<WaveManageDto>>>> SearchWaveManage(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? waveNumber,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? businessType,
        [FromQuery] string? applyOrderNumber,
        [FromQuery] string? noticeNumber,
        [FromQuery] string? businessOrderNumber,
        [FromQuery] string? priorityLevels,
        [FromQuery] string? flowStatuses)
    {
        try
        {
            var query = from m in _context.MfBcs
                        select m;

            if (dateFrom.HasValue)
                query = query.Where(x => x.BC_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.BC_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(waveNumber))
                query = query.Where(x => x.BC_NO.Contains(waveNumber));
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.WH != null && x.WH.Contains(warehouseCode));
            if (!string.IsNullOrWhiteSpace(businessType))
                query = query.Where(x => x.BIL_TYPE != null && x.BIL_TYPE.Contains(businessType));

            // 进单等级筛选
            if (!string.IsNullOrWhiteSpace(priorityLevels))
            {
                var levels = priorityLevels.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(s => int.TryParse(s, out var v) ? v : (int?)null)
                    .Where(v => v.HasValue)
                    .Select(v => v!.Value)
                    .ToList();
                if (levels.Count > 0)
                    query = query.Where(x => levels.Contains(x.PRIORITY ?? 0));
            }

            // 流程状态筛选
            if (!string.IsNullOrWhiteSpace(flowStatuses))
            {
                // CLS_ID: F=未结案 T/F, STATUS_PG: 0/1
                foreach (var status in flowStatuses.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries))
                {
                    if (status == "未派工")
                        query = query.Where(x => x.CLS_ID != "T" || x.CLS_ID == null);
                    else if (status == "已结案")
                        query = query.Where(x => x.CLS_ID == "T");
                }
            }

            var rawList = await query
                .OrderByDescending(x => x.BC_DD)
                .ThenBy(x => x.BC_NO)
                .ToListAsync();

            var list = rawList.Select((x, idx) => new WaveManageDto
            {
                ItemNo = idx + 1,
                WaveDate = x.BC_DD,
                WaveNo = x.BC_NO ?? "",
                WarehouseName = x.WH ?? "",
                DeptName = x.DEP ?? "",
                OperatorName = x.USR ?? "",
                ResultMark = x.CLS_ID ?? "",
                Remark = x.REM ?? "",
                PriorityLevel = x.PRIORITY ?? 0,
                DispatchStatus = "" // 需根据业务逻辑计算
            }).ToList();

            return Ok(new ApiResult<List<WaveManageDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询波次管理列表时发生错误");
            return StatusCode(500, new ApiResult<List<WaveManageDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据波次单号查询波次单明细（TF_BC）
    /// </summary>
    [HttpGet("wave-manage/detail/{waveNo}")]
    public async Task<ActionResult<ApiResult<List<WaveManageDetailDto>>>> GetWaveManageDetail(string waveNo)
    {
        try
        {
            var details = await _context.TfBcs
                .Where(t => t.BC_NO == waveNo)
                .OrderBy(t => t.ITM)
                .Select(t => new WaveManageDetailDto
                {
                    WaveDate = t.BC_DD,
                    WaveNo = t.BC_NO ?? "",
                    ItemNo = t.ITM ?? 0,
                    WarehouseName = t.WH ?? "",
                    PrdNo = t.PRD_NO ?? "",
                    PrdName = t.PRD_NAME ?? "",
                    BatchNo = t.BAT_NO ?? "",
                    Unit = t.UNIT ?? "",
                    Qty = t.QTY,
                    BoxQty = t.PICK_QTY,
                    Remark = t.REM ?? ""
                })
                .ToListAsync();

            return Ok(new ApiResult<List<WaveManageDetailDto>>
            {
                Success = true,
                Data = details,
                Total = details.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询波次单明细时发生错误");
            return StatusCode(500, new ApiResult<List<WaveManageDetailDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    #endregion

    #region Tab3: 波次拣货任务管理

    /// <summary>
    /// 查询波次拣货任务管理列表（MF_JHRW 主表）
    /// </summary>
    [HttpGet("wave-pick-task-manage/search")]
    public async Task<ActionResult<ApiResult<List<WavePickTaskManageDto>>>> SearchWavePickTaskManage(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? waveNumber,
        [FromQuery] string? warehouseCode,
        [FromQuery] string? businessType,
        [FromQuery] string? applyOrderNumber,
        [FromQuery] string? noticeNumber,
        [FromQuery] string? businessOrderNumber,
        [FromQuery] string? priorityLevels,
        [FromQuery] string? processStatusPg)
    {
        try
        {
            var query = from m in _context.MfJhrws
                        select m;

            if (dateFrom.HasValue)
                query = query.Where(x => x.JR_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.JR_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));
            if (!string.IsNullOrWhiteSpace(waveNumber))
                query = query.Where(x => x.BC_NO != null && x.BC_NO.Contains(waveNumber));
            if (!string.IsNullOrWhiteSpace(warehouseCode))
                query = query.Where(x => x.WH != null && x.WH.Contains(warehouseCode));
            if (!string.IsNullOrWhiteSpace(businessType))
                query = query.Where(x => x.TYPE_ID != null && x.TYPE_ID.Contains(businessType));

            // 派工状态筛选
            if (!string.IsNullOrWhiteSpace(processStatusPg) && processStatusPg != "全部")
            {
                if (processStatusPg == "未派工")
                    query = query.Where(x => x.STATUS_PG == "0" || x.STATUS_PG == null);
                else if (processStatusPg == "已派工")
                    query = query.Where(x => x.STATUS_PG == "1");
            }

            var rawList = await query
                .OrderByDescending(x => x.JR_DD)
                .ThenBy(x => x.JR_NO)
                .ToListAsync();

            var list = rawList.Select((x, idx) => new WavePickTaskManageDto
            {
                ItemNo = idx + 1,
                AcceptDate = x.JR_DD,
                TaskNo = x.JR_NO ?? "",
                DeptName = x.DEP ?? "",
                AcceptorName = x.USR_PK ?? "",
                AcceptResultMark = x.CLS_ID ?? "",
                Remark = x.REM ?? "",
                PriorityLevel = x.PRIORITY ?? 0,
                DispatchStatus = x.STATUS_PG == "1" ? "已派工" : "未派工"
            }).ToList();

            return Ok(new ApiResult<List<WavePickTaskManageDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询波次拣货任务管理列表时发生错误");
            return StatusCode(500, new ApiResult<List<WavePickTaskManageDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据任务单号查询波次拣货任务明细（TF_JHRW）
    /// </summary>
    [HttpGet("wave-pick-task-manage/detail/{taskNo}")]
    public async Task<ActionResult<ApiResult<List<WavePickTaskManageDetailDto>>>> GetWavePickTaskManageDetail(string taskNo)
    {
        try
        {
            var details = await _context.TfJhrws
                .Where(t => t.JR_NO == taskNo)
                .OrderBy(t => t.ITM)
                .Select(t => new WavePickTaskManageDetailDto
                {
                    TaskNo = t.JR_NO ?? "",
                    WaveNo = t.BC_NO ?? "",
                    PrdNo = t.PRD_NO ?? "",
                    PrdName = t.PRD_NAME ?? "",
                    BatchNo = t.BAT_NO ?? "",
                    LocationCode = t.CHUW ?? "",
                    Unit = t.UNIT ?? "",
                    Qty = t.QTY,
                    BoxQty = t.QTY_PK,
                    PreparedQty = t.QTY_MISS,
                    PreparedBoxQty = t.QTY_IMPERFECT
                })
                .ToListAsync();

            return Ok(new ApiResult<List<WavePickTaskManageDetailDto>>
            {
                Success = true,
                Data = details,
                Total = details.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询波次拣货任务明细时发生错误");
            return StatusCode(500, new ApiResult<List<WavePickTaskManageDetailDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    #endregion

    #region Tab4: 参数设定

    /// <summary>
    /// 获取参数设定
    /// </summary>
    [HttpGet("param-setting")]
    public async Task<ActionResult<ApiResult<ParamSettingDto>>> GetParamSetting()
    {
        try
        {
            // TODO: 从配置表或数据库读取参数设定
            // 目前返回默认值
            return Ok(new ApiResult<ParamSettingDto>
            {
                Success = true,
                Data = new ParamSettingDto
                {
                    PickerAutoAssign = false,
                    ShowWaveDetailWindow = false
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取参数设定时发生错误");
            return StatusCode(500, new ApiResult<ParamSettingDto>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 保存参数设定
    /// </summary>
    [HttpPost("param-setting")]
    public async Task<ActionResult<ApiResult<bool>>> SaveParamSetting([FromBody] ParamSettingDto setting)
    {
        try
        {
            // TODO: 保存参数到配置表或数据库
            return Ok(new ApiResult<bool>
            {
                Success = true,
                Data = true,
                Message = "保存成功"
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "保存参数设定时发生错误");
            return StatusCode(500, new ApiResult<bool>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    #endregion
}

/// <summary>波次管理-波次单</summary>
public class WaveManageDto
{
    public int ItemNo { get; set; }
    public DateTime? WaveDate { get; set; }
    public string WaveNo { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string OperatorName { get; set; } = "";
    public string ResultMark { get; set; } = "";
    public string Remark { get; set; } = "";
    public int PriorityLevel { get; set; }
    public string DispatchStatus { get; set; } = "";
}

/// <summary>波次管理-波次单明细</summary>
public class WaveManageDetailDto
{
    public DateTime? WaveDate { get; set; }
    public string WaveNo { get; set; } = "";
    public int ItemNo { get; set; }
    public string WarehouseName { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string BatchNo { get; set; } = "";
    public string Unit { get; set; } = "";
    public decimal? Qty { get; set; }
    public decimal? BoxQty { get; set; }
    public string Remark { get; set; } = "";
}

/// <summary>波次拣货任务管理-任务单</summary>
public class WavePickTaskManageDto
{
    public int ItemNo { get; set; }
    public DateTime? AcceptDate { get; set; }
    public string TaskNo { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string AcceptorName { get; set; } = "";
    public string AcceptResultMark { get; set; } = "";
    public string Remark { get; set; } = "";
    public int PriorityLevel { get; set; }
    public string DispatchStatus { get; set; } = "";
}

/// <summary>波次拣货任务管理-任务单明细</summary>
public class WavePickTaskManageDetailDto
{
    public string TaskNo { get; set; } = "";
    public string WaveNo { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string BatchNo { get; set; } = "";
    public string LocationCode { get; set; } = "";
    public string Unit { get; set; } = "";
    public decimal? Qty { get; set; }
    public decimal? BoxQty { get; set; }
    public decimal? PreparedQty { get; set; }
    public decimal? PreparedBoxQty { get; set; }
}

/// <summary>参数设定DTO</summary>
public class ParamSettingDto
{
    public bool PickerAutoAssign { get; set; }
    public bool ShowWaveDetailWindow { get; set; }
}

/// <summary>出库通知明细（扁平结构）</summary>
public class OutboundNoticeItemDto
{
    public string NoticeNo { get; set; } = "";
    public string WaveNo { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string BatchNo { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string Unit { get; set; } = "";
    public decimal? OrigQty { get; set; }
    public decimal? ConvertedWaveQty { get; set; }
    public decimal? ReturnQty { get; set; }
    public decimal? PickedQty { get; set; }
}
