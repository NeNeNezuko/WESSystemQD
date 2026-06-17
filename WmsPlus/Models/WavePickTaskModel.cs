namespace WmsPlus.Models
{
    public class WavePickTaskModel
    {
        public int ItemNo { get; set; }
        public DateTime JrDate { get; set; }
        public string JrNo { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string DepCode { get; set; } = "";
        public string DepName { get; set; } = "";
        public string UsrName { get; set; } = "";
        public string AreaSh { get; set; } = "";
        public string BcNo { get; set; } = "";
        public string TzNo { get; set; } = "";
        public string OrgBilNo { get; set; } = "";
        public string StatusPg { get; set; } = "0";
        public string ClsId { get; set; } = "N";
        public string PkFlow { get; set; } = "";
        public int Priority { get; set; }
        public string UsrPk { get; set; } = "";
        public string SalNo { get; set; } = "";
        public string TypeId { get; set; } = "";
        // 表身明细字段
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string PrdMark { get; set; } = "";
        public decimal Qty { get; set; }
        public decimal QtyPk { get; set; }
        public string Unit { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string Chuw { get; set; } = "";
        public string CarNo { get; set; } = "";
    }

    public class WavePickTaskQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string JrNo { get; set; } = "";           // 波次拣货任务单号
        public string TzNo { get; set; } = "";             // 出库通知单号
        public string OrgBilNo { get; set; } = "";         // 业务单号
        public string ApplyNo { get; set; } = "";          // 申请单号
        public string DepCode { get; set; } = "";          // 部门代号
        public string WarehouseCode { get; set; } = "";    // 仓库代号
        public string AreaSh { get; set; } = "";           // 收货点
        public string PkFlowMark { get; set; } = "全部";   // 捡线策略标记
        public string ClsMark { get; set; } = "全部";      // 拣货结案标记
        public string BusinessType { get; set; } = "";     // 业务类型
    }
}
