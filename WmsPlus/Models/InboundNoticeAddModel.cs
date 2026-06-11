namespace WmsPlus.Models
{
    /// <summary>
    /// 入库通知单*新增 - 表头模型
    /// </summary>
    public class InboundNoticeAddHeader
    {
        // 第1列（左侧）
        public DateTime DocumentDate { get; set; } = DateTime.Today;
        public string VendorCode { get; set; } = "";
        public string VendorName { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string ReceiveOrderNo { get; set; } = "";          // 收货单号
        public string ErpGenerateMode { get; set; } = "";         // ERP存单据生成方式
        public bool IsInboundOrder { get; set; } = false;         // 入库单据

        // 第2列（中间）
        public string DocumentNumber { get; set; } = "";          // 单据号码（只读，自动编号）
        public string DocCategoryCode { get; set; } = "";         // 单据类别
        public string DocCategoryName { get; set; } = "";         // 单据类别名称
        public string OperatorCode { get; set; } = "";            // 经办人
        public string OperatorName { get; set; } = "";            // 经办人名称
        public string ApplyOrderNumber { get; set; } = "";        // 申请单号
        public DateTime? ExpectedDate { get; set; }               // 预入日期
        public string Remark { get; set; } = "";                  // 备注

        // 第3列（右侧）
        public string BusinessType { get; set; } = "";            // 业务类型
        public string BusinessTypeName { get; set; } = "";        // 业务类型名称
        public string VendorCodeRight { get; set; } = "";         // 厂商代号（右侧）
        public string ExternalSystemId { get; set; } = "";         // 外部系统标识
        public string ExternalSystemOrderNo { get; set; } = "";   // 外部系统单号
        public string SourceOrderNo { get; set; } = "";           // 来源单号
        public bool GenerateErpInbound { get; set; } = false;     // 生成ERP入库单据则
    }

    /// <summary>
    /// 入库通知单*新增 - 货品信息明细行
    /// </summary>
    public class InboundNoticeAddProductItem
    {
        public int ItemNo { get; set; }
        public string PrdNo { get; set; } = "";                   // 货品代号
        public string PrdName { get; set; } = "";                 // 货品名称
        public string WhCode { get; set; } = "";                  // 仓库代号
        public string UnitName { get; set; } = "";                // 单位名称
        public decimal Qty { get; set; }                          // 数量
        public string BatchNo { get; set; } = "";                 // 批号
        public decimal ReceivedQty { get; set; }                  // 已入数量
        public string ExtSysOrderNo { get; set; } = "";           // 外部系统单号
        public string ErpApplyNo { get; set; } = "";              // ERP申请单号
        public string BizOrderNo { get; set; } = "";              // 业务单号
    }

    /// <summary>
    /// 入库通知单*新增 - 条码明细行
    /// </summary>
    public class InboundNoticeAddBarcodeItem
    {
        public int ItemNo { get; set; }
        public string BarcodeType { get; set; } = "";             // 条码类型
        public string Barcode { get; set; } = "";                 // 条码
        public decimal ScanQty { get; set; }                      // 扫描数量
        public decimal ActualQty { get; set; }                    // 实品数量
        public string ContainerBarcode { get; set; } = "";        // 容器条码
        public decimal BoxQty { get; set; }                       // 原箱数量
        public string BoxLabelTag { get; set; } = "";             // 箱码条码标签
        public string ContainerPartLabel { get; set; } = "";      // 容器部件标签
    }
}
