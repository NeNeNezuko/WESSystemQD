namespace WmsPlus.Models
{
    public class WaveOrderReportModel
    {
        public int ItemNo { get; set; }
        public string BcNo { get; set; } = "";
        public DateTime BcDd { get; set; }
        public string Wh { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string PrdMark { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string Unit { get; set; } = "";
        public decimal Qty { get; set; }
        public decimal PickQty { get; set; }
        public string Rem { get; set; } = "";
        // 统计表字段
        public string Dep { get; set; } = "";
        public string BilType { get; set; } = "";
        public string SalNo { get; set; } = "";
        public string ClsId { get; set; } = "";
        public string Usr { get; set; } = "";
        public DateTime SysDate { get; set; }
    }

    public class WaveOrderReportQuery
    {
        public string DateRange { get; set; } = "";
        public string DocNo { get; set; } = "";
        public bool FuzzyDocNo { get; set; } = false;
        public string PrdNoFrom { get; set; } = "";
        public string PrdNoTo { get; set; } = "";
        public string Wh { get; set; } = "";
        public bool IncludeChildWh { get; set; } = false;
        public string NoticeNo { get; set; } = "";
        public bool FuzzyNoticeNo { get; set; } = false;
        public string ErpApplyNo { get; set; } = "";
        public bool FuzzyErpApplyNo { get; set; } = false;
        public string BizOrderNo { get; set; } = "";
        public bool FuzzyBizOrderNo { get; set; } = false;
        public string TaskCloseStatus { get; set; } = "全部";
        public string PickCloseStatus { get; set; } = "全部";
        public string OutCloseStatus { get; set; } = "全部";
    }
}
