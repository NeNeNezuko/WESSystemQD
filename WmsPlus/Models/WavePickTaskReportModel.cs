namespace WmsPlus.Models
{
    public class WavePickTaskReportModel
    {
        public int ItemNo { get; set; }
        public string JrNo { get; set; } = "";
        public DateTime JrDd { get; set; }
        public string Wh { get; set; } = "";
        public string TzNo { get; set; } = "";       // 来源单号
        public int? TzItm { get; set; }               // 来源单项次
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string PrdMark { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string Chuw { get; set; } = "";         // 储位
        public string Unit { get; set; } = "";
        public decimal Qty { get; set; }
        public decimal QtyPk { get; set; }             // 拣货数量
        public string ContainCode { get; set; } = "";  // 容器编号
        public string CarNo { get; set; } = "";        // 车号
        public string Rem { get; set; } = "";
        // 统计表字段
        public string Dep { get; set; } = "";
        public string SalNo { get; set; } = "";
        public string TypeId { get; set; } = "";
        public string ClsId { get; set; } = "";
        public string PriorityWcs { get; set; } = "";  // 优先级
        public string WorkStation { get; set; } = "";   // 工作站
        public string Usr { get; set; } = "";
        public DateTime SysDate { get; set; }
    }

    public class WavePickTaskReportQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string JrNo { get; set; } = "";          // 任务单号
        public string Wh { get; set; } = "";
        public string BcNo { get; set; } = "";           // 波次单号
    }
}
