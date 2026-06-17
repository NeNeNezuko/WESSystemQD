namespace WmsPlus.Models
{
    public class OutboundPackageModel
    {
        public int SeqNo { get; set; }
        public string PackageNo { get; set; } = "";
        public DateTime? PackageDate { get; set; }
        public string BusinessType { get; set; } = "";
        public string BusinessTypeName { get; set; } = "";
        public string Packager { get; set; } = "";
        public string PackagerName { get; set; } = "";
        public DateTime? PackTime { get; set; }
        public string OutBilNo { get; set; } = "";
        public string CusNo { get; set; } = "";
    }

    public class OutboundPackageQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string PackageNo { get; set; } = "";
        public string OutBilNo { get; set; } = "";
        public string Wh { get; set; } = "";
        public string OutStatus { get; set; } = "";
    }
}
