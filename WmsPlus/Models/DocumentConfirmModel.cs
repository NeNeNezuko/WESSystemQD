namespace WmsPlus.Models
{
    /// <summary>
    /// 单据确认作业 - 数据模型（对应表格行）
    /// </summary>
    public class DocumentConfirmModel
    {
        /// <summary>单据别代码（PD/YN/KU）</summary>
        public string DocType { get; set; } = "";
        /// <summary>单据别名称（盘点单/盘盈单/盘亏单）</summary>
        public string DocTypeName { get; set; } = "";
        /// <summary>单据日期</summary>
        public DateTime DocumentDate { get; set; }
        /// <summary>单据号码</summary>
        public string DocumentNumber { get; set; } = "";
        /// <summary>仓库代号</summary>
        public string WarehouseCode { get; set; } = "";
        /// <summary>仓库名称</summary>
        public string WarehouseName { get; set; } = "";
        /// <summary>制单人代号</summary>
        public string CreatorCode { get; set; } = "";
        /// <summary>制单人名称</summary>
        public string CreatorName { get; set; } = "";
        /// <summary>制单时间</summary>
        public DateTime CreateTime { get; set; }
        /// <summary>确认状态（待确认/已确认）</summary>
        public string ConfirmStatus { get; set; } = "待确认";
        /// <summary>确认人</summary>
        public string ConfirmUser { get; set; } = "";
        /// <summary>确认时间</summary>
        public DateTime? ConfirmTime { get; set; }
    }

    /// <summary>
    /// 单据确认作业 - 查询模型
    /// </summary>
    public class DocumentConfirmQuery
    {
        /// <summary>单据别（PD-盘点单 / YN-盘盈单 / KU-盘亏单）</summary>
        public string DocType { get; set; } = "PD";

        /// <summary>日期范围（"yyyy-MM-dd → yyyy-MM-dd" 格式）</summary>
        public string DateRange { get; set; } = "";

        /// <summary>仓库代号</summary>
        public string WarehouseCode { get; set; } = "";

        /// <summary>起止货品 起</summary>
        public string PrdNoFrom { get; set; } = "";

        /// <summary>起止货品 止</summary>
        public string PrdNoTo { get; set; } = "";
    }
}
