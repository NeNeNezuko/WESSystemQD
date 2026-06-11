namespace WmsPlus.Models
{
    public class OutboundNoticeModel
    {
        public int ItemNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string CustomerCode { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public bool IsClosed { get; set; }
        // 表身明细字段
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string PrdSpec { get; set; } = "";
        public decimal Qty { get; set; }
        public string Unit { get; set; } = "";
    }

    public class OutboundNoticeQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string CustomerCode { get; set; } = "";
        public string CloseStatus { get; set; } = "全部";
        public string BusinessType { get; set; } = "";
        // 以下为参考图中的扩展筛选项（样式已实现，查询功能后续补充）
        public string DeliveryDateRange { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string ErpOrderId { get; set; } = "";
        public string ErpOrderNo { get; set; } = "";
        public string DispatchStatus { get; set; } = "";
        public string DesignatedName { get; set; } = "";
        public string BizParam { get; set; } = "";
        public string ApplyOrderNumber { get; set; } = "";
        public string ContractCode { get; set; } = "";
        public string TaxStatus { get; set; } = "";
        public string DeliveryAddress { get; set; } = "全部";
        public string ReceiveCheck { get; set; } = "全部";
        public string OutboundStrategy { get; set; } = "全部";
    }
}
