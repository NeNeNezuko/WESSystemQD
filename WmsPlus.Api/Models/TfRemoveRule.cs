namespace WmsPlus.Api.Models;

/// <summary>
/// 拆码规则表身（TF_REMVOE_RULE）
/// 对应数据库 db_gz01.TF_REMVOE_RULE 表
/// </summary>
public class TfRemoveRule
{
    public string? RULE_ID { get; set; }
    public int? ITM { get; set; }
    public string? FIELD_NAME { get; set; }
    public int? FIELD_POS { get; set; }
    public int? FIELD_LEN { get; set; }
}
