namespace WmsPlus.Api.Models;

/// <summary>
/// 库存调拨单表身（TF_IC）
/// 对应数据库 db_gz01.TF_IC 表
/// 通过 IC_NO 与 MF_IC 关联
/// </summary>
public class TfIc
{
    /// <summary>调拨单号（关联MF_IC.IC_NO，主键之一）</summary>
    public string IC_NO { get; set; } = "";

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }

    /// <summary>单据日期</summary>
    public DateTime? IC_DD { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品名称</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>货品特征</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>拨入批号</summary>
    public string? BAT_NO_IN { get; set; }

    /// <summary>有效日期</summary>
    public DateTime? VALID_DD { get; set; }

    /// <summary>生产日期</summary>
    public DateTime? SC_DD { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>数量(副)</summary>
    public decimal? QTY1 { get; set; }

    /// <summary>到货数量</summary>
    public decimal? QTY_IZ { get; set; }

    /// <summary>到货数量(副)</summary>
    public decimal? QTY1_IZ { get; set; }

    /// <summary>拨出仓库</summary>
    public string? WH1 { get; set; }

    /// <summary>拨出仓库（ERP仓）</summary>
    public string? WH1_ERP { get; set; }

    /// <summary>拨入仓库</summary>
    public string? WH2 { get; set; }

    /// <summary>拨入仓库（ERP仓）</summary>
    public string? WH2_ERP { get; set; }

    /// <summary>拨出储位</summary>
    public string? CHUW1 { get; set; }

    /// <summary>拨入储位</summary>
    public string? CHUW2 { get; set; }

    /// <summary>收货点</summary>
    public string? AREA_SH { get; set; }

    /// <summary>摘要</summary>
    public string? REM { get; set; }

    /// <summary>转入单据别</summary>
    public string? BIL_ID { get; set; }

    /// <summary>转入单项次</summary>
    public int? BIL_ITM { get; set; }

    /// <summary>转入单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>唯一项次</summary>
    public int? KEY_ITM { get; set; }

    /// <summary>最近入库时间</summary>
    public DateTime? LST_IND2 { get; set; }

    /// <summary>最近检验日期</summary>
    public string? LST_TYD { get; set; }

    /// <summary>拣货/分拣单据别</summary>
    public string? PK_ID { get; set; }

    /// <summary>拣货/分拣单项次</summary>
    public int? PK_ITM { get; set; }

    /// <summary>拣货/分拣单号</summary>
    public string? PK_NO { get; set; }

    /// <summary>请检单号</summary>
    public string? QJ_NO { get; set; }

    /// <summary>请检单项次</summary>
    public int? QJ_ITM { get; set; }

    /// <summary>ERP申请单据别</summary>
    public string? ERP_AP_ID { get; set; }

    /// <summary>ERP申请单项次</summary>
    public int? ERP_AP_ITM { get; set; }

    /// <summary>ERP申请单号</summary>
    public string? ERP_AP_NO { get; set; }
}
