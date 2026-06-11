namespace WmsPlus.Api.Models;

/// <summary>
/// 调拨通知单表头（MF_ICTZ）
/// 对应数据库 db_gz01.MF_ICTZ 表
/// </summary>
public class MfIctz
{
    /// <summary>通知单号（主键）</summary>
    public string TZ_NO { get; set; } = "";

    /// <summary>单据日期</summary>
    public DateTime? TZ_DD { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>经办人</summary>
    public string? SAL_NO { get; set; }

    /// <summary>制单人</summary>
    public string? USR { get; set; }

    /// <summary>拣货员</summary>
    public string? USR_PK { get; set; }

    /// <summary>单据类别</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>转入单据别</summary>
    public string? BIL_ID { get; set; }

    /// <summary>转入单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>出库仓库代号</summary>
    public string? WH1 { get; set; }

    /// <summary>入库仓库代号</summary>
    public string? WH2 { get; set; }

    /// <summary>预出库日</summary>
    public DateTime? EST_DD { get; set; }

    /// <summary>稽催日期（单据日期）</summary>
    public DateTime? UP_DD { get; set; }

    /// <summary>收货点</summary>
    public string? AREA_SH { get; set; }

    /// <summary>收货区域</summary>
    public string? RECEI_AREA { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>业务类型</summary>
    public string? TYPE_ID { get; set; }

    /// <summary>业务单ID</summary>
    public string? ORG_BIL_ID { get; set; }

    /// <summary>业务单号</summary>
    public string? ORG_BIL_NO { get; set; }

    /// <summary>派工状态(0.未派工 1.已派工)</summary>
    public string? STATUS_PG { get; set; }

    /// <summary>波次结案标记</summary>
    public string? CLS_ID_BC { get; set; }

    /// <summary>出库结案标记</summary>
    public string? CLS_ID_CK { get; set; }

    /// <summary>拣货结案标记</summary>
    public string? CLS_ID_PK { get; set; }

    /// <summary>到货确认标记</summary>
    public string? CFM_SW { get; set; }

    /// <summary>出库流程</summary>
    public string? CK_FLOW { get; set; }

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

    /// <summary>排单等级(0.低 1.中 2.高)</summary>
    public int? PRIORITY { get; set; }

    /// <summary>产线代号</summary>
    public string? LINE_CODE { get; set; }

    /// <summary>发货单号</summary>
    public string? FH_NO { get; set; }

    /// <summary>第三方系统单号</summary>
    public string? OTH_BIL_NO { get; set; }

    /// <summary>第三方系统标识</summary>
    public string? REF_ID { get; set; }

    /// <summary>ERP申请单ID</summary>
    public string? ERP_BIL_ID { get; set; }

    /// <summary>ERP申请单号</summary>
    public string? ERP_BIL_NO { get; set; }

    /// <summary>ERP库存单据生成方式</summary>
    public string? ERP_GEN_METHOD { get; set; }

    /// <summary>ERP部门代号</summary>
    public string? DEP_ERP { get; set; }

    /// <summary>ERP部门名称</summary>
    public string? DEP_NAME_ERP { get; set; }

    /// <summary>ERP业务员代号</summary>
    public string? SAL_NO_ERP { get; set; }

    /// <summary>ERP业务员名称</summary>
    public string? SAL_NAME_ERP { get; set; }
}
