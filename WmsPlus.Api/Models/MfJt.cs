namespace WmsPlus.Api.Models;

/// <summary>
/// 拣货退回单表头（MF_JT）
/// 对应数据库 db_gz01.MF_JT 表
/// </summary>
public class MfJt
{
    /// <summary>拣货退回单号（主键）</summary>
    public string JT_NO { get; set; } = "";

    /// <summary>退回日期</summary>
    public DateTime? JT_DD { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>经办人</summary>
    public string? SAL_NO { get; set; }

    /// <summary>开单人</summary>
    public string? USR { get; set; }

    /// <summary>拣货人</summary>
    public string? USR_PK { get; set; }

    /// <summary>结案标记（Y/N）</summary>
    public string? CLS_ID { get; set; }

    /// <summary>出库通知单号</summary>
    public string? CK_TZ_NO { get; set; }

    /// <summary>业务单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>申请单号</summary>
    public string? APPLY_NO { get; set; }

    /// <summary>转入单ID</summary>
    public string? OTH_ID { get; set; }

    /// <summary>转入单单据代号</summary>
    public string? OTH_BIL_ID { get; set; }

    /// <summary>转入单单号</summary>
    public string? OTH_BIL_NO { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>开单日期</summary>
    public DateTime? SYS_DATE { get; set; }
}
