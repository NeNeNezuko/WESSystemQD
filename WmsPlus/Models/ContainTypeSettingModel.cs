namespace WmsPlus.Models
{
    public class ContainTypeSettingModel
    {
        public int RowNo { get; set; }
        public string TypeCode { get; set; } = "";
        public string TypeName { get; set; } = "";
        public string CodePrefix { get; set; } = "";
        public string StopFlag { get; set; } = "";
        public string IsSystem { get; set; } = "";
        public string RcsType { get; set; } = "";
    }

    public class ContainTypeSettingQuery
    {
        public string TypeCode { get; set; } = "";
        public string TypeName { get; set; } = "";
    }
}
