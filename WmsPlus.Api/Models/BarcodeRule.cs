namespace WmsPlus.Api.Models;

/// <summary>
/// 条码编码规则表（BARCODE_RULE）
/// 对应数据库 db_gz01.BARCODE_RULE 表
/// </summary>
public class BarcodeRule
{
    public string? RULE_ID { get; set; }
    public string? RuleType { get; set; }
    public string? RuleCode { get; set; }
    public string? RuleName { get; set; }
    public int? FlowLength { get; set; }
    public string? Separator { get; set; }
    public string? Prefix { get; set; }
}
