namespace WmsPlus.Api.Models;

/// <summary>
/// 条码属性表（PSWD_PROP）
/// 对应数据库 db_gz01.PSWD_PROP 表
/// </summary>
public class PswdProp
{
    public int ID { get; set; }
    public string? PROP_TYPE { get; set; }
    public string? PROP_CODE { get; set; }
    public string? PROP_NAME { get; set; }
    public string? PROP_VALUE { get; set; }
}
