namespace WmsPlus.Models
{
    public class BatchNoPickingSettingModel
    {
        public int ItemNo { get; set; }
        public string Guid { get; set; } = "";
        public string Wh { get; set; } = "";
        public string WhName { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string PrdMark { get; set; } = "";
        public string BatNo { get; set; } = "";
        public decimal? Qty { get; set; }
        public string Usr { get; set; } = "";
        public DateTime? SysDate { get; set; }
        public string Rem { get; set; } = "";
    }

    public class BatchNoPickingSettingQuery
    {
        public string Wh { get; set; } = "";
        public string PrdNoFrom { get; set; } = "";
        public string PrdNoTo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string QtyOperator { get; set; } = "=";
        public decimal? QtyValue { get; set; }
        public string IdxNo { get; set; } = "";
    }
}
