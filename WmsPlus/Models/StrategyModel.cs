namespace WmsPlus.Models
{
    public class StrategyModel
    {
        public string RuleId { get; set; } = "";
        public string Name { get; set; } = "";
        public string StopId { get; set; } = "";
        public string WhType { get; set; } = "";
        public string Usr { get; set; } = "";
        public DateTime? SysDate { get; set; }
        public string ModifyMan { get; set; } = "";
        public DateTime? ModifyDd { get; set; }
    }

    public class StrategyQuery
    {
        public string RuleId { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
