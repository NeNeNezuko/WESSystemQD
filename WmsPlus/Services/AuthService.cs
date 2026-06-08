using System.Text.Json;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace WmsPlus.Services
{
    public class UserInfo
    {
        public string Name { get; set; } = "";
        public string Dept { get; set; } = "";
        public string CompNo { get; set; } = "";
        public string Usr { get; set; } = "";
    }

    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public string? Token { get; set; }
        public UserInfo? User { get; set; }
    }

    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
        private readonly TokenProvider _tokenProvider;
        private const string ApiBaseUrl = "http://localhost:5102";

        public event Action? OnAuthStateChanged;

        private UserInfo? _currentUser;
        public UserInfo? CurrentUser
        {
            get => _currentUser;
            private set
            {
                _currentUser = value;
                NotifyAuthStateChanged();
            }
        }

        private string? _token;
        public string? Token => _token;

        public bool IsAuthenticated => CurrentUser != null && !string.IsNullOrEmpty(_token);

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime, TokenProvider tokenProvider)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
            _tokenProvider = tokenProvider;
        }

        public async Task InitializeAsync()
        {
            await LoadUserFromStorage();
        }

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{ApiBaseUrl}/api/auth/login", new
                {
                    Username = username,
                    Password = password
                });

                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                if (result != null && result.Success && result.User != null && !string.IsNullOrEmpty(result.Token))
                {
                    CurrentUser = result.User;
                    _token = result.Token;
                    _tokenProvider.Token = result.Token;
                    await SaveUserToStorage(result.User, result.Token);
                }

                return result ?? new LoginResponse { Success = false, Message = "登录失败" };
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = $"连接服务器失败: {ex.Message}"
                };
            }
        }

        public async Task<LoginResponse> ChangePasswordAsync(string username, string oldPassword, string newPassword)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, $"{ApiBaseUrl}/api/auth/changepassword")
                {
                    Content = JsonContent.Create(new
                    {
                        Username = username,
                        OldPassword = oldPassword,
                        NewPassword = newPassword
                    })
                };

                if (!string.IsNullOrEmpty(_token))
                {
                    request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);
                }

                var response = await _httpClient.SendAsync(request);
                var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                return result ?? new LoginResponse { Success = false, Message = "修改密码失败" };
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = $"连接服务器失败: {ex.Message}"
                };
            }
        }

        public async Task Logout()
        {
            CurrentUser = null;
            _token = null;
            _tokenProvider.Token = null;
            await ClearUserFromStorage();
            NotifyAuthStateChanged();
        }

        /// <summary>
        /// 向后端验证 token 是否仍然有效
        /// </summary>
        public async Task<bool> ValidateTokenAsync()
        {
            if (string.IsNullOrEmpty(_token)) return false;

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"{ApiBaseUrl}/api/auth/check");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _token);

                var response = await _httpClient.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        private async Task SaveUserToStorage(UserInfo user, string token)
        {
            try
            {
                var userData = JsonSerializer.Serialize(user);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "wms_user", userData);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "wms_token", token);
            }
            catch
            {
                // 忽略错误
            }
        }

        private async Task LoadUserFromStorage()
        {
            try
            {
                var tokenData = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "wms_token");
                var userData = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "wms_user");

                if (!string.IsNullOrEmpty(tokenData) && !string.IsNullOrEmpty(userData))
                {
                    var user = JsonSerializer.Deserialize<UserInfo>(userData);
                    if (user != null)
                    {
                        _token = tokenData;
                        _tokenProvider.Token = tokenData;
                        CurrentUser = user;
                    }
                }
            }
            catch
            {
                await ClearUserFromStorage();
            }
        }

        private async Task ClearUserFromStorage()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "wms_user");
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "wms_token");
            }
            catch
            {
                // 忽略错误
            }
        }

        private void NotifyAuthStateChanged()
        {
            OnAuthStateChanged?.Invoke();
        }

        public async Task SetRememberMe(string username, bool remember)
        {
            try
            {
                if (remember)
                {
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "wms_remember_username", username);
                    await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "wms_remember_me", "true");
                }
                else
                {
                    await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "wms_remember_username");
                    await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "wms_remember_me");
                }
            }
            catch
            {
                // 忽略错误
            }
        }

        public async Task<(string? username, bool remember)> GetRememberMeAsync()
        {
            try
            {
                var savedUsername = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "wms_remember_username");
                var savedRemember = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "wms_remember_me");

                return (savedUsername, savedRemember == "true");
            }
            catch
            {
                return (null, false);
            }
        }
    }
}
