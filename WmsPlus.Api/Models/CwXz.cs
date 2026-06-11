namespace WmsPlus.Api.Models;

/// <summary>
/// 储存性质（CW_XZ）
/// 对应数据库 db_gz01.CW_XZ 表
/// </summary>
public class CwXz
{
    /// <summary>储存性质代号（主键）</summary>
    public string? CWXZ_NO { get; set; }

    /// <summary>性质说明</summary>
    public string? NAME { get; set; }

    /// <summary>时间戳</summary>
    public DateTime? UP_DD { get; set; }
}
