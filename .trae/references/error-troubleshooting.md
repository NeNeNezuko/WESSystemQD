# Blazor 前端错误排查记录

> **触发词**：`报错排查`、`错误记录`、`异常定位`、`Unhandled error`、`404`、`JSON解析异常`
>
> **文件路径**：`.trae/references/error-troubleshooting.md`
>
> **用途**：当 Blazor WASM 前端出现报错时，按症状快速匹配历史案例，避免重复踩坑。

---

## 错误索引（按症状速查）

| 症状 | 关键报错信息 | 对应章节 |
|------|-------------|---------|
| 页面底部弹出 "An unhandled error has occurred. Reload" | `does not have a property matching the name 'xxx'` | [#1 组件不支持透传属性导致渲染崩溃](#1-组件不支持透传属性导致渲染崩溃) |
| API 返回 404 Not Found + JSON 解析异常 | `JsonException: ExpectedJsonTokens`, `Status=404 Body=` | [#2 URL 查询字符串缺少 ? 分隔符](#2-url-查询字符串缺少--分隔符导致-404) |
| 选择器弹窗空白无数据 | 控制台 401/500 错误 | [#3 EF Core 实体缺少 HasKey 配置](#3-ef-core-实体缺少-haskey-配置导致查询崩溃) |

---

## 1. 组件不支持透传属性导致渲染崩溃

### 症状

- 页面底部红色横幅：**"An unhandled error has occurred. Reload"**
- 控制台报错：
  ```
  Unhandled exception rendering component of type 'WmsPlus.Components.XxxInput'.
  Microsoft.AspNetCore.Components.WebAssembly.Rendering.WebAssemblyRenderer[100]
  Object of type 'WmsPlus.Components.XxxInput' does not have a property matching the name 'style'
  ```

### 触发条件

在 Razor 页面中给**自定义组件**传入 HTML 原生属性（如 `style`、`class`、`title` 等），但该组件未声明 `[Parameter(CaptureUnmatchedValues = true)]` 参数来接收这些额外属性。

```html
<!-- 触发示例 -->
<WarehouseCodeInput style="flex:1;" @bind-Value="query.Wh" />
```

### 根因

Blazor 组件默认只接收显式声明的 `[Parameter]` 属性。当父组件传入未声明的属性时，如果子组件没有 `CaptureUnmatchedValues` 参数，渲染引擎会抛出异常并导致**整个页面崩溃**（不仅仅是该组件区域）。

### 影响范围

所有自定义输入组件都可能受影响：
- `WarehouseCodeInput.razor`
- `DeptCodeInput.razor`
- `BizTypeInput.razor`
- `CkTypeInput.razor`
- `CustomerCodeInput.razor`
- `ProductCodeInput.razor`
- `DateRangeInput.razor`

### 修复方法

在组件中添加透传属性参数，并在根元素上使用 `@attributes` 展开：

```csharp
// @code 区域添加
[Parameter(CaptureUnmatchedValues = true)]
public Dictionary<string, object>? AdditionalAttributes { get; set; }
```

```html
<!-- 根元素绑定 -->
<div class="form-item" @attributes="AdditionalAttributes">
    <!-- 原有内容 -->
</div>
```

### 预防措施

- 所有自定义组件**统一添加** `CaptureUnmatchedValues` 支持
- 新建组件时将此作为标准模板的一部分
- 参考修复文件：[WarehouseCodeInput.razor](../../WmsPlus/Components/WarehouseCodeInput.razor)

---

## 2. URL 查询字符串缺少 `?` 分隔符导致 404

### 症状

- 页面正常加载，但数据表格为空
- 控制台报错：
  ```
  [ReceivingReport] 请求API: http://localhost:5102/api/xxx/search&page=1&pageSize=20
  API失败! Status=404 Body=
  System.Text.Json.JsonException: ExpectedJsonTokens Path: $ | LineNumber: 0 | BytePositionInLine: 0
  ```

**关键特征**：URL 中 `search` 后面直接是 `&page=1`，**没有 `?`**！

### 触发条件

动态构建 API 请求 URL 时，所有查询条件字段都为空（用户未填写任何筛选条件），导致基础 URL 后面没有任何 `?key=value` 参数。此时分页参数直接用 `&` 拼接：

```csharp
// BUG 代码模式
var url = $"{ApiBaseUrl}/api/xxx/search";
// 所有 query 字段都为空，没有进入任何 if 分支
url += $"&page={currentPage}&pageSize={pageSize}"; // ← 直接 & 拼接！
// 结果：http://localhost:5102/api/xxx/search&page=1  → 路径不匹配 → 404
```

### 根因

分页参数拼接时假设 URL 中已存在 `?`（由前面的查询条件字段添加），但当所有查询条件为空时，URL 中没有 `?`，导致 `&page=1` 被当作路径的一部分。

### 诊断方法

1. 在 `LoadData()` 方法中打印实际请求 URL：`Console.WriteLine($"[PageName] 请求API: {url}");`
2. 或用 JS Interop 调用原生 `fetch()` 测试，获取完整的网络请求信息
3. 对比后端日志确认请求是否到达后端（如果后端从未收到请求，说明是前端 URL 构建问题）

### 修复方法

在拼接分页参数前检查是否需要添加 `?`：

```csharp
// 修复后的代码
var separator = url.Contains("?") ? "&" : "?";
url += $"{separator}page={currentPage}&pageSize={pageSize}&tabType={tabType}";
```

每个查询条件字段的拼接也应遵循相同模式：

```csharp
if (!string.IsNullOrWhiteSpace(query.SomeField))
    url += $"{(url.Contains("?") ? "&" : "?")}someField={Uri.EscapeDataString(query.SomeField)}";
```

### 影响范围

所有使用动态 URL 构建 + 分页的页面都可能有此问题。需批量检查以下模式：
- `LoadData()` 方法中的 URL 拼接逻辑
- 其他报表页面的数据加载方法

### 预防措施

- 封装通用的 URL 参数拼接工具方法
- 或在每个项目中统一使用 QueryHelper 类构建查询字符串
- 参考修复文件：[ReceivingReport.razor](../../WmsPlus/Pages/ReceivingReport.razor) 第686-688行

---

## 3. EF Core 实体缺少 HasKey 配置导致查询崩溃

### 症状

- 选择器弹窗（如仓库选择器）打开后显示空白，无数据
- 后端 API 返回 500 Internal Server Error
- **连锁反应**：同一个 DbContext 下**所有 API 接口全部返回 500**

### 触发条件

在 `WarehouseDbContext` 中新增了 DbSet 注册和实体类，但在 `OnModelCreating` 中**遗漏了 Fluent API 主键配置**（`HasKey()`）。

```csharp
// DbContext 中注册了实体
public DbSet<SomeNewEntity> SomeNewEntities { get; set; }

// 但 OnModelCreating 中忘记配置主键！
// EF Core 无法推断主键 → 首次查询时抛出 InvalidOperationException
```

### 根因

EF Core 的模型验证是**延迟执行**的——编译通过不代表模型正确。首次对 DbContext 执行任何查询时，EF Core 才会验证所有已注册实体的模型配置。如果某个实体缺少主键定义，会抛出 `InvalidOperationException`，导致整个 DbContext 不可用。

### 诊断方法

1. 检查后端控制台日志，查找 `InvalidOperationException` 相关堆栈
2. 确认最近新增了哪些 DbSet，逐一核对是否有对应的 `modelBuilder.Entity<T>().HasKey(x => x.Id)` 配置
3. 用 Swagger/Postman 直接调用任意一个该 DbContext 下的 API，如果全部 500 则确认是模型配置问题

### 修复方法

在 `OnModelCreating` 中补充缺失的主键配置：

```csharp
modelBuilder.Entity<SomeNewEntity>(entity =>
{
    entity.HasKey(e => e.GUID); // 或其他主键字段
    entity.ToTable("SOME_TABLE");
});
```

### 预防措施

- 新增实体类的** checklist**：①写实体类 ②注册 DbSet ③写 Fluent API 配置（含 HasKey）④重启后端冒烟测试
- 参考修复文件：[WarehouseDbContext.cs](../../WmsPlus.Data/WarehouseDbContext.cs)

---

## 通用调试工具箱

### 快速定位 WASM 前端 404 问题

```
步骤1：打印实际 URL
  Console.WriteLine($"[组件名] 请求API: {url}");

步骤2：后端直接测试（排除网络/认证问题）
  PowerShell: Invoke-RestMethod -Uri "http://localhost:5102/api/xxx/search?page=1" -Headers @{Authorization="Bearer <token>"}

步骤3：浏览器地址栏直接访问（验证路由存在）
  访问 http://localhost:5102/api/xxx/search （不带 token 应返回 401，而非 404）

步骤4：JS Interop 原生 fetch 测试（绕过 .NET HttpClient）
  await JS.InvokeAsync<string>("fetch", url, { headers: { "Authorization": "Bearer " + token } })
    .then(r => r.text())
    .then(text => console.log("JS-Fetch结果:", text));
```

### 常见 HTTP 状态码速查

| 状态码 | 含义 | 可能原因 |
|--------|------|---------|
| 200 | 成功 | 正常 |
| 400 | Bad Request | 参数格式错误、必填参数缺失 |
| 401 | Unauthorized | Token 缺失或过期、未使用 AuthHttpClient |
| 403 | Forbidden | 无权限访问该资源 |
| 404 | Not Found | URL 路径错误、路由不存在、**查询字符串格式错误（缺 ?）** |
| 500 | Internal Server Error | 后端代码异常、**EF Core 实体配置缺失**、数据库连接失败 |

### Blazor WASM vs Blazor Server 注意事项

本项目使用的是 **Blazor WebAssembly (WASM)** 模式（非 Blazor Server）：
- HttpClient 的 BaseAddress 指向**前端自身**（浏览器同源）
- 跨域请求依赖后端 CORS 配置
- JS Interop 使用 `fetch()` 而非 XMLHttpRequest
- 认证 Token 通过 localStorage 存储，页面刷新后需从 JS Interop 恢复
