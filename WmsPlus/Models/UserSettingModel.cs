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

    /// <summary>用户设定新增/编辑请求体</summary>
    public class UserSettingCreateRequest
    {
        /// <summary>用户代号（主键）</summary>
        public string Usr { get; set; } = "";
        /// <summary>用户名称</summary>
        public string Name { get; set; } = "";
        /// <summary>密码</summary>
        public string? Pwd { get; set; }
        /// <summary>部门代号</summary>
        public string Dep { get; set; } = "";
        /// <summary>主管</summary>
        public string Mng { get; set; } = "";
        /// <summary>下次登陆必须修改密码</summary>
        public bool PwdChg { get; set; }
        /// <summary>截止有效期</summary>
        public DateTime? EDat { get; set; }
        /// <summary>开始有效期</summary>
        public DateTime? BDat { get; set; }
        /// <summary>所属类别</summary>
        public string DeproNo { get; set; } = "";
        /// <summary>集团用户</summary>
        public bool IsGroup { get; set; }
        /// <summary>供应商</summary>
        public bool IsCust { get; set; }
    }
}
