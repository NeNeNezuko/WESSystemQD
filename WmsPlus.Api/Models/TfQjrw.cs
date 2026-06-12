namespace WmsPlus.Api.Models;

/// <summary>
/// 请检任务单表身（TF_QJRW）
/// 对应数据库 db_gz01.TF_QJRW 表
/// 通过 QJ_NO 与 MF_QJRW 关联
/// </summary>
public class TfQjrw
{
    /// <summary>请检任务单号（关联MF_QJRW.QJ_NO，主键之一）</summary>
    public string QJ_NO { get; set; } = "";

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品名称</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>最近验日期</summary>
    public DateTime? LST_TYD { get; set; }

    /// <summary>储位代号</summary>
    public string? CHUW { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>数量2</summary>
    public decimal? QTY1 { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>调拨通知单号</summary>
    public string? TN_NO { get; set; }
}
