namespace WmsPlus.Models
{
    public class CategoryPermissionSettingModel
    {
        public string RoleNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string Dep { get; set; } = "";
        public string CompNo { get; set; } = "";
        public string TypeId { get; set; } = "";
        public string PublicId { get; set; } = "";
        public string Rem { get; set; } = "";
    }

    public class CategoryPermissionSettingQuery
    {
        public string RoleNo { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
