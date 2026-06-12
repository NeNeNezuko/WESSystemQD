namespace WmsPlus.Api.Models;

/// <summary>
/// ONLINE角色权限表（FX_PSWD）
/// 对应数据库 db_gz01.FX_PSWD 表
/// </summary>
public class FxPswd
{
    /// <summary>角色代号(外键->ROLE)</summary>
    public string? ROLENO { get; set; }
    
    /// <summary>程序代号</summary>
    public string? PGM { get; set; }
    
    /// <summary>权限分类</summary>
    public string? TYPE_ID { get; set; }
    
    /// <summary>帐套名称</summary>
    public string? COMPNO { get; set; }
    
    /// <summary>部门群组</summary>
    public string? DEPRO_NO { get; set; }
    
    /// <summary>查询</summary>
    public string? QRY { get; set; }
    
    /// <summary>新增</summary>
    public string? INS { get; set; }
    
    /// <summary>更新</summary>
    public string? UPD { get; set; }
    
    /// <summary>删除</summary>
    public string? DEL { get; set; }
    
    /// <summary>列印</summary>
    public string? PRN { get; set; }
    
    /// <summary>金额</summary>
    public string? U_AMT { get; set; }
    
    /// <summary>单价</summary>
    public string? UPR { get; set; }
    
    /// <summary>数量</summary>
    public string? QTY { get; set; }
    
    /// <summary>折扣</summary>
    public string? DIS_CNT { get; set; }
    
    /// <summary>成本</summary>
    public string? R_CST { get; set; }
    
    /// <summary>字段</summary>
    public string? FLD { get; set; }
    
    /// <summary>属性</summary>
    public string? PROPERTY { get; set; }
    
    /// <summary>允许否</summary>
    public string? ALLOW_ID { get; set; }
    
    /// <summary>作废</summary>
    public string? CANCEL { get; set; }
    
    /// <summary>锁单</summary>
    public string? LCK { get; set; }
    
    /// <summary>转出</summary>
    public string? EPT { get; set; }
    
    /// <summary>导入</summary>
    public string? IMPORT { get; set; }
    
    /// <summary>展开</summary>
    public string? EXPAND { get; set; }
    
    /// <summary>附件</summary>
    public string? ATTACH { get; set; }
    
    /// <summary>范围</summary>
    public string? RANGE { get; set; }
    
    /// <summary>范围-删除</summary>
    public string? RANGE_DEL { get; set; }
    
    /// <summary>分享</summary>
    public string? SHARE { get; set; }
}
