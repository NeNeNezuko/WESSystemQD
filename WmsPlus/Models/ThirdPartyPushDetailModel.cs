namespace WmsPlus.Models
{
    public class ThirdPartyPushDetailModel
    {
        public string ActNo { get; set; } = "";
        public string MethodNo { get; set; } = "";
        public string HttpMethod { get; set; } = "";
        public string Path { get; set; } = "";
        public string BilId { get; set; } = "";
        public string BilNo { get; set; } = "";
        public string OthBilId { get; set; } = "";
        public string OthBilNo { get; set; } = "";
        public string RefId { get; set; } = "";
        public string StatusId { get; set; } = "";
        public string ErrMsg { get; set; } = "";
        public string HttpCode { get; set; } = "";
        public int? RunCount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? SysDate { get; set; }
    }

    public class ThirdPartyPushDetailQuery
    {
        public string ActNo { get; set; } = "";
        public string BilNo { get; set; } = "";
        public string SupNo { get; set; } = "";
        public string Status { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
