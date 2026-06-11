namespace WmsPlus.Models
{
    public class InboundNoticeChangeModel
    {
        public int ItemNo { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ChangeNumber { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string DeptName { get; set; } = "";
        public string BusinessType { get; set; } = "";
        public string BusinessTypeName { get; set; } = "";
        public string ExecuteStatus { get; set; } = "";
        public string Creator { get; set; } = "";
        public string CreatorName { get; set; } = "";
    }

    public class InboundNoticeChangeQuery
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string ChangeNumber { get; set; } = "";
        public string DocType { get; set; } = "全部";
        public string WarehouseCode { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string BusinessType { get; set; } = "";
    }
}
