using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DocTypeSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<DocTypeSettingController> _logger;

    public DocTypeSettingController(WarehouseDbContext context, ILogger<DocTypeSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<DocTypeSettingDto>>>> Search(
        [FromQuery] string? spcId,
        [FromQuery] string? spcNo)
    {
        try
        {
            var query = _context.BilSpcs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(spcId) && spcId != "全部")
                query = query.Where(x => x.SPC_ID == spcId);

            if (!string.IsNullOrWhiteSpace(spcNo))
                query = query.Where(x => x.SPC_NO != null && x.SPC_NO.Contains(spcNo));

            query = query.OrderBy(x => x.SPC_ID).ThenBy(x => x.SPC_NO);

            var list = await query.Select(x => new DocTypeSettingDto
            {
                SpcId = x.SPC_ID ?? "",
                SpcNo = x.SPC_NO ?? "",
                Name = x.NAME ?? "",
                Rem = x.REM ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<DocTypeSettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询单据类别设定时发生错误");
            return StatusCode(500, new ApiResult<List<DocTypeSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>单据类别设定列表行数据（对应前端表格）</summary>
public class DocTypeSettingDto
{
    public string SpcId { get; set; } = "";
    public string SpcNo { get; set; } = "";
    public string Name { get; set; } = "";
    public string Rem { get; set; } = "";
}
