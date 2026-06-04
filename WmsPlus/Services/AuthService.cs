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
        public UserInfo? User { get; set; }
    }

    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _jsRuntime;
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

        public bool IsAuthenticated => CurrentUser != null;

        public AuthService(HttpClient httpClient, IJSRuntime jsRuntime)
        {
            _httpClient = httpClient;
            _jsRuntime = jsRuntime;
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

                if (result != null && result.Success && result.User != null)
                {
                    CurrentUser = result.User;
                    await SaveUserToStorage(result.User);
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
                var response = await _httpClient.PostAsJsonAsync($"{ApiBaseUrl}/api/auth/changepassword", new 
                { 
                    Username = username, 
                    OldPassword = oldPassword,
                    NewPassword = newPassword
                });

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

        public void Logout()
        {
            CurrentUser = null;
            ClearUserFromStorage();
            NotifyAuthStateChanged();
        }

        private async Task SaveUserToStorage(UserInfo user)
        {
            try
            {
                var userData = System.Text.Json.JsonSerializer.Serialize(user);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "wms_user", userData);
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
                var userData = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "wms_user");
                if (!string.IsNullOrEmpty(userData))
                {
                    var user = System.Text.Json.JsonSerializer.Deserialize<UserInfo>(userData);
                    if (user != null)
                    {
                        CurrentUser = user;
                    }
                }
            }
            catch
            {
                ClearUserFromStorage();
            }
        }

        private async void ClearUserFromStorage()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "wms_user");
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
