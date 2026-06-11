namespace WmsPlus.Models
{
    public class WaveOrderModel
    {
        public int ItemNo { get; set; }
        public DateTime WaveDate { get; set; }
        public string BcNo { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string DeptName { get; set; } = "";
        public string HandlerName { get; set; } = "";
        public string TicketRemark { get; set; } = "";
    }

    public class WaveOrderQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string BcNo { get; set; } = "";
        public string OutboundNoticeNo { get; set; } = "";
        public string BusinessNo { get; set; } = "";
        public string ApplyNo { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string ReceivingPoint { get; set; } = "";
        public string BusinessType { get; set; } = "";
    }
}
