namespace WmsPlus.Models
{
    public class InspectionOrderDetailModel
    {
        public int ItemNo { get; set; }
        public DateTime? TyDd { get; set; }
        public string TyNo { get; set; } = "";
        public string BilKnd { get; set; } = "";
        public string Tywz { get; set; } = "";
        public string CusNo { get; set; } = "";
        public string CusName { get; set; } = "";
        public string Dep { get; set; } = "";
        public string TypeId { get; set; } = "";
        public string BilNo { get; set; } = "";
        public string OthBilNo { get; set; } = "";
        public string ClsIdSpc { get; set; } = "";
        public string Rem { get; set; } = "";
        public string Usr { get; set; } = "";
        public DateTime? SysDate { get; set; }
    }

    public class InspectionOrderDetailQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string TyNo { get; set; } = "";
        public string Usr { get; set; } = "";
        public string BilKnd { get; set; } = "全部";
        public string Tywz { get; set; } = "全部";
    }

    // 统计表数据模型
    public class InspectionOrderDetailStatisticsModel
    {
        public string GroupBy { get; set; } = "";
        public int TotalCount { get; set; }
        public int CompleteCount { get; set; }
        public int PendingCount { get; set; }
    }
}
