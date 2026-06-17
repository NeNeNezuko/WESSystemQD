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
        public string CategoryCode { get; set; } = "";   // 所属类别
        public string CategoryName { get; set; } = "";    // 所属类别名称
    }

    public class UserSettingQuery
    {
        public string UserCode { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Dept { get; set; } = "";
        public bool IncludeSub { get; set; } = false;
        public bool ShowExpired { get; set; } = false;
        public string Manager { get; set; } = "";         // 主管
        public bool SunlikeUser { get; set; } = true;
        public bool Vendor { get; set; } = true;
        public bool IsEmpty { get; set; } = true;
    }
}
