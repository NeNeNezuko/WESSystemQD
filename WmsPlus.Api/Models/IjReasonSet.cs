namespace WmsPlus.Api.Models;

/// <summary>
/// 查盘/与原因设定表（IJ_REASON_SET）
/// 对应数据库 db_gz01.IJ_REASON_SET 表
/// </summary>
public class IjReasonSet
{
    public string? BIL_ID { get; set; }
    public string? IJ_REASON { get; set; }
    public string? REASON_REM { get; set; }
}
