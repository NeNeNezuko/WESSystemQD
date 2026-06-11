namespace WmsPlus.Models
{
    public class InspectionOrderModel
    {
        public int ItemNo { get; set; }
        public string DocumentNumber { get; set; } = "";
        public DateTime DocumentDate { get; set; }
        public string BilKnd { get; set; } = "";
        public string Tywz { get; set; } = "";
        public string Dep { get; set; } = "";
        public string BilType { get; set; } = "";
        public string TypeId { get; set; } = "";
        public string BilNo { get; set; } = "";
        public string Rem { get; set; } = "";
    }

    public class InspectionOrderQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string DocumentNumber { get; set; } = "";
        public string Usr { get; set; } = "";
    }
}
