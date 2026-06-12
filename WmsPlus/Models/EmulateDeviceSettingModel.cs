namespace WmsPlus.Models
{
    public class EmulateDeviceSettingModel
    {
        public string EmulateId { get; set; } = "";
        public string DeviceId { get; set; } = "";
        public string TypeId { get; set; } = "";
        public string StatusId { get; set; } = "";
        public DateTime? ModifyDd { get; set; }
    }

    public class EmulateDeviceSettingQuery
    {
        public string DeviceId { get; set; } = "";
        public string TypeId { get; set; } = "";
    }
}
