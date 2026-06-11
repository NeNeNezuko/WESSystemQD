namespace WmsPlus.Models
{
    public class BarcodePrintProductModel
    {
        public string ScanCode { get; set; } = "";
        public string Barcode { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string SourceNo { get; set; } = "";
        public string SourceDoc { get; set; } = "";
        public DateTime? ValidDate { get; set; }
        public DateTime? LastPrintTime { get; set; }
    }

    public class BarcodePrintProductQuery
    {
        public string DateRange { get; set; } = "";
        public string LastPrintTimeRange { get; set; } = "";
        public string SourceNo { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string InputUser { get; set; } = "";
        public string InputBatch { get; set; } = "";
        public string CusNo { get; set; } = "";
        public string BarcodeFrom { get; set; } = "";
        public string BarcodeTo { get; set; } = "";
    }
}
