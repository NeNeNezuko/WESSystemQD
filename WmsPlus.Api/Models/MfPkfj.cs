namespace WmsPlus.Api.Models;

/// <summary>
/// 二次分拣单表头（MF_PKFJ）
/// 对应数据库 db_gz01.MF_PKFJ 表
/// </summary>
public class MfPkfj
{
    /// <summary>分拣单号（主键）</summary>
    public string PKFJ_NO { get; set; } = "";

    /// <summary>分拣日期</summary>
    public DateTime? PKFJ_DD { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>经办人代号</summary>
    public string? USR { get; set; }

    /// <summary>经办人名称</summary>
    public string? USR_NAME { get; set; }

    /// <summary>出库通知单号</summary>
    public string? CK_TZ_NO { get; set; }

    /// <summary>业务单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>申请单号</summary>
    public string? APPLY_NO { get; set; }

    /// <summary>转入单单据ID</summary>
    public string? OTH_BIL_ID { get; set; }

    /// <summary>转入单单据号码</summary>
    public string? OTH_BIL_NO { get; set; }

    /// <summary>结案否</summary>
    public string? CLS_ID { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>制单人</summary>
    public string? SYS_USR { get; set; }

    /// <summary>系统建立时间</summary>
    public DateTime? SYS_DATE { get; set; }
}
