namespace WmsPlus.Models
{
    public class KcInspectExpiryModel
    {
        public string WarehouseName { get; set; } = "";
        public string Chuw { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string UnitName { get; set; } = "";
        public decimal Qty { get; set; }
        public decimal Qty1 { get; set; }
        public DateTime? LstTyd { get; set; }
    }

    public class KcInspectExpiryQuery
    {
        public string WarehouseCode { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public int ExceedDays { get; set; } = 0;
    }
}
