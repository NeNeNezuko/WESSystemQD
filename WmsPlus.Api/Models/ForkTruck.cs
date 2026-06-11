namespace WmsPlus.Api.Models;

/// <summary>
/// 叉车车号管理表（FORK_TRUCK）
/// 对应数据库 db_gz01.FORK_TRUCK 表
/// </summary>
public class ForkTruck
{
    public string? TRUCK_NO { get; set; }
    public string? NAME { get; set; }
    public string? WH { get; set; }
    public string? REM { get; set; }
}
