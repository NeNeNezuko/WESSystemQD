namespace WmsPlus.Models
{
    // 打印网点列表模型
    public class PrintSiteListModel
    {
        public int SeqNo { get; set; }
        public string SiteName { get; set; } = "";
        public string MachineIp { get; set; } = "";
        public string MachineName { get; set; } = "";
        public string StopFlag { get; set; } = "";
        public DateTime? StopDate { get; set; }
    }

    // 打印网点列表查询
    public class PrintSiteListQuery
    {
        public string SiteName { get; set; } = "";
        public string MachineIp { get; set; } = "";
        public string StopFlagText { get; set; } = "";
        public bool StopFlag { get; set; }
    }

    // 打印网点资料查询模型
    public class PrintSiteQueryModel
    {
        public int SeqNo { get; set; }
        public DateTime? PrintTime { get; set; }
        public string VersionCode { get; set; } = "";
        public string PrinterUser { get; set; } = "";
        public string SiteName { get; set; } = "";
        public string ProgramCode { get; set; } = "";
        public string TemplateCode { get; set; } = "";
        public string PrintStatus { get; set; } = "";
        public int? FailCount { get; set; }
        public string FailReason { get; set; } = "";
        public string PrintNo { get; set; } = "";
    }

    // 打印网点资料查询条件
    public class PrintSiteQueryQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string MakeDateRange { get; set; } = "";
        public string VersionCode { get; set; } = "";
        public string PrinterUser { get; set; } = "";
        public string SiteName { get; set; } = "";
    }
}
