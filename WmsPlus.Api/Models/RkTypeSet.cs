namespace WmsPlus.Api.Models;

/// <summary>
/// 入库业务类型设置 cr_type_set（CR_TYPE=1 为入库类型）
/// </summary>
public class RkTypeSet
{
    /// <summary>单据类别（1=入库, 2=出库）</summary>
    public string CR_TYPE { get; set; } = "";

    /// <summary>业务类型代码</summary>
    public string TYPE_ID { get; set; } = "";

    /// <summary>业务类型名称</summary>
    public string? NAME { get; set; }
}
