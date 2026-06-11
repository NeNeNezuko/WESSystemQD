namespace WmsPlus.Api.Models;

/// <summary>
/// 波次单表头（MF_BC）
/// 对应数据库 db_gz01.MF_BC 表
/// </summary>
public class MfBc
{
    /// <summary>波次单号（主键）</summary>
    public string BC_NO { get; set; } = "";

    /// <summary>波次日期</summary>
    public DateTime? BC_DD { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>业务员代号</summary>
    public string? SAL_NO { get; set; }

    /// <summary>业务类型</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>规格型号</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>出库通知单号</summary>
    public string? CK_TZ_NO { get; set; }

    /// <summary>业务单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>申请单号</summary>
    public string? APPLY_NO { get; set; }

    /// <summary>收货点</summary>
    public string? RCV_POINT { get; set; }

    /// <summary>结案否</summary>
    public string? CLS_ID { get; set; }

    /// <summary>优先级</summary>
    public int? PRIORITY { get; set; }

    /// <summary>检票备注</summary>
    public string? REM { get; set; }

    /// <summary>经办人</summary>
    public string? USR { get; set; }

    /// <summary>系统建立时间</summary>
    public DateTime? SYS_DATE { get; set; }
}
