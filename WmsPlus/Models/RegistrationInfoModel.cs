namespace WmsPlus.Models
{
    /// <summary>
    /// 注册信息 - 数据模型（对应表格行）
    /// </summary>
    public class RegistrationInfoModel
    {
        /// <summary>主模块代号</summary>
        public string ModuleCode { get; set; } = "";

        /// <summary>主模块名称</summary>
        public string ModuleName { get; set; } = "";

        /// <summary>最大用户数</summary>
        public int MaxUsers { get; set; }

        /// <summary>用户数</summary>
        public int UserCount { get; set; }
    }

    /// <summary>
    /// 注册信息 - 查询条件模型
    /// </summary>
    public class RegistrationInfoQuery
    {
        /// <summary>序列号</summary>
        public string SerialNo { get; set; } = "GZ001";

        /// <summary>应用日期（yyyy-MM-dd）</summary>
        public string ApplyDate { get; set; } = "";

        /// <summary>版本</summary>
        public string Version { get; set; } = "02.WMVC版";

        /// <summary>合同到期日（yyyy-MM-dd）</summary>
        public string ContractExpireDate { get; set; } = "";

        /// <summary>停用日（yyyy-MM-dd）</summary>
        public string StopDate { get; set; } = "";

        /// <summary>停用否</summary>
        public bool StopFlag { get; set; }

        /// <summary>可用模组基数</summary>
        public string AvailableModuleBase { get; set; } = "3";

        /// <summary>客户代号（只读显示）</summary>
        public string CustomerCode { get; set; } = "GZ001";

        /// <summary>客户名称（只读显示）</summary>
        public string CustomerName { get; set; } = "岱览";
    }
}
