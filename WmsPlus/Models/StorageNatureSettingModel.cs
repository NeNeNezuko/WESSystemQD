namespace WmsPlus.Models
{
    public class StorageNatureSettingModel
    {
        public string CwxzNo { get; set; } = "";
        public string Name { get; set; } = "";
        public DateTime? UpDd { get; set; }
    }

    public class StorageNatureSettingQuery
    {
        public string CwxzNo { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
