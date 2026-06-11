using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DataReplaceController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<DataReplaceController> _logger;

    public DataReplaceController(WarehouseDbContext context, ILogger<DataReplaceController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询资料替换列表（根据基础资料类型返回对应表的代号字段）
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<DataReplaceDto>>>> Search(
        [FromQuery] string? baseType,
        [FromQuery] string? replaceType)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(baseType))
            {
                return Ok(new ApiResult<List<DataReplaceDto>>
                {
                    Success = true,
                    Data = new List<DataReplaceDto>(),
                    Total = 0
                });
            }

            List<DataReplaceDto> list = baseType.ToLower() switch
            {
                "dept" => await QueryDeptAsync(),
                "warehouse" => await QueryWarehouseAsync(),
                "midclass" => await QueryMidClassAsync(),
                "product" => await QueryProductAsync(),
                "mark" => await QueryMarkAsync(),
                _ => new List<DataReplaceDto>()
            };

            // 替换类型过滤：new_not_exists 表示新代号取不存在的（当前仅展示原代号，新代号留空由用户填写）
            // new_exists 表示新代号取已存在的（同样先展示原代号）
            // 本期仅做查询展示，替换逻辑后续补全

            return Ok(new ApiResult<List<DataReplaceDto>>
            {
                Success = true,
                Data = list,
                Total = list.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询资料替换时发生错误");
            return StatusCode(500, new ApiResult<List<DataReplaceDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    private async Task<List<DataReplaceDto>> QueryDeptAsync()
    {
        return await _context.Depts
            .OrderBy(x => x.DEP)
            .Select(x => new DataReplaceDto
            {
                OldCode = x.DEP ?? "",
                NewCode = ""
            }).ToListAsync();
    }

    private async Task<List<DataReplaceDto>> QueryWarehouseAsync()
    {
        return await _context.MyWhs
            .OrderBy(x => x.WH)
            .Select(x => new DataReplaceDto
            {
                OldCode = x.WH ?? "",
                NewCode = ""
            }).ToListAsync();
    }

    private async Task<List<DataReplaceDto>> QueryMidClassAsync()
    {
        return await _context.Indxes
            .OrderBy(x => x.IDX_NO)
            .Select(x => new DataReplaceDto
            {
                OldCode = x.IDX_NO ?? "",
                NewCode = ""
            }).ToListAsync();
    }

    private async Task<List<DataReplaceDto>> QueryProductAsync()
    {
        return await _context.Prdts
            .OrderBy(x => x.PRD_NO)
            .Select(x => new DataReplaceDto
            {
                OldCode = x.PRD_NO ?? "",
                NewCode = ""
            }).ToListAsync();
    }

    private async Task<List<DataReplaceDto>> QueryMarkAsync()
    {
        return await _context.PrdMarks
            .OrderBy(x => x.MOB_ID)
            .Select(x => new DataReplaceDto
            {
                OldCode = x.MOB_ID ?? "",
                NewCode = ""
            }).ToListAsync();
    }
}

/// <summary>资料替换DTO</summary>
public class DataReplaceDto
{
    public string OldCode { get; set; } = "";
    public string NewCode { get; set; } = "";
}
