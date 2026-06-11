namespace WmsPlus.Models
{
    public class InboundOrderModel
    {
        public int ItemNo { get; set; }
        public DateTime InboundDate { get; set; }
        public string OrderNumber { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string DeptName { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string VendorCode { get; set; } = "";
        public string VendorName { get; set; } = "";
        public string TransferInId { get; set; } = "";
        public string TransferInOrderNumber { get; set; } = "";
    }

    public class InboundOrderQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string OrderNumber { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string VendorCode { get; set; } = "";
    }
}
