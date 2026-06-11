namespace WmsPlus.Api.Models.Entities;

/// <summary>
/// 仓库库存结余表（SPRD）
/// 对应数据库 db_gz01.SPRD 表
/// </summary>
public class Sprd
{
    /// <summary>仓库</summary>
    public string? WH { get; set; }

    /// <summary>年度</summary>
    public int? YY { get; set; }

    /// <summary>月份</summary>
    public int? MM { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>品号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>特征</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>有效日期</summary>
    public DateTime? VALID_DD { get; set; }

    /// <summary>入库数量</summary>
    public decimal? QTY_IN { get; set; }

    /// <summary>出库数量</summary>
    public decimal? QTY_OUT { get; set; }

    /// <summary>入库数量(副)</summary>
    public decimal? QTY1_IN { get; set; }

    /// <summary>出库数量(副)</summary>
    public decimal? QTY1_OUT { get; set; }

    /// <summary>最近入库日</summary>
    public DateTime? LST_IND { get; set; }

    /// <summary>最近出库日</summary>
    public DateTime? LST_OTD { get; set; }
}
