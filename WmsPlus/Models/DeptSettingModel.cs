namespace WmsPlus.Models
{
    public class DeptSettingModel
    {
        public string Dep { get; set; } = "";
        public string Name { get; set; } = "";
        public string Up { get; set; } = "";
        public string MakeId { get; set; } = "";
        public DateTime? StopDd { get; set; }
    }

    public class DeptSettingQuery
    {
        public string Dep { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
