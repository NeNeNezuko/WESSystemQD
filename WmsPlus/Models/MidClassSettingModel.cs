namespace WmsPlus.Models
{
    public class MidClassSettingModel
    {
        public string IdxNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string IdxUp { get; set; } = "";
        public DateTime? StopDd { get; set; }
        public string Rem { get; set; } = "";
    }

    public class MidClassSettingQuery
    {
        public string IdxNo { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
