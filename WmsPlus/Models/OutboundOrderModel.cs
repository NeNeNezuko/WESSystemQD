namespace WmsPlus.Models
{
    public class OutboundOrderModel
    {
        public int ItemNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string DepCode { get; set; } = "";
        public string DepName { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string RcvPoint { get; set; } = "";
        public string CustomerCode { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string BatId { get; set; } = "";
        public bool IsClosed { get; set; }
        // 表身明细字段
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string PrdSpec { get; set; } = "";
        public decimal Qty { get; set; }
        public string Unit { get; set; } = "";
    }

    public class OutboundOrderQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string CkTzNo { get; set; } = "";
        public string BilNo { get; set; } = "";
        public string ApplyNo { get; set; } = "";
        public string DepCode { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string RcvPoint { get; set; } = "";
        public string CustomerCode { get; set; } = "";
    }
}
