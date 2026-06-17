namespace WmsPlus.Models
{
    public class BarcodePrintBoxModel
    {
        public string ScanCode { get; set; } = "";
        public string BoxBarcode { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string BatNo { get; set; } = "";
        public decimal? QTY { get; set; }
        public string SourceNo { get; set; } = "";
        public int? SourceItm { get; set; }
        public DateTime? ValidDate { get; set; }
        public string ChangeHistory { get; set; } = "";
        public DateTime? LastPrintTime { get; set; }
    }

    public class BarcodePrintBoxQuery
    {
        public string DateRange { get; set; } = "";
        public string LastPrintTimeRange { get; set; } = "";
        public string SourceNo { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string InputUser { get; set; } = "";
        public string InputBatch { get; set; } = "";
        public string CusNo { get; set; } = "";
        public string BoxBarcodeFrom { get; set; } = "";
        public string BoxBarcodeTo { get; set; } = "";
        public string InventoryDateRange { get; set; } = "";
        public bool ShowEmptyOnly { get; set; }
        public bool ShowSpecialInspect { get; set; }
        public string OuterBoxFlag { get; set; } = "";
    }
}
