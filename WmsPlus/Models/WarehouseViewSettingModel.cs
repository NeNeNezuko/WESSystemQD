namespace WmsPlus.Models
{
    public class WarehouseViewSettingModel
    {
        public string VwNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string StopId { get; set; } = "";
        public string SysId { get; set; } = "";
        public string ChkUsrs { get; set; } = "";
    }

    public class WarehouseViewSettingQuery
    {
        public string VwNo { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
