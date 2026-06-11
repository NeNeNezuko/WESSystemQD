namespace WmsPlus.Models
{
    public class WaveOrderReportModel
    {
        public int ItemNo { get; set; }
        public string BcNo { get; set; } = "";
        public DateTime BcDd { get; set; }
        public string Wh { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string PrdMark { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string Unit { get; set; } = "";
        public decimal Qty { get; set; }
        public decimal PickQty { get; set; }
        public string Rem { get; set; } = "";
        // 统计表字段
        public string Dep { get; set; } = "";
        public string BilType { get; set; } = "";
        public string SalNo { get; set; } = "";
        public string ClsId { get; set; } = "";
        public string Usr { get; set; } = "";
        public DateTime SysDate { get; set; }
    }

    public class WaveOrderReportQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string BcNo { get; set; } = "";
        public string Wh { get; set; } = "";
        public string Status { get; set; } = "全部";
    }
}
