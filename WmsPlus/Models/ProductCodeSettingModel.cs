namespace WmsPlus.Models
{
    public class ProductCodeSettingModel
    {
        public string PrdNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string Snm { get; set; } = "";
        public string Idx1 { get; set; } = "";
        public string Ut { get; set; } = "";
        public string Spc { get; set; } = "";
        public string CwxzNo { get; set; } = "";
        public DateTime? NouseDd { get; set; }
    }

    public class ProductCodeSettingQuery
    {
        public string PrdNo { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
