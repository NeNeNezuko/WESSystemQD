namespace WmsPlus.Models
{
    public class PickReturnModel
    {
        public int ItemNo { get; set; }
        public DateTime ReturnDate { get; set; }
        public string PickReturnNo { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string DeptName { get; set; } = "";
        public string SalName { get; set; } = "";
        public string OthId { get; set; } = "";
        public string OthBilNo { get; set; } = "";
        // 表身明细字段
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string PrdSpec { get; set; } = "";
        public decimal Qty { get; set; }
        public string Unit { get; set; } = "";
    }

    public class PickReturnQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string PickReturnNo { get; set; } = "";
        public string OutboundNoticeNo { get; set; } = "";
        public string BusinessOrderNo { get; set; } = "";
        public string ApplyOrderNumber { get; set; } = "";
        public string DeptCode { get; set; } = "";
    }
}
