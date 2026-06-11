namespace WmsPlus.Models
{
    public class ContainHistoryQueryModel
    {
        public string ContainCode { get; set; } = "";
        public string ContainStatus { get; set; } = "";
        public string ContainType { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string TransitFlag { get; set; } = "";
        public string InspectFlag { get; set; } = "";
        public string ChangeDocName { get; set; } = "";
        public string ChangeNo { get; set; } = "";
        public string ChangeMan { get; set; } = "";
        public DateTime? ChangeTime { get; set; }
    }

    public class ContainHistoryQueryQuery
    {
        public string ChangeTimeRange { get; set; } = "";
        public DateTime? ChangeTimeFrom { get; set; }
        public DateTime? ChangeTimeTo { get; set; }
        public string ContainCode { get; set; } = "";
    }
}
