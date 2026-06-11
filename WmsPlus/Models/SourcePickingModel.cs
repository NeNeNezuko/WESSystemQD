namespace WmsPlus.Models
{
    // ====== 批量上架相关模型 ======
    public class BatchShelvingModel
    {
        public int ItemNo { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string BusinessType { get; set; } = "";
        public string ErpApplyOrderNumber { get; set; } = "";
        public string BusinessOrderNumber { get; set; } = "";
        public DateTime? PreOutboundDate { get; set; }
        public string CustomerName { get; set; } = "";
        public string DeptName { get; set; } = "";
        public string Priority { get; set; } = ""; // 优先级
    }

    public class BatchShelvingQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string WarehouseCode { get; set; } = "";
        public string DocumentNumber { get; set; } = "";
        public string BusinessType { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string CustomerCode { get; set; } = "";
        public string BusinessOrderNumber { get; set; } = "";
        public string ErpApplyOrderNumber { get; set; } = "";
        public string ReceivePoint { get; set; } = "";
        public string ProductFrom { get; set; } = "";   // 起止货品-起
        public string ProductTo { get; set; } = "";     // 起止货品-止
        public bool ExcludeShelvedTasks { get; set; } = false; // 不手唤已下架任务量
        public bool SourceNoticeOutbound { get; set; } = true;  // 来源: 出库通知单
        public bool SourceTransferNotice { get; set; } = true;  // 来源: 调拨通知单
    }

    // ====== 依来源单配货 - 直接拣货任务单Tab 主表模型 ======
    public class SourcePickMainModel
    {
        public int ItemNo { get; set; }
        public string TaskNumber { get; set; } = "";       // 直接拣货任务单号 JR_NO
        public DateTime? ShelvingDate { get; set; }         // 下架日期(MODIFY_DD)
        public string DeptName { get; set; } = "";          // 部门名称
        public string Priority { get; set; } = "";          // 优先级(PRIORITY_WCS)
        public string WarehouseName { get; set; } = "";     // 仓库名称(WH)
        public string ContainerBarcode { get; set; } = "";  // 容器条码(CONTAIN_CODE)
        public string CloseMark { get; set; } = "";         // 结案标记(CLS_ID)
        public string ShelvingMark { get; set; } = "";      // 下架标记(XJ_FLAG)
    }

    public class SourcePickTaskQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DocumentNumber { get; set; } = "";    // 单据号码(JR_NO)
        public string BusinessType { get; set; } = "";      // 业务类型(TYPE_ID)
        public string WarehouseCode { get; set; } = "";     // 仓库代号(WH)
        public string ApplyOrderNumber { get; set; } = "";  // 申请单号
        public string NoticeNumber { get; set; } = "";      // 通知单号(PR_NO/TZ_NO)
        public string BusinessOrderNumber { get; set; } = "";// 业务单号
        public string PrintLabelType { get; set; } = "全部";// 打印标签列示
    }

    // ====== 依来源单配货 - 直接拣货任务单Tab 明细模型（TF_XJRW）======
    public class SourcePickDetailModel
    {
        public int ItemNo { get; set; }
        public string TaskNumber { get; set; } = "";       // 直接拣货任务单号(JR_NO)
        public int Itm { get; set; }                        // 项次
        public string PrdNo { get; set; } = "";            // 货品代号
        public string PrdName { get; set; } = "";          // 货品名称
        public string BatNo { get; set; } = "";            // 批号
        public string Unit { get; set; } = "";             // 单位
        public decimal Qty { get; set; }                    // 数量(QTY)
        public decimal PickQty { get; set; }               // 已拣货量(QTY_PK)
        public string PasteId { get; set; } = "";          // 通贴单ID(ERP_BIL_ID)
        public string PasteNo { get; set; } = "";          // 通贴单号(ERP_BIL_NO)
        public int PasteItm { get; set; }                  // 通贴单次(ERP_BIL_ITM)
        public string Ef { get; set; } = "";               // EF
    }
}
