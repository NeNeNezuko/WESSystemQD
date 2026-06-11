namespace WmsPlus.Models
{
    /// <summary>验收退回单数据模型</summary>
    public class AcceptanceReturnModel
    {
        public int ItemNo { get; set; }
        public string DocumentNumber { get; set; } = "";
        public DateTime DocumentDate { get; set; }
        public string BilKnd { get; set; } = "";
        public string DepName { get; set; } = "";
        public string BilType { get; set; } = "";
        public string TyNo { get; set; } = "";
        public string Rem { get; set; } = "";
        public string CusNo { get; set; } = "";
        public string CusName { get; set; } = "";
    }

    /// <summary>验收退回单查询条件</summary>
    public class AcceptanceReturnQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string Usr { get; set; } = "";
    }
}
