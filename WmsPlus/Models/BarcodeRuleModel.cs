namespace WmsPlus.Models
{
    // 数据展示模型（对应前端表格一行数据）
    public class BarcodeRuleModel
    {
        public string RuleCode { get; set; } = "";
        public string RuleName { get; set; } = "";
        public int? FlowLength { get; set; }
        public string Separator { get; set; } = "";
    }

    // 查询条件模型
    public class BarcodeRuleQuery
    {
        public string RuleCode { get; set; } = "";
        public string RuleName { get; set; } = "";
    }
}
