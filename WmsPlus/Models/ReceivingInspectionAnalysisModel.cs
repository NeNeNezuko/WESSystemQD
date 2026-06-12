namespace WmsPlus.Models
{
    public class ReceivingInspectionAnalysisModel
    {
        public int ItemNo { get; set; }
        public string ShNo { get; set; } = "";
        public DateTime? ShDd { get; set; }
        public string Wh { get; set; } = "";
        public string CusNo { get; set; } = "";
        public string CusName { get; set; } = "";
        public string JyFlag { get; set; } = "";
        public string TyNo { get; set; } = "";
        public DateTime? TyDd { get; set; }
        public string RkNo { get; set; } = "";
        public DateTime? RkDd { get; set; }
        public double? ShToTyHours { get; set; }
        public double? TyToRkHours { get; set; }
        public double? TotalHours { get; set; }
        public string Usr { get; set; } = "";
    }

    public class ReceivingInspectionAnalysisQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string ShNo { get; set; } = "";
        public string Wh { get; set; } = "";
        public string CusNo { get; set; } = "";
        public string JyFlag { get; set; } = "全部";
    }
}
