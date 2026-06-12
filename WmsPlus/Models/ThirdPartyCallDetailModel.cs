namespace WmsPlus.Models
{
    public class ThirdPartyCallDetailModel
    {
        public string ActNo { get; set; } = "";
        public string ActNoMain { get; set; } = "";
        public string MethodNo { get; set; } = "";
        public string HttpMethod { get; set; } = "";
        public string BilId { get; set; } = "";
        public string BilNo { get; set; } = "";
        public string OthBilId { get; set; } = "";
        public string Wh { get; set; } = "";
        public string StatusId { get; set; } = "";
        public string ErrCode { get; set; } = "";
        public int RequestSize { get; set; }
        public DateTime? SysDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class ThirdPartyCallDetailQuery
    {
        public string ActNo { get; set; } = "";
        public string BilNo { get; set; } = "";
        public string SupNo { get; set; } = "";
        public string Status { get; set; } = "";
        public string StartDate { get; set; } = "";
        public string EndDate { get; set; } = "";
    }
}
