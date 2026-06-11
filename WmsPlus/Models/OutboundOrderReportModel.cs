namespace WmsPlus.Models
{
    public class OutboundOrderReportModel
    {
        // 明细表字段
        public int ItemNo { get; set; }
        public string CK_NO { get; set; } = "";
        public DateTime? CK_DD { get; set; }
        public string WH { get; set; } = "";
        public string CUS_NO { get; set; } = "";
        public string CUS_NAME { get; set; } = "";
        public string PRD_NO { get; set; } = "";
        public string PRD_NAME { get; set; } = "";
        public string PRD_MARK { get; set; } = "";
        public string BAT_NO { get; set; } = "";
        public string CHUW { get; set; } = "";
        public string UNIT { get; set; } = "";
        public decimal QTY { get; set; }
        public decimal QTY_PK { get; set; }
        public string REM { get; set; } = "";

        // 统计表字段（表头信息）
        public string DEP { get; set; } = "";
        public string BIL_TYPE { get; set; } = "";
        public string SAL_NO { get; set; } = "";
        public string CLS_ID { get; set; } = "";
        public string USR { get; set; } = "";
        public DateTime? SYS_DATE { get; set; }
    }

    public class OutboundOrderReportQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string OrderNumber { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string CustomerCode { get; set; } = "";
        public string CloseStatus { get; set; } = "全部";
    }
}
