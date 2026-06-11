namespace WmsPlus.Models
{
    // 数据展示模型（对应前端表格一行数据）
    public class CountReasonSettingModel
    {
        public string BilId { get; set; } = "";
        public string IjReason { get; set; } = "";
        public string ReasonRem { get; set; } = "";
    }

    // 查询条件模型
    public class CountReasonSettingQuery
    {
        public string BilId { get; set; } = "全部";
        public string IjReason { get; set; } = "";
    }
}
