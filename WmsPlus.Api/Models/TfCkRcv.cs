namespace WmsPlus.Api.Models;

/// <summary>
/// 出库单收货信息（TF_CK_RCV）
/// 对应数据库 db_gz01.TF_CK_RCV 表
/// </summary>
public class TfCkRcv
{
    /// <summary>单据号码（主键部分）</summary>
    public string CK_ID { get; set; } = "";

    /// <summary>项次（主键部分）</summary>
    public int ITM { get; set; }

    /// <summary>收货点代号</summary>
    public string? RCV_ID { get; set; }

    /// <summary>收货点名称</summary>
    public string? RCV_NAME { get; set; }

    /// <summary>联系人</summary>
    public string? CONTACT { get; set; }

    /// <summary>联系电话</summary>
    public string? TEL { get; set; }

    /// <summary>地址</summary>
    public string? ADDR { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }
}
