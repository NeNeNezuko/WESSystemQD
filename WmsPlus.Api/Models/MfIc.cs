namespace WmsPlus.Api.Models;

/// <summary>
/// 库存调拨单表头（MF_IC）
/// 对应数据库 db_gz01.MF_IC 表
/// </summary>
public class MfIc
{
    /// <summary>调拨单号（主键）</summary>
    public string IC_NO { get; set; } = "";

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>单据日期</summary>
    public DateTime? IC_DD { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>经办人</summary>
    public string? SAL_NO { get; set; }

    /// <summary>制单人</summary>
    public string? USR { get; set; }

    /// <summary>单据类别</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>来源单据别</summary>
    public string? BIL_ID { get; set; }

    /// <summary>来源单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>拨入货主编码</summary>
    public string? CON_NO_IN { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>业务类型</summary>
    public string? TYPE_ID { get; set; }

    /// <summary>单据类型(WMS;ERP;ERP1)</summary>
    public string? BIL_KND { get; set; }

    /// <summary>调拨类型</summary>
    public string? IC_TYPE { get; set; }

    /// <summary>到货确认标记</summary>
    public string? CFM_SW { get; set; }

    /// <summary>到货确认结案标记</summary>
    public string? IZ_CLS_ID { get; set; }

    /// <summary>自动产生标记</summary>
    public string? AUTO_ID { get; set; }

    /// <summary>立库单据标志</summary>
    public string? LK_ID { get; set; }

    /// <summary>制单时间</summary>
    public DateTime? SYS_DATE { get; set; }

    /// <summary>修改时间</summary>
    public DateTime? MODIFY_DD { get; set; }

    /// <summary>修改人</summary>
    public string? MODIFY_MAN { get; set; }

    /// <summary>打印时间</summary>
    public DateTime? PRT_DATE { get; set; }

    /// <summary>打印人员</summary>
    public string? PRT_USR { get; set; }

    /// <summary>打印注记</summary>
    public string? PRT_SW { get; set; }

    /// <summary>拣选点</summary>
    public string? PICK_POINT { get; set; }

    /// <summary>拣选工作站台</summary>
    public string? WORK_STATION { get; set; }

    /// <summary>收货区域</summary>
    public string? RECEI_AREA { get; set; }

    /// <summary>收货点</summary>
    public string? AREA_SH { get; set; }

    /// <summary>产线代号</summary>
    public string? LINE_CODE { get; set; }

    /// <summary>跨货主调拨</summary>
    public string? SPAN_ERP_IC { get; set; }

    /// <summary>请检任务单号/关联检验</summary>
    public string? TZ_NO_UO { get; set; }

    /// <summary>ERP库存单据生成方式</summary>
    public string? ERP_GEN_METHOD { get; set; }
}
