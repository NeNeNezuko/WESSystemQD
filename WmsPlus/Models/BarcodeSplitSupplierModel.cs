namespace WmsPlus.Models
{
    public class BarcodeSplitSupplierModel
    {
        public int SeqNo { get; set; }
        public string CusNo { get; set; } = "";
        public string CusName { get; set; } = "";
        public string RuleCode { get; set; } = "";
        public string RuleName { get; set; } = "";
    }

    public class BarcodeSplitSupplierQuery
    {
        public string CusNo { get; set; } = "";
    }
}
