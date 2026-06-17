namespace WmsPlus.Models
{
    public class WarehouseInventoryReportModel
    {
        public string WH { get; set; } = "";
        public string PRD_NO { get; set; } = "";
        public string PRD_MARK { get; set; } = "";
        public string BAT_NO { get; set; } = "";
        public DateTime? VALID_DD { get; set; }
        public decimal? QTY_IN { get; set; }
        public decimal? QTY_OUT { get; set; }
        public decimal? QTY_PK { get; set; }
        public decimal? QTY_QC { get; set; }
        public decimal? QTY_TY { get; set; }
        public decimal? QTY_UO { get; set; }
        public decimal? QTY_UP { get; set; }
        public decimal? QTY_BC { get; set; }
        public decimal? QTY1_IN { get; set; }
        public decimal? QTY1_OUT { get; set; }
        public decimal? QTY1_PK { get; set; }
        public string LOCK_ID { get; set; } = "";
        public DateTime? LST_IND { get; set; }
        public DateTime? LST_OTD { get; set; }
        public DateTime? LST_TYD { get; set; }
        public DateTime? INSERT_DD { get; set; }
    }

    public class WarehouseInventoryReportQuery
    {
        public string WarehouseCode { get; set; } = "";
        public string PrdNoFrom { get; set; } = "";
        public string PrdNoTo { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string QtyOperator { get; set; } = "";
        public decimal? QtyValue { get; set; }
        public string IdxNo { get; set; } = "";
    }
}
