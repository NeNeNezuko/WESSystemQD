namespace WmsPlus.Api.Models;

/// <summary>
/// 拆码规则表头（MF_REMVOE_RULE）
/// 对应数据库 db_gz01.MF_REMVOE_RULE 表
/// </summary>
public class MfRemoveRule
{
    public string? RULE_ID { get; set; }
    public string? RULE_CODE { get; set; }
    public string? RULE_NAME { get; set; }
    public string? BASE_BARCODE { get; set; }
    public string? ENCODING_METHOD { get; set; }
    public string? SEPARATOR { get; set; }
    public int? TOTAL_LENGTH { get; set; }
    public string? DEFAULT_FLAG { get; set; }
}
