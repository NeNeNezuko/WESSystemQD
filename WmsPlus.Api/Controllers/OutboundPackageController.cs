using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OutboundPackageController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<OutboundPackageController> _logger;

    public OutboundPackageController(WarehouseDbContext context, ILogger<OutboundPackageController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询出库包装单列表（MF_PACKAGE表）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<OutboundPackageDto>>>> Search(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] string? packageNo,
        [FromQuery] string? outBilNo,
        [FromQuery] string? outStatus)
    {
        try
        {
            var query = _context.MfPackages.AsQueryable();

            // 包装日期范围筛选
            if (dateFrom.HasValue)
                query = query.Where(x => x.PACKAGE_DD >= dateFrom.Value);
            if (dateTo.HasValue)
                query = query.Where(x => x.PACKAGE_DD <= dateTo.Value.AddDays(1).AddSeconds(-1));

            // 包装箱号模糊匹配
            if (!string.IsNullOrWhiteSpace(packageNo))
                query = query.Where(x => x.PACKAGE_NO != null && x.PACKAGE_NO.Contains(packageNo));

            // 出库业务单号模糊匹配
            if (!string.IsNullOrWhiteSpace(outBilNo))
                query = query.Where(x => x.OUT_BIL_NO != null && x.OUT_BIL_NO.Contains(outBilNo));

            // 出库状态筛选
            if (!string.IsNullOrWhiteSpace(outStatus))
                query = query.Where(x => x.OUT_STATUS != null && x.OUT_STATUS == outStatus);

            // 按包装箱号排序
            query = query.OrderBy(x => x.PACKAGE_NO);

            var rawList = await query.ToListAsync();

            var seqNo = 0;
            var list = rawList.Select(x => new OutboundPackageDto
            {
                SeqNo = ++seqNo,
                PackageNo = x.PACKAGE_NO ?? "",
                PackageDate = x.PACKAGE_DD,
                BusinessType = x.TYPE_ID ?? "",
                BusinessTypeName = "",  // 需要关联cr_type_set获取，先留空
                Packager = x.PACKAGER ?? "",
                PackagerName = "",  // 需要关联用户表获取，先留空
                PackTime = x.PACK_TIME,
                OutBilNo = x.OUT_BIL_NO ?? "",
                CusNo = x.CUS_NO ?? ""
            }).ToList();

            return Ok(new ApiResult<List<OutboundPackageDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询出库包装单时发生错误");
            return StatusCode(500, new ApiResult<List<OutboundPackageDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>出库包装单列表行数据（对应前端表格）</summary>
public class OutboundPackageDto
{
    public int SeqNo { get; set; }
    public string PackageNo { get; set; } = "";
    public DateTime? PackageDate { get; set; }
    public string BusinessType { get; set; } = "";
    public string BusinessTypeName { get; set; } = "";
    public string Packager { get; set; } = "";
    public string PackagerName { get; set; } = "";
    public DateTime? PackTime { get; set; }
    public string OutBilNo { get; set; } = "";
    public string CusNo { get; set; } = "";
}
