namespace WmsPlus.Api.Models;

/// <summary>
/// 出库单收货信息（TF_CK_RCV）
/// 对应数据库 db_gz01.TF_CK_RCV 表
/// </summary>
public class TfCkRcv
{
    /// <summary>出库单号（主键）</summary>
    public string CK_NO { get; set; } = "";

    /// <summary>地址</summary>
    public string? ADR { get; set; }

    /// <summary>电话</summary>
    public string? CELL_NO { get; set; }

    /// <summary>联系人</summary>
    public string? CON_MAN { get; set; }

    /// <summary>国家ID</summary>
    public string? COT_ID { get; set; }

    /// <summary>县/区ID</summary>
    public string? COUN_ID { get; set; }

    /// <summary>客户名称</summary>
    public string? CUS_NAME { get; set; }

    /// <summary>客户代号</summary>
    public string? CUS_NO { get; set; }

    /// <summary>发货单号</summary>
    public string? FH_NO { get; set; }

    /// <summary>邮编</summary>
    public string? ZIP { get; set; }
}
