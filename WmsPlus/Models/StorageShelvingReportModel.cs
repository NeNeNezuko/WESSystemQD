namespace WmsPlus.Models
{
    public class StorageShelvingReportModel
    {
        // 明细表字段
        public int ItemNo { get; set; }
        public string SJ_NO { get; set; } = "";
        public DateTime? SJ_DD { get; set; }
        public string WH { get; set; } = "";
        public string PRD_NO { get; set; } = "";
        public string PRD_NAME { get; set; } = "";
        public decimal? QTY { get; set; }
        public string REM { get; set; } = "";

        // 统计表字段
        public string DEP { get; set; } = "";
        public string SAL_NO { get; set; } = "";
        public string BIL_TYPE { get; set; } = "";
        public string BIL_ID { get; set; } = "";
        public string BIL_NO { get; set; } = "";
        public string USR { get; set; } = "";
        public DateTime? SYS_DATE { get; set; }
    }

    public class StorageShelvingReportQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string SJ_NO { get; set; } = "";
        public string WarehouseCode { get; set; } = "";
    }
}
