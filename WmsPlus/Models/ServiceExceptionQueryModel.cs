namespace WmsPlus.Models
{
    public class ServiceExceptionQueryModel
    {
        public string YcNo { get; set; } = "";
        public string SvcNo { get; set; } = "";
        public string SvcNo1 { get; set; } = "";
        public string Rem { get; set; } = "";
        public DateTime? SysDate { get; set; }
    }

    public class ServiceExceptionQueryQuery
    {
        public string SvcNo { get; set; } = "";
        public string YcNo { get; set; } = "";
        public string StartDate { get; set; } = "";
        public string EndDate { get; set; } = "";
    }
}
