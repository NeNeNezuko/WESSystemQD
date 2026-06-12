namespace WmsPlus.Api.Models;

/// <summary>
/// 条形码属性设定档表（BAR_PSWD_PROP）
/// 对应数据库 db_gz01.BAR_PSWD_PROP 表
/// </summary>
public class BarPswdProp
{
    /// <summary>帐套代号</summary>
    public string? COMPNO { get; set; }
    
    /// <summary>类别代号</summary>
    public string? ROLENO { get; set; }
    
    /// <summary>类别种类</summary>
    public string? TYPE_ID { get; set; }
    
    /// <summary>程序代号</summary>
    public string? PGM { get; set; }
    
    /// <summary>属性名</summary>
    public string? FLD_NAME { get; set; }
    
    /// <summary>属性值</summary>
    public string? FLD_VALUE { get; set; }
    
    /// <summary>参数备注</summary>
    public string? REM { get; set; }
}
