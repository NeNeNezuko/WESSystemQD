namespace WmsPlus.Models;

/// <summary>
/// 业务类型树节点
/// </summary>
public class BizTypeTreeNode
{
    /// <summary>节点唯一标识</summary>
    public string Id { get; set; } = "";

    /// <summary>节点显示名称</summary>
    public string Name { get; set; } = "";

    /// <summary>出入库区分（1=入库, 2=出库），根节点和分类节点为空</summary>
    public string? CrType { get; set; }

    /// <summary>业务类型代码，叶子节点有值</summary>
    public string? TypeId { get; set; }

    /// <summary>是否为叶子节点（可配置的节点）</summary>
    public bool IsLeaf { get; set; }

    /// <summary>子节点列表</summary>
    public List<BizTypeTreeNode> Children { get; set; } = new();

    /// <summary>是否展开</summary>
    public bool IsExpanded { get; set; } = true;

    /// <summary>是否选中</summary>
    public bool IsSelected { get; set; }

    /// <summary>是否可见（搜索过滤用，非序列化字段）</summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public bool _isVisible { get; set; } = true;
}

/// <summary>
/// 超交管制配置项
/// </summary>
public class ExceedConfigDto
{
    /// <summary>超交检测方式（管制/不管制/允许在货品交别所设超交/允许在货品超交率范围内超交）</summary>
    public string CheckMode { get; set; } = "管制";

    /// <summary>超交比例(%)</summary>
    public int RateValue { get; set; } = 0;
}

/// <summary>
/// 保存请求模型
/// </summary>
public class SaveExceedConfigRequest
{
    /// <summary>出入库区分</summary>
    public string CrType { get; set; } = "";

    /// <summary>业务类型代码</summary>
    public string TypeId { get; set; } = "";

    /// <summary>超交检测方式</summary>
    public string CheckMode { get; set; } = "管制";

    /// <summary>超交比例(%)</summary>
    public int RateValue { get; set; } = 0;
}
