namespace WmsPlus.Api.Models.Entities;

/// <summary>
/// 入库单表头（MF_RK）
/// 对应数据库 db_gz01.MF_RK 表
/// </summary>
public class MfRk
{
    /// <summary>验收单号</summary>
    public string? ACT_NO { get; set; }

    /// <summary>自动编号</summary>
    public string? AUTO_ID { get; set; }

    /// <summary>转入单ID</summary>
    public string? BIL_ID { get; set; }

    /// <summary>转入单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>单据类别</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>业务类型</summary>
    public string? BIL_TYPE_ID { get; set; }

    /// <summary>验收单号</summary>
    public string? CE_NO { get; set; }

    /// <summary>车间代号</summary>
    public string? CJ_NO { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>公司别</summary>
    public string? CPY_SW { get; set; }

    /// <summary>客户/厂商名称</summary>
    public string? CUS_NAME { get; set; }

    /// <summary>客户/厂商代号</summary>
    public string? CUS_NO { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>ERP应付帐款编号</summary>
    public decimal? ERP_AP_ID { get; set; }

    /// <summary>ERP应付帐款单号</summary>
    public string? ERP_AP_NO { get; set; }

    /// <summary>ERP生成方式</summary>
    public string? ERP_GEN_METHOD { get; set; }

    /// <summary>外部系统标记</summary>
    public string? EXT_SYS_FLAG { get; set; }

    /// <summary>外部系统ID</summary>
    public decimal? EXT_SYS_ID { get; set; }

    /// <summary>外部系统单号</summary>
    public string? EXT_SYS_NO { get; set; }

    /// <summary>最终工序</summary>
    public string? FINAL_PROC { get; set; }

    /// <summary>生成ERP单据ID</summary>
    public string? GEN_ERP_BIL_ID { get; set; }

    /// <summary>入库单种类</summary>
    public string? IC_BIL_KND { get; set; }

    /// <summary>是否匹配</summary>
    public string? IS_MATCH { get; set; }

    /// <summary>多级物料属性活动号</summary>
    public string? MMCLS_ACT_NO { get; set; }

    /// <summary>制令单号</summary>
    public string? MO_NO { get; set; }

    /// <summary>修改时间</summary>
    public DateTime? MODIFY_DD { get; set; }

    /// <summary>修改人</summary>
    public string? MODIFY_MAN { get; set; }

    /// <summary>内销单号</summary>
    public string? NL_NO { get; set; }

    /// <summary>打印日期</summary>
    public DateTime? PRT_DATE { get; set; }

    /// <summary>打印否</summary>
    public string? PRT_SW { get; set; }

    /// <summary>打印人</summary>
    public string? PRT_USR { get; set; }

    /// <summary>质检标记</summary>
    public string? QC_FLAG { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>入库日期</summary>
    public DateTime? RK_DD { get; set; }

    /// <summary>入库单号（主键）</summary>
    public string RK_NO { get; set; } = "";

    /// <summary>业务员代号</summary>
    public string? SAL_NO { get; set; }

    /// <summary>生产通知单号</summary>
    public string? SCTZ_NO { get; set; }

    /// <summary>检验状态</summary>
    public string? STATUS_JY { get; set; }

    /// <summary>制单时间</summary>
    public DateTime? SYS_DATE { get; set; }

    /// <summary>是否有制令</summary>
    public string? TW_HAS_MO { get; set; }

    /// <summary>业务类型</summary>
    public string? TYPE_ID { get; set; }

    /// <summary>制单人</summary>
    public string? USR { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>WMS编号</summary>
    public string? WMS_ID { get; set; }

    /// <summary>产生入库通知单号/关联检验单</summary>
    public string? TZ_NO_UO { get; set; }
}
