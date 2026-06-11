namespace WmsPlus.Models
{
    public class BarcodeSplitRuleModel
    {
        public string RuleCode { get; set; } = "";
        public string RuleName { get; set; } = "";
        public string BaseBarcode { get; set; } = "";
        public string EncodingMethod { get; set; } = "";
        public string Separator { get; set; } = "";
        public int? TotalLength { get; set; }
        public string DefaultFlag { get; set; } = "";
    }

    public class BarcodeSplitRuleQuery
    {
        public string RuleCode { get; set; } = "";
        public string RuleName { get; set; } = "";
    }
}
