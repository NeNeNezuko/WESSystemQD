namespace WmsPlus.Models
{
    public class UserSettingModel
    {
        public int RowNo { get; set; }
        public string UserCode { get; set; } = "";
        public string UserName { get; set; } = "";
        public string DeptCode { get; set; } = "";
        public string DeptName { get; set; } = "";
        public string ParentDept { get; set; } = "";
        public string ParentDeptName { get; set; } = "";
        public string Email { get; set; } = "";
        public string IsAdmin { get; set; } = "";
        public string IsResigned { get; set; } = "";
        public string Supplier { get; set; } = "";
    }

    public class UserSettingQuery
    {
        public string UserCode { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Dept { get; set; } = "";
        public bool IncludeSub { get; set; } = false;
        public bool ShowExpired { get; set; } = false;
        public string Theme { get; set; } = "";
        public bool SunlikeUser { get; set; } = true;
        public bool Vendor { get; set; } = true;
        public bool IsEmpty { get; set; } = true;
    }
}
