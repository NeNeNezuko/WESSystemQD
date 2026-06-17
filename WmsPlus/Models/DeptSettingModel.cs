namespace WmsPlus.Models
{
    public class DeptSettingModel
    {
        public string Dep { get; set; } = "";
        public string Name { get; set; } = "";
        public string Up { get; set; } = "";
        public string UpName { get; set; } = "";
        public string MakeId { get; set; } = "";
        public DateTime? StopDd { get; set; }
    }

    public class DeptSettingQuery
    {
        public string Dep { get; set; } = "";
        public string Name { get; set; } = "";
    }

    /// <summary>部门设定-新增/编辑表单模型</summary>
    public class DeptSettingAddModel
    {
        /// <summary>部门代号(主键)</summary>
        public string Dep { get; set; } = "";

        /// <summary>部门名称</summary>
        public string Name { get; set; } = "";

        /// <summary>上层部门</summary>
        public string Up { get; set; } = "";

        /// <summary>英文名称</summary>
        public string EngName { get; set; } = "";

        /// <summary>停用日期</summary>
        public DateTime? StopDd { get; set; }

        /// <summary>部门性质</summary>
        public string MakeId { get; set; } = "";

        /// <summary>群组代号</summary>
        public string GroupId { get; set; } = "";

        /// <summary>助记码</summary>
        public string NamePy { get; set; } = "";

        /// <summary>类型代号</summary>
        public string TpId { get; set; } = "";
    }
}
