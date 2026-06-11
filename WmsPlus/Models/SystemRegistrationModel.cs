namespace WmsPlus.Models
{
    /// <summary>
    /// 系统注册 - 数据模型
    /// </summary>
    public class SystemRegistrationModel
    {
        /// <summary>模拟用户-用户名</summary>
        public string UserName { get; set; } = "";

        /// <summary>模拟用户-密码</summary>
        public string Password { get; set; } = "";

        /// <summary>模拟用户-域名</summary>
        public string Domain { get; set; } = "";

        /// <summary>原注册号（只读显示）</summary>
        public string OriginalRegNo { get; set; } = "GZ001";

        /// <summary>注册号</summary>
        public string RegNo { get; set; } = "GZ001";

        /// <summary>稳模信息</summary>
        public string StableInfo { get; set; } = "";

        /// <summary>代理服务器地址</summary>
        public string ProxyServer { get; set; } = "";

        /// <summary>代理服务器端口</summary>
        public string ProxyPort { get; set; } = "";
    }
}
