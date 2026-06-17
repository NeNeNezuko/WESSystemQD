namespace WmsPlus.Models
{
    public class AcceptanceReturnDetailModel
    {
        public int ItemNo { get; set; }
        public DateTime? YbDd { get; set; }
        public string YbNo { get; set; } = "";
        public string CusNo { get; set; } = "";
        public string CusName { get; set; } = "";
        public string TyNo { get; set; } = "";
        public string BilKnd { get; set; } = "";
        public string Dep { get; set; } = "";
        public string TypeId { get; set; } = "";
        public string Rem { get; set; } = "";
        public string Usr { get; set; } = "";
        public DateTime? SysDate { get; set; }
    }

    public class AcceptanceReturnDetailQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string YbNo { get; set; } = "";
        public string CusNo { get; set; } = "";
        public string TyNo { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string Wh { get; set; } = "";
        public string Usr { get; set; } = "";
        public string BilKnd { get; set; } = "全部";
    }

    // 统计表数据模型
    public class AcceptanceReturnDetailStatisticsModel
    {
        public string GroupBy { get; set; } = "";
        public int TotalCount { get; set; }
        public int CompleteCount { get; set; }
        public int PendingCount { get; set; }
    }
}
