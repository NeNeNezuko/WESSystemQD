namespace WmsPlus.Models
{
    public class SecondarySortReportModel
    {
        public int ItemNo { get; set; }
        public string PkfjNo { get; set; } = "";       // 分拣单号
        public DateTime PkfjDd { get; set; }
        public string Wh { get; set; } = "";             // 仓库代号(从关联获取)
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public decimal Qty { get; set; }
        public string Rem { get; set; } = "";
        // 统计表字段
        public string Dep { get; set; } = "";
        public string ClsId { get; set; } = "";
        public string Usr { get; set; } = "";
        public DateTime SysDate { get; set; }
    }

    public class SecondarySortReportQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string PkfjNo { get; set; } = "";         // 分拣单号
        public string Wh { get; set; } = "";
    }
}
