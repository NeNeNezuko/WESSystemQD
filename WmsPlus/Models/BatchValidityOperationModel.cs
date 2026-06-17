namespace WmsPlus.Models;

/// <summary>
/// 批号有效期作业 - 表格行数据模型
/// </summary>
public class BatchValidityOperationModel
{
    public int HisNo { get; set; }
    public string Wh { get; set; } = "";
    public string WhName { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string BatNo { get; set; } = "";
    public DateTime? ValidDDCur { get; set; }
    public DateTime? ValidDDOrg { get; set; }
    public DateTime? LastOutDate { get; set; }      // 最近出库日
    public DateTime? LastInDate { get; set; }        // 最近入库日
    public DateTime? LastInspectDate { get; set; }   // 最近检验日期
    public DateTime? ProduceDate { get; set; }       // 生产日期
    public string IdxNo { get; set; } = "";          // 中类代号
    public string IdxName { get; set; } = "";        // 中类名称
    public decimal Qty { get; set; }                 // 数量
}

/// <summary>
/// 批号有效期作业 - 查询条件模型
/// </summary>
public class BatchValidityOperationQuery
{
    public string DateRange { get; set; } = "";
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string Wh { get; set; } = "";
    public string PrdNoFrom { get; set; } = "";
    public string PrdNoTo { get; set; } = "";
    public string BatNo { get; set; } = "";
    public string IdxNo { get; set; } = "";
    public bool EmptyValidDateOnly { get; set; } = false;
}
