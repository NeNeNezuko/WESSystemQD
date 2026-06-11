namespace WmsPlus.Api.Models;

/// <summary>
/// 储位调拨单表身（TF_CWDB）
/// 对应数据库 db_gz01.TF_CWDB 表
/// 通过 DB_NO 与 MF_CWDB 关联
/// </summary>
public class TfCwdb
{
    /// <summary>调拨单号（关联MF_CWDB.DB_NO，主键之一）</summary>
    public string DB_NO { get; set; } = "";

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }

    /// <summary>调拨单ID</summary>
    public string? DB_ID { get; set; }

    /// <summary>品号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>品名</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>货品特征</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>有效日期</summary>
    public DateTime? VALID_DD { get; set; }

    /// <summary>仓库</summary>
    public string? WH { get; set; }

    /// <summary>出库储位</summary>
    public string? CHUW1 { get; set; }

    /// <summary>入库储位</summary>
    public string? CHUW2 { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>数量副</summary>
    public decimal? QTY1 { get; set; }

    /// <summary>摘要</summary>
    public string? REM { get; set; }

    /// <summary>来源单据别</summary>
    public string? BIL_ID { get; set; }

    /// <summary>来源单项次</summary>
    public int? BIL_ITM { get; set; }

    /// <summary>来源单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>唯一项次</summary>
    public int? KEY_ITM { get; set; }

    /// <summary>最近入库日</summary>
    public DateTime? LST_IND { get; set; }
}
