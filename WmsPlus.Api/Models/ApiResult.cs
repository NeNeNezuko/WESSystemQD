namespace WmsPlus.Api.Models;

/// <summary>通用API返回结果</summary>
public class ApiResult<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
    public T? Data { get; set; }
    public int Total { get; set; }
}
