namespace WmsPlus.Api.Models;

/// <summary>
/// 部门 dept
/// </summary>
public class Dept
{
    /// <summary>部门代号</summary>
    public string DEP { get; set; } = "";

    /// <summary>部门名称</summary>
    public string? NAME { get; set; }

    /// <summary>英文名称</summary>
    public string? ENG_NAME { get; set; }

    /// <summary>上层部门</summary>
    public string? UP { get; set; }

    /// <summary>停用日期</summary>
    public DateTime? STOP_DD { get; set; }

    /// <summary>部门性质</summary>
    public string? MAKE_ID { get; set; }

    /// <summary>群组代号</summary>
    public string? GROUP_ID { get; set; }

    /// <summary>制单人</summary>
    public string? USR { get; set; }

    /// <summary>系统日期</summary>
    public DateTime? SYS_DATE { get; set; }

    /// <summary>修改日期</summary>
    public DateTime? UP_DD { get; set; }

    /// <summary>名称拼音</summary>
    public string? NAME_PY { get; set; }

    /// <summary>类型代号</summary>
    public string? TP_ID { get; set; }
}
