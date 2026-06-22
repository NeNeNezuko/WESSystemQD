using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;
using WmsPlus.Api.Utils;

namespace WmsPlus.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserSettingController : ControllerBase
{
    private readonly AppDbContext _appDb;
    private readonly WarehouseDbContext _whDb;
    private readonly ILogger<UserSettingController> _logger;

    public UserSettingController(AppDbContext appDb, WarehouseDbContext whDb, ILogger<UserSettingController> logger)
    {
        _appDb = appDb;
        _whDb = whDb;
        _logger = logger;
    }

    /// <summary>
    /// 查询用户设定列表
    /// </summary>
    [HttpGet("search")]
    public async Task<ActionResult<ApiResult<List<UserSettingDto>>>> Search(
        [FromQuery] string userCode = "",
        [FromQuery] string userName = "",
        [FromQuery] string dept = "",
        [FromQuery] bool includeSub = false,
        [FromQuery] bool showExpired = false,
        [FromQuery] string manager = "",
        [FromQuery] bool sunlikeUser = true,
        [FromQuery] bool vendor = true,
        [FromQuery] bool isEmpty = true,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var query = _appDb.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(userCode))
                query = query.Where(x => x.USR != null && x.USR.Contains(userCode));

            if (!string.IsNullOrWhiteSpace(userName))
                query = query.Where(x => x.NAME != null && x.NAME.Contains(userName));

            if (!string.IsNullOrWhiteSpace(dept))
                query = query.Where(x => x.DEP != null && x.DEP.Contains(dept));

            if (!string.IsNullOrWhiteSpace(manager))
                query = query.Where(x => x.MNG != null && x.MNG.Contains(manager));

            // SUNLIKE用户过滤
            if (sunlikeUser)
                query = query.Where(x => x.ISGROUP == "T" || x.ISGROUP != "F");

            // 供应商过滤
            if (vendor)
                query = query.Where(x => x.ISCUST == "T" || x.ISCUST != "F");

            // 过期显示否：false=只显示未过期的（E_DAT为空或E_DAT>=今天）
            if (!showExpired)
                query = query.Where(x => x.E_DAT == null || x.E_DAT >= DateTime.Today);

            var totalCount = await query.CountAsync();

            if (pageIndex < 1) pageIndex = 1;
            if (pageSize < 1) pageSize = 20;

            query = query.OrderBy(x => x.USR)
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize);

            var list = await query.Select(x => new UserSettingDto
            {
                Usr = x.USR ?? "",
                Name = x.NAME ?? "",
                Dep = x.DEP ?? "",
                Mng = x.MNG ?? "",
                EDat = x.E_DAT,
                DeproNo = x.DEPRO_NO ?? "",
                IsGroup = x.ISGROUP == "T",
                IsCust = x.ISCUST == "T"
            }).ToListAsync();

            // 查询部门名称
            var depCodes = list.Where(x => !string.IsNullOrWhiteSpace(x.Dep)).Select(x => x.Dep).Distinct().ToList();
            var depDict = new Dictionary<string, string>();
            if (depCodes.Count > 0)
            {
                depDict = await _whDb.Depts
                    .Where(d => depCodes.Contains(d.DEP))
                    .ToDictionaryAsync(d => d.DEP, d => d.NAME ?? "");
            }

            // 查询类别名称
            var categoryCodes = list.Where(x => !string.IsNullOrWhiteSpace(x.DeproNo)).Select(x => x.DeproNo).Distinct().ToList();
            var categoryDict = new Dictionary<string, string>();
            if (categoryCodes.Count > 0)
            {
                categoryDict = await _whDb.DefNss
                    .Where(d => categoryCodes.Contains(d.NS_NO))
                    .ToDictionaryAsync(d => d.NS_NO, d => d.NAME ?? "");
            }

            var seq = (pageIndex - 1) * pageSize;
            var result = list.Select(x => new UserSettingDto
            {
                Seq = ++seq,
                Usr = x.Usr,
                Name = x.Name,
                Dep = x.Dep,
                DeptName = depDict.GetValueOrDefault(x.Dep, ""),
                Mng = x.Mng,
                EDat = x.EDat,
                DeproNo = x.DeproNo,
                CategoryName = categoryDict.GetValueOrDefault(x.DeproNo, ""),
                IsGroup = x.IsGroup,
                IsCust = x.IsCust
            }).ToList();

            return Ok(new ApiResult<List<UserSettingDto>>
            {
                Success = true,
                Data = result,
                Total = totalCount
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "查询用户设定时发生错误");
            return StatusCode(500, new ApiResult<List<UserSettingDto>>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 根据用户代号获取详情（编辑回填用）
    /// </summary>
    [HttpGet("getByUsr")]
    public async Task<ActionResult<ApiResult<UserSettingCreateRequest>>> GetByUsr([FromQuery] string usr)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(usr))
                return BadRequest(new ApiResult<UserSettingCreateRequest> { Success = false, Message = "用户代号不能为空" });

            var entity = await _appDb.Users.FirstOrDefaultAsync(x => x.USR == usr);
            if (entity == null)
            {
                return Ok(new ApiResult<UserSettingCreateRequest> { Success = false, Message = $"用户 [{usr}] 不存在" });
            }

            var result = new UserSettingCreateRequest
            {
                Usr = entity.USR ?? "",
                Name = entity.NAME ?? "",
                Pwd = "", // 编辑时不回传密码
                Dep = entity.DEP ?? "",
                Mng = entity.MNG ?? "",
                PwdChg = entity.PWD_CHG == "T",
                EDat = MySqlDateHelper.SafeToDateTime(entity.E_DAT),
                BDat = MySqlDateHelper.SafeToDateTime(entity.B_DAT),
                DeproNo = entity.DEPRO_NO ?? "",
                IsGroup = entity.ISGROUP == "T",
                IsCust = entity.ISCUST == "T"
            };

            return Ok(new ApiResult<UserSettingCreateRequest>
            {
                Success = true,
                Data = result
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取用户详情时发生错误: {Usr}", usr);
            return StatusCode(500, new ApiResult<UserSettingCreateRequest>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }

    /// <summary>
    /// 新增用户设定
    /// </summary>
    [HttpPost("create")]
    public async Task<ActionResult<ApiResult<object>>> Create([FromBody] UserSettingCreateRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Usr))
                return BadRequest(new ApiResult<object> { Success = false, Message = "用户代号不能为空" });

            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest(new ApiResult<object> { Success = false, Message = "用户名称不能为空" });

            // 校验用户代号是否重复
            var existing = await _appDb.Users.FirstOrDefaultAsync(x => x.USR == request.Usr);
            if (existing != null)
            {
                return Ok(new ApiResult<object>
                {
                    Success = false,
                    Message = $"用户代号 [{request.Usr}] 已存在，请使用其他代号"
                });
            }

            var entity = new User
            {
                USR = request.Usr,
                NAME = request.Name,
                PWD = string.IsNullOrWhiteSpace(request.Pwd) ? "" : request.Pwd,
                DEP = request.Dep,
                MNG = request.Mng,
                PWD_CHG = request.PwdChg ? "T" : "F",
                E_DAT = request.EDat,
                B_DAT = request.BDat,
                DEPRO_NO = request.DeproNo,
                ISGROUP = request.IsGroup ? "T" : "F",
                ISCUST = request.IsCust ? "T" : "F",
                SYS_DATE = DateTime.Now,
                CREATOR = "ADMIN",
                COMPNO = "GZ01"
            };

            _appDb.Users.Add(entity);
            await _appDb.SaveChangesAsync();

            return Ok(new ApiResult<object>
            {
                Success = true,
                Message = "保存成功",
                Data = new { Usr = entity.USR }
            });
        }
        catch (Exception ex)
        {
            var innerMsg = ex.InnerException?.Message ?? "";
            var deepInner = ex.InnerException?.InnerException?.Message ?? "";
            _logger.LogError(ex, "新增用户设定时发生错误: {Msg} | Inner: {Inner} | DeepInner: {DeepInner}",
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
    /// 更新用户设定
    /// </summary>
    [HttpPut("update")]
    public async Task<ActionResult<ApiResult<object>>> Update([FromBody] UserSettingCreateRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Usr))
                return BadRequest(new ApiResult<object> { Success = false, Message = "用户代号不能为空" });

            var entity = await _appDb.Users.FirstOrDefaultAsync(x => x.USR == request.Usr);
            if (entity == null)
            {
                return Ok(new ApiResult<object> { Success = false, Message = $"用户 [{request.Usr}] 不存在，无法更新" });
            }

            entity.NAME = request.Name;
            entity.DEP = request.Dep;
            entity.MNG = request.Mng;
            entity.PWD_CHG = request.PwdChg ? "T" : "F";
            entity.E_DAT = request.EDat;
            entity.B_DAT = request.BDat;
            entity.DEPRO_NO = request.DeproNo;
            entity.ISGROUP = request.IsGroup ? "T" : "F";
            entity.ISCUST = request.IsCust ? "T" : "F";

            // 仅当密码非空时才更新密码
            if (!string.IsNullOrWhiteSpace(request.Pwd))
            {
                entity.PWD = request.Pwd;
            }

            entity.SYS_DATE = DateTime.Now;

            await _appDb.SaveChangesAsync();

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
            _logger.LogError(ex, "更新用户设定时发生错误: {Usr} | Msg: {Msg} | Inner: {Inner} | DeepInner: {DeepInner}",
                request.Usr, ex.Message, innerMsg, deepInner);
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
    /// 删除用户设定
    /// </summary>
    [HttpDelete("{usr}")]
    public async Task<ActionResult<ApiResult<object>>> Delete(string usr)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(usr))
                return BadRequest(new ApiResult<object> { Success = false, Message = "用户代号不能为空" });

            var entity = await _appDb.Users.FirstOrDefaultAsync(x => x.USR == usr);
            if (entity == null)
            {
                return Ok(new ApiResult<object> { Success = false, Message = $"用户 [{usr}] 不存在" });
            }

            _appDb.Users.Remove(entity);
            await _appDb.SaveChangesAsync();

            return Ok(new ApiResult<object> { Success = true, Message = "删除成功" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "删除用户设定时发生错误: {Usr}", usr);
            return StatusCode(500, new ApiResult<object>
            {
                Success = false,
                Message = $"服务器内部错误: {ex.Message}"
            });
        }
    }
}

/// <summary>用户设定DTO</summary>
public class UserSettingDto
{
    public int Seq { get; set; }
    public string Usr { get; set; } = "";
    public string Name { get; set; } = "";
    public string Dep { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string Mng { get; set; } = "";
    public DateTime? EDat { get; set; }
    public string DeproNo { get; set; } = "";
    public string CategoryName { get; set; } = "";
    public bool IsGroup { get; set; }
    public bool IsCust { get; set; }
}

/// <summary>用户设定新增/编辑请求体</summary>
public class UserSettingCreateRequest
{
    /// <summary>用户代号（主键）</summary>
    public string Usr { get; set; } = "";
    /// <summary>用户名称</summary>
    public string Name { get; set; } = "";
    /// <summary>密码</summary>
    public string? Pwd { get; set; }
    /// <summary>部门代号</summary>
    public string Dep { get; set; } = "";
    /// <summary>主管</summary>
    public string Mng { get; set; } = "";
    /// <summary>下次登陆必须修改密码</summary>
    public bool PwdChg { get; set; }
    /// <summary>截止有效期</summary>
    public DateTime? EDat { get; set; }
    /// <summary>开始有效期</summary>
    public DateTime? BDat { get; set; }
    /// <summary>所属类别</summary>
    public string DeproNo { get; set; } = "";
    /// <summary>集团用户</summary>
    public bool IsGroup { get; set; }
    /// <summary>供应商</summary>
    public bool IsCust { get; set; }
}
