namespace WmsPlus.Models
{
    public class WarehouseCodeSettingModel
    {
        public int Seq { get; set; }
        public string Wh { get; set; } = "";
        public string Name { get; set; } = "";
        public string Attrib { get; set; } = "";
        public string CwFlag { get; set; } = "";
        public string WhType { get; set; } = "";
        public string Dep { get; set; } = "";
        public string DepName { get; set; } = "";
        public string? StopDd { get; set; }
        public string UpWh { get; set; } = "";
        public string UpWhName { get; set; } = "";
    }

    public class WarehouseCodeSettingQuery
    {
        public string Wh { get; set; } = "";
        public string Name { get; set; } = "";
        public string Attrib { get; set; } = "";
        public string UpWh { get; set; } = "";
        public string Dep { get; set; } = "";
    }
}
