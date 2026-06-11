namespace WmsPlus.Api.Models;

/// <summary>
/// 出库退回通知单表头（MF_CKTB）
/// 对应数据库 db_gz01.MF_CKTB 表
/// </summary>
public class MfCktb
{
    /// <summary>单据号码（主键）</summary>
    public string TB_NO { get; set; } = "";

    /// <summary>单据日期</summary>
    public DateTime? TB_DD { get; set; }

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

    /// <summary>预出库日期</summary>
    public DateTime? EST_DD { get; set; }

    /// <summary>结案否</summary>
    public string? CLS_ID { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>外部系统标识</summary>
    public string? OTH_ID { get; set; }

    /// <summary>外部系统单据ID</summary>
    public string? OTH_BIL_ID { get; set; }

    /// <summary>外部系统单号</summary>
    public string? OTH_BIL_NO { get; set; }

    /// <summary>ERP申请单ID（ERP单据编号）</summary>
    public string? ERP_BIL_ID { get; set; }

    /// <summary>ERP申请单号（ERP号）</summary>
    public string? ERP_BIL_NO { get; set; }

    /// <summary>业务单ID</summary>
    public string? ORG_BIL_ID { get; set; }

    /// <summary>业务单号</summary>
    public string? ORG_BIL_NO { get; set; }

    /// <summary>已转单据别ID</summary>
    public string? RTN_BIL_ID { get; set; }

    /// <summary>已转单号</summary>
    public string? RTN_BIL_NO { get; set; }

    /// <summary>制单人</summary>
    public string? USR { get; set; }

    /// <summary>输单日期（系统建立时间）</summary>
    public DateTime? SYS_DATE { get; set; }

    /// <summary>打印注记</summary>
    public string? PRT_SW { get; set; }

    /// <summary>打印人</summary>
    public string? PRT_USR { get; set; }

    /// <summary>打印日期</summary>
    public DateTime? PRT_DATE { get; set; }
}
