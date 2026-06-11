namespace WmsPlus.Models
{
    public class ProductAttrSettingModel
    {
        public string PrdNo { get; set; } = "";
        public string QtyCollect { get; set; } = "";
        public string BarcodeType { get; set; } = "";
        public string NeedScale { get; set; } = "";
        public string QtyQzMode { get; set; } = "";
        public int? UtPoint { get; set; }
        public int? Ut1Point { get; set; }
        public string Ut1Disp { get; set; } = "";
        public string QtyType { get; set; } = "";
        public string ShowType { get; set; } = "";
        public int? ScalePoint { get; set; }
        public string ScaleQz { get; set; } = "";
        public string ShowPak { get; set; } = "";
    }

    public class ProductAttrSettingQuery
    {
        public string PrdNo { get; set; } = "";
    }
}
