using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DocPropertySettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<DocPropertySettingController> _logger;

    public DocPropertySettingController(WarehouseDbContext context, ILogger<DocPropertySettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<DocPropertySettingDto>>>> Search(
        [FromQuery] string? pgm,
        [FromQuery] string? roleNo)
    {
        try
        {
            var query = _context.BarPswdProps.AsQueryable();

            if (!string.IsNullOrWhiteSpace(pgm))
                query = query.Where(x => x.PGM != null && x.PGM.Contains(pgm));

            if (!string.IsNullOrWhiteSpace(roleNo))
                query = query.Where(x => x.ROLENO != null && x.ROLENO.Contains(roleNo));

            query = query.OrderBy(x => x.PGM).ThenBy(x => x.ROLENO);

            var list = await query.Select(x => new DocPropertySettingDto
            {
                CompNo = x.COMPNO ?? "",
                Pgm = x.PGM ?? "",
                RoleNo = x.ROLENO ?? "",
                TypeId = x.TYPE_ID ?? "",
                FldName = x.FLD_NAME ?? "",
                FldValue = x.FLD_VALUE ?? "",
                Rem = x.REM ?? ""
            }).ToListAsync();

            return Ok(new ApiResult<List<DocPropertySettingDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询单据属性设定时发生错误");
            return StatusCode(500, new ApiResult<List<DocPropertySettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>单据属性设定列表行数据（对应前端表格）</summary>
public class DocPropertySettingDto
{
    public string CompNo { get; set; } = "";
    public string Pgm { get; set; } = "";
    public string RoleNo { get; set; } = "";
    public string TypeId { get; set; } = "";
    public string FldName { get; set; } = "";
    public string FldValue { get; set; } = "";
    public string Rem { get; set; } = "";
}
