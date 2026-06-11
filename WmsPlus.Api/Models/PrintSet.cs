namespace WmsPlus.Api.Models;

/// <summary>
/// 打印网点设置表（PRINT_SET）
/// 对应数据库 db_gz01.PRINT_SET 表
/// </summary>
public class PrintSet
{
    /// <summary>序号（主键）</summary>
    public int SEQ_NO { get; set; }

    /// <summary>网点名称</summary>
    public string? SITE_NAME { get; set; }

    /// <summary>机器IP</summary>
    public string? MACHINE_IP { get; set; }

    /// <summary>机器名称</summary>
    public string? MACHINE_NAME { get; set; }

    /// <summary>停用标记（Y/N）</summary>
    public string? STOP_FLAG { get; set; }

    /// <summary>停用日期</summary>
    public DateTime? STOP_DATE { get; set; }
}
