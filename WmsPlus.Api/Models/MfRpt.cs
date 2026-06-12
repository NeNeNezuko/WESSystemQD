namespace WmsPlus.Api.Models;

/// <summary>
/// 报表类型设定表头（MF_RPT）
/// 对应数据库 db_gz01.MF_RPT 表
/// </summary>
public class MfRpt
{
    /// <summary>报表代号(PGM)</summary>
    public string? PGM { get; set; }
    
    /// <summary>项次</summary>
    public int? ITM { get; set; }
    
    /// <summary>简体名称</summary>
    public string? NAME_GB { get; set; }
    
    /// <summary>繁体名称</summary>
    public string? NAME_BIG5 { get; set; }
    
    /// <summary>英文名称</summary>
    public string? NAME_ENG { get; set; }
    
    /// <summary>以菜单显示</summary>
    public string? CHK_MENU { get; set; }
    
    /// <summary>可查看用户</summary>
    public string? CHK_USRS { get; set; }
    
    /// <summary>制单人</summary>
    public string? USR { get; set; }
    
    /// <summary>制单时间</summary>
    public DateTime? SYS_DATE { get; set; }
}
