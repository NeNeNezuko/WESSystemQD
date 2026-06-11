namespace WmsPlus.Models
{
    public class PickingOrderModel
    {
        public int ItemNo { get; set; }
        public DateTime PickDate { get; set; }
        public string PickNumber { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string DeptName { get; set; } = "";
        public string UserName { get; set; } = "";
        public string SourceType { get; set; } = "";
        public string SourceNo { get; set; } = "";
    }

    public class PickingOrderQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string PickNumber { get; set; } = "";
        public string OutboundNoticeNumber { get; set; } = "";
        public string BusinessOrderNumber { get; set; } = "";
        public string ApplyOrderNumber { get; set; } = "";
        public string DeptCode { get; set; } = "";
    }
}
