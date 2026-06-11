namespace WmsPlus.Api.Models;

/// <summary>
/// 盘点单据表身（TF_PD）
/// 对应数据库 db_gz01.TF_PD 表
/// 通过 PD_NO 与 MF_PD 关联
/// </summary>
public class TfPd
{
    /// <summary>盘点单号（关联MF_PD.PD_NO，主键之一）</summary>
    public string PD_NO { get; set; } = "";

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品特征</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>储位代号</summary>
    public string? CHUW { get; set; }

    /// <summary>帐载数量</summary>
    public decimal? QTY1 { get; set; }

    /// <summary>盘点数量</summary>
    public decimal? QTY2 { get; set; }

    /// <summary>差异数</summary>
    public decimal? QTY_RNG { get; set; }

    /// <summary>唯一项次</summary>
    public int? KEY_ITM { get; set; }

    /// <summary>帐载数量(副)</summary>
    public decimal? QTY11 { get; set; }

    /// <summary>盘点数量(副)</summary>
    public decimal? QTY21 { get; set; }

    /// <summary>差异数(副)</summary>
    public decimal? QTY1_RNG { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }
}
