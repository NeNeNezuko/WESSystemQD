namespace WmsPlus.Models
{
    public class StorageShelvingModel
    {
        public int ItemNo { get; set; }
        public DateTime DocumentDate { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string SalNo { get; set; } = "";
        public string BilType { get; set; } = "";
        public string BilId { get; set; } = "";
        public string BilNo { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string PrdMark { get; set; } = "";
        public decimal QTY { get; set; }
        public string Unit { get; set; } = "";
        public string WH { get; set; } = "";
        public string CHUW { get; set; } = "";
    }

    public class StorageShelvingQuery
    {
        public string DateRange { get; set; } = "";
        public string DocumentNumber { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string SalNo { get; set; } = "";
        public string BilType { get; set; } = "";
    }
}
