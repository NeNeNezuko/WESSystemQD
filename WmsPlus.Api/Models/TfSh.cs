namespace WmsPlus.Api.Models;

/// <summary>
/// 收货单表身（TF_SH）
/// 对应数据库 db_gz01.TF_SH 表
/// 通过 SH_NO 与 MF_SH 关联
/// </summary>
public class TfSh
{
    /// <summary>收货单号（关联MF_SH.SH_NO，主键之一）</summary>
    public string SH_NO { get; set; } = "";

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品名称</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>特征</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>数量(副)</summary>
    public decimal? QTY1 { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>检验标志</summary>
    public string? JY_FLAG { get; set; }

    /// <summary>转入单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>转入单ID</summary>
    public string? BIL_ID { get; set; }

    /// <summary>转入单项次</summary>
    public int? BIL_ITM { get; set; }

    /// <summary>仓库</summary>
    public string? WH { get; set; }

    /// <summary>生产日期</summary>
    public DateTime? SC_DD { get; set; }

    /// <summary>有效日期</summary>
    public DateTime? VALID_DD { get; set; }

    /// <summary>收货日期</summary>
    public DateTime? SH_DD { get; set; }

    /// <summary>摘要</summary>
    public string? REM { get; set; }
}
