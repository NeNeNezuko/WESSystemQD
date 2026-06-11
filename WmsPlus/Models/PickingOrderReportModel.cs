namespace WmsPlus.Models
{
    public class PickingOrderReportModel
    {
        public int ItemNo { get; set; }
        public string JrNo { get; set; } = "";          // 拣货单号
        public DateTime JrDd { get; set; }
        public string Wh { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string PrdMark { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string Chuw { get; set; } = "";
        public string Unit { get; set; } = "";
        public decimal Qty { get; set; }
        public decimal QtyPk { get; set; }
        public string Rem { get; set; } = "";
        // 统计表字段
        public string Dep { get; set; } = "";
        public string SalNo { get; set; } = "";
        public string TypeId { get; set; } = "";
        public string ClsId { get; set; } = "";
        public string Usr { get; set; } = "";
        public DateTime SysDate { get; set; }
    }

    public class PickingOrderReportQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string JrNo { get; set; } = "";            // 拣货单号
        public string Wh { get; set; } = "";
    }
}
