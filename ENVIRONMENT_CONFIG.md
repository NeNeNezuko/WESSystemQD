# WMS 环境配置说明

## 配置文件结构

```
publish/wasm/wwwroot/
── appsettings.json              # 本地开发配置（默认）
└── appsettings.Production.json   # 生产环境配置
```

## 本地开发环境

### 配置文件：`appsettings.json`

```json
{
  "ApiBaseUrl": "http://localhost:5102"
}
```

**使用场景：**
- 本地开发和测试
- API 运行在 `http://localhost:5102`
- 前端运行在 `http://localhost:8190`

---

## 服务器生产环境

### 配置文件：`appsettings.Production.json`

```json
{
  "ApiBaseUrl": "http://8.129.239.141:5102"
}
```

**使用场景：**
- 服务器部署
- API 运行在 `http://8.129.239.141:5102`
- 前端运行在 `http://8.129.239.141:8190`

---

## 部署步骤

### 方式一：手动切换配置文件（简单）

#### 本地开发时：
确保 `appsettings.json` 内容为：
```json
{
  "ApiBaseUrl": "http://localhost:5102"
}
```

#### 部署到服务器时：
1. 上传所有文件到服务器 `D:\WMSSystem\publish\wasm\wwwroot\`
2. **手动修改** `D:\WMSSystem\publish\wasm\wwwroot\appsettings.json` 为：
   ```json
   {
     "ApiBaseUrl": "http://8.129.239.141:5102"
   }
   ```
3. 重启 WMS 站点：
   ```powershell
   Restart-Website -Name "WMS"
   ```

---

### 方式二：使用 PowerShell 脚本自动切换（推荐）

创建部署脚本 `deploy-to-server.ps1`：

```powershell
# deploy-to-server.ps1
param(
    [string]$ServerIP = "8.129.239.141",
    [string]$ApiPort = "5102",
    [string]$FrontendPort = "8190"
)

$apiUrl = "http://${ServerIP}:${ApiPort}"
$configPath = "D:\WMSSystem\publish\wasm\wwwroot\appsettings.json"

Write-Host "正在配置生产环境..." -ForegroundColor Yellow
Write-Host "API URL: $apiUrl" -ForegroundColor Cyan

# 读取现有配置
$config = Get-Content -Path $configPath | ConvertFrom-Json

# 更新 ApiBaseUrl
$config.ApiBaseUrl = $apiUrl

# 保存配置
$config | ConvertTo-Json -Depth 10 | Set-Content -Path $configPath

Write-Host "✓ 配置已更新" -ForegroundColor Green
Write-Host "正在重启 WMS 站点..." -ForegroundColor Yellow
Restart-Website -Name "WMS"
Write-Host "✓ 部署完成！" -ForegroundColor Green
```

**使用方法：**
```powershell
# 在服务器上执行
.\deploy-to-server.ps1
```

---

### 方式三：使用环境变量（高级）

修改 Blazor WASM 的 Program.cs，从环境变量读取配置：

```csharp
// 在 wwwroot/index.html 中设置环境变量
<script>
    window.environment = {
        apiBaseUrl: 'http://8.129.239.141:5102'
    };
</script>
```

然后在代码中使用：
```csharp
var apiBaseUrl = JSRuntime.InvokeAsync<string>("window.environment.apiBaseUrl");
```

---

## API CORS 配置

### 本地开发

`WmsPlus.Api/appsettings.json`:
```json
{
  "CorsOrigins": [
    "http://localhost:8190",
    "http://localhost:5196",
    "http://localhost:5193",
    "http://localhost:5192",
    "http://localhost:5000",
    "http://localhost:5001"
  ]
}
```

### 服务器生产

`WmsPlus.Api/appsettings.json`（需要同时包含本地和服务器地址）:
```json
{
  "CorsOrigins": [
    "http://8.129.239.141:8190",
    "http://localhost:8190",
    "http://localhost:5196",
    "http://localhost:5193",
    "http://localhost:5192",
    "http://localhost:5000",
    "http://localhost:5001"
  ]
}
```

**注意：** CORS 配置需要同时包含本地和服务器地址，这样无论在哪里部署都能正常工作。

---

## 快速参考

| 环境 | 前端地址 | API 地址 | 配置文件 |
|------|---------|---------|---------|
| 本地开发 | http://localhost:8190 | http://localhost:5102 | appsettings.json |
| 服务器生产 | http://8.129.239.141:8190 | http://8.129.239.141:5102 | appsettings.json（需手动修改） |

---

## 常见问题

### Q1: 为什么不能直接使用 localhost？

**A:** 浏览器出于安全考虑，不允许从公网 IP（如 `http://8.129.239.141:8190`）访问本地回环地址（`http://localhost:5102`）。这会触发 CORS 策略阻止。

### Q2: 每次发布都要手动修改配置吗？

**A:** 是的，除非使用自动化部署脚本或 CI/CD 流程。建议使用方式二的 PowerShell 脚本简化操作。

### Q3: 如何验证配置是否正确？

**A:** 
1. 打开浏览器开发者工具（F12）
2. 查看 Console 标签，确认没有 CORS 错误
3. 查看 Network 标签，确认 `/api/auth/login` 请求成功（状态码 200）

---

## 总结

- ✅ **本地开发**：保持 `appsettings.json` 为 `http://localhost:5102`
- ✅ **服务器部署**：手动修改或使用脚本将 `appsettings.json` 改为 `http://8.129.239.141:5102`
- ✅ **CORS 配置**：同时包含本地和服务器地址，确保两边都能正常工作
- ⚠️ **重要**：每次发布到服务器后，记得修改前端配置并重启站点
