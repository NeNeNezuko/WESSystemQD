namespace WmsPlus.Models
{
    public class BatchNoPickingSettingModel
    {
        public string Guid { get; set; } = "";
        public string Wh { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdMark { get; set; } = "";
        public string BatNo { get; set; } = "";
        public string Usr { get; set; } = "";
        public DateTime? SysDate { get; set; }
        public string Rem { get; set; } = "";
    }

    public class BatchNoPickingSettingQuery
    {
        public string Wh { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string BatNo { get; set; } = "";
    }
}
