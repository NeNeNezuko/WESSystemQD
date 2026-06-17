namespace WmsPlus.Models
{
    public class StorageLocationDetailModel
    {
        public int Seq { get; set; }
        public string Chuw { get; set; } = "";
        public string Name { get; set; } = "";
        public string Wh { get; set; } = "";
        public string WhName { get; set; } = "";
        public string Gs { get; set; } = "";
        public string Gl { get; set; } = "";
        public string Layer { get; set; } = "";
        public string CwStatus { get; set; } = "";
        public string LockCw { get; set; } = "";
        public string LayerProp { get; set; } = "";
        public string LkhjType { get; set; } = "";
        public string CwUnmatch { get; set; } = "";
        public string GsPat { get; set; } = "";
        public string AreaId { get; set; } = "";
    }

    public class StorageLocationDetailQuery
    {
        public string Chuw { get; set; } = "";
        public string Wh { get; set; } = "";
        public string CwStatus { get; set; } = "全部";
        public string Gs { get; set; } = "";
        public string Gl { get; set; } = "";
        public string Layer { get; set; } = "";
        public string LockCw { get; set; } = "全部";
        public string LayerProp { get; set; } = "全部";
        public string LkhjType { get; set; } = "全部";
        public string CwUnmatch { get; set; } = "全部";
    }
}
