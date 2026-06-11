namespace WmsPlus.Api.Models;

/// <summary>
/// 货品条码打印套版表（PRDT_BAR_RPT）
/// 对应数据库 db_gz01.PRDT_BAR_RPT 表
/// </summary>
public class PrdtBarRpt
{
    public int ID { get; set; }
    public string? BAR_TYPE { get; set; }
    public string? PRD_NO { get; set; }
    public string? PRD_NAME { get; set; }
    public string? MID_CLASS_NO { get; set; }
    public string? MID_CLASS_NAME { get; set; }
    public string? TEMPLATE_CODE { get; set; }
    public string? TEMPLATE_NAME { get; set; }
}
