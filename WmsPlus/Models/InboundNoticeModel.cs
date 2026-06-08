namespace WmsPlus.Models
{
    public class InboundNoticeModel
    {
        public int ItemNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string VendorCode { get; set; } = "";
        public string VendorName { get; set; } = "";
        public string ApplyOrderNumber { get; set; } = "";
        public string OrderNumber { get; set; } = "";
        public bool IsClosed { get; set; }
        // 表身明细字段
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string PrdSpec { get; set; } = "";
        public decimal Qty { get; set; }
        public string Unit { get; set; } = "";
    }

    public class InboundNoticeQuery
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string VendorCode { get; set; } = "";
        public string CloseStatus { get; set; } = "全部";
        public string ApplyOrderNumber { get; set; } = "";
        public string BusinessType { get; set; } = "";
    }
}
