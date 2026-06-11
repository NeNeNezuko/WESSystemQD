using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OutboundNoticeChangeController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<OutboundNoticeChangeController> _logger;

    public OutboundNoticeChangeController(WarehouseDbContext context, ILogger<OutboundNoticeChangeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询出库通知变更单列表（以表身TF_CKTZ_CHG为主，LEFT JOIN表头MF_CKTZ_CHG）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<OutboundNoticeChangeDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? changeNumber,
        [FromQuery] string? businessType)
    {
        try
        {
            var query = from t in _context.TfCktzChgs
                        join m in _context.MfCktzChgs on t.CHG_NO equals m.CHG_NO into mj
                        from m in mj.DefaultIfEmpty()
                        join d in _context.Depts on (m != null ? m.DEP : null) equals d.DEP into dj
                        from d in dj.DefaultIfEmpty()
                        select new { T = t, M = m, D = d };

            // 变更日期范围筛选
            if (dateFrom.HasValue)
                query = query.Where(x => x.M != null && x.M.CHG_DATE >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.M != null && x.M.CHG_DATE <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 变更单号模糊匹配
            if (!string.IsNullOrWhiteSpace(changeNumber))
                query = query.Where(x => x.T.CHG_NO.Contains(changeNumber));

            // 业务类型筛选
            if (!string.IsNullOrWhiteSpace(businessType))
            {
                var types = businessType.Split(',', StringSplitOptions.RemoveEmptyEntries);
                query = query.Where(x => x.M != null && types.Contains(x.M.BIL_TYPE));
            }

            // 按变更单号+项次排序
            query = query.OrderBy(x => x.T.CHG_NO).ThenBy(x => x.T.ITM);

            var rawList = await query.ToListAsync();

            // 在内存中做投影
            var list = rawList.Select(x => new OutboundNoticeChangeDto
            {
                ItemNo = x.T.ITM,
                ChangeDate = x.M?.CHG_DATE ?? DateTime.MinValue,
                ChangeNumber = x.T.CHG_NO,
                DeptCode = x.M?.DEP ?? "",
                DeptName = x.D?.NAME ?? "",
                BusinessType = x.M?.BIL_TYPE ?? "",
                BusinessTypeName = "",  // 后续可关联业务类型表获取名称
                ExecuteStatus = x.M?.EXE_STATUS ?? "",
                Creator = x.M?.USR ?? "",
                CreatorName = x.M?.USR_NAME ?? ""
            }).ToList();

            return Ok(new ApiResult<List<OutboundNoticeChangeDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询出库通知变更单时发生错误");
            return StatusCode(500, new ApiResult<List<OutboundNoticeChangeDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>出库通知变更单列表行数据（对应前端表格）</summary>
public class OutboundNoticeChangeDto
{
    public int ItemNo { get; set; }
    public DateTime ChangeDate { get; set; }
    public string ChangeNumber { get; set; } = "";
    public string DeptCode { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string BusinessType { get; set; } = "";
    public string BusinessTypeName { get; set; } = "";
    public string ExecuteStatus { get; set; } = "";
    public string Creator { get; set; } = "";
    public string CreatorName { get; set; } = "";
}
