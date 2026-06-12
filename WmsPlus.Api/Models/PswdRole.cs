namespace WmsPlus.Api.Models;

/// <summary>
/// ONLINE角色所属用户表（PSWD_ROLE）
/// 对应数据库 db_gz01.PSWD_ROLE 表
/// </summary>
public class PswdRole
{
    /// <summary>公司代号</summary>
    public string? COMPNO { get; set; }
    
    /// <summary>角色代号(外键->ROLE)</summary>
    public string? ROLENO { get; set; }
    
    /// <summary>权限分类</summary>
    public string? TYPE_ID { get; set; }
    
    /// <summary>用户代号</summary>
    public string? USR { get; set; }
}
