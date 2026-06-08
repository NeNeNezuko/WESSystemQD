namespace WmsPlus.Services
{
    /// <summary>
    /// 轻量级 token 持有者，用于打破 AuthDelegatingHandler 和 AuthService 之间的循环依赖
    /// </summary>
    public class TokenProvider
    {
        public string? Token { get; set; }
    }
}
