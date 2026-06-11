namespace WmsPlus.Models
{
    public class ProductMarkSettingModel
    {
        public string MobId { get; set; } = "";
        public string MobName { get; set; } = "";
        public string PrdMark { get; set; } = "";
        public string Rem { get; set; } = "";
        public DateTime? EndDd { get; set; }
    }

    public class ProductMarkSettingQuery
    {
        public string MobId { get; set; } = "";
        public string MobName { get; set; } = "";
    }
}
