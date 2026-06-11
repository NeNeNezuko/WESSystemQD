namespace WmsPlus.Models
{
    public class BarcodePrintBoxSourceModel
    {
        public int SeqNo { get; set; }
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string BatNo { get; set; } = "";
        public int? OrigQty { get; set; }
        public int? PrintedQty { get; set; }
        public int? ThisPrintQty { get; set; }
        public int? StandardBoxQty { get; set; }
        public int? TailBoxQty { get; set; }
        public int? LabelCount { get; set; }
    }

    public class BarcodePrintBoxSourceQuery
    {
        public string SourceDoc { get; set; } = "";
        public string SourceNo { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string CusName { get; set; } = "";
    }
}
