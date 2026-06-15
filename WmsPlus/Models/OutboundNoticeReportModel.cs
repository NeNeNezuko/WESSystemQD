namespace WmsPlus.Models
{
    public class OutboundNoticeReportModel
    {
        // 明细表字段
        public int ItemNo { get; set; }
        public string TZ_NO { get; set; } = "";
        public DateTime? TZ_DD { get; set; }
        public string WH { get; set; } = "";
        public string CUS_NO { get; set; } = "";
        public string CUS_NAME { get; set; } = "";
        public string PRD_NO { get; set; } = "";
        public string PRD_NAME { get; set; } = "";
        public string PRD_MARK { get; set; } = "";
        public string BAT_NO { get; set; } = "";
        public DateTime? VALID_DD { get; set; }
        public string UNIT { get; set; } = "";
        public decimal QTY { get; set; }
        public string REM { get; set; } = "";

        // 统计表字段（表头信息）
        public string DEP { get; set; } = "";
        public string SAL_NO { get; set; } = "";
        public string BIL_TYPE { get; set; } = "";
        public string CLS_ID { get; set; } = "";
        public string USR { get; set; } = "";
        public DateTime? SYS_DATE { get; set; }
    }

    public class OutboundNoticeReportQuery
    {
        public string DateRange { get; set; } = "";
        public string DocNo { get; set; } = "";
        public bool FuzzyDocNo { get; set; } = false;
        public string PrdNoFrom { get; set; } = "";
        public string PrdNoTo { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public bool IncludeChildWh { get; set; } = false;
        public string ErpApplyNo { get; set; } = "";
        public bool FuzzyErpApplyNo { get; set; } = false;
        public string ExtSysNo { get; set; } = "";
        public bool FuzzyExtSysNo { get; set; } = false;
        public string BizOrderNo { get; set; } = "";
        public bool FuzzyBizOrderNo { get; set; } = false;
        public string WaveCloseStatus { get; set; } = "全部";
        public string PickCloseStatus { get; set; } = "全部";
        public string OutCloseStatus { get; set; } = "全部";
    }
}
