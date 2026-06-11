namespace WmsPlus.Models
{
    public class StockProfitModel
    {
        public int ItemNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string PdNo { get; set; } = "";
        public string DepName { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string BilTypeName { get; set; } = "";
        public string SalName { get; set; } = "";
    }

    public class StockProfitQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string SalNo { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
    }
}
