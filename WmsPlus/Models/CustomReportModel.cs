namespace WmsPlus.Models
{
    public class CustomReportModel
    {
        public string Pgm { get; set; } = "";
        public string NameGb { get; set; } = "";
        public string NameBig5 { get; set; } = "";
        public string NameEng { get; set; } = "";
        public int Itm { get; set; }
        public string ChkMenu { get; set; } = "";
        public string ChkUsrs { get; set; } = "";
        public string Usr { get; set; } = "";
        public DateTime? SysDate { get; set; }

        // 统计表用
        public int Count { get; set; }
    }

    public class CustomReportQuery
    {
        public string Name { get; set; } = "";
        public string Usr { get; set; } = "";
    }
}
