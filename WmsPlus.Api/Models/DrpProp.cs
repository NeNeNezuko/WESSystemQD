namespace WmsPlus.Api.Models;

/// <summary>
/// 属性/下拉选项参数表 DRP_PROP
/// </summary>
public class DrpProp
{
    /// <summary>集团公司</summary>
    public string DEP { get; set; } = "";

    /// <summary>项次</summary>
    public int ITEM { get; set; }

    /// <summary>值</summary>
    public string? VALUE { get; set; }

    /// <summary>参数说明</summary>
    public string? REM { get; set; }

    /// <summary>时间戳</summary>
    public DateTime? UP_DD { get; set; }
}
