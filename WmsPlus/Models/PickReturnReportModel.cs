namespace WmsPlus.Models
{
    public class PickReturnReportModel
    {
        public int ItemNo { get; set; }
        public string JtNo { get; set; } = "";           // 退回单号
        public DateTime JtDd { get; set; }
        public string Wh { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public decimal Qty { get; set; }
        public string Rem { get; set; } = "";
        // 统计表字段
        public string Dep { get; set; } = "";
        public string SalNo { get; set; } = "";
        public string ClsId { get; set; } = "";
        public string Usr { get; set; } = "";
        public DateTime SysDate { get; set; }
    }

    public class PickReturnReportQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string JtNo { get; set; } = "";            // 退回单号
        public string Wh { get; set; } = "";
    }
}
