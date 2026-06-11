namespace WmsPlus.Models;

/// <summary>
/// 系统设定 - 单个设置项
/// </summary>
public class SystemSettingItem
{
    /// <summary>选项代号（CTRL_ID）</summary>
    public string CtrlId { get; set; } = "";

    /// <summary>选项值（SPC_ID）</summary>
    public string? Value { get; set; }

    /// <summary>说明（REM）</summary>
    public string? Rem { get; set; }
}

/// <summary>
/// 系统设定 - DRP_PROP前缀参数项
/// </summary>
public class DrpPropItem
{
    /// <summary>项次</summary>
    public int Item { get; set; }

    /// <summary>值</summary>
    public string? Value { get; set; }

    /// <summary>参数说明</summary>
    public string? Rem { get; set; }
}

/// <summary>
/// 系统设定 - 保存请求
/// </summary>
public class SystemSettingSaveRequest
{
    /// <summary>SPC_COMP设置项列表</summary>
    public List<SystemSettingItem>? Settings { get; set; }

    /// <summary>DRP_PROP前缀参数列表</summary>
    public List<DrpPropItem>? PrefixItems { get; set; }
}
