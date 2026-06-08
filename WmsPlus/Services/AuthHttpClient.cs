using System.Net.Http.Headers;

namespace WmsPlus.Services
{
    /// <summary>
    /// 认证 HTTP 客户端工具，自动在请求中附加 Bearer token
    /// </summary>
    public static class AuthHttpClient
    {
        /// <summary>
        /// 创建带认证头的 HttpRequestMessage
        /// </summary>
        public static HttpRequestMessage CreateRequest(HttpMethod method, string url, string? token)
        {
            var request = new HttpRequestMessage(method, url);
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return request;
        }

        /// <summary>
        /// 为 HttpClient 添加默认认证头
        /// </summary>
        public static void SetBearerToken(this HttpClient client, string? token)
        {
            client.DefaultRequestHeaders.Authorization = string.IsNullOrEmpty(token)
                ? null
                : new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
