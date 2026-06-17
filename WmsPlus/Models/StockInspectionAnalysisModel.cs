namespace WmsPlus.Models
{
    public class StockInspectionAnalysisModel
    {
        public int ItemNo { get; set; }
        public string QjNo { get; set; } = "";
        public DateTime? QjDd { get; set; }
        public string WhTy { get; set; } = "";
        public string BilKnd { get; set; } = "";
        public string Tywz { get; set; } = "";
        public string TyNo { get; set; } = "";
        public DateTime? TyDd { get; set; }
        public string IcNo { get; set; } = "";
        public DateTime? IcDd { get; set; }
        public double? QjToTyHours { get; set; }
        public double? TotalHours { get; set; }
        public string Usr { get; set; } = "";
    }

    public class StockInspectionAnalysisQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string QjNo { get; set; } = "";
        public string WhTy { get; set; } = "";
        public string PrdNoFrom { get; set; } = "";
        public string PrdNoTo { get; set; } = "";
        public string JyHoursOperator { get; set; } = "";
        public double? JyHoursValue { get; set; }
        public string BilKnd { get; set; } = "全部";
    }
}
