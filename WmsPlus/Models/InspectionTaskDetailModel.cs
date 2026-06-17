namespace WmsPlus.Models
{
    public class InspectionTaskDetailModel
    {
        public int ItemNo { get; set; }
        public DateTime? QjDd { get; set; }
        public string QjNo { get; set; } = "";
        public string WhTy { get; set; } = "";
        public string ConNo { get; set; } = "";
        public string Dep { get; set; } = "";
        public string BilKnd { get; set; } = "";
        public string Tywz { get; set; } = "";
        public string TnNo { get; set; } = "";
        public string XjFlag { get; set; } = "";
        public string Rem { get; set; } = "";
        public string Usr { get; set; } = "";
        public DateTime? SysDate { get; set; }
    }

    public class InspectionTaskDetailQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string QjNoFrom { get; set; } = "";
        public string QjNoTo { get; set; } = "";
        public string ConBarcode { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string Wh { get; set; } = "";
        public string WhTy { get; set; } = "";
        public string Usr { get; set; } = "";
        public string Tywz { get; set; } = "全部";
    }

    // 统计表数据模型
    public class InspectionTaskDetailStatisticsModel
    {
        public string GroupBy { get; set; } = "";
        public int TotalCount { get; set; }
        public int CompleteCount { get; set; }
        public int PendingCount { get; set; }
    }
}
