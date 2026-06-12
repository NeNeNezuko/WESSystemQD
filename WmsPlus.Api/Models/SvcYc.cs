namespace WmsPlus.Api.Models;

/// <summary>
/// 自动服务执行异常表（SVC_YC）
/// 对应数据库 db_gz01.SVC_YC 表
/// </summary>
public class SvcYc
{
    /// <summary>异常代号</summary>
    public string? YC_NO { get; set; }
    
    /// <summary>服务代号</summary>
    public string? SVC_NO { get; set; }
    
    /// <summary>子服务代号</summary>
    public string? SVC_NO1 { get; set; }
    
    /// <summary>异常说明</summary>
    public string? REM { get; set; }
    
    /// <summary>发生时间</summary>
    public DateTime? SYS_DATE { get; set; }
}
