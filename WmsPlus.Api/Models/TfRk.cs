namespace WmsPlus.Api.Models;

/// <summary>
/// 入库单表身（TF_RK）
/// 对应数据库 db_gz01.TF_RK 表
/// 通过 RK_NO 与 MF_RK 关联
/// 复合主键：(RK_NO, ITM)
/// </summary>
public class TfRk
{
    /// <summary>入库单号（关联MF_RK.RK_NO，主键之一）</summary>
    public string RK_NO { get; set; } = "";

    /// <summary>项次（主键之二）</summary>
    public int? ITM { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>单据别</summary>
    public string? BIL_ID { get; set; }

    /// <summary>转入单项次</summary>
    public int? BIL_ITM { get; set; }

    /// <summary>转入单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>业务单ID</summary>
    public decimal? BUS_ID { get; set; }

    /// <summary>业务单项次</summary>
    public int? BUS_ITM { get; set; }

    /// <summary>业务单号</summary>
    public string? BUS_NO { get; set; }

    /// <summary>储位代号</summary>
    public string? CHUW { get; set; }

    /// <summary>ERP应付单ID</summary>
    public decimal? ERP_AP_ID { get; set; }

    /// <summary>ERP应付单项次</summary>
    public int? ERP_AP_ITM { get; set; }

    /// <summary>ERP应付单号</summary>
    public string? ERP_AP_NO { get; set; }

    /// <summary>外部系统标记</summary>
    public string? EXT_SYS_FLAG { get; set; }

    /// <summary>外部系统项次</summary>
    public int? EXT_SYS_ITM { get; set; }

    /// <summary>外部系统单号</summary>
    public string? EXT_SYS_NO { get; set; }

    /// <summary>超收标志</summary>
    public string? FCP_FLAG { get; set; }

    /// <summary>超收来源项次</summary>
    public int? FCP_UP_ITM { get; set; }

    /// <summary>关键项次</summary>
    public int? KEY_ITM { get; set; }

    /// <summary>最后上架日期</summary>
    public DateTime? LST_IND { get; set; }

    /// <summary>最后移储日期</summary>
    public DateTime? LST_TYD { get; set; }

    /// <summary>内联单项次</summary>
    public int? NL_ITM { get; set; }

    /// <summary>内联单号</summary>
    public string? NL_NO { get; set; }

    /// <summary>货品特征</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>货品名称</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>数量(主单位)</summary>
    public decimal? QTY { get; set; }

    /// <summary>损耗数量(主单位)</summary>
    public decimal? QTY_LOST { get; set; }

    /// <summary>数量(副单位)</summary>
    public decimal? QTY1 { get; set; }

    /// <summary>损耗数量(副单位)</summary>
    public decimal? QTY1_LOST { get; set; }

    /// <summary>备注/摘要</summary>
    public string? REM { get; set; }

    /// <summary>入库日期</summary>
    public DateTime? RK_DD { get; set; }

    /// <summary>入库流程</summary>
    public string? RK_FLOW { get; set; }

    /// <summary>生产日期</summary>
    public DateTime? SC_DD { get; set; }

    /// <summary>任务ID</summary>
    public string? TI_ID { get; set; }

    /// <summary>任务项次</summary>
    public int? TI_ITM { get; set; }

    /// <summary>任务单号</summary>
    public string? TI_NO { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>有效日期</summary>
    public DateTime? VALID_DD { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }
}
