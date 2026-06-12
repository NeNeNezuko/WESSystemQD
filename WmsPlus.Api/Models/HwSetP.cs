namespace WmsPlus.Api.Models;

/// <summary>
/// 系统设备表身（HW_SET_P）
/// 对应数据库 db_gz01.HW_SET_P 表
/// </summary>
public class HwSetP
{
    /// <summary>设备代号(外键->HW_SET)</summary>
    public string? HW_NO { get; set; }
    
    /// <summary>参数名</summary>
    public string? PROP_NO { get; set; }
    
    /// <summary>参数值</summary>
    public string? VALUE { get; set; }
    
    /// <summary>备注</summary>
    public string? REM { get; set; }
}
