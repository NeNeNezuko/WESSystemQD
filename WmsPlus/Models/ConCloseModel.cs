namespace WmsPlus.Models;

/// <summary>
/// 关账作业 - 数据模型（对应表格行）
/// </summary>
public class ConCloseModel
{
    /// <summary>任务编号</summary>
    public string ActNo { get; set; } = "";
    /// <summary>货主编码</summary>
    public string ConNo { get; set; } = "";
    /// <summary>关帐日期</summary>
    public DateTime? CloseDate { get; set; }
    /// <summary>最近修改人</summary>
    public string ModifyMan { get; set; } = "";
    /// <summary>最近修改日期</summary>
    public DateTime? ModifyDate { get; set; }
}

/// <summary>
/// 关账作业 - 查询模型
/// </summary>
public class ConCloseQuery
{
    /// <summary>货主编码（模糊查询）</summary>
    public string ConNo { get; set; } = "";
    /// <summary>关账日期起</summary>
    public string CloseDateFrom { get; set; } = "";
    /// <summary>关账日期止</summary>
    public string CloseDateTo { get; set; } = "";
}
