namespace WmsPlus.Api.Models;

/// <summary>
/// 出库单表身（TF_CK）
/// 对应数据库 db_gz01.TF_CK 表
/// </summary>
public class TfCk
{
    /// <summary>单据号码（主键部分）</summary>
    public string CK_ID { get; set; } = "";

    /// <summary>项次（主键部分）</summary>
    public int ITM { get; set; }

    /// <summary>单据日期</summary>
    public DateTime? CK_DD { get; set; }

    /// <summary>料号/品号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>品名</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>规格型号</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>批次号码</summary>
    public string? BAT_NO { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }
}
