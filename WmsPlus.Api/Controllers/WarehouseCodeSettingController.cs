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

            // 只Select非日期字段，避免EF Core读取MySQL日期列时Byte[]→DateTime转换异常
            var list = await query.Select(x => new WarehouseCodeSettingDto
            {
                Wh = x.WH,
                Name = x.NAME ?? "",
                Attrib = x.ATTRIB ?? "",
                CwFlag = x.CW_FLAG ?? "",
                WhType = x.WH_TYPE ?? "",
                Dep = x.DEP ?? "",
                StopDd = "",  // 日期字段单独处理
                UpWh = x.UP_WH ?? ""
            }).ToListAsync();

            // 查询部门名称
            var depCodes = list.Where(x => !string.IsNullOrWhiteSpace(x.Dep)).Select(x => x.Dep).Distinct().ToList();
            var depDict = new Dictionary<string, string>();
            if (depCodes.Count > 0)
            {
                var depList = await _context.Depts
                    .Where(d => depCodes.Contains(d.DEP))
                    .Select(d => new { d.DEP, d.NAME })
                    .ToListAsync();
                depDict = depList.ToDictionary(d => d.DEP, d => d.NAME ?? "");
            }

            // 查询上层仓库名称（只Select非日期字段避免Byte[]→DateTime异常）
            var upWhCodes = list.Where(x => !string.IsNullOrWhiteSpace(x.UpWh)).Select(x => x.UpWh).Distinct().ToList();
            var upWhDict = new Dictionary<string, string>();
            if (upWhCodes.Count > 0)
            {
                var upWhList = await _context.MyWhs
                    .Where(w => upWhCodes.Contains(w.WH))
                    .Select(w => new { w.WH, w.NAME })
                    .ToListAsync();
                upWhDict = upWhList.ToDictionary(w => w.WH, w => w.NAME ?? "");
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

            // 使用Select避免读取日期字段导致Byte[]→DateTime转换异常
            var entity = await _context.MyWhs
                .Where(x => x.WH == wh)
                .Select(x => new
                {
                    x.WH, x.NAME, x.ATTRIB, x.UP_WH, x.CNT_MAN, x.TEL_NO, x.FAX_NO,
                    x.DEP, x.DEPRO_NO, x.REM,
                    x.RK_FLOW, x.PK_FLOW, x.CK_FLOW, x.IC_TYPE, x.MODE_PG_PK, x.PD_MTH,
                    x.XJ_BILL_COUNT, x.XJ_GROUP_COND, x.XJ_KCBZCL, x.XJ_PWCKYJ, x.XJ_WHS,
                    x.RULE_ID_BC, x.RULE_ID_PK, x.RULE_ID_XJ,
                    x.CW_FLAG, x.WH_TYPE, x.HJFL, x.MULT_CW_BY,
                    x.SHUTTLE_AQYCF, x.SHUTTLE_CFZLYJ, x.SHUTTLE_GS, x.SHUTTLE_SORT,
                    x.RK_CHUW_SORT, x.RK_CHUW_SORT2, x.KRQRK_CHUW_SORT,
                    x.ALLOW_KRQSJ, x.ALLOW_BHRQSJ, x.ALLOW_STATUS_JY,
                    x.QTY_KEEP_CW, x.CAPACITY_TYPE, x.FLAG_DG, x.FLAG_FKC, x.PTL_SW,
                    x.LKIF_ID, x.MAP_NO
                })
                .FirstOrDefaultAsync();
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
                StopDd = null,  // 日期字段暂不回填
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
            // 校验仓库代号不能为空
            if (string.IsNullOrWhiteSpace(request.Wh))
            {
                return Ok(new ApiResult<object>
                {
                    Success = false,
                    Message = "仓库代号不能为空"
                });
            }

            // 校验仓库代号是否重复（使用原始SQL避免EF Core读取日期列异常）
            var existingCount = await _context.Database.SqlQueryRaw<int>(
                "SELECT COUNT(*) AS Value FROM MY_WH WHERE WH = {0}", request.Wh)
                .FirstOrDefaultAsync();
            if (existingCount > 0)
            {
                return Ok(new ApiResult<object>
                {
                    Success = false,
                    Message = $"仓库代号 [{request.Wh}] 已存在，请使用其他代号"
                });
            }

            // 使用原始SQL插入避免EF Core读取日期字段异常
            var stopDdValue = request.StopDd?.ToString("yyyy-MM-dd");
            var sql = @"INSERT INTO MY_WH (
                WH, NAME, ATTRIB, UP_WH, CNT_MAN, TEL_NO, FAX_NO,
                STOP_DD, DEP, DEPRO_NO, REM,
                RK_FLOW, PK_FLOW, CK_FLOW, IC_TYPE, MODE_PG_PK, PD_MTH,
                XJ_BILL_COUNT, XJ_GROUP_COND, XJ_KCBZCL, XJ_PWCKYJ, XJ_WHS,
                RULE_ID_BC, RULE_ID_PK, RULE_ID_XJ,
                CW_FLAG, WH_TYPE, HJFL, MULT_CW_BY,
                SHUTTLE_AQYCF, SHUTTLE_CFZLYJ, SHUTTLE_GS, SHUTTLE_SORT,
                RK_CHUW_SORT, RK_CHUW_SORT2, KRQRK_CHUW_SORT,
                ALLOW_KRQSJ, ALLOW_BHRQSJ, ALLOW_STATUS_JY,
                QTY_KEEP_CW, CAPACITY_TYPE, FLAG_DG, FLAG_FKC, PTL_SW,
                LKIF_ID, MAP_NO, USR, SYS_DATE
            ) VALUES (
                @p0, @p1, @p2, @p3, @p4, @p5, @p6,
                @p7, @p8, @p9, @p10,
                @p11, @p12, @p13, @p14, @p15, @p16,
                @p17, @p18, @p19, @p20, @p21,
                @p22, @p23, @p24,
                @p25, @p26, @p27, @p28,
                @p29, @p30, @p31, @p32,
                @p33, @p34, @p35,
                @p36, @p37, @p38,
                @p39, @p40, @p41, @p42, @p43,
                @p44, @p45, @p46, @p47
            )";

            var parameters = new object[]
            {
                request.Wh ?? "",
                request.Name ?? "",
                request.Attrib ?? "",
                request.UpWh ?? "",
                request.CntMan ?? "",
                request.TelNo ?? "",
                request.FaxNo ?? "",
                (object?)stopDdValue ?? (object?)null,
                request.Dep ?? "",
                request.DeproNo ?? "",
                request.Rem ?? "",
                request.RkFlow ?? "",
                request.PkFlow ?? "",
                request.CkFlow ?? "",
                request.IcType ?? "",
                request.ModePgPk ?? "",
                request.PdMth ?? "",
                (object?)request.XjBillCount ?? (object?)null,
                request.XjGroupCond ?? "",
                request.XjKcbzcl ?? "",
                request.XjPwckyj ?? "",
                request.XjWhs ?? "",
                request.RuleIdBc ?? "",
                request.RuleIdPk ?? "",
                request.RuleIdXj ?? "",
                request.CwFlag ? "T" : "F",
                request.WhType ?? "",
                request.Hjfl ?? "",
                request.MultCwBy ?? "",
                request.ShuttleAqycf ? "T" : "F",
                request.ShuttleCfzlyj ?? "",
                request.ShuttleGs ?? "",
                request.ShuttleSort ?? "",
                request.RkChuwSort ?? "",
                request.RkChuwSort2 ?? "",
                request.KrqrkChuwSort ?? "",
                request.AllowKrqsj ? "T" : "F",
                request.AllowBhrqsj ? "T" : "F",
                request.AllowStatusJy ?? "",
                (object?)request.QtyKeepCw ?? (object?)null,
                request.CapacityType ?? "",
                request.FlagDg ? "T" : "F",
                request.FlagFkc ? "T" : "F",
                request.PtlSw ? "T" : "F",
                request.LkifId ?? "",
                request.MapNo ?? "",
                "ADMIN",
                DateTime.Now
            };

            await _context.Database.ExecuteSqlRawAsync(sql, parameters);

            return Ok(new ApiResult<object>
            {
                Success = true,
                Message = "保存成功",
                Data = new { Wh = request.Wh }
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

            // 使用原始SQL查询避免EF Core读取日期字段异常
            var exists = await _context.Database.SqlQueryRaw<int>(
                "SELECT COUNT(*) AS Value FROM MY_WH WHERE WH = {0}", request.Wh)
                .FirstOrDefaultAsync();
            if (exists == 0)
            {
                return Ok(new ApiResult<object> { Success = false, Message = $"仓库代号 [{request.Wh}] 不存在，无法更新" });
            }

            // 使用原始SQL更新，避免EF Core读取日期字段异常
            var stopDdValue = request.StopDd?.ToString("yyyy-MM-dd");
            var sql = @"UPDATE MY_WH SET
                NAME=@p0, ATTRIB=@p1, UP_WH=@p2, CNT_MAN=@p3, TEL_NO=@p4, FAX_NO=@p5,
                STOP_DD=@p6, DEP=@p7, DEPRO_NO=@p8, REM=@p9,
                RK_FLOW=@p10, PK_FLOW=@p11, CK_FLOW=@p12, IC_TYPE=@p13, MODE_PG_PK=@p14,
                PD_MTH=@p15, XJ_BILL_COUNT=@p16, XJ_GROUP_COND=@p17, XJ_KCBZCL=@p18,
                XJ_PWCKYJ=@p19, XJ_WHS=@p20, RULE_ID_BC=@p21, RULE_ID_PK=@p22, RULE_ID_XJ=@p23,
                CW_FLAG=@p24, WH_TYPE=@p25, HJFL=@p26, MULT_CW_BY=@p27,
                SHUTTLE_AQYCF=@p28, SHUTTLE_CFZLYJ=@p29, SHUTTLE_GS=@p30, SHUTTLE_SORT=@p31,
                RK_CHUW_SORT=@p32, RK_CHUW_SORT2=@p33, KRQRK_CHUW_SORT=@p34,
                ALLOW_KRQSJ=@p35, ALLOW_BHRQSJ=@p36, ALLOW_STATUS_JY=@p37,
                QTY_KEEP_CW=@p38, CAPACITY_TYPE=@p39, FLAG_DG=@p40, FLAG_FKC=@p41,
                PTL_SW=@p42, LKIF_ID=@p43, MAP_NO=@p44, UP_DD=@p45
                WHERE WH=@p46";

            var parameters = new object[]
            {
                request.Name ?? "",
                request.Attrib ?? "",
                request.UpWh ?? "",
                request.CntMan ?? "",
                request.TelNo ?? "",
                request.FaxNo ?? "",
                (object?)stopDdValue ?? (object?)null,
                request.Dep ?? "",
                request.DeproNo ?? "",
                request.Rem ?? "",
                request.RkFlow ?? "",
                request.PkFlow ?? "",
                request.CkFlow ?? "",
                request.IcType ?? "",
                request.ModePgPk ?? "",
                request.PdMth ?? "",
                (object?)request.XjBillCount ?? (object?)null,
                request.XjGroupCond ?? "",
                request.XjKcbzcl ?? "",
                request.XjPwckyj ?? "",
                request.XjWhs ?? "",
                request.RuleIdBc ?? "",
                request.RuleIdPk ?? "",
                request.RuleIdXj ?? "",
                request.CwFlag ? "T" : "F",
                request.WhType ?? "",
                request.Hjfl ?? "",
                request.MultCwBy ?? "",
                request.ShuttleAqycf ? "T" : "F",
                request.ShuttleCfzlyj ?? "",
                request.ShuttleGs ?? "",
                request.ShuttleSort ?? "",
                request.RkChuwSort ?? "",
                request.RkChuwSort2 ?? "",
                request.KrqrkChuwSort ?? "",
                request.AllowKrqsj ? "T" : "F",
                request.AllowBhrqsj ? "T" : "F",
                request.AllowStatusJy ?? "",
                (object?)request.QtyKeepCw ?? (object?)null,
                request.CapacityType ?? "",
                request.FlagDg ? "T" : "F",
                request.FlagFkc ? "T" : "F",
                request.PtlSw ? "T" : "F",
                request.LkifId ?? "",
                request.MapNo ?? "",
                DateTime.Now,
                request.Wh
            };

            await _context.Database.ExecuteSqlRawAsync(sql, parameters);

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

            // 使用原始SQL删除避免EF Core读取日期字段异常
            var exists = await _context.Database.SqlQueryRaw<int>(
                "SELECT COUNT(*) AS Value FROM MY_WH WHERE WH = {0}", wh)
                .FirstOrDefaultAsync();
            if (exists == 0)
            {
                return Ok(new ApiResult<object> { Success = false, Message = $"仓库代号 [{wh}] 不存在" });
            }

            await _context.Database.ExecuteSqlRawAsync("DELETE FROM MY_WH WHERE WH = {0}", wh);

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
    public string? StopDd { get; set; }
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
