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
}
