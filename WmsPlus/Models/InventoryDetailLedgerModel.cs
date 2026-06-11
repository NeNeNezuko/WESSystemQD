namespace WmsPlus.Models
{
    public class InventoryDetailLedgerModel
    {
        public DateTime Date { get; set; }
        public string DocumentType { get; set; } = "";
        public string DocumentNumber { get; set; } = "";
        public string Summary { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string Unit { get; set; } = "";
        public decimal InQty { get; set; }
        public decimal OutQty { get; set; }
        public decimal BalanceQty { get; set; }
        public string BatNo { get; set; } = "";
        public string Chuw { get; set; } = "";
        public string WhCode { get; set; } = "";
        public string Rem { get; set; } = "";
    }

    public class InventoryDetailLedgerQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string PrdNo { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
    }
}
