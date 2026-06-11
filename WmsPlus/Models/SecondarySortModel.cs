namespace WmsPlus.Models
{
    /// <summary>
    /// 二次分拣单数据模型（对应前端表格行）
    /// </summary>
    public class SecondarySortModel
    {
        /// <summary>项次（来自表身 TF_PKFJ）</summary>
        public int ItemNo { get; set; }

        /// <summary>分拣日期（来自表头 MF_PKFJ）</summary>
        public DateTime SortDate { get; set; }

        /// <summary>分拣单号（关联主键）</summary>
        public string SortNumber { get; set; } = "";

        /// <summary>部门名称（来自表头 MF_PKFJ.DEP 关联 DEPT 表）</summary>
        public string DeptName { get; set; } = "";

        /// <summary>经办人名称（来自表头 MF_PKFJ.USR_NAME）</summary>
        public string OperatorName { get; set; } = "";

        /// <summary>转入单ID（来自表头 MF_PKFJ.OTH_BIL_ID）</summary>
        public string SourceOrderId { get; set; } = "";

        /// <summary>转入单号（来自表头 MF_PKFJ.OTH_BIL_NO）</summary>
        public string SourceOrderNo { get; set; } = "";
    }

    /// <summary>
    /// 二次分拣单查询条件模型
    /// </summary>
    public class SecondarySortQuery
    {
        /// <summary>分拣日期范围（格式：yyyy-MM-dd → yyyy-MM-dd）</summary>
        public string DateRange { get; set; } = "";

        /// <summary>分拣单号</summary>
        public string SortNumber { get; set; } = "";

        /// <summary>出库通知单号</summary>
        public string OutboundNoticeNo { get; set; } = "";

        /// <summary>业务单号</summary>
        public string BizOrderNumber { get; set; } = "";

        /// <summary>申请单号</summary>
        public string ApplyOrderNumber { get; set; } = "";

        /// <summary>部门代号</summary>
        public string DeptCode { get; set; } = "";
    }
}
