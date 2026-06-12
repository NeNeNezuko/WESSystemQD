namespace WmsPlus.Models
{
    public class ThirdPartyPushHistoryModel
    {
        public long? HisNo { get; set; }
        public string ActId { get; set; } = "";
        public string ActNo { get; set; } = "";
        public string MethodNo { get; set; } = "";
        public string HttpMethod { get; set; } = "";
        public string BilId { get; set; } = "";
        public string BilNo { get; set; } = "";
        public string OthBilId { get; set; } = "";
        public string StatusId { get; set; } = "";
        public string ErrMsg { get; set; } = "";
        public string HttpCode { get; set; } = "";
        public int? PushSize { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }

    public class ThirdPartyPushHistoryQuery
    {
        public string ActNo { get; set; } = "";
        public string ActId { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
