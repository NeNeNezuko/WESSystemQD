namespace WmsPlus.Models
{
    public class ServiceLogQueryModel
    {
        public string SvcNo { get; set; } = "";
        public string SvcNo1 { get; set; } = "";
        public string Name { get; set; } = "";
        public string Name1 { get; set; } = "";
        public string Path { get; set; } = "";
        public string IntervalTime { get; set; } = "";
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Rem { get; set; } = "";
    }

    public class ServiceLogQueryQuery
    {
        public string SvcNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string StartDate { get; set; } = "";
        public string EndDate { get; set; } = "";
    }
}
