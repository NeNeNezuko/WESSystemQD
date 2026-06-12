namespace WmsPlus.Api.Models;

/// <summary>
/// 自定义报表表头（MF_QRY）
/// 对应数据库 db_gz01.MF_QRY 表
/// </summary>
public class MfQry
{
    /// <summary>代号(主键)</summary>
    public string? ID { get; set; }
    
    /// <summary>名称</summary>
    public string? NAME { get; set; }
    
    /// <summary>SQL语句</summary>
    public string? SQL { get; set; }
    
    /// <summary>可运行用户</summary>
    public string? USRS { get; set; }
    
    /// <summary>审核人</summary>
    public string? CHK_MAN { get; set; }
    
    /// <summary>制单人</summary>
    public string? USR { get; set; }
    
    /// <summary>制单时间</summary>
    public DateTime? SYS_DATE { get; set; }
    
    /// <summary>终审时间</summary>
    public DateTime? CLS_DATE { get; set; }
}
