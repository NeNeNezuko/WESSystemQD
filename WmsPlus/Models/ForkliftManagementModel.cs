namespace WmsPlus.Models
{
    public class ForkliftManagementModel
    {
        public string TruckNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string Wh { get; set; } = "";
        public string Rem { get; set; } = "";
    }

    public class ForkliftManagementQuery
    {
        public string TruckNo { get; set; } = "";
        public string Wh { get; set; } = "";
    }
}
