namespace WmsPlus.Models
{
    public class InventoryTransferModel
    {
        public int ItemNo { get; set; }
        public string IcNo { get; set; } = "";
        public DateTime DocumentDate { get; set; }
        public string DeptCode { get; set; } = "";
        public string DeptName { get; set; } = "";
        public string SalNo { get; set; } = "";
        public string SalName { get; set; } = "";
        public string BilType { get; set; } = "";
        public string BilNo { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string PrdMark { get; set; } = "";
        public decimal QTY { get; set; }
        public string Unit { get; set; } = "";
        public string WhOut { get; set; } = "";
        public string WhIn { get; set; } = "";
        public string Rem { get; set; } = "";
    }

    public class InventoryTransferQuery
    {
        public string DateRange { get; set; } = "";
        public string DocumentNumber { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string SalNo { get; set; } = "";
        public string BilType { get; set; } = "";
    }
}
