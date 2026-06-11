namespace WmsPlus.Models
{
    public class DocTypeSettingModel
    {
        public string SpcId { get; set; } = "";
        public string SpcNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string Rem { get; set; } = "";
    }

    public class DocTypeSettingQuery
    {
        public string SpcId { get; set; } = "全部";
        public string SpcNo { get; set; } = "";
    }
}
