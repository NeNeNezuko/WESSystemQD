namespace WmsPlus.Models
{
    /// <summary>
    /// 收货报表-明细表行数据
    /// </summary>
    public class ReceivingReportDetailDto
    {
        public int RowNo { get; set; }
        public int? Itm { get; set; }
        public DateTime? ShDate { get; set; }
        public string BilNo { get; set; } = "";
        public string ShNo { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string WhName { get; set; } = "";
        public decimal? Qty { get; set; }
        public decimal? Qty1 { get; set; }
        public string Unit { get; set; } = "";
        public string DepName { get; set; } = "";
    }

    /// <summary>
    /// 收货报表-统计表行数据
    /// </summary>
    public class ReceivingReportSummaryDto
    {
        public int RowNo { get; set; }
        public string? ShDateYear { get; set; }
        public string? ShDateQuarter { get; set; }
        public string? ShDateMonth { get; set; }
        public string? ShDateWeek { get; set; }
        public DateTime? ShDateDay { get; set; }
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string Spc { get; set; } = "";
        public string WhErp { get; set; } = "";
        public string WhName { get; set; } = "";
        public string CusName { get; set; } = "";
        public decimal? TotalQty { get; set; }
    }

    /// <summary>
    /// 收货报表查询条件
    /// </summary>
    public class ReceivingReportQuery
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string BilNoFrom { get; set; } = "";
        public string BilNoTo { get; set; } = "";
        public string ShNoFrom { get; set; } = "";
        public string ShNoTo { get; set; } = "";
        public string PrdNoFrom { get; set; } = "";
        public string PrdNoTo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public bool PrdNameFuzzy { get; set; }
        public string Wh { get; set; } = "";
        public bool IncludeOffShelf { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
