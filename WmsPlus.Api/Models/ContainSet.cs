namespace WmsPlus.Api.Models;

/// <summary>
/// 物流容器类型设定（CONTAIN_SET）
/// 对应数据库 db_gz01.CONTAIN_SET 表
/// </summary>
public class ContainSet
{
    /// <summary>类型代号（主键）</summary>
    public string? TYPE_CODE { get; set; }

    /// <summary>类型名称</summary>
    public string? TYPE_NAME { get; set; }

    /// <summary>编码前缀</summary>
    public string? CODE_PREFIX { get; set; }

    /// <summary>停用标记</summary>
    public string? STOP_FLAG { get; set; }

    /// <summary>是否系统标准框</summary>
    public string? IS_SYSTEM { get; set; }

    /// <summary>RCS容器类型</summary>
    public string? RCS_TYPE { get; set; }
}
