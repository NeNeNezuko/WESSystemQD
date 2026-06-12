namespace WmsPlus.Api.Models;

/// <summary>
/// 自动服务执行日志表（SVC_LOG）
/// 对应数据库 db_gz01.SVC_LOG 表
/// </summary>
public class SvcLog
{
    /// <summary>服务代号</summary>
    public string? SVC_NO { get; set; }
    
    /// <summary>子服务代号</summary>
    public string? SVC_NO1 { get; set; }
    
    /// <summary>服务名称</summary>
    public string? NAME { get; set; }
    
    /// <summary>子服务名称</summary>
    public string? NAME1 { get; set; }
    
    /// <summary>服务路径</summary>
    public string? PATH { get; set; }
    
    /// <summary>轮询时间(秒)</summary>
    public int? INTERVAL_TIME { get; set; }
    
    /// <summary>开始时间</summary>
    public DateTime? START_TIME { get; set; }
    
    /// <summary>结束时间</summary>
    public DateTime? END_TIME { get; set; }
    
    /// <summary>说明</summary>
    public string? REM { get; set; }
}
