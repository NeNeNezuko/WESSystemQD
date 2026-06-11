namespace WmsPlus.Models
{
    public class BarcodeBoxHistoryModel
    {
        public int SeqNo { get; set; }
        public DateTime? ChangeTime { get; set; }
        public string BoxBarcode { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string SourceDocType { get; set; } = "";
        public string DocName { get; set; } = "";
    }

    public class BarcodeBoxHistoryQuery
    {
        public string ChangeDateRange { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string BoxBarcodeFrom { get; set; } = "";
        public string BoxBarcodeTo { get; set; } = "";
    }
}
