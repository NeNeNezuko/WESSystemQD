namespace WmsPlus.Api.Models;

/// <summary>
/// 盘盈(验收入库)单表身（TF_YN）
/// 对应数据库 db_gz01.TF_YN 表
/// 通过 YN_NO 与 MF_YN 关联
/// </summary>
public class TfYn
{
    /// <summary>盘盈单号（关联MF_YN.YN_NO，主键之一）</summary>
    public string YN_NO { get; set; } = "";

    /// <summary>单据日期</summary>
    public DateTime? YN_DD { get; set; }

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品名称</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>特征</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>仓库</summary>
    public string? WH { get; set; }

    /// <summary>储位代号</summary>
    public string? CHUW { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>数量(副)</summary>
    public decimal? QTY1 { get; set; }

    /// <summary>有效日期</summary>
    public DateTime? VALID_DD { get; set; }

    /// <summary>盘点单号</summary>
    public string? PD_NO { get; set; }

    /// <summary>盘点单项次</summary>
    public int? PD_ITM { get; set; }

    /// <summary>摘要</summary>
    public string? REM { get; set; }

    /// <summary>转单唯一项次</summary>
    public int? KEY_ITM { get; set; }

    /// <summary>最近检验日期</summary>
    public DateTime? LST_TYD { get; set; }

    /// <summary>最近入库日</summary>
    public DateTime? LST_IND { get; set; }

    /// <summary>生产日期</summary>
    public DateTime? SC_DD { get; set; }
}
