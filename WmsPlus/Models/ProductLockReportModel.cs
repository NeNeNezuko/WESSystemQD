namespace WmsPlus.Models
{
    public class ProductLockReportModel
    {
        public string GUID { get; set; } = "";
        public string WH { get; set; } = "";
        public string PRD_NO { get; set; } = "";
        public string PRD_MARK { get; set; } = "";
        public string BAT_NO { get; set; } = "";
        public string LOCK_NO { get; set; } = "";
        public string ACT_NO { get; set; } = "";
        public DateTime? LOCK_DD { get; set; }
        public DateTime? UNLOCK_DD { get; set; }
    }

    public class ProductLockReportQuery
    {
        public string WarehouseCode { get; set; } = "";
        public string PrdNoFrom { get; set; } = "";
        public string PrdNoTo { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string LockStatus { get; set; } = "";
    }
}
