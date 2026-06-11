namespace WmsPlus.Models
{
    public class NoticeSettingModel
    {
        public string SetNo { get; set; } = "";
        public string TypeId { get; set; } = "";
        public string Wh { get; set; } = "";
        public string SendObj { get; set; } = "";
        public string SendType { get; set; } = "";
        public string StopId { get; set; } = "";
    }

    public class NoticeSettingQuery
    {
        public string SetNo { get; set; } = "";
        public string Wh { get; set; } = "";
    }
}
