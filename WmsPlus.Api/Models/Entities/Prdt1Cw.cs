namespace WmsPlus.Api.Models.Entities;

/// <summary>
/// 储位库存表（PRDT1_CW）
/// 对应数据库 db_gz01.PRDT1_CW 表
/// </summary>
public class Prdt1Cw
{
    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>储位代号</summary>
    public string? CHUW { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品特征</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>有效日期</summary>
    public DateTime? VALID_DD { get; set; }

    /// <summary>自设字段</summary>
    public string? FIELD_ZS { get; set; }

    /// <summary>入库数量</summary>
    public decimal? QTY_IN { get; set; }

    /// <summary>出库数量</summary>
    public decimal? QTY_OUT { get; set; }

    /// <summary>拣货量</summary>
    public decimal? QTY_PK { get; set; }

    /// <summary>库存检验量</summary>
    public decimal? QTY_TY { get; set; }

    /// <summary>计划出库量</summary>
    public decimal? QTY_BC { get; set; }

    /// <summary>入库数量(副)</summary>
    public decimal? QTY1_IN { get; set; }

    /// <summary>出库数量(副)</summary>
    public decimal? QTY1_OUT { get; set; }

    /// <summary>拣货量(副)</summary>
    public decimal? QTY1_PK { get; set; }

    /// <summary>库存检验量(副)</summary>
    public decimal? QTY1_TY { get; set; }

    /// <summary>最近上架日</summary>
    public DateTime? LST_IND { get; set; }

    /// <summary>最近下架日</summary>
    public DateTime? LST_OTD { get; set; }

    /// <summary>最近检验日期</summary>
    public DateTime? LST_TYD { get; set; }

    /// <summary>插入时间</summary>
    public DateTime? INSERT_DD { get; set; }
}
