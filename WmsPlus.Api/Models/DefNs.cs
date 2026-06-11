namespace WmsPlus.Api.Models;

/// <summary>
/// 行业代号设定表（DEF_NS）
/// 对应数据库 db_gz01.DEF_NS 表
/// </summary>
public class DefNs
{
    public string? NS_NO { get; set; }
    public string? NAME { get; set; }
    public string? REM { get; set; }
    public string? INC_SYS { get; set; }
    public string? INC_UNI { get; set; }
}
