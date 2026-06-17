namespace WmsPlus.Services
{
    /// <summary>
    /// API 基地址配置，从 wwwroot/appsettings.json 读取
    /// 部署时只需修改 appsettings.json 中的 ApiBaseUrl 即可
    /// </summary>
    public static class ApiConfig
    {
        public static string BaseUrl { get; set; } = "http://localhost:5102";
    }
}
