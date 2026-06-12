namespace WmsPlus.Api.Models;

/// <summary>
/// 仓储可视化图表（MY_WH_VIEW）
/// 对应数据库 db_gz01.MY_WH_VIEW 表
/// </summary>
public class MyWhView
{
    /// <summary>代号(主键)</summary>
    public string? VW_NO { get; set; }
    
    /// <summary>名称</summary>
    public string? NAME { get; set; }
    
    /// <summary>停用否</summary>
    public string? STOP_ID { get; set; }
    
    /// <summary>标准档</summary>
    public string? SYS_ID { get; set; }
    
    /// <summary>SVG内容</summary>
    public string? SVG_CONTENT { get; set; }
    
    /// <summary>可查看用户</summary>
    public string? CHK_USRS { get; set; }
}
