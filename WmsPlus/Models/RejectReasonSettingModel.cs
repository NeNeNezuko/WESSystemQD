namespace WmsPlus.Models
{
    public class RejectReasonModel
    {
        public string SpcNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string SpcUp { get; set; } = "";
        public string Rem { get; set; } = "";
    }

    public class RejectReasonQuery
    {
        public string SpcNo { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
