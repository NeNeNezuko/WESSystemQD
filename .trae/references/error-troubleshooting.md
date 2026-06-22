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
| 查询页面显示"暂无数据"（数据库有数据） | `InvalidCastException: Unable to cast object of type 'System.Byte[]' to type 'System.DateTime'` | [#5 MySQL DATE字段映射DateTime导致Byte[]转换异常](#5-mysql-date字段映射datetime导致byte转换异常) |
| 登录成功后页面卡死（白屏转圈） | 无报错，页面无限加载 | [#4 OnAfterRenderAsync + StateHasChanged 导致无限循环渲染](#4-onafterrenderasync--statehaschanged-导致无限循环渲染) |

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

## 4. OnAfterRenderAsync + StateHasChanged 导致无限循环渲染

### 症状

- 用户登录成功后，页面跳转到 `/dashboard` 后**白屏卡死**（一直显示加载中/转圈）
- **浏览器控制台无任何报错信息**——不是异常导致的崩溃
- 页面完全无响应，无法进行任何操作

### 触发条件

在 Blazor 组件的 `OnAfterRenderAsync` 方法中调用 JS Interop 获取数据后，无条件调用 `StateHasChanged()` 触发重渲染：

```csharp
// BUG 模式：每次渲染完成后都执行 JS + 触发重渲染 → 无限循环
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    var result = await JS.InvokeAsync<bool[]>("getTagsScrollState", tagsContainerRef);
    canScrollLeft = result[0];
    canScrollRight = result[1];
    StateHasChanged(); // ← 无条件触发重渲染！
}
```

**循环链路**：
```
OnAfterRenderAsync → JS.InvokeAsync → 更新状态 → StateHasChanged() 
→ 重新渲染 → OnAfterRenderAsync → ... (死循环)
```

### 根因

Blazor 的 `OnAfterRenderAsync` 在**每次组件渲染完成后**都会被调用。如果在该方法中无条件调用 `StateHasChanged()`，会立即触发下一次渲染，而下次渲染又会再次调用 `OnAfterRenderAsync`，形成**无限递归渲染循环**。

这种问题与普通异常不同——不会抛出错误、不会触发 ErrorBoundary，只是页面永远处于"正在渲染"状态，表现为白屏转圈。

### 影响范围

所有使用 `OnAfterRenderAsync` + `StateHasChanged()` 组合的 Blazor 组件都可能受影响。本项目中的具体位置：

- [DashboardLayout.razor](../../WmsPlus/Layout/DashboardLayout.razor) 第 1154-1168 行（`OnAfterRenderAsync`）
- [DashboardLayout.razor](../../WmsPlus/Layout/DashboardLayout.razor) 第 1139-1159 行（`UpdateArrowVisibility`）

### 修复方法

**双重防护**：限制执行时机 + 状态变更检测

```csharp
// 修复1: OnAfterRenderAsync - 仅首次渲染时执行
protected override async Task OnAfterRenderAsync(bool firstRender)
{
    if (firstRender)  // ✅ 只在首次渲染后执行，后续不再调用
    {
        try
        {
            await UpdateArrowVisibility();
        }
        catch { /* JS 未就绪时忽略 */ }
    }
}

// 修复2: UpdateArrowVisibility - 状态未变时不触发重渲染
private async Task UpdateArrowVisibility()
{
    try
    {
        var result = await JS.InvokeAsync<bool[]>("getTagsScrollState", tagsContainerRef);
        if (result != null && result.Length >= 2)
        {
            bool newCanScrollLeft = result[0];
            bool newCanScrollRight = result[1];

            // ✅ 只有状态真正改变时才更新并触发重渲染
            if (canScrollLeft != newCanScrollLeft || canScrollRight != newCanScrollRight)
            {
                canScrollLeft = newCanScrollLeft;
                canScrollRight = newCanScrollRight;
                StateHasChanged();
            }
        }
    }
    catch { /* 忽略 SSR 等异常 */ }
}
```

### 预防措施

- **`OnAfterRenderAsync` 中调用 `StateHasChanged()` 必须有条件守卫**：要么用 `firstRender` 限定只执行一次，要么用状态比较确保只在值变化时触发
- **JS Interop 返回值更新组件状态前先做 diff 比较**：避免不必要的渲染开销
- **开发调试技巧**：如果页面白屏无报错，优先检查 `OnAfterRenderAsync` / `OnParametersSetAsync` 中是否存在无条件 `StateHasChanged()` 调用
- 参考修复文件：[DashboardLayout.razor](../../WmsPlus/Layout/DashboardLayout.razor) 第 1139-1168 行

---

## 5. MySQL DATE 字段映射 DateTime 导致 Byte[] 转换异常

### 症状

- 查询页面显示 **"暂无数据"**，但数据库中确认有数据
- 后端日志报错：
  ```
  System.InvalidCastException: Unable to cast object of type 'System.Byte[]' to type 'System.DateTime'.
     at MySqlConnector.Core.Row.GetDateTime(Int32 ordinal)
  ```
- 前端 catch 异常后静默设为空列表，用户只看到"暂无数据"

### 触发条件

- EF Core 实体类中将数据库 `DATE` 类型字段定义为 C# `DateTime?` 属性
- MySQL 的 DATE 列在特定连接配置下被驱动返回为 `Byte[]` 而非 `DateTime`
- **受影响表**：`MY_WH`（STOP_DD、UP_DD、SYS_DATE），可能还有其他表的日期字段

### 根因分析

MySQL Connector/NET 在某些情况下将 DATE 类型的列值作为 `Byte[]` 返回，EF Core 尝试将其转换为 `DateTime?` 时抛出 `InvalidCastException`。这导致整个查询失败（不是单条记录错误），前端收到异常后显示空数据。

### 修复方法

1. 将实体类中对应属性从 `DateTime?` 改为 `string?`
2. 同步修改：
   - 实体模型类（如 `MyWh.cs`）
   - Controller 中的 DTO 类（如 `WarehouseCodeSettingDto.StopDd`）
   - 前端 Model 类
   - 前端页面中的 `.ToString("yyyy-MM-dd")` 格式化改为直接使用字符串
3. 如果需要在 Update 时写入当前时间，改用 `DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")`

**示例修改**：
```csharp
// 修改前
public DateTime? STOP_DD { get; set; }

// 修改后
public string? STOP_DD { get; set; }  // 数据库为DATE类型，用string接收避免Byte[]转换异常
```

### 影响范围

所有使用 db_gz01 数据库且包含 DATE 类型字段的实体类。以下表已确认受影响：
- `MY_WH`：STOP_DD、UP_DD、SYS_DATE
- 其他含日期字段但实际存储格式非标准 DATETIME/TIMESTAMP 的表

### 预防措施

- 新增 DbSet 实体时，对 DATE 类型字段优先使用 `string?` 接收
- 如果必须用 DateTime，需先通过原始 SQL 确认该列的实际数据类型
- 参考修复文件：[MyWh.cs](../../WmsPlus.Api/Models/MyWh.cs) 第15-16行

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
