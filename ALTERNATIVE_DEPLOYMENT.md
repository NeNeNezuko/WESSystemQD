# WMS 备选部署方案 - 独立站点模式

## 问题描述
当前使用 URL 重写规则将 `/api/*` 请求传递给 api 子应用的方式遇到问题，静态文件 `/api/test.html` 返回 404。

## 解决方案：将 API 部署为独立 IIS 站点

### 步骤 1：创建新的 IIS 站点用于 API

1. 打开 **IIS 管理器**
2. 右键点击 **"网站"** → **"添加网站"**
3. 填写信息：
   - **网站名称**：`WMS-API`
   - **物理路径**：`D:\WMSSystem\publish\api`
   - **绑定**：
     - 类型：http
     - IP 地址：`*` 或 `8.129.239.141`
     - 端口：`5102`（或其他未被占用的端口）
     - 主机名：留空
   - **应用程序池**：选择 `WMSAppPool` 或创建新的
4. 点击 **确定**

### 步骤 2：修改前端配置

修改文件：`D:\WMSSystem\publish\wasm\wwwroot\appsettings.json`

```json
{
  "ApiBaseUrl": "http://localhost:5102"
}
```

或者使用服务器实际 IP：
```json
{
  "ApiBaseUrl": "http://8.129.239.141:5102"
}
```

### 步骤 3：简化 wasm 的 web.config

由于不再需要 URL 重写来排除 API，可以简化配置。

修改文件：`D:\WMSSystem\publish\wasm\web.config`

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

**注意**：删除了 "Exclude API requests" 规则，因为不再需要。

### 步骤 4：配置 CORS

确保 API 的 `appsettings.json` 中包含前端的地址：

文件：`D:\WMSSystem\publish\api\appsettings.json`

```json
{
  "CorsOrigins": [
    "http://localhost:8190",
    "http://8.129.239.141:8190"
  ]
}
```

### 步骤 5：重启 IIS

```powershell
iisreset
```

### 步骤 6：测试验证

1. **测试 API**：
   ```
   http://localhost:5102/api/auth/login
   ```
   应该能访问（可能需要 POST 请求）

2. **测试前端**：
   ```
   http://localhost:8190/
   ```
   清除浏览器缓存后登录

3. **检查 Network 标签**：
   - 前端请求应该是：`POST http://localhost:5102/api/auth/login`
   - 返回 200 和 JSON 数据

## 优势

- ✅ 不依赖 URL Rewrite Module
- ✅ API 和前端完全独立，互不影响
- ✅ 更容易调试和维护
- ✅ 可以分别重启 API 和前端

## 注意事项

1. **防火墙**：确保服务器防火墙开放了两个端口（8190 和 5102）
2. **CORS**：确保 API 的 CORS 配置包含前端地址
3. **负载均衡**：如果使用负载均衡器，需要配置两个后端服务

## 回滚方案

如果想恢复原来的单站点模式：

1. 删除 `WMS-API` 站点
2. 恢复 `wasm/web.config` 中的 "Exclude API requests" 规则
3. 将 `appsettings.json` 改回 `"ApiBaseUrl": ""`
4. 重启 IIS
