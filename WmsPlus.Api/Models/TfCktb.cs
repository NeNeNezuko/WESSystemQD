namespace WmsPlus.Api.Models;

/// <summary>
/// 出库退回通知单表身（TF_CKTB）
/// 对应数据库 db_gz01.TF_CKTB 表
/// 通过 TB_NO 与 MF_CKTB 关联
/// </summary>
public class TfCktb
{
    /// <summary>出库退回通知单号（关联MF_CKTB.TB_NO，主键之一）</summary>
    public string TB_NO { get; set; } = "";

    /// <summary>退回日期</summary>
    public DateTime? TB_DD { get; set; }

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品名称</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>特征/规格型号</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>数量(副)</summary>
    public decimal? QTY1 { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>外部系统单据ID</summary>
    public string? OTH_BIL_ID { get; set; }

    /// <summary>外部系统单号</summary>
    public string? OTH_BIL_NO { get; set; }

    /// <summary>外部系统单据项次</summary>
    public string? OTH_BIL_ITM { get; set; }

    /// <summary>ERP申请单ID（ERP单据编号）</summary>
    public string? ERP_BIL_ID { get; set; }

    /// <summary>ERP申请单号（ERP号）</summary>
    public string? ERP_BIL_NO { get; set; }
}
