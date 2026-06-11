namespace WmsPlus.Api.Models;

/// <summary>
/// 超交管制设置 EXCEED_CTRL
/// </summary>
public class ExceedCtrl
{
    /// <summary>出入库区分（1=入库, 2=出库）</summary>
    public string CR_TYPE { get; set; } = "";

    /// <summary>集团公司</summary>
    public string? GROUP_DEP { get; set; }

    /// <summary>参数ID（CHECK=超交检测方式, RATE=超交比例）</summary>
    public string PARAMS_ID { get; set; } = "";

    /// <summary>参数值</summary>
    public string? PARAMS_VALUE { get; set; }

    /// <summary>业务类型（关联 cr_type_set.TYPE_ID）</summary>
    public string TYPE_ID { get; set; } = "";
}
