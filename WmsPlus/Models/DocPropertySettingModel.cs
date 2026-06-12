namespace WmsPlus.Models
{
    public class DocPropertySettingModel
    {
        public string CompNo { get; set; } = "";
        public string Pgm { get; set; } = "";
        public string RoleNo { get; set; } = "";
        public string TypeId { get; set; } = "";
        public string FldName { get; set; } = "";
        public string FldValue { get; set; } = "";
        public string Rem { get; set; } = "";
    }

    public class DocPropertySettingQuery
    {
        public string Pgm { get; set; } = "";
        public string RoleNo { get; set; } = "";
    }
}
