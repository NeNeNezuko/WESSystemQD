namespace WmsPlus.Api.Models;

/// <summary>
/// 不合格原因设定（SPC_SET）
/// 对应数据库 db_gz01.SPC_SET 表
/// </summary>
public class SpcSet
{
    /// <summary>原因代号（主键）</summary>
    public string SPC_NO { get; set; } = "";

    /// <summary>原因说明</summary>
    public string? NAME { get; set; }

    /// <summary>上层原因代号</summary>
    public string? SPC_UP { get; set; }

    /// <summary>原因备注</summary>
    public string? REM { get; set; }

    /// <summary>第三方标记</summary>
    public string? TP_ID { get; set; }
}
