namespace WmsPlus.Models
{
    public class ProductStorageByNatureModel
    {
        public string Guid { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string CwxzNo { get; set; } = "";
        public string Chuw { get; set; } = "";
        public string Wh { get; set; } = "";
        public string Gs { get; set; } = "";
        public string Gl { get; set; } = "";
        public string Layer { get; set; } = "";
        public string ZoneId { get; set; } = "";
    }

    public class ProductStorageByNatureQuery
    {
        public string CwxzNo { get; set; } = "";
        public string Wh { get; set; } = "";
    }
}
