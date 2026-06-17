namespace WmsPlus.Api.Models;

/// <summary>
/// 物流容器变动历史（MF_CONTAIN_HIS）
/// 对应数据库 db_gz01.MF_CONTAIN_HIS 表
/// </summary>
public class MfContainHis
{
    /// <summary>容器条码</summary>
    public string? CONTAIN_CODE { get; set; }

    /// <summary>容器状态</summary>
    public string? CONTAIN_STATUS { get; set; }

    /// <summary>容器类型</summary>
    public string? CONTAIN_TYPE { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>在途标记</summary>
    public string? TRANSIT_FLAG { get; set; }

    /// <summary>述检标记</summary>
    public string? INSPECT_FLAG { get; set; }

    /// <summary>变动单据别名</summary>
    public string? CHANGE_DOC_NAME { get; set; }

    /// <summary>变动单号</summary>
    public string? CHANGE_NO { get; set; }

    /// <summary>变动人</summary>
    public string? CHANGE_MAN { get; set; }

    /// <summary>变动时间</summary>
    public DateTime? CHANGE_TIME { get; set; }

    /// <summary>散件项次</summary>
    public string? SCATTER_ITM { get; set; }

    /// <summary>条码类型</summary>
    public string? BARCODE_TYPE { get; set; }

    /// <summary>条码</summary>
    public string? BARCODE { get; set; }

    /// <summary>件数</summary>
    public decimal? PIECE_COUNT { get; set; }

    /// <summary>箱条码散出数量</summary>
    public decimal? SCATTER_QTY { get; set; }

    /// <summary>外箱码</summary>
    public string? OUTER_BOX_CODE { get; set; }

    /// <summary>储位代号</summary>
    public string? CW_CODE { get; set; }

    /// <summary>储位名称</summary>
    public string? CW_NAME { get; set; }

    /// <summary>储位位置</summary>
    public string? CW_POSITION { get; set; }

    /// <summary>拣货标记</summary>
    public string? PICK_FLAG { get; set; }

    /// <summary>是否散出</summary>
    public string? IS_SCATTER { get; set; }
}
