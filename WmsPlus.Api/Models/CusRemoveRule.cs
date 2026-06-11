namespace WmsPlus.Api.Models;

/// <summary>
/// 供应商拆码规则表（CUS_REMVOE_RULE）
/// 对应数据库 db_gz01.CUS_REMVOE_RULE 表
/// </summary>
public class CusRemoveRule
{
    public int ID { get; set; }
    public int? SEQ_NO { get; set; }
    public string? CUS_NO { get; set; }
    public string? CUS_NAME { get; set; }
    public string? RULE_CODE { get; set; }
    public string? RULE_NAME { get; set; }
}
