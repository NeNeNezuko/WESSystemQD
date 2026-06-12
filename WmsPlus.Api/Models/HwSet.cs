namespace WmsPlus.Api.Models;

/// <summary>
/// 系统设备表头（HW_SET）
/// 对应数据库 db_gz01.HW_SET 表
/// </summary>
public class HwSet
{
    /// <summary>设备代号(主键)</summary>
    public string? HW_NO { get; set; }
    
    /// <summary>设备名称</summary>
    public string? NAME { get; set; }
    
    /// <summary>IP地址</summary>
    public string? IP { get; set; }
    
    /// <summary>端口</summary>
    public string? PORT { get; set; }
    
    /// <summary>设备型号代号</summary>
    public string? MODEL_NO { get; set; }
    
    /// <summary>设备类型代号</summary>
    public string? TYPE_NO { get; set; }
    
    /// <summary>所属仓库</summary>
    public string? WH { get; set; }
    
    /// <summary>停用标记</summary>
    public string? STOP_ID { get; set; }
}
