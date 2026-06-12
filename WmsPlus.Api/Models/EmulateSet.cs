namespace WmsPlus.Api.Models;

/// <summary>
/// 仿真布局设备设定表（EMULATE_SET）
/// 对应数据库 db_gz01.EMULATE_SET 表
/// </summary>
public class EmulateSet
{
    /// <summary>设备ID(主键)</summary>
    public int? EMULATE_ID { get; set; }
    
    /// <summary>设备代号</summary>
    public string? DEVICE_ID { get; set; }
    
    /// <summary>设备类型(1.自动化-调用 2.自动化-推送 3.设备组合 4.系统设备)</summary>
    public string? TYPE_ID { get; set; }
    
    /// <summary>当前状态(1.正常 2.异常 3.故障)</summary>
    public string? STATUS_ID { get; set; }
    
    /// <summary>最近修改时间</summary>
    public DateTime? MODIFY_DD { get; set; }
}
