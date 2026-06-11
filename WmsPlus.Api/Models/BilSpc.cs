namespace WmsPlus.Api.Models;

/// <summary>
/// 单据类别设定表（BIL_SPC）
/// 对应数据库 db_gz01.BIL_SPC 表
/// </summary>
public class BilSpc
{
    public string? SPC_ID { get; set; }
    public string? SPC_NO { get; set; }
    public string? NAME { get; set; }
    public string? REM { get; set; }
}
