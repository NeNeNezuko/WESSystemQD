namespace WmsPlus.Api.Models;

/// <summary>
/// 出库单表头（MF_CK）
/// 对应数据库 db_gz01.MF_CK 表
/// </summary>
public class MfCk
{
    /// <summary>单据号码（主键）</summary>
    public string CK_ID { get; set; } = "";

    /// <summary>单据日期</summary>
    public DateTime? CK_DD { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>业务员代号</summary>
    public string? SAL_NO { get; set; }

    /// <summary>业务类型</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>客户代号</summary>
    public string? CUS_NO { get; set; }

    /// <summary>客户名称</summary>
    public string? CUS_NAME { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>出库通知单单号</summary>
    public string? CK_TZ_NO { get; set; }

    /// <summary>申请单号</summary>
    public string? APPLY_NO { get; set; }

    /// <summary>业务单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>收货点</summary>
    public string? RCV_POINT { get; set; }

    /// <summary>批混</summary>
    public string? BAT_ID { get; set; }

    /// <summary>结案否</summary>
    public string? CLS_ID { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>制单人</summary>
    public string? USR { get; set; }

    /// <summary>系统建立时间</summary>
    public DateTime? SYS_DATE { get; set; }
}
