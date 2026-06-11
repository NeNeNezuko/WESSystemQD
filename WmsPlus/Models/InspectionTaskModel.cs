namespace WmsPlus.Models
{
    public class InspectionTaskModel
    {
        public int ItemNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string WarehouseTy { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string Rem { get; set; } = "";
        public string UsrName { get; set; } = "";
    }

    public class InspectionTaskQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string Usr { get; set; } = "";
        public string WarehouseTy { get; set; } = "";
    }
}
