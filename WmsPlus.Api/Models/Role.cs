namespace WmsPlus.Api.Models;

/// <summary>
/// 角色定义表（ROLE）
/// 对应数据库 db_gz01.ROLE 表
/// </summary>
public class Role
{
    /// <summary>角色代号(主键)</summary>
    public string? ROLENO { get; set; }
    
    /// <summary>角色名称</summary>
    public string? NAME { get; set; }
    
    /// <summary>部门代号</summary>
    public string? DEP { get; set; }
    
    /// <summary>部门群组代号</summary>
    public string? DEPRO_NO { get; set; }
    
    /// <summary>公司代号</summary>
    public string? COMPNO { get; set; }
    
    /// <summary>权限分类</summary>
    public string? TYPE_ID { get; set; }
    
    /// <summary>公用注记</summary>
    public string? PUBLIC_ID { get; set; }
    
    /// <summary>用户代号</summary>
    public string? USR { get; set; }
    
    /// <summary>备注</summary>
    public string? REM { get; set; }
    
    /// <summary>简体条码菜单</summary>
    public string? MENUINFO_0 { get; set; }
    
    /// <summary>繁体条码菜单</summary>
    public string? MENUINFO_1 { get; set; }
    
    /// <summary>英文条码菜单</summary>
    public string? MENUINFO_2 { get; set; }
    
    /// <summary>部门含下属否</summary>
    public string? SUB_ID { get; set; }
}
