namespace WmsPlus.Api.Models;

/// <summary>
/// 拣货策略表（PK_RULE）
/// 对应数据库 db_gz01.PK_RULE 表
/// </summary>
public class PkRule
{
    /// <summary>规则ID（主键）</summary>
    public string RULE_ID { get; set; } = "";

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>策略名称</summary>
    public string? NAME { get; set; }

    /// <summary>仓库类型/上下架模式</summary>
    public string? WH_TYPE { get; set; }

    /// <summary>停用标记</summary>
    public string? STOP_ID { get; set; }

    /// <summary>制单人/录入人员</summary>
    public string? USR { get; set; }

    /// <summary>系统建立时间</summary>
    public DateTime? SYS_DATE { get; set; }

    /// <summary>修改人</summary>
    public string? MODIFY_MAN { get; set; }

    /// <summary>修改日期</summary>
    public DateTime? MODIFY_DD { get; set; }
}
