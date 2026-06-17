# WMS API 部署清单

## 发布时间
2026-06-16

## 更新内容
1. ✅ Program.cs - 添加了 `app.UseStaticFiles()` 中间件
2. ✅ web.config - 启用了日志记录（stdoutLogEnabled="true"）
3. ✅ wwwroot/test.html - 创建了静态文件测试页面

## 需要上传到服务器的文件

### 方式一：完整覆盖（推荐）
将整个 `publish/api/` 目录上传覆盖到服务器的 `D:\WMSSystem\publish\api\`

**源目录**：`e:\AICode\WESSystemQD\publish\api\`  
**目标目录**：`D:\WMSSystem\publish\api\`

包含以下文件：
- ✅ WmsPlus.Api.dll（新编译，包含 UseStaticFiles）
- ✅ WmsPlus.Api.exe
- ✅ WmsPlus.Api.deps.json
- ✅ WmsPlus.Api.runtimeconfig.json
- ✅ web.config（已启用日志）
- ✅ appsettings.json
- ✅ appsettings.Development.json
- ✅ wwwroot/test.html（新增测试文件）
- ✅ 所有依赖的 DLL 文件

### 方式二：仅上传更新的文件
如果带宽有限，可以只上传以下文件：

**必须上传**：
1. `WmsPlus.Api.dll` - 核心应用文件（已修改）
2. `web.config` - 配置文件（已修改）
3. `wwwroot/test.html` - 新增测试文件

**可选上传**（如果没有变化可以不传）：
- 其他 DLL 文件（通常不需要更新）
- JSON 配置文件（除非您修改了配置）

## 部署步骤

### 1. 上传文件
使用 FTP、远程桌面复制、或其他文件传输工具将文件上传到服务器。

**推荐使用向日葵远程控制的"文件传输"功能**（从截图看到您正在使用）

### 2. 重启应用程序池
在服务器上以管理员身份运行 PowerShell：

```powershell
Restart-WebAppPool -Name "WMSAppPool"
```

或者重启整个 IIS：

```powershell
iisreset
```

### 3. 验证部署

#### 测试 1：静态文件
访问：`http://localhost:8190/api/test.html`

**预期结果**：看到绿色文字 "Static file OK"

#### 测试 2：API 端点
在 PowerShell 中运行：

```powershell
Invoke-WebRequest -Uri "http://localhost:8190/api/auth/login" -Method POST -ContentType "application/json" -Body '{"username":"ADMIN","password":""}'
```

**预期结果**：返回 JSON 数据，包含 token

#### 测试 3：前端登录
1. 清除浏览器缓存（Ctrl + Shift + Delete）
2. 访问：`http://localhost:8190/`
3. 硬刷新（Ctrl + F5）
4. 尝试登录

**预期结果**：登录成功，进入系统

### 4. 检查日志（如果失败）
查看 API 日志文件：

```
D:\WMSSystem\publish\api\logs\stdout_*.log
```

应该有启动信息和可能的错误信息。

## 回滚方案

如果部署后出现问题，可以：

1. 备份当前服务器上的 `D:\WMSSystem\publish\api\` 目录
2. 如果失败，用备份恢复
3. 重启应用程序池

## 常见问题排查

### Q1: /api/test.html 仍然 404
**可能原因**：
- 文件没有正确上传到 wwwroot 目录
- 应用程序池没有重启

**解决方法**：
1. 确认服务器上存在 `D:\WMSSystem\publish\api\wwwroot\test.html`
2. 再次重启应用程序池

### Q2: API 端点返回 500 错误
**可能原因**：
- 数据库连接失败
- 应用程序启动异常

**解决方法**：
1. 查看 logs 目录下的日志文件
2. 检查 appsettings.json 中的数据库连接字符串是否正确

### Q3: 前端登录仍然失败
**可能原因**：
- CORS 配置问题
- 前端配置文件未更新

**解决方法**：
1. 确认 `D:\WMSSystem\publish\wasm\wwwroot\appsettings.json` 中的 ApiBaseUrl 配置正确
2. 检查 API 的 appsettings.json 中 CorsOrigins 是否包含前端地址

## 联系支持

如果遇到问题，请提供：
1. 诊断脚本的输出（diagnose-deployment.ps1）
2. API 日志文件内容
3. 浏览器控制台的错误信息
