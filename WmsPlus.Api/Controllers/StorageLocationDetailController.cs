using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StorageLocationDetailController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<StorageLocationDetailController> _logger;

    public StorageLocationDetailController(WarehouseDbContext context, ILogger<StorageLocationDetailController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<StorageLocationDetailDto>>>> Search(
        [FromQuery] string? chuw,
        [FromQuery] string? wh,
        [FromQuery] string? cwStatus,
        [FromQuery] string? gs,
        [FromQuery] string? gl,
        [FromQuery] string? layer,
        [FromQuery] string? lockCw,
        [FromQuery] string? layerProp,
        [FromQuery] string? lkhjType,
        [FromQuery] string? cwUnmatch)
    {
        try
        {
            var query = _context.CwWhs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(chuw))
                query = query.Where(x => x.CHUW != null && x.CHUW.Contains(chuw));

            if (!string.IsNullOrWhiteSpace(wh))
                query = query.Where(x => x.WH != null && x.WH.Contains(wh));

            if (!string.IsNullOrWhiteSpace(cwStatus) && cwStatus != "全部")
                query = query.Where(x => x.CW_STATUS != null && x.CW_STATUS == cwStatus);

            if (!string.IsNullOrWhiteSpace(gs))
                query = query.Where(x => x.GS != null && x.GS.Contains(gs));

            if (!string.IsNullOrWhiteSpace(gl))
                query = query.Where(x => x.GL != null && x.GL.Contains(gl));

            if (!string.IsNullOrWhiteSpace(layer))
                query = query.Where(x => x.LAYER != null && x.LAYER.Contains(layer));

            if (!string.IsNullOrWhiteSpace(lockCw) && lockCw != "全部")
                query = query.Where(x => x.LOCK_CW != null && x.LOCK_CW == lockCw);

            if (!string.IsNullOrWhiteSpace(layerProp) && layerProp != "全部")
                query = query.Where(x => x.LAYER_PROP != null && x.LAYER_PROP == layerProp);

            if (!string.IsNullOrWhiteSpace(lkhjType) && lkhjType != "全部")
                query = query.Where(x => x.LKHJ_TYPE != null && x.LKHJ_TYPE == lkhjType);

            if (!string.IsNullOrWhiteSpace(cwUnmatch) && cwUnmatch != "全部")
                query = query.Where(x => x.CW_UNMATCH != null && x.CW_UNMATCH == cwUnmatch);

            query = query.OrderBy(x => x.CHUW);

            var list = await query.Select(x => new StorageLocationDetailDto
            {
                Chuw = x.CHUW ?? "",
                Name = x.NAME ?? "",
                Wh = x.WH ?? "",
                Gs = x.GS ?? "",
                Gl = x.GL ?? "",
                Layer = x.LAYER ?? "",
                CwStatus = x.CW_STATUS ?? "",
                LockCw = x.LOCK_CW ?? "",
                LayerProp = x.LAYER_PROP ?? "",
                LkhjType = x.LKHJ_TYPE ?? "",
                CwUnmatch = x.CW_UNMATCH ?? "",
                GsPat = x.GS_PAT ?? "",
                AreaId = x.AREA_ID ?? ""
            }).ToListAsync();

            // 查询仓库名称
            var whCodes = list.Where(x => !string.IsNullOrWhiteSpace(x.Wh)).Select(x => x.Wh).Distinct().ToList();
            var whDict = new Dictionary<string, string>();
            if (whCodes.Count > 0)
            {
                whDict = await _context.MyWhs
                    .Where(w => whCodes.Contains(w.WH))
                    .ToDictionaryAsync(w => w.WH, w => w.NAME ?? "");
            }

            var seq = 0;
            var result = list.Select(x => new StorageLocationDetailDto
            {
                Seq = ++seq,
                Chuw = x.Chuw,
                Name = x.Name,
                Wh = x.Wh,
                WhName = whDict.GetValueOrDefault(x.Wh, ""),
                Gs = x.Gs,
                Gl = x.Gl,
                Layer = x.Layer,
                CwStatus = x.CwStatus,
                LockCw = x.LockCw,
                LayerProp = x.LayerProp,
                LkhjType = x.LkhjType,
                CwUnmatch = x.CwUnmatch,
                GsPat = x.GsPat,
                AreaId = x.AreaId
            }).ToList();

            return Ok(new ApiResult<List<StorageLocationDetailDto>>
            {
                Success = true,
                Data = result,
                Total = result.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询储位明细时发生错误");
            return StatusCode(500, new ApiResult<List<StorageLocationDetailDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>储位明细列表行数据（对应前端表格）</summary>
public class StorageLocationDetailDto
{
    public int Seq { get; set; }
    public string Chuw { get; set; } = "";
    public string Name { get; set; } = "";
    public string Wh { get; set; } = "";
    public string WhName { get; set; } = "";
    public string Gs { get; set; } = "";
    public string Gl { get; set; } = "";
    public string Layer { get; set; } = "";
    public string CwStatus { get; set; } = "";
    public string LockCw { get; set; } = "";
    public string LayerProp { get; set; } = "";
    public string LkhjType { get; set; } = "";
    public string CwUnmatch { get; set; } = "";
    public string GsPat { get; set; } = "";
    public string AreaId { get; set; } = "";
}

// ApiResult<T> 已在 InboundNoticeController.cs 等处全局定义，此处移除避免 CS0101 冲突
