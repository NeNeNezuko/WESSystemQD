namespace WmsPlus.Api.Models;

/// <summary>
/// 拣货单表头（MF_PK）
/// 对应数据库 db_gz01.MF_PK 表
/// </summary>
public class MfPk
{
    /// <summary>拣货单号（主键）</summary>
    public string PK_NO { get; set; } = "";

    /// <summary>拣货日期</summary>
    public DateTime? PK_DD { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>业务员代号</summary>
    public string? SAL_NO { get; set; }

    /// <summary>业务类型</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>出库通知单号</summary>
    public string? CK_TZ_NO { get; set; }

    /// <summary>申请单号</summary>
    public string? APPLY_NO { get; set; }

    /// <summary>业务单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>转入单据别</summary>
    public string? OTH_BIL_ID { get; set; }

    /// <summary>转入单号</summary>
    public string? OTH_BIL_NO { get; set; }

    /// <summary>结案否</summary>
    public string? CLS_ID { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>制单人/经办人</summary>
    public string? USR { get; set; }

    /// <summary>系统建立时间</summary>
    public DateTime? SYS_DATE { get; set; }
}

/// <summary>
/// 拣货单表身（TF_PK）
/// 对应数据库 db_gz01.TF_PK 表
/// </summary>
public class TfPk
{
    /// <summary>拣货单号</summary>
    public string PK_NO { get; set; } = "";

    /// <summary>项次</summary>
    public int ITM { get; set; }

    /// <summary>拣货日期</summary>
    public DateTime? PK_DD { get; set; }

    /// <summary>料号/品号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>品名</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>规格型号</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>储位代号</summary>
    public string? CHUW { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>应拣数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>已拣数量</summary>
    public decimal? QTY_PK { get; set; }

    /// <summary>出库通知单号</summary>
    public string? TZ_NO { get; set; }

    /// <summary>出库通知单项次</summary>
    public int? TZ_ITM { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }
}
