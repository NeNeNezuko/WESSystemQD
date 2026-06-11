using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExceedCtrlSettingController : ControllerBase
{
    private readonly WarehouseDbContext _ctx;
    private readonly ILogger<ExceedCtrlSettingController> _logger;

    public ExceedCtrlSettingController(WarehouseDbContext ctx, ILogger<ExceedCtrlSettingController> logger)
    {
        _ctx = ctx;
        _logger = logger;
    }

    /// <summary>
    /// 获取业务类型树数据（全部 → 入库/出库 → 各业务类型）
    /// </summary>
    [HttpGet("tree")]
    public async Task<IActionResult> GetTreeData()
    {
        try
        {
            var allTypes = await _ctx.RkTypeSets
                .OrderBy(t => t.CR_TYPE)
                .ThenBy(t => t.TYPE_ID)
                .ToListAsync();

            var root = new TreeNode
            {
                Id = "all",
                Name = "全部",
                IsLeaf = false,
                IsExpanded = true
            };

            var rkNodes = allTypes.Where(t => t.CR_TYPE == "1").Select(t => new TreeNode
            {
                Id = $"rk-{t.TYPE_ID}",
                Name = t.NAME ?? t.TYPE_ID,
                CrType = t.CR_TYPE,
                TypeId = t.TYPE_ID,
                IsLeaf = true
            }).ToList();

            var ckNodes = allTypes.Where(t => t.CR_TYPE == "2").Select(t => new TreeNode
            {
                Id = $"ck-{t.TYPE_ID}",
                Name = t.NAME ?? t.TYPE_ID,
                CrType = t.CR_TYPE,
                TypeId = t.TYPE_ID,
                IsLeaf = true
            }).ToList();

            root.Children.Add(new TreeNode
            {
                Id = "rk-group",
                Name = "入库",
                CrType = "1",
                IsLeaf = false,
                IsExpanded = true,
                Children = rkNodes
            });

            root.Children.Add(new TreeNode
            {
                Id = "ck-group",
                Name = "出库",
                CrType = "2",
                IsLeaf = false,
                IsExpanded = true,
                Children = ckNodes
            });

            return Ok(root);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取超交管制树数据失败");
            return StatusCode(500, new { message = "获取树数据失败", error = ex.Message });
        }
    }

    /// <summary>
    /// 获取指定业务类型的超交管制配置
    /// </summary>
    [HttpGet("config")]
    public async Task<IActionResult> GetConfig([FromQuery] string crType, [FromQuery] string typeId)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(crType) || string.IsNullOrWhiteSpace(typeId))
                return BadRequest(new { message = "参数不完整" });

            var records = await _ctx.ExceedCtrls
                .Where(e => e.CR_TYPE == crType && e.TYPE_ID == typeId)
                .ToListAsync();

            var config = new ExceedConfigDto();
            var checkRecord = records.FirstOrDefault(r => r.PARAMS_ID == "CHECK");
            var rateRecord = records.FirstOrDefault(r => r.PARAMS_ID == "RATE");

            config.CheckMode = checkRecord?.PARAMS_VALUE ?? "管制";
            if (int.TryParse(rateRecord?.PARAMS_VALUE, out var rate))
                config.RateValue = rate;

            return Ok(config);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取超交配置失败: crType={crType}, typeId={typeId}", crType, typeId);
            return StatusCode(500, new { message = "获取配置失败", error = ex.Message });
        }
    }

    /// <summary>
    /// 保存超交管制配置（upsert 方式）
    /// </summary>
    [HttpPost("save")]
    public async Task<IActionResult> SaveConfig([FromBody] SaveExceedConfigRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.CrType) || string.IsNullOrWhiteSpace(request.TypeId))
                return BadRequest(new { message = "参数不完整" });

            // upsert CHECK 记录
            var checkRecord = await _ctx.ExceedCtrls
                .FirstOrDefaultAsync(e => e.CR_TYPE == request.CrType && e.TYPE_ID == request.TypeId && e.PARAMS_ID == "CHECK");

            if (checkRecord == null)
            {
                checkRecord = new ExceedCtrl
                {
                    CR_TYPE = request.CrType,
                    TYPE_ID = request.TypeId,
                    PARAMS_ID = "CHECK"
                };
                _ctx.ExceedCtrls.Add(checkRecord);
            }
            checkRecord.PARAMS_VALUE = request.CheckMode;

            // upsert RATE 记录
            var rateRecord = await _ctx.ExceedCtrls
                .FirstOrDefaultAsync(e => e.CR_TYPE == request.CrType && e.TYPE_ID == request.TypeId && e.PARAMS_ID == "RATE");

            if (rateRecord == null)
            {
                rateRecord = new ExceedCtrl
                {
                    CR_TYPE = request.CrType,
                    TYPE_ID = request.TypeId,
                    PARAMS_ID = "RATE"
                };
                _ctx.ExceedCtrls.Add(rateRecord);
            }
            rateRecord.PARAMS_VALUE = request.RateValue.ToString();

            await _ctx.SaveChangesAsync();

            return Ok(new { success = true, message = "保存成功" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "保存超交配置失败: crType={crType}, typeId={typeId}", request.CrType, request.TypeId);
            return StatusCode(500, new { message = "保存失败", error = ex.Message });
        }
    }
}

/// <summary>
/// 业务类型树节点（API 内部使用，与前端模型结构一致）
/// </summary>
public class TreeNode
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public string? CrType { get; set; }
    public string? TypeId { get; set; }
    public bool IsLeaf { get; set; }
    public List<TreeNode> Children { get; set; } = new();
    public bool IsExpanded { get; set; } = true;
    public bool IsSelected { get; set; }
}

/// <summary>
/// 超交管制配置项
/// </summary>
public class ExceedConfigDto
{
    public string CheckMode { get; set; } = "管制";
    public int RateValue { get; set; } = 0;
}

/// <summary>
/// 保存请求模型
/// </summary>
public class SaveExceedConfigRequest
{
    public string CrType { get; set; } = "";
    public string TypeId { get; set; } = "";
    public string CheckMode { get; set; } = "管制";
    public int RateValue { get; set; } = 0;
}
