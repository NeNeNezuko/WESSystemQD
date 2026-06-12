namespace WmsPlus.Models
{
    public class CategoryMemberSettingModel
    {
        public string CompNo { get; set; } = "";
        public string RoleNo { get; set; } = "";
        public string Usr { get; set; } = "";
        public string TypeId { get; set; } = "";
    }

    public class CategoryMemberSettingQuery
    {
        public string Usr { get; set; } = "";
        public string RoleNo { get; set; } = "";
    }
}
