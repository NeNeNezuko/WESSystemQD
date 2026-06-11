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
    public string PrdMark { get; set; } = "";
    public DateTime? ValidDDCur { get; set; }
    public DateTime? ValidDDOrg { get; set; }
    public string UpUser { get; set; } = "";
    public DateTime? UpdDate { get; set; }
    public string ConNo { get; set; } = "";
    public string TaskNo { get; set; } = "";
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
