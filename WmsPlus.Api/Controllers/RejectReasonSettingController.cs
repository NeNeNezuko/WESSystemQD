using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class RejectReasonSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<RejectReasonSettingController> _logger;

    public RejectReasonSettingController(WarehouseDbContext context, ILogger<RejectReasonSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<RejectReasonDto>>>> Search(
        [FromQuery] string? spcNo,
        [FromQuery] string? name)
    {
        try
        {
            var query = _context.SpcSets.AsQueryable();

            if (!string.IsNullOrWhiteSpace(spcNo))
                query = query.Where(x => x.SPC_NO != null && x.SPC_NO.Contains(spcNo));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            query = query.OrderBy(x => x.SPC_NO);

            var list = await query.Select(x => new RejectReasonDto
            {
                SpcNo = x.SPC_NO ?? "",
                Name = x.NAME ?? "",
                SpcUp = x.SPC_UP ?? "",
                Rem = x.REM ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<RejectReasonDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询不合格原因设定时发生错误");
            return StatusCode(500, new ApiResult<List<RejectReasonDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>不合格原因设定列表行数据（对应前端表格）</summary>
public class RejectReasonDto
{
    public string SpcNo { get; set; } = "";
    public string Name { get; set; } = "";
    public string SpcUp { get; set; } = "";
    public string Rem { get; set; } = "";
}
