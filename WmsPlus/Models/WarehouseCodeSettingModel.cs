namespace WmsPlus.Models
{
    public class WarehouseCodeSettingModel
    {
        public string Wh { get; set; } = "";
        public string Name { get; set; } = "";
        public string Attrib { get; set; } = "";
        public string Dep { get; set; } = "";
        public string CwFlag { get; set; } = "";
        public string WhType { get; set; } = "";
        public DateTime? StopDd { get; set; }
    }

    public class WarehouseCodeSettingQuery
    {
        public string Wh { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
