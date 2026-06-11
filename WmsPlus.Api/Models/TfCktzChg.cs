namespace WmsPlus.Api.Models;

/// <summary>
/// 出库通知变更单表身（TF_CKTZ_CHG）
/// 对应数据库 db_gz01.TF_CKTZ_CHG 表
/// 通过 CHG_NO 与 MF_CKTZ_CHG 关联
/// </summary>
public class TfCktzChg
{
    /// <summary>变更单号（关联MF_CKTZ_CHG.CHG_NO，主键之一）</summary>
    public string CHG_NO { get; set; } = "";

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }

    /// <summary>行日期</summary>
    public DateTime? CHG_DD { get; set; }

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

    /// <summary>备注</summary>
    public string? REM { get; set; }
}
