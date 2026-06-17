using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using AntDesign;
using WmsPlus;
using WmsPlus.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 注册 TokenProvider（轻量级，用于在 AuthService 和其他组件间共享 token）
builder.Services.AddScoped<TokenProvider>();

// 注册 AuthService
builder.Services.AddScoped<AuthService>();

// 注册简单的 HttpClient（WASM 兼容，不使用 DelegatingHandler）
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

// 从 appsettings.json 读取 API 基地址，部署时只需修改配置文件
ApiConfig.BaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5102";

builder.Services.AddAntDesign();

await builder.Build().RunAsync();
