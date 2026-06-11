namespace WmsPlus.Api.Models;

/// <summary>
/// 系统参数/公司设定表 SPC_COMP
/// </summary>
public class SpcComp
{
    /// <summary>集团分公司</summary>
    public string DEP { get; set; } = "";

    /// <summary>选项代号</summary>
    public string CTRL_ID { get; set; } = "";

    /// <summary>选项值</summary>
    public string? SPC_ID { get; set; }

    /// <summary>说明</summary>
    public string? REM { get; set; }

    /// <summary>时间戳</summary>
    public DateTime? UP_DD { get; set; }
}
