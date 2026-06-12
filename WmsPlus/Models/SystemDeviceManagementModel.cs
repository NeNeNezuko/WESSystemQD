namespace WmsPlus.Models
{
    public class SystemDeviceManagementModel
    {
        public string HwNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string Ip { get; set; } = "";
        public string Port { get; set; } = "";
        public string ModelNo { get; set; } = "";
        public string TypeNo { get; set; } = "";
        public string Wh { get; set; } = "";
        public string StopId { get; set; } = "";
    }

    public class SystemDeviceManagementQuery
    {
        public string HwNo { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
