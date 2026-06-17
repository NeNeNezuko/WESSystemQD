using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WarehouseCodeSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<WarehouseCodeSettingController> _logger;

    public WarehouseCodeSettingController(WarehouseDbContext context, ILogger<WarehouseCodeSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<WarehouseCodeSettingDto>>>> Search(
        [FromQuery] string? wh,
        [FromQuery] string? name,
        [FromQuery] string? attrib,
        [FromQuery] string? upWh,
        [FromQuery] string? dep)
    {
        try
        {
            var query = _context.MyWhs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(wh))
                query = query.Where(x => x.WH.Contains(wh));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            if (!string.IsNullOrWhiteSpace(attrib))
                query = query.Where(x => x.ATTRIB != null && x.ATTRIB == attrib);

            if (!string.IsNullOrWhiteSpace(upWh))
                query = query.Where(x => x.UP_WH != null && x.UP_WH.Contains(upWh));

            if (!string.IsNullOrWhiteSpace(dep))
                query = query.Where(x => x.DEP != null && x.DEP.Contains(dep));

            query = query.OrderBy(x => x.WH);

            var list = await query.Select(x => new WarehouseCodeSettingDto
            {
                Wh = x.WH,
                Name = x.NAME ?? "",
                Attrib = x.ATTRIB ?? "",
                CwFlag = x.CW_FLAG ?? "",
                WhType = x.WH_TYPE ?? "",
                Dep = x.DEP ?? "",
                StopDd = x.STOP_DD,
                UpWh = x.UP_WH ?? ""
            }).ToListAsync();

            // 查询部门名称
            var depCodes = list.Where(x => !string.IsNullOrWhiteSpace(x.Dep)).Select(x => x.Dep).Distinct().ToList();
            var depDict = new Dictionary<string, string>();
            if (depCodes.Count > 0)
            {
                depDict = await _context.Depts
                    .Where(d => depCodes.Contains(d.DEP))
                    .ToDictionaryAsync(d => d.DEP, d => d.NAME ?? "");
            }

            // 查询上层仓库名称
            var upWhCodes = list.Where(x => !string.IsNullOrWhiteSpace(x.UpWh)).Select(x => x.UpWh).Distinct().ToList();
            var upWhDict = new Dictionary<string, string>();
            if (upWhCodes.Count > 0)
            {
                upWhDict = await _context.MyWhs
                    .Where(w => upWhCodes.Contains(w.WH))
                    .ToDictionaryAsync(w => w.WH, w => w.NAME ?? "");
            }

            var seq = 0;
            var result = list.Select(x => new WarehouseCodeSettingDto
            {
                Seq = ++seq,
                Wh = x.Wh,
                Name = x.Name,
                Attrib = x.Attrib,
                CwFlag = x.CwFlag,
                WhType = x.WhType,
                Dep = x.Dep,
                DepName = depDict.GetValueOrDefault(x.Dep, ""),
                StopDd = x.StopDd,
                UpWh = x.UpWh,
                UpWhName = upWhDict.GetValueOrDefault(x.UpWh, "")
            }).ToList();

            return Ok(new ApiResult<List<WarehouseCodeSettingDto>>
            {
                Success = true,
                Data = result,
                Total = result.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询仓库代号设定时发生错误");
            return StatusCode(500, new ApiResult<List<WarehouseCodeSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据仓库代号获取详情（编辑回填用）
    /// </summary>
    [HttpGet("getByWh")]
    public async Task<ActionResult<ApiResult<WarehouseCodeSettingCreateRequest>>> GetByWh([FromQuery] string wh)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(wh))
                return BadRequest(new ApiResult<WarehouseCodeSettingCreateRequest> { Success = false, Message = "仓库代号不能为空" });

            var entity = await _context.MyWhs.FirstOrDefaultAsync(x => x.WH == wh);
            if (entity == null)
            {
                return Ok(new ApiResult<WarehouseCodeSettingCreateRequest> { Success = false, Message = $"仓库 [{wh}] 不存在" });
            }

            var result = new WarehouseCodeSettingCreateRequest
            {
                // 基本信息
                Wh = entity.WH ?? "",
                Name = entity.NAME ?? "",
                Attrib = entity.ATTRIB ?? "",
                UpWh = entity.UP_WH ?? "",
                CntMan = entity.CNT_MAN ?? "",
                TelNo = entity.TEL_NO ?? "",
                FaxNo = entity.FAX_NO ?? "",
                StopDd = entity.STOP_DD,
                Dep = entity.DEP ?? "",
                DeproNo = entity.DEPRO_NO ?? "",
                Rem = entity.REM ?? "",

                // 仓库管理
                RkFlow = entity.RK_FLOW ?? "",
                PkFlow = entity.PK_FLOW ?? "",
                CkFlow = entity.CK_FLOW ?? "",
                IcType = entity.IC_TYPE ?? "",
                ModePgPk = entity.MODE_PG_PK ?? "",
                PdMth = entity.PD_MTH ?? "",
                XjBillCount = entity.XJ_BILL_COUNT,
                XjGroupCond = entity.XJ_GROUP_COND ?? "",
                XjKcbzcl = entity.XJ_KCBZCL ?? "",
                XjPwckyj = entity.XJ_PWCKYJ ?? "",
                XjWhs = entity.XJ_WHS ?? "",
                RuleIdBc = entity.RULE_ID_BC ?? "",
                RuleIdPk = entity.RULE_ID_PK ?? "",
                RuleIdXj = entity.RULE_ID_XJ ?? "",

                // 储位管理
                CwFlag = entity.CW_FLAG == "T",
                WhType = entity.WH_TYPE ?? "",
                Hjfl = entity.HJFL ?? "",
                MultCwBy = entity.MULT_CW_BY ?? "",
                ShuttleAqycf = entity.SHUTTLE_AQYCF == "T",
                ShuttleCfzlyj = entity.SHUTTLE_CFZLYJ ?? "",
                ShuttleGs = entity.SHUTTLE_GS ?? "",
                ShuttleSort = entity.SHUTTLE_SORT ?? "",
                RkChuwSort = entity.RK_CHUW_SORT ?? "",
                RkChuwSort2 = entity.RK_CHUW_SORT2 ?? "",
                KrqrkChuwSort = entity.KRQRK_CHUW_SORT ?? "",
                AllowKrqsj = entity.ALLOW_KRQSJ == "T",
                AllowBhrqsj = entity.ALLOW_BHRQSJ == "T",
                AllowStatusJy = entity.ALLOW_STATUS_JY ?? "",
                QtyKeepCw = entity.QTY_KEEP_CW,
                CapacityType = entity.CAPACITY_TYPE ?? "",
                FlagDg = entity.FLAG_DG == "T",
                FlagFkc = entity.FLAG_FKC == "T",
                PtlSw = entity.PTL_SW == "T",
                LkifId = entity.LKIF_ID ?? "",
                MapNo = entity.MAP_NO ?? ""
            };

            return Ok(new ApiResult<WarehouseCodeSettingCreateRequest>
            {
                Success = true,
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取仓库详情时发生错误: {Wh}", wh);
            return StatusCode(500, new ApiResult<WarehouseCodeSettingCreateRequest>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 新增仓库代号设定
    /// </summary>
    [HttpPost("create")]
    public async Task<ActionResult<ApiResult<object>>> Create([FromBody] WarehouseCodeSettingCreateRequest request)
    {
        try
        {
            // 校验仓库代号是否重复
            var existing = await _context.MyWhs.FirstOrDefaultAsync(x => x.WH == request.Wh);
            if (existing != null)
            {
                return Ok(new ApiResult<object>
                {
                    Success = false,
                    Message = $"仓库代号 [{request.Wh}] 已存在，请使用其他代号"
                });
            }

            var entity = new MyWh
            {
                // 基本信息
                WH = request.Wh,
                NAME = request.Name,
                ATTRIB = request.Attrib,
                UP_WH = request.UpWh,
                CNT_MAN = request.CntMan,
                TEL_NO = request.TelNo,
                FAX_NO = request.FaxNo,
                STOP_DD = request.StopDd,
                DEP = request.Dep,
                DEPRO_NO = request.DeproNo,
                REM = request.Rem,

                // 仓库管理
                RK_FLOW = request.RkFlow,
                PK_FLOW = request.PkFlow,
                CK_FLOW = request.CkFlow,
                IC_TYPE = request.IcType,
                MODE_PG_PK = request.ModePgPk,
                PD_MTH = request.PdMth,
                XJ_BILL_COUNT = request.XjBillCount,
                XJ_GROUP_COND = request.XjGroupCond,
                XJ_KCBZCL = request.XjKcbzcl,
                XJ_PWCKYJ = request.XjPwckyj,
                XJ_WHS = request.XjWhs,
                RULE_ID_BC = request.RuleIdBc,
                RULE_ID_PK = request.RuleIdPk,
                RULE_ID_XJ = request.RuleIdXj,

                // 储位管理
                CW_FLAG = request.CwFlag ? "T" : "F",
                WH_TYPE = request.WhType,
                HJFL = request.Hjfl,
                MULT_CW_BY = request.MultCwBy,
                SHUTTLE_AQYCF = request.ShuttleAqycf ? "T" : "F",
                SHUTTLE_CFZLYJ = request.ShuttleCfzlyj,
                SHUTTLE_GS = request.ShuttleGs,
                SHUTTLE_SORT = request.ShuttleSort,
                RK_CHUW_SORT = request.RkChuwSort,
                RK_CHUW_SORT2 = request.RkChuwSort2,
                KRQRK_CHUW_SORT = request.KrqrkChuwSort,
                ALLOW_KRQSJ = request.AllowKrqsj ? "T" : "F",
                ALLOW_BHRQSJ = request.AllowBhrqsj ? "T" : "F",
                ALLOW_STATUS_JY = request.AllowStatusJy,
                QTY_KEEP_CW = request.QtyKeepCw,
                CAPACITY_TYPE = request.CapacityType,
                FLAG_DG = request.FlagDg ? "T" : "F",
                FLAG_FKC = request.FlagFkc ? "T" : "F",
                PTL_SW = request.PtlSw ? "T" : "F",
                LKIF_ID = request.LkifId,
                MAP_NO = request.MapNo,

                // 系统字段
                USR = "ADMIN"
            };

            _context.MyWhs.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(new ApiResult<object>
            {
                Success = true,
                Message = "保存成功",
                Data = new { Wh = entity.WH }
            });
        }
        catch (Exception ex)
        {
            var innerMsg = ex.InnerException?.Message ?? "";
            var deepInner = ex.InnerException?.InnerException?.Message ?? "";
            _logger.LogError(ex, "新增仓库代号设定时发生错误: {Msg} | Inner: {Inner} | DeepInner: {DeepInner}",
                ex.Message, innerMsg, deepInner);
            return StatusCode(500, new ApiResult<object>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}" +
                          (!string.IsNullOrEmpty(innerMsg) ? $" | 内部异常: {innerMsg}" : "") +
                          (!string.IsNullOrEmpty(deepInner) ? $" | 深层异常: {deepInner}" : "")
            });
        }
    }

    /// <summary>
    /// 更新仓库代号设定
    /// </summary>
    [HttpPut("update")]
    public async Task<ActionResult<ApiResult<object>>> Update([FromBody] WarehouseCodeSettingCreateRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Wh))
                return BadRequest(new ApiResult<object> { Success = false, Message = "仓库代号不能为空" });

            var entity = await _context.MyWhs.FirstOrDefaultAsync(x => x.WH == request.Wh);
            if (entity == null)
            {
                return Ok(new ApiResult<object> { Success = false, Message = $"仓库代号 [{request.Wh}] 不存在，无法更新" });
            }

            // 基本信息
            entity.NAME = request.Name;
            entity.ATTRIB = request.Attrib;
            entity.UP_WH = request.UpWh;
            entity.CNT_MAN = request.CntMan;
            entity.TEL_NO = request.TelNo;
            entity.FAX_NO = request.FaxNo;
            entity.STOP_DD = request.StopDd;
            entity.DEP = request.Dep;
            entity.DEPRO_NO = request.DeproNo;
            entity.REM = request.Rem;

            // 仓库管理
            entity.RK_FLOW = request.RkFlow;
            entity.PK_FLOW = request.PkFlow;
            entity.CK_FLOW = request.CkFlow;
            entity.IC_TYPE = request.IcType;
            entity.MODE_PG_PK = request.ModePgPk;
            entity.PD_MTH = request.PdMth;
            entity.XJ_BILL_COUNT = request.XjBillCount;
            entity.XJ_GROUP_COND = request.XjGroupCond;
            entity.XJ_KCBZCL = request.XjKcbzcl;
            entity.XJ_PWCKYJ = request.XjPwckyj;
            entity.XJ_WHS = request.XjWhs;
            entity.RULE_ID_BC = request.RuleIdBc;
            entity.RULE_ID_PK = request.RuleIdPk;
            entity.RULE_ID_XJ = request.RuleIdXj;

            // 储位管理
            entity.CW_FLAG = request.CwFlag ? "T" : "F";
            entity.WH_TYPE = request.WhType;
            entity.HJFL = request.Hjfl;
            entity.MULT_CW_BY = request.MultCwBy;
            entity.SHUTTLE_AQYCF = request.ShuttleAqycf ? "T" : "F";
            entity.SHUTTLE_CFZLYJ = request.ShuttleCfzlyj;
            entity.SHUTTLE_GS = request.ShuttleGs;
            entity.SHUTTLE_SORT = request.ShuttleSort;
            entity.RK_CHUW_SORT = request.RkChuwSort;
            entity.RK_CHUW_SORT2 = request.RkChuwSort2;
            entity.KRQRK_CHUW_SORT = request.KrqrkChuwSort;
            entity.ALLOW_KRQSJ = request.AllowKrqsj ? "T" : "F";
            entity.ALLOW_BHRQSJ = request.AllowBhrqsj ? "T" : "F";
            entity.ALLOW_STATUS_JY = request.AllowStatusJy;
            entity.QTY_KEEP_CW = request.QtyKeepCw;
            entity.CAPACITY_TYPE = request.CapacityType;
            entity.FLAG_DG = request.FlagDg ? "T" : "F";
            entity.FLAG_FKC = request.FlagFkc ? "T" : "F";
            entity.PTL_SW = request.PtlSw ? "T" : "F";
            entity.LKIF_ID = request.LkifId;
            entity.MAP_NO = request.MapNo;

            // 系统字段
            entity.UP_DD = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok(new ApiResult<object>
            {
                Success = true,
                Message = "修改成功"
            });
        }
        catch (Exception ex)
        {
            var innerMsg = ex.InnerException?.Message ?? "";
            var deepInner = ex.InnerException?.InnerException?.Message ?? "";
            _logger.LogError(ex, "更新仓库代号设定时发生错误: {Wh} | Msg: {Msg} | Inner: {Inner} | DeepInner: {DeepInner}",
                request.Wh, ex.Message, innerMsg, deepInner);
            return StatusCode(500, new ApiResult<object>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}" +
                          (!string.IsNullOrEmpty(innerMsg) ? $" | 内部异常: {innerMsg}" : "") +
                          (!string.IsNullOrEmpty(deepInner) ? $" | 深层异常: {deepInner}" : "")
            });
        }
    }

    /// <summary>
    /// 删除仓库代号设定
    /// </summary>
    [HttpDelete("{wh}")]
    public async Task<ActionResult<ApiResult<object>>> Delete(string wh)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(wh))
                return BadRequest(new ApiResult<object> { Success = false, Message = "仓库代号不能为空" });

            var entity = await _context.MyWhs.FirstOrDefaultAsync(x => x.WH == wh);
            if (entity == null)
            {
                return Ok(new ApiResult<object> { Success = false, Message = $"仓库代号 [{wh}] 不存在" });
            }

            _context.MyWhs.Remove(entity);
            await _context.SaveChangesAsync();

            return Ok(new ApiResult<object> { Success = true, Message = "删除成功" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除仓库代号设定时发生错误: {Wh}", wh);
            return StatusCode(500, new ApiResult<object>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

// ====== DTO 定义 ======

/// <summary>仓库代号设定列表行数据（对应前端表格）</summary>
public class WarehouseCodeSettingDto
{
    public int Seq { get; set; }
    public string Wh { get; set; } = "";
    public string Name { get; set; } = "";
    public string Attrib { get; set; } = "";
    public string CwFlag { get; set; } = "";
    public string WhType { get; set; } = "";
    public string Dep { get; set; } = "";
    public string DepName { get; set; } = "";
    public DateTime? StopDd { get; set; }
    public string UpWh { get; set; } = "";
    public string UpWhName { get; set; } = "";
}

/// <summary>仓库代号设定新增/编辑请求体</summary>
public class WarehouseCodeSettingCreateRequest
{
    // ===== 基本信息 =====
    public string Wh { get; set; } = "";              // 仓库代号*
    public string Name { get; set; } = "";            // 仓库名称*
    public string Attrib { get; set; } = "";          // 属性
    public string UpWh { get; set; } = "";            // 上层仓库
    public string CntMan { get; set; } = "";          // 联系人
    public string TelNo { get; set; } = "";           // 电话
    public string FaxNo { get; set; } = "";           // 传真
    public DateTime? StopDd { get; set; }             // 停用日期
    public string Dep { get; set; } = "";             // 所属部门
    public string DeproNo { get; set; } = "";         // 部门群组
    public string Rem { get; set; } = "";             // 备注

    // ===== 仓库管理 =====
    public string RkFlow { get; set; } = "";          // 出库流程
    public string PkFlow { get; set; } = "";          // 拣货流程
    public string CkFlow { get; set; } = "";          // 库存不足管制
    public string IcType { get; set; } = "";          // 调拨方式
    public string ModePgPk { get; set; } = "";        // 拣货派工方式
    public string PdMth { get; set; } = "";           // 盘点方式
    public int? XjBillCount { get; set; }             // 分组单据容量
    public string XjGroupCond { get; set; } = "";     // 自动分组条件
    public string XjKcbzcl { get; set; } = "";        // 库存不足处理
    public string XjPwckyj { get; set; } = "";        // 配位仓库依据
    public string XjWhs { get; set; } = "";           // 指定仓库
    public string RuleIdBc { get; set; } = "";        // 波次策略代号
    public string RuleIdPk { get; set; } = "";        // 拣货策略代号
    public string RuleIdXj { get; set; } = "";        // 下架策略代号

    // ===== 储位管理 =====
    public bool CwFlag { get; set; }                  // 启用储位管理
    public string WhType { get; set; } = "";          // 上下架模式
    public string Hjfl { get; set; } = "";            // 货架分类
    public string MultCwBy { get; set; } = "";        // 多深位依据
    public bool ShuttleAqycf { get; set; }            // 按区域设定存放规则
    public string ShuttleCfzlyj { get; set; } = "";   // 巷道存放种类依据
    public string ShuttleGs { get; set; } = "";       // 巷道规则
    public string ShuttleSort { get; set; } = "";     // 配位顺序
    public string RkChuwSort { get; set; } = "";      // 入库储位配位排序（非穿梭式）
    public string RkChuwSort2 { get; set; } = "";     // 入库储位配位排序（穿梭式）
    public string KrqrkChuwSort { get; set; } = "";   // 空容器入库储位配位排序
    public bool AllowKrqsj { get; set; }              // 空容器允许上架
    public bool AllowBhrqsj { get; set; }             // 启用备货容器上架管理
    public string AllowStatusJy { get; set; } = "";   // 允许上架的检验状态
    public int? QtyKeepCw { get; set; }               // 预留空储位数
    public string CapacityType { get; set; } = "";    // 储位容量管制方式
    public bool FlagDg { get; set; }                  // 是代管仓
    public bool FlagFkc { get; set; }                 // 负库存控制
    public bool PtlSw { get; set; }                   // 启用PTL电子标签拣货
    public string LkifId { get; set; } = "";          // 立库接口代号
    public string MapNo { get; set; } = "";           // 地图编号
}
