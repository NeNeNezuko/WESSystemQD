namespace WmsPlus.Api.Models;

/// <summary>
/// 关帐信息表（CON_CLOSE）
/// 对应数据库 db_gz01.CON_CLOSE 表
/// </summary>
public class ConClose
{
    /// <summary>唯一号(主键)</summary>
    public string? GUID { get; set; }
    /// <summary>任务编号</summary>
    public string? ACT_NO { get; set; }
    /// <summary>关帐日期</summary>
    public DateTime? CLOSE_DD { get; set; }
    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }
    /// <summary>最近修改人</summary>
    public string? MODIFY_MAN { get; set; }
    /// <summary>最近修改日期</summary>
    public DateTime? MODIFY_DD { get; set; }
}
