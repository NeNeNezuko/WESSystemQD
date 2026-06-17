using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductCodeSettingController : ControllerBase
{
    private readonly WarehouseDbContext _context;
    private readonly ILogger<ProductCodeSettingController> _logger;

    public ProductCodeSettingController(WarehouseDbContext context, ILogger<ProductCodeSettingController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// 查询货品代号设定列表
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<ProductCodeSettingDto>>>> Search(
        [FromQuery] string? prdNo,
        [FromQuery] string? name,
        [FromQuery] string? idx1,
        [FromQuery] string? dep,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var query = _context.Prdts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(prdNo))
                query = query.Where(x => x.PRD_NO != null && x.PRD_NO.Contains(prdNo));

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(name));

            if (!string.IsNullOrWhiteSpace(idx1))
                query = query.Where(x => x.IDX1 != null && x.IDX1.Contains(idx1));

            if (!string.IsNullOrWhiteSpace(dep))
                query = query.Where(x => x.DEP != null && x.DEP.Contains(dep));

            var totalCount = await query.CountAsync();

            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 20;

            query = query.OrderBy(x => x.PRD_NO)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize);

            var list = await query.Select(x => new ProductCodeSettingDto
            {
                PrdNo = x.PRD_NO ?? "",
                Name = x.NAME ?? "",
                Snm = x.SNM ?? "",
                Idx1 = x.IDX1 ?? "",
                Ut = x.UT ?? "",
                Ut1 = x.UT1 ?? "",
                Spc = x.SPC ?? "",
                Knd = x.KND ?? "",
                CwxzNo = x.CWXZ_NO ?? "",
                Dep = x.DEP ?? "",
                NouseDd = x.NOUSE_DD,
                Pkg1Ut = x.PK2_UT ?? "",
                Pkg1QtyRaw = x.PK2_QTY,
                Pkg2Ut = x.PK3_UT ?? "",
                Pkg2QtyRaw = x.PK3_QTY
            }).ToListAsync();

            // 将可空数量转为字符串
            foreach (var item in list)
            {
                item.Pkg1Qty = item.Pkg1QtyRaw?.ToString() ?? "";
                item.Pkg2Qty = item.Pkg2QtyRaw?.ToString() ?? "";
            }

            // 查询部门名称
            var depCodes = list.Where(x => !string.IsNullOrWhiteSpace(x.Dep)).Select(x => x.Dep).Distinct().ToList();
            var depDict = new Dictionary<string, string>();
            if (depCodes.Count > 0)
            {
                depDict = await _context.Depts
                    .Where(d => depCodes.Contains(d.DEP))
                    .ToDictionaryAsync(d => d.DEP, d => d.NAME ?? "");
            }

            var seq = (pageIndex - 1) * pageSize;
            var result = list.Select(x => new ProductCodeSettingDto
            {
                Seq = ++seq,
                PrdNo = x.PrdNo,
                Name = x.Name,
                Snm = x.Snm,
                Idx1 = x.Idx1,
                Ut = x.Ut,
                Ut1 = x.Ut1,
                Spc = x.Spc,
                Knd = x.Knd,
                CwxzNo = x.CwxzNo,
                Dep = x.Dep,
                DepName = depDict.GetValueOrDefault(x.Dep, ""),
                NouseDd = x.NouseDd,
                Pkg1Ut = x.Pkg1Ut,
                Pkg1Qty = x.Pkg1Qty,
                Pkg2Ut = x.Pkg2Ut,
                Pkg2Qty = x.Pkg2Qty
            }).ToList();

            return Ok(new ApiResult<List<ProductCodeSettingDto>>
            {
                Success = true,
                Data = result,
                Total = totalCount
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询货品代号设定时发生错误");
            return StatusCode(500, new ApiResult<List<ProductCodeSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 删除货品代号设定
    /// </summary>
    [HttpDelete("{prdNo}")]
    public async Task<ActionResult<ApiResult<object>>> Delete(string prdNo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(prdNo))
                return BadRequest(new ApiResult<object> { Success = false, Message = "货品代号不能为空" });

            var entity = await _context.Prdts.FirstOrDefaultAsync(x => x.PRD_NO == prdNo);
            if (entity == null)
            {
                return Ok(new ApiResult<object> { Success = false, Message = $"货品 [{prdNo}] 不存在" });
            }

            _context.Prdts.Remove(entity);
            await _context.SaveChangesAsync();

            return Ok(new ApiResult<object> { Success = true, Message = "删除成功" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除货品代号设定时发生错误: {PrdNo}", prdNo);
            return StatusCode(500, new ApiResult<object>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据货品代号获取详情（编辑回填用）
    /// </summary>
    [HttpGet("getByPrdNo")]
    public async Task<ActionResult<ApiResult<ProductCodeSettingCreateRequest>>> GetByPrdNo([FromQuery] string prdNo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(prdNo))
                return BadRequest(new ApiResult<ProductCodeSettingCreateRequest> { Success = false, Message = "货品代号不能为空" });

            var entity = await _context.Prdts.FirstOrDefaultAsync(x => x.PRD_NO == prdNo);
            if (entity == null)
            {
                return Ok(new ApiResult<ProductCodeSettingCreateRequest> { Success = false, Message = $"货品 [{prdNo}] 不存在" });
            }

            var result = new ProductCodeSettingCreateRequest
            {
                // 一般资料
                PrdNo = entity.PRD_NO ?? "",
                Name = entity.NAME ?? "",
                Snm = entity.SNM ?? "",
                NameEng = entity.NAME_ENG ?? "",
                Spc = entity.SPC ?? "",
                UsrWh = entity.USR_WH ?? "",
                ChkBat = entity.CHK_BAT == "T",
                ChkNum = entity.CHK_NUM == "T",
                Knd = entity.KND ?? "",
                Idx1 = entity.IDX1 ?? "",
                Ut = entity.UT ?? "",
                Ut1 = entity.UT1 ?? "",
                Rem = entity.REM ?? "",

                // 其他资料
                Wh = entity.WH ?? "",
                Chuw = entity.CHUW ?? "",
                Dep = entity.DEP ?? "",
                ValidId = entity.VALID_ID ?? "",
                ValidDays = entity.VALID_DAYS,
                StartDD = entity.START_DD,
                NouseDD = entity.NOUSE_DD,
                TplNo = entity.TPL_NO ?? "",
                MobId = entity.MOB_ID ?? "",
                CfProp = entity.CF_PROP ?? "",
                NotBarcode = entity.NOT_BARCODE == "T",
                AllowShqFh = entity.ALLOW_SHQ_FH == "T",
                CwctrlId = entity.CWCTRL_ID ?? "",
                CwxzNo = entity.CWXZ_NO ?? "",
                RtoPc = entity.RTO_PC ?? 0m,
                RtoMm = entity.RTO_MM ?? 0m,
                RtoTb = entity.RTO_TB ?? 0m,
                RtoSa = entity.RTO_SA ?? 0m,

                // 包装资料
                Pk2Ut = entity.PK2_UT ?? "",
                Pk2Qty = entity.PK2_QTY,
                Pk3Ut = entity.PK3_UT ?? "",
                Pk3Qty = entity.PK3_QTY,
                QtyWeight = entity.QTY_WEIGHT ?? 0m,
                UnitWeight = entity.UNIT_WEIGHT ?? "",
                MlUt = entity.ML_UT ?? "",
                PakUnit = entity.PAK_UNIT ?? "",
                PakExc = entity.PAK_EXC ?? 0m,
                PakNw = entity.PAK_NW ?? 0m,
                PakWeightUnit = entity.PAK_WEIGHT_UNIT ?? "",
                PakGw = entity.PAK_GW ?? 0m,
                PakMeast = entity.PAK_MEAST ?? "",
                PakMeastUnit = entity.PAK_MEAST_UNIT ?? "",
                EffectId = entity.EFFECT_ID ?? "",

                // 品质检验
                TyIn = (entity.NEED_CHK_FLAG?.Contains("I") == true),
                TyOut = (entity.NEED_CHK_FLAG?.Contains("O") == true),
                TyStock = (entity.NEED_CHK_FLAG?.Contains("S") == true),
                TyInr = int.TryParse(entity.TY_INR, out var tir) ? tir : 30
            };

            return Ok(new ApiResult<ProductCodeSettingCreateRequest>
            {
                Success = true,
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取货品详情时发生错误: {PrdNo}", prdNo);
            return StatusCode(500, new ApiResult<ProductCodeSettingCreateRequest>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 更新货品代号设定
    /// </summary>
    [HttpPut("update")]
    public async Task<ActionResult<ApiResult<object>>> Update([FromBody] ProductCodeSettingCreateRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.PrdNo))
                return BadRequest(new ApiResult<object> { Success = false, Message = "货品代号不能为空" });

            var entity = await _context.Prdts.FirstOrDefaultAsync(x => x.PRD_NO == request.PrdNo);
            if (entity == null)
            {
                return Ok(new ApiResult<object> { Success = false, Message = $"货品 [{request.PrdNo}] 不存在，无法更新" });
            }

            // 一般资料
            entity.NAME = request.Name;
            entity.SNM = request.Snm;
            entity.NAME_ENG = request.NameEng;
            entity.SPC = request.Spc;
            entity.USR_WH = request.UsrWh;
            entity.CHK_BAT = request.ChkBat ? "T" : "F";
            entity.CHK_NUM = request.ChkNum ? "T" : "F";
            entity.KND = request.Knd;
            entity.IDX1 = request.Idx1;
            entity.UT = request.Ut;
            entity.UT1 = request.Ut1;
            entity.REM = request.Rem;

            // 其他资料
            entity.WH = request.Wh;
            entity.CHUW = request.Chuw;
            entity.DEP = request.Dep;
            entity.VALID_ID = request.ValidId;
            entity.VALID_DAYS = request.ValidDays;
            entity.START_DD = request.StartDD;
            entity.NOUSE_DD = request.NouseDD;
            entity.TPL_NO = request.TplNo;
            entity.MOB_ID = request.MobId;
            entity.CF_PROP = request.CfProp;
            entity.NOT_BARCODE = request.NotBarcode ? "T" : "F";
            entity.ALLOW_SHQ_FH = request.AllowShqFh ? "T" : "F";
            entity.CWCTRL_ID = request.CwctrlId;
            entity.CWXZ_NO = request.CwxzNo;
            entity.RTO_PC = request.RtoPc;
            entity.RTO_MM = request.RtoMm;
            entity.RTO_TB = request.RtoTb;
            entity.RTO_SA = request.RtoSa;

            // 包装资料
            entity.PK2_UT = request.Pk2Ut;
            entity.PK2_QTY = request.Pk2Qty;
            entity.PK3_UT = request.Pk3Ut;
            entity.PK3_QTY = request.Pk3Qty;
            entity.QTY_WEIGHT = request.QtyWeight;
            entity.UNIT_WEIGHT = request.UnitWeight;
            entity.ML_UT = request.MlUt;
            entity.PAK_UNIT = request.PakUnit;
            entity.PAK_EXC = request.PakExc;
            entity.PAK_NW = request.PakNw;
            entity.PAK_WEIGHT_UNIT = request.PakWeightUnit;
            entity.PAK_GW = request.PakGw;
            entity.PAK_MEAST = request.PakMeast;
            entity.PAK_MEAST_UNIT = request.PakMeastUnit;
            entity.EFFECT_ID = request.EffectId;

            // 品质检验
            entity.NEED_CHK_FLAG = (request.TyIn || request.TyOut || request.TyStock) ? "Y" : "";
            entity.TY_INR = request.TyInr.ToString();

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
            _logger.LogError(ex, "更新货品代号设定时发生错误: {PrdNo} | Msg: {Msg} | Inner: {Inner} | DeepInner: {DeepInner}",
                request.PrdNo, ex.Message, innerMsg, deepInner);
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
    /// 新增货品代号设定
    /// </summary>
    [HttpPost("create")]
    public async Task<ActionResult<ApiResult<object>>> Create([FromBody] ProductCodeSettingCreateRequest request)
    {
        try
        {
            // 校验物料编码是否重复
            var existing = await _context.Prdts.FirstOrDefaultAsync(x => x.PRD_NO == request.PrdNo);
            if (existing != null)
            {
                return Ok(new ApiResult<object>
                {
                    Success = false,
                    Message = $"物料编码 [{request.PrdNo}] 已存在，请使用其他编码"
                });
            }

            var entity = new Prdt
            {
                // 一般资料
                PRD_NO = request.PrdNo,
                NAME = request.Name,
                SNM = request.Snm,
                NAME_ENG = request.NameEng,
                SPC = request.Spc,
                USR_WH = request.UsrWh,
                CHK_BAT = request.ChkBat ? "T" : "F",
                CHK_NUM = request.ChkNum ? "T" : "F",
                KND = request.Knd,
                IDX1 = request.Idx1,
                UT = request.Ut,
                UT1 = request.Ut1,
                REM = request.Rem,

                // 其他资料
                WH = request.Wh,
                CHUW = request.Chuw,
                DEP = request.Dep,
                VALID_ID = request.ValidId,
                VALID_DAYS = request.ValidDays,
                START_DD = request.StartDD,
                NOUSE_DD = request.NouseDD,
                TPL_NO = request.TplNo,
                MOB_ID = request.MobId,
                CF_PROP = request.CfProp,
                NOT_BARCODE = request.NotBarcode ? "T" : "F",
                ALLOW_SHQ_FH = request.AllowShqFh ? "T" : "F",
                CWCTRL_ID = request.CwctrlId,
                CWXZ_NO = request.CwxzNo,
                RTO_PC = request.RtoPc,
                RTO_MM = request.RtoMm,
                RTO_TB = request.RtoTb,
                RTO_SA = request.RtoSa,

                // 包装资料
                PK2_UT = request.Pk2Ut,
                PK2_QTY = request.Pk2Qty,
                PK3_UT = request.Pk3Ut,
                PK3_QTY = request.Pk3Qty,
                QTY_WEIGHT = request.QtyWeight,
                UNIT_WEIGHT = request.UnitWeight,
                ML_UT = request.MlUt,
                PAK_UNIT = request.PakUnit,
                PAK_EXC = request.PakExc,
                PAK_NW = request.PakNw,
                PAK_WEIGHT_UNIT = request.PakWeightUnit,
                PAK_GW = request.PakGw,
                PAK_MEAST = request.PakMeast,
                PAK_MEAST_UNIT = request.PakMeastUnit,
                EFFECT_ID = request.EffectId,

                // 品质检验
                NEED_CHK_FLAG = (request.TyIn || request.TyOut || request.TyStock) ? "Y" : "",
                TY_INR = request.TyInr.ToString(),

                // 系统字段
                USR = "ADMIN"
            };

            _context.Prdts.Add(entity);
            await _context.SaveChangesAsync();

            return Ok(new ApiResult<object>
            {
                Success = true,
                Message = "保存成功",
                Data = new { PrdNo = entity.PRD_NO }
            });
        }
        catch (Exception ex)
        {
            // 逐层输出内部异常，便于定位数据库约束/类型不匹配等根因
            var innerMsg = ex.InnerException?.Message ?? "";
            var deepInner = ex.InnerException?.InnerException?.Message ?? "";
            _logger.LogError(ex, "新增货品代号设定时发生错误: {Msg} | Inner: {Inner} | DeepInner: {DeepInner}",
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
}

/// <summary>货品代号设定DTO</summary>
public class ProductCodeSettingDto
{
    public int Seq { get; set; }
    public string PrdNo { get; set; } = "";
    public string Name { get; set; } = "";
    public string Snm { get; set; } = "";
    public string Idx1 { get; set; } = "";
    public string Ut { get; set; } = "";
    public string Ut1 { get; set; } = "";
    public string Spc { get; set; } = "";
    public string Knd { get; set; } = "";
    public string CwxzNo { get; set; } = "";
    public string Dep { get; set; } = "";
    public string DepName { get; set; } = "";
    public string Pkg1Ut { get; set; } = "";
    public string Pkg1Qty { get; set; } = "";
    public string Pkg2Ut { get; set; } = "";
    public string Pkg2Qty { get; set; } = "";
    public DateTime? NouseDd { get; set; }
    // 内部用于接收数据库值
    [System.Text.Json.Serialization.JsonIgnore]
    public int? Pkg1QtyRaw { get; set; }
    [System.Text.Json.Serialization.JsonIgnore]
    public int? Pkg2QtyRaw { get; set; }
}

/// <summary>新增货品请求体</summary>
public class ProductCodeSettingCreateRequest
{
    // 一般资料
    public string PrdNo { get; set; } = "";
    public string Name { get; set; } = "";
    public string Snm { get; set; } = "";
    public string NameEng { get; set; } = "";
    public string Spc { get; set; } = "";
    public string UsrWh { get; set; } = "";
    public bool ChkBat { get; set; }
    public bool ChkNum { get; set; }
    public string Knd { get; set; } = "";
    public string Idx1 { get; set; } = "";
    public string Ut { get; set; } = "";
    public string Ut1 { get; set; } = "";
    public string Rem { get; set; } = "";

    // 其他资料
    public string Wh { get; set; } = "";
    public string Chuw { get; set; } = "";
    public string Dep { get; set; } = "";
    public string ValidId { get; set; } = "";
    public int? ValidDays { get; set; }
    public DateTime? StartDD { get; set; }
    public DateTime? NouseDD { get; set; }
    public string TplNo { get; set; } = "";
    public string MobId { get; set; } = "";
    public string CfProp { get; set; } = "";
    public bool NotBarcode { get; set; }
    public bool AllowShqFh { get; set; }
    public string CwctrlId { get; set; } = "";
    public string CwxzNo { get; set; } = "";
    public decimal RtoPc { get; set; }
    public decimal RtoMm { get; set; }
    public decimal RtoTb { get; set; }
    public decimal RtoSa { get; set; }

    // 包装资料
    public string Pk2Ut { get; set; } = "";
    public int? Pk2Qty { get; set; }
    public string Pk3Ut { get; set; } = "";
    public int? Pk3Qty { get; set; }
    public decimal QtyWeight { get; set; }
    public string UnitWeight { get; set; } = "";
    public string MlUt { get; set; } = "";
    public string PakUnit { get; set; } = "";
    public decimal PakExc { get; set; }
    public decimal PakNw { get; set; }
    public string PakWeightUnit { get; set; } = "";
    public decimal PakGw { get; set; }
    public string PakMeast { get; set; } = "";
    public string PakMeastUnit { get; set; } = "";
    public string EffectId { get; set; } = "";

    // 品质检验
    public bool TyIn { get; set; }
    public bool TyOut { get; set; }
    public bool TyStock { get; set; }
    public int TyInr { get; set; } = 30;
}
