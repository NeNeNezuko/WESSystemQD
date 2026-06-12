namespace WmsPlus.Models
{
    public class ApiExceptionTableModel
    {
        public string ActNo { get; set; } = "";
        public string MethodNo { get; set; } = "";
        public string HttpMethod { get; set; } = "";
        public string Path { get; set; } = "";
        public string BilId { get; set; } = "";
        public string BilNo { get; set; } = "";
        public string Wh { get; set; } = "";
        public string ConNo { get; set; } = "";
        public string StatusId { get; set; } = "";
        public string ErrCode { get; set; } = "";
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // 统计表用
        public int Count { get; set; }
    }

    public class ApiExceptionTableQuery
    {
        public string ActNo { get; set; } = "";
        public string BilNo { get; set; } = "";
        public string Method { get; set; } = "";
        public string Status { get; set; } = "";
        public string StartDate { get; set; } = "";
        public string EndDate { get; set; } = "";
    }
}
