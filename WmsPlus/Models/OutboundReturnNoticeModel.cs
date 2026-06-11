namespace WmsPlus.Models
{
    public class OutboundReturnNoticeModel
    {
        public int ItemNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string DeptName { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string CustomerCode { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string ErpBillId { get; set; } = "";
        public string ErpBillNo { get; set; } = "";
        // 表身明细字段（默认隐藏）
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string PrdMark { get; set; } = "";
        public decimal Qty { get; set; }
        public string Unit { get; set; } = "";
    }

    public class OutboundReturnNoticeQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DocumentNumber { get; set; } = "";       // 单据号码
        public string OutboundNoticeNumber { get; set; } = "";// 出库通知单号
        public string BusinessOrderNumber { get; set; } = "";// 业务单号
        public string ApplyOrderNumber { get; set; } = "";   // 申请单号
        public string DeptCode { get; set; } = "";           // 部门代号
        public string WarehouseCode { get; set; } = "";       // 仓库代号
        public string CustomerCode { get; set; } = "";        // 客户代号
    }
}
