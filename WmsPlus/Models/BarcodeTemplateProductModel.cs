namespace WmsPlus.Models
{
    public class BarcodeTemplateProductModel
    {
        public string BarType { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string MidClassNo { get; set; } = "";
        public string MidClassName { get; set; } = "";
        public string TemplateCode { get; set; } = "";
        public string TemplateName { get; set; } = "";
    }

    public class BarcodeTemplateProductQuery
    {
        public string BarType { get; set; } = "";
    }
}
