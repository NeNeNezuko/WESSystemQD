namespace WmsPlus.Api.Models;

/// <summary>
/// 依储类设定货品储位（PRDT_CW）
/// 对应数据库 db_gz01.PRDT_CW 表
/// </summary>
public class PrdtCw
{
    /// <summary>GUID（主键）</summary>
    public string? GUID { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品特征码</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>中类代号</summary>
    public string? IDX_NO { get; set; }

    /// <summary>储位代号</summary>
    public string? CHUW { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>排</summary>
    public string? GS { get; set; }

    /// <summary>列</summary>
    public string? GL { get; set; }

    /// <summary>层</summary>
    public string? LAYER { get; set; }

    /// <summary>区域代号</summary>
    public string? ZONE_ID { get; set; }

    /// <summary>条件代号</summary>
    public string? CON_NO { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>更新日期</summary>
    public DateTime? UP_DD { get; set; }
}
