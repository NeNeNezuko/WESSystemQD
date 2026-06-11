namespace WmsPlus.Api.Models;

/// <summary>
/// 调拨通知单表身（TF_ICTZ）
/// 对应数据库 db_gz01.TF_ICTZ 表
/// 通过 TZ_NO 与 MF_ICTZ 关联
/// </summary>
public class TfIctz
{
    /// <summary>通知单号（关联MF_ICTZ.TZ_NO，主键之一）</summary>
    public string TZ_NO { get; set; } = "";

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }

    /// <summary>单据日期</summary>
    public DateTime? TZ_DD { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品代名称</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>货品特征</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>拨入批号</summary>
    public string? BAT_NO_IN { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>数量(副)</summary>
    public decimal? QTY1 { get; set; }

    /// <summary>出货仓库</summary>
    public string? WH1 { get; set; }

    /// <summary>入货仓库</summary>
    public string? WH2 { get; set; }

    /// <summary>拣货量</summary>
    public decimal? QTY_PK { get; set; }

    /// <summary>拣货量(副)</summary>
    public decimal? QTY1_PK { get; set; }

    /// <summary>出库量</summary>
    public decimal? QTY_CK { get; set; }

    /// <summary>出库量(副)</summary>
    public decimal? QTY1_CK { get; set; }

    /// <summary>波次量</summary>
    public decimal? QTY_BC { get; set; }

    /// <summary>波次量(副)</summary>
    public decimal? QTY1_BC { get; set; }

    /// <summary>退回量</summary>
    public decimal? QTY_RTN { get; set; }

    /// <summary>退回量(副)</summary>
    public decimal? QTY1_RTN { get; set; }

    /// <summary>拣货退回量</summary>
    public decimal? QTY_JT { get; set; }

    /// <summary>拣货退回量（副）</summary>
    public decimal? QTY1_JT { get; set; }

    /// <summary>下架任务量</summary>
    public decimal? QTY_XR { get; set; }

    /// <summary>下架任务量(副)</summary>
    public decimal? QTY1_XR { get; set; }

    /// <summary>检验不合格量</summary>
    public decimal? QTY_LOST { get; set; }

    /// <summary>检验不合格量(副)</summary>
    public decimal? QTY1_LOST { get; set; }

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

    /// <summary>变更单号</summary>
    public string? CHG_NO { get; set; }

    /// <summary>变更项次</summary>
    public int? CHG_ITM { get; set; }

    /// <summary>ERP申请单ID</summary>
    public string? ERP_BIL_ID { get; set; }

    /// <summary>ERP申请单项次</summary>
    public int? ERP_BIL_ITM { get; set; }

    /// <summary>ERP申请单号</summary>
    public string? ERP_BIL_NO { get; set; }

    /// <summary>ERP业务单ID</summary>
    public string? ORG_BIL_ID { get; set; }

    /// <summary>ERP业务单项次</summary>
    public int? ORG_BIL_ITM { get; set; }

    /// <summary>ERP业务单号</summary>
    public string? ORG_BIL_NO { get; set; }
}
