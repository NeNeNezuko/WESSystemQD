namespace WmsPlus.Api.Models.Entities;

/// <summary>
/// 出库通知单附属信息表（TF_CKTZ_RCV）
/// 对应数据库 db_gz01.TF_CKTZ_RCV 表
/// </summary>
public class TfCktzRcv
{
    /// <summary>出库通知单号（主键）</summary>
    public string? TZ_NO { get; set; }

    /// <summary>收货地址</summary>
    public string? ADR { get; set; }

    /// <summary>收货人电话</summary>
    public string? CELL_NO { get; set; }

    /// <summary>收货人</summary>
    public string? CON_MAN { get; set; }

    /// <summary>收货国家</summary>
    public string? COT_ID { get; set; }

    /// <summary>收货省市区</summary>
    public string? COUN_ID { get; set; }

    /// <summary>快递公司名称</summary>
    public string? CUS_NAME { get; set; }

    /// <summary>快递公司</summary>
    public string? CUS_NO { get; set; }

    /// <summary>快递单号</summary>
    public string? FH_NO { get; set; }

    /// <summary>邮编</summary>
    public string? ZIP { get; set; }
}
