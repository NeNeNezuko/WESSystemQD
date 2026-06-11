namespace WmsPlus.Models
{
    public class StorageUnshelvingReportModel
    {
        // 明细表字段
        public int ItemNo { get; set; }
        public string XJ_NO { get; set; } = "";
        public DateTime? XJ_DD { get; set; }
        public string WH { get; set; } = "";
        public string PRD_NO { get; set; } = "";
        public string PRD_NAME { get; set; } = "";
        public decimal? QTY { get; set; }
        public string REM { get; set; } = "";

        // 统计表字段
        public string DEP { get; set; } = "";
        public string SAL_NO { get; set; } = "";
        public string BIL_TYPE { get; set; } = "";
        public string USR { get; set; } = "";
        public DateTime? SYS_DATE { get; set; }
    }

    public class StorageUnshelvingReportQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string XJ_NO { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
    }
}
