namespace WmsPlus.Models
{
    public class BarcodePrintSerialModel
    {
        public string ScanCode { get; set; } = "";
        public string SerialNo { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string SourceNo { get; set; } = "";
        public int? SourceItm { get; set; }
        public DateTime? ValidDate { get; set; }
        public DateTime? LastPrintTime { get; set; }
    }

    public class BarcodePrintSerialQuery
    {
        public string DateRange { get; set; } = "";
        public string LastPrintTimeRange { get; set; } = "";
        public string SourceNo { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string InputUser { get; set; } = "";
        public string CusNo { get; set; } = "";
        public string SerialFrom { get; set; } = "";
        public string SerialTo { get; set; } = "";
        public string InventoryDateRange { get; set; } = "";
        public bool ShowEmptyOnly { get; set; }
    }
}
