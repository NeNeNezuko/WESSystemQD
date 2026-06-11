namespace WmsPlus.Api.Models;

/// <summary>
/// 二次分拣单表身（TF_PKFJ）
/// 对应数据库 db_gz01.TF_PKFJ 表
/// </summary>
public class TfPkfj
{
    /// <summary>分拣单号（主键的一部分）</summary>
    public string PKFJ_NO { get; set; } = "";

    /// <summary>项次（主键的一部分）</summary>
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

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>已分拣数量</summary>
    public decimal? PICK_QTY { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }
}
