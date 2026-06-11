namespace WmsPlus.Api.Models;

/// <summary>
/// 储位上架单表身（TF_CWSJ）
/// 对应数据库 db_gz01.TF_CWSJ 表
/// 通过 SJ_NO 与 MF_CWSJ 关联
/// </summary>
public class TfCwsj
{
    /// <summary>上架单号（关联MF_CWSJ.SJ_NO，主键之一）</summary>
    public string SJ_NO { get; set; } = "";

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }

    /// <summary>上架单ID</summary>
    public string? SJ_ID { get; set; }

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
    public string? WH1 { get; set; }

    /// <summary>储位</summary>
    public string? CHUW1 { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>副单位数量</summary>
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

    /// <summary>最近上架日</summary>
    public DateTime? LST_IND { get; set; }

    /// <summary>最近检验日期</summary>
    public string? LST_TYD { get; set; }
}
