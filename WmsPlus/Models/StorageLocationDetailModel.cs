namespace WmsPlus.Models
{
    public class StorageLocationDetailModel
    {
        public string Chuw { get; set; } = "";
        public string Name { get; set; } = "";
        public string Wh { get; set; } = "";
        public string Gs { get; set; } = "";
        public string Gl { get; set; } = "";
        public string Layer { get; set; } = "";
        public string CwStatus { get; set; } = "";
        public string LockCw { get; set; } = "";
        public string AreaId { get; set; } = "";
    }

    public class StorageLocationDetailQuery
    {
        public string Chuw { get; set; } = "";
        public string Wh { get; set; } = "";
        public string CwStatus { get; set; } = "全部";
    }
}
