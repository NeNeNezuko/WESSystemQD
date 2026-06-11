namespace WmsPlus.Models
{
    public class BarcodePrintProductSourceModel
    {
        public int SeqNo { get; set; }
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string BatNo { get; set; } = "";
        public int? OrigQty { get; set; }
        public int? PrintedQty { get; set; }
        public int? LabelCount { get; set; }
        public string PrintBarcode { get; set; } = "";
        public string CusNo { get; set; } = "";
        public string SoNo { get; set; } = "";
    }

    public class BarcodePrintProductSourceQuery
    {
        public string SourceDoc { get; set; } = "";
        public string SourceNo { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string CusName { get; set; } = "";
    }
}
