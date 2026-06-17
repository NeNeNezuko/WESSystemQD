namespace WmsPlus.Models
{
    public class TransferNoticeModel
    {
        public int ItemNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string WhOut { get; set; } = "";
        public string WhOutName { get; set; } = "";
        public string WhIn { get; set; } = "";
        public string WhInName { get; set; } = "";
        public string SalNo { get; set; } = "";
        public string SalName { get; set; } = "";
        public DateTime? EstDate { get; set; }
        public DateTime? UpDate { get; set; }
        public string AreaSh { get; set; } = "";
        public string Rem { get; set; } = "";
        public string StatusPg { get; set; } = "";
        public string ClsIdBc { get; set; } = "";
        public string ClsIdCk { get; set; } = "";
        public string TypeId { get; set; } = "";
        public string TypeName { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public decimal Qty { get; set; }
        public string Unit { get; set; } = "";
    }

    public class TransferNoticeQuery
    {
        public string DateRange { get; set; } = "";
        public string EstDateRange { get; set; } = "";
        public string DocumentNumber { get; set; } = "";
        public string WhOut { get; set; } = "";
        public string WhIn { get; set; } = "";
        public string AreaSh { get; set; } = "";
        public string ClsIdBc { get; set; } = "全部";
        public string TypeId { get; set; } = "";
    }
}
