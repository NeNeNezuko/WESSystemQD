namespace WmsPlus.Models
{
    /// <summary>
    /// 出库通知单派工 — 主表格行数据（通知单）
    /// 数据来源：MF_CKTZ（表头）+ TF_CKTZ（表身）
    /// </summary>
    public class OutboundNoticeDispatchModel
    {
        public int ItemNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string ErpOrderNo { get; set; } = "";
        public DateTime? EstDeliveryDate { get; set; }
        public string CustomerCode { get; set; } = "";
        public string CustomerName { get; set; } = "";
        public string HandlerName { get; set; } = "";
        public string BillCategory { get; set; } = "";       // 单据类别：出库通知单/调拨通知单
        public int Priority { get; set; }                    // 优先级（数据来源待确认，先留空）
        public bool IsClosed { get; set; }
        public string DispatchStatus { get; set; } = "";     // 派工状态：未派工/已派工
        public string Remark { get; set; } = "";             // 单据说明
        public string PickerName { get; set; } = "";         // 拣货员名称
        public bool IsSelected { get; set; }                 // 勾选状态
    }

    /// <summary>
    /// 出库通知单派工 — 明细表格行数据（通知单单据明细）
    /// 数据来源：TF_CKTZ（表身）
    /// </summary>
    public class OutboundNoticeDispatchDetailModel
    {
        public string DocumentNumber { get; set; } = "";
        public int ItemNo { get; set; }
        public string PrdNo { get; set; } = "";             // 货品代号/料号
        public string PrdName { get; set; } = "";           // 货品名称/品名
        public string PrdSpec { get; set; } = "";           // 批号/规格型号
        public string WarehouseCode { get; set; } = "";     // 仓库代号
        public string WarehouseName { get; set; } = "";     // 仓库名称
        public string Unit { get; set; } = "";              // 单位
        public decimal Qty { get; set; }                     // 数量
        public decimal PlannedDeductionQty { get; set; }     // 已转计划出库量（数据来源待确认，先留空）
        public decimal PickedQty { get; set; }               // 已提数量（数据来源待确认，先留空）
        public decimal ReturnedQty { get; set; }             // 已退数量
        public decimal OutboundQty { get; set; }             // 已出库量
        public int ErpApplyItemNo { get; set; }              // ERP申请单项次
        public string BizOrderNo { get; set; } = "";         // 业务单号
        public string Summary { get; set; } = "";            // 摘要
    }

    /// <summary>
    /// 出库通知单派工 — 查询条件模型
    /// </summary>
    public class OutboundNoticeDispatchQuery
    {
        /// <summary>来源单据类别（多选逗号分隔）：出库通知单,调拨通知单</summary>
        public string SourceTypes { get; set; } = "出库通知单,调拨通知单";

        /// <summary>预计出货日期范围 "yyyy-MM-dd → yyyy-MM-dd"</summary>
        public string EstDateRange { get; set; } = "";

        /// <summary>单据日期范围 "yyyy-MM-dd → yyyy-MM-dd"</summary>
        public string DateRange { get; set; } = "";

        public string WarehouseCode { get; set; } = "";
        public string BusinessType { get; set; } = "";
        public string ErpOrderNo { get; set; } = "";
        public string DocumentNumber { get; set; } = "";
        public string BizOrderNo { get; set; } = "";

        /// <summary>派工状态：全部/未派工/已派工</summary>
        public string DispatchStatus { get; set; } = "全部";

        /// <summary>拣货结案标记：全部/未派工/已派工</summary>
        public string PickCloseStatus { get; set; } = "未派工";
    }
}
