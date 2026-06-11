namespace WmsPlus.Api.Models;

/// <summary>
/// 拣货退回单表身（TF_JT）
/// 对应数据库 db_gz01.TF_JT 表
/// 通过 JT_NO 与 MF_JT 关联
/// </summary>
public class TfJt
{
    /// <summary>拣货退回单号（关联MF_JT.JT_NO，主键之一）</summary>
    public string JT_NO { get; set; } = "";

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }

    /// <summary>料号/品号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>品名</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>规格型号</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>储位代号</summary>
    public string? CHUW { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>已退回数量</summary>
    public decimal? QTY_TH { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }
}
