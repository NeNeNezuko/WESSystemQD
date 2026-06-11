namespace WmsPlus.Models
{
    public class DirectPickTaskModel
    {
        public int ItemNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string DeptName { get; set; } = "";
        public int? Priority { get; set; }
        public string WarehouseCode { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string ContainCode { get; set; } = "";
        public bool ClsId { get; set; }
        public string XjFlag { get; set; } = "";
    }

    public class DirectPickTaskQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string NoticeNumber { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
    }
}
