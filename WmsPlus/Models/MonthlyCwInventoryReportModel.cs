namespace WmsPlus.Models
{
    public class MonthlyCwInventoryReportModel
    {
        public string WH { get; set; } = "";
        public int? YY { get; set; }
        public int? MM { get; set; }
        public string CHUW { get; set; } = "";
        public string PRD_NO { get; set; } = "";
        public string PRD_MARK { get; set; } = "";
        public string BAT_NO { get; set; } = "";
        public DateTime? VALID_DD { get; set; }
        public decimal? QTY_IN { get; set; }
        public decimal? QTY_OUT { get; set; }
        public decimal? QTY1_IN { get; set; }
        public decimal? QTY1_OUT { get; set; }
        public DateTime? LST_IND { get; set; }
        public DateTime? LST_OTD { get; set; }
    }

    public class MonthlyCwInventoryReportQuery
    {
        public string Year { get; set; } = "";
        public string Month { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
        public string Chuw { get; set; } = "";
        public string PrdNo { get; set; } = "";
    }
}
