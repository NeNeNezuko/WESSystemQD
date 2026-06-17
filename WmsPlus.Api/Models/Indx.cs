namespace WmsPlus.Api.Models;

/// <summary>
/// 中类代号设定（INDX）
/// 对应数据库 db_gz01.INDX 表
/// </summary>
public class Indx
{
    /// <summary>中类代号（主键）</summary>
    public string IDX_NO { get; set; } = "";

    /// <summary>名称</summary>
    public string? NAME { get; set; }

    /// <summary>上层中类</summary>
    public string? IDX_UP { get; set; }

    /// <summary>停用日期</summary>
    public DateTime? STOP_DD { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>制单人</summary>
    public string? USR { get; set; }

    /// <summary>输入日期</summary>
    public DateTime? SYS_DATE { get; set; }

    /// <summary>上次更新时间</summary>
    public DateTime? UP_DD { get; set; }
}
