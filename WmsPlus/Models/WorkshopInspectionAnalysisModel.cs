namespace WmsPlus.Models
{
    public class WorkshopInspectionAnalysisModel
    {
        public int ItemNo { get; set; }
        public string CjNo { get; set; } = "";
        public DateTime? CjDd { get; set; }
        public string Wh { get; set; } = "";
        public string SalNo { get; set; } = "";
        public string FlagJy { get; set; } = "";
        public string TyNo { get; set; } = "";
        public DateTime? TyDd { get; set; }
        public string RkNo { get; set; } = "";
        public DateTime? RkDd { get; set; }
        public double? CjToTyHours { get; set; }
        public double? TyToRkHours { get; set; }
        public double? TotalHours { get; set; }
        public string Usr { get; set; } = "";
    }

    public class WorkshopInspectionAnalysisQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string CjNo { get; set; } = "";
        public string Wh { get; set; } = "";
        public string PrdNoFrom { get; set; } = "";
        public string PrdNoTo { get; set; } = "";
        public string ShHoursOperator { get; set; } = "";
        public double? ShHoursValue { get; set; }
        public string JyHoursOperator { get; set; } = "";
        public double? JyHoursValue { get; set; }
        public string SalNo { get; set; } = "";
        public string FlagJy { get; set; } = "全部";
    }
}
