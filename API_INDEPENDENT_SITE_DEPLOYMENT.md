# WMS API 独立站点部署指南

## 问题说明

当前配置下，WMS 主站点（Blazor WASM）和 api 子应用在同一端口（8190）上运行，但 URL 重写规则无法正确将 `/api/*` 请求传递给 api 子应用，导致所有 API 端点返回 404。

## 解决方案：将 API 部署为独立 IIS 站点

### 优点：
- ✅ 完全隔离，避免路径冲突
- ✅ 更简单的配置
- ✅ 更容易调试和维护
- ✅ 性能更好（独立应用程序池）

### 缺点：
- ⚠️ 需要使用不同的端口
- ⚠️ 需要配置 CORS

---

## 部署步骤

### 第 1 步：在 IIS 中创建新的 API 站点

1. 打开 IIS 管理器
2. 右键点击 **"网站"** → **"添加网站..."**
3. 填写以下信息：
   - **网站名称**: `WMS-API`
   - **应用程序池**: 选择现有的 `WMSAppPool` 或创建新的
   - **物理路径**: `D:\WMSSystem\publish\api`
   - **绑定**:
     - 类型: `http`
     - IP 地址: `*`（或特定 IP）
     - 端口: `5102`（或其他可用端口）
     - 主机名: （留空）
4. 点击 **"确定"**

### 第 2 步：验证 API 站点配置

```powershell
# 检查新站点是否创建成功
Get-Website -Name "WMS-API" | Format-List Name, State, Bindings, PhysicalPath

# 应该看到类似输出：
# Name         : WMS-API
# State        : Started
# Bindings     : http *:5102:
# PhysicalPath : D:\WMSSystem\publish\api
```

### 第 3 步：测试 API 站点

```powershell
# 测试 API 端点（直接访问 API 站点）
Write-Host "=== 测试 API 站点 ===" -ForegroundColor Yellow

# 测试 1: /test
try {
    $response = Invoke-WebRequest -Uri "http://localhost:5102/api/test" -UseBasicParsing -TimeoutSec 10
    Write-Host "✓ /api/test 成功! 状态码: $($response.StatusCode)" -ForegroundColor Green
    Write-Host $response.Content
} catch {
    Write-Host "✗ /api/test 失败: $($_.Exception.Message)" -ForegroundColor Red
}

# 测试 2: /auth/login
try {
    $response = Invoke-WebRequest -Uri "http://localhost:5102/api/auth/login" `
        -Method POST `
        -ContentType "application/json" `
        -Body '{"username":"ADMIN","password":""}' `
        -UseBasicParsing `
        -TimeoutSec 10
    
    Write-Host "`n✓ /api/auth/login 成功! 状态码: $($response.StatusCode)" -ForegroundColor Green
    Write-Host $response.Content
} catch {
    Write-Host "`n✗ /api/auth/login 失败: $($_.Exception.Message)" -ForegroundColor Red
}
```

### 第 4 步：配置 CORS（如果尚未配置）

API 站点的 `appsettings.json` 中已经配置了 CORS：

```json
{
  "CorsOrigins": [
    "http://localhost:8190",
    "http://localhost:5196",
    "http://localhost:5193",
    "http://localhost:5192"
  ]
}
```

确保包含前端站点的地址（`http://localhost:8190`）。

### 第 5 步：修改前端配置

修改 WMS 主站点的 `wwwroot/appsettings.json`：

**服务器上的文件**: `D:\WMSSystem\publish\wasm\wwwroot\appsettings.json`

```json
{
  "ApiBaseUrl": "http://localhost:5102"
}
```

**注意**: 
- 本地开发时使用: `"http://localhost:5102"`
- 生产环境使用服务器 IP: `"http://8.129.239.141:5102"`

### 第 6 步：简化 WMS 主站点的 web.config

由于 API 现在是独立站点，不再需要 URL 重写规则来转发 API 请求。

修改 `D:\WMSSystem\publish\wasm\web.config`：

```xml
<?xml version="1.0" encoding="UTF-8"?>
<configuration>
  <system.webServer>
    <staticContent>
      <remove fileExtension=".blat" />
      <remove fileExtension=".dat" />
      <remove fileExtension=".dll" />
      <remove fileExtension=".webcil" />
      <remove fileExtension=".json" />
      <remove fileExtension=".wasm" />
      <remove fileExtension=".woff" />
      <remove fileExtension=".woff2" />
      <mimeMap fileExtension=".blat" mimeType="application/octet-stream" />
      <mimeMap fileExtension=".dll" mimeType="application/octet-stream" />
      <mimeMap fileExtension=".webcil" mimeType="application/octet-stream" />
      <mimeMap fileExtension=".dat" mimeType="application/octet-stream" />
      <mimeMap fileExtension=".json" mimeType="application/json" />
      <mimeMap fileExtension=".wasm" mimeType="application/wasm" />
      <mimeMap fileExtension=".woff" mimeType="application/font-woff" />
      <mimeMap fileExtension=".woff2" mimeType="application/font-woff" />
    </staticContent>
    <httpCompression>
      <dynamicTypes>
        <add mimeType="application/octet-stream" enabled="true" />
        <add mimeType="application/wasm" enabled="true" />
      </dynamicTypes>
    </httpCompression>
    <rewrite>
      <rules>
        <!-- Blazor WASM SPA 路由 -->
        <rule name="Serve subdir">
          <match url=".*" />
          <action type="Rewrite" url="wwwroot\{R:0}" />
        </rule>
        <rule name="SPA fallback routing" stopProcessing="true">
          <match url=".*" />
          <conditions logicalGrouping="MatchAll">
            <add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true" />
          </conditions>
          <action type="Rewrite" url="wwwroot\" />
        </rule>
      </rules>
    </rewrite>
  </system.webServer>
</configuration>
```

**关键变化**: 删除了 "Exclude API" 规则，因为 API 现在是独立站点。

### 第 7 步：重启站点

```powershell
# 重启 WMS 主站点
Restart-Website -Name "WMS"

# 重启 API 站点
Restart-Website -Name "WMS-API"

Start-Sleep -Seconds 3
```

### 第 8 步：测试完整流程

#### 测试 1: 前端静态文件

```powershell
# 应该能正常访问
Invoke-WebRequest -Uri "http://localhost:8190/" -UseBasicParsing
```

#### 测试 2: API 端点（通过前端代理）

前端会自动将 API 请求发送到 `http://localhost:5102`（根据 appsettings.json 配置）。

打开浏览器访问：`http://localhost:8190/login`

尝试登录，应该能正常工作！

#### 测试 3: 直接测试 API

```powershell
# 直接访问 API 站点
$response = Invoke-WebRequest -Uri "http://localhost:5102/api/auth/login" `
    -Method POST `
    -ContentType "application/json" `
    -Body '{"username":"ADMIN","password":""}' `
    -UseBasicParsing

Write-Host "状态码: $($response.StatusCode)" -ForegroundColor Green
Write-Host "响应内容:" -ForegroundColor Cyan
Write-Host $response.Content
```

---

## 故障排查

### 问题 1: API 站点无法启动

**症状**: 访问 `http://localhost:5102/api/test` 返回错误

**解决**:
1. 检查应用程序池状态: `Get-WebAppPoolState -Name "WMSAppPool"`
2. 检查日志: `Get-Content D:\WMSSystem\publish\api\logs\*.log -Tail 50`
3. 手动启动测试: `cd D:\WMSSystem\publish\api; dotnet WmsPlus.Api.dll --urls "http://localhost:5102"`

### 问题 2: CORS 错误

**症状**: 浏览器控制台显示 CORS 错误

**解决**:
1. 确认 `appsettings.json` 中的 `CorsOrigins` 包含前端地址
2. 重启 API 站点使配置生效

### 问题 3: 前端仍然调用错误的 API 地址

**症状**: 浏览器 Network 标签显示请求发送到 `http://localhost:8190/api/...`

**解决**:
1. 清除浏览器缓存（Ctrl+F5 强制刷新）
2. 确认 `wwwroot/appsettings.json` 中的 `ApiBaseUrl` 正确
3. 重新构建并部署前端

---

## 生产环境配置

### 服务器 IP: 8.129.239.141

#### API 站点绑定
- 端口: `5102`
- 完整 URL: `http://8.129.239.141:5102`

#### 前端配置
`D:\WMSSystem\publish\wasm\wwwroot\appsettings.json`:
```json
{
  "ApiBaseUrl": "http://8.129.239.141:5102"
}
```

#### API CORS 配置
`D:\WMSSystem\publish\api\appsettings.json`:
```json
{
  "CorsOrigins": [
    "http://8.129.239.141:8190",
    "http://localhost:8190"
  ]
}
```

---

## 总结

通过将 API 部署为独立的 IIS 站点，我们避免了复杂的路径重写问题，使架构更清晰、更易维护。

**架构**:
```
客户端
  ├─ http://8.129.239.141:8190  → WMS 主站点 (Blazor WASM)
  └─ http://8.129.239.141:5102  → WMS-API 站点 (ASP.NET Core API)
```

**优势**:
- ✅ 简单明了的架构
- ✅ 独立的日志和监控
- ✅ 可以独立重启和扩展
- ✅ 避免路径冲突问题
