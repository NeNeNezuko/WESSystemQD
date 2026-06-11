namespace WmsPlus.Api.Models;

/// <summary>
/// 物流容器表头（MF_CONTAIN）
/// 对应数据库 db_gz01.MF_CONTAIN 表
/// </summary>
public class MfContain
{
    /// <summary>扫描码</summary>
    public string? SCAN_CODE { get; set; }

    /// <summary>容器条码（主键）</summary>
    public string? CONTAIN_CODE { get; set; }

    /// <summary>容器类型</summary>
    public string? CONTAIN_TYPE { get; set; }

    /// <summary>容器状态</summary>
    public string? CONTAIN_STATUS { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>储位代号</summary>
    public string? CHUW { get; set; }

    /// <summary>储位位置</summary>
    public string? CHUW_POS { get; set; }

    /// <summary>在途标记</summary>
    public string? TRANSIT_FLAG { get; set; }

    /// <summary>述检标记</summary>
    public string? INSPECT_FLAG { get; set; }

    /// <summary>容器明细</summary>
    public string? CONTAIN_DETAIL { get; set; }

    /// <summary>变动历史</summary>
    public string? MODIFY_HISTORY { get; set; }

    /// <summary>合单代号</summary>
    public string? COMBINE_NO { get; set; }

    /// <summary>打印日期</summary>
    public DateTime? PRT_DATE { get; set; }

    /// <summary>盘点日期</summary>
    public DateTime? INVENTORY_DATE { get; set; }

    /// <summary>盘点数量</summary>
    public string? INVENTORY_QTY { get; set; }
}
