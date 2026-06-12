# 项目规则

## Git 仓库配置

本项目使用以下 Git 仓库进行代码管理：

- **仓库地址**: https://github.com/NeNeNezuko/WESSystemQD.git
- **默认分支**: main
- **用户名**: NeNeNezuko
- **邮箱**: 985604681@qq.com

### 提交代码时的注意事项

1. 提交前确保代码已通过 lint 和 typecheck 检查
2. 使用有意义的提交信息
3. 不要提交敏感信息（如 appsettings.Development.json 中的密码等）

## 项目结构

- `WmsPlus/` - Blazor WebAssembly 前端项目
- `WmsPlus.Api/` - ASP.NET Core API 后端项目

## 数据库配置

本项目使用 MySQL 数据库，包含两个数据库实例：

### 数据库列表

| 连接名称 | 数据库名 | 用途 | 对应 DbContext |
|---------|---------|------|---------------|
| `DefaultConnection` | `wmssystem` | 管理账户数据（用户、权限等） | `AppDbContext` |
| `WarehouseConnection` | `db_gz01` | 管理仓库信息（库存、出入库等） | `WarehouseDbContext` |

### 数据库连接信息

**数据库类型**: MySQL  
**服务器地址**: localhost  
**端口**: 3306（默认）  
**用户名**: root  
**字符集**: utf8mb4  

### 配置文件位置

数据库连接字符串配置在 `WmsPlus.Api/appsettings.json` 文件中：

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=wmssystem;Uid=root;Pwd=xxx;Charset=utf8mb4;",
    "WarehouseConnection": "Server=localhost;Database=db_gz01;Uid=root;Pwd=xxx;Charset=utf8mb4;"
  }
}
```

### 数据库上下文类

| 类名 | 文件路径 | 对应数据库 |
|------|---------|-----------|
| `AppDbContext` | `WmsPlus.Api/Data/AppDbContext.cs` | `wmssystem` |
| `WarehouseDbContext` | `WmsPlus.Api/Data/WarehouseDbContext.cs` | `db_gz01` |

### 注意事项

1. **敏感信息保护**: 生产环境中密码不应硬编码在配置文件中，应使用环境变量或 .NET Secret Manager
2. **数据库初始化**: 确保两个数据库实例已在 MySQL 中创建
3. **连接池**: 使用默认连接池配置，可根据需要调整连接池大小

## 通用组件使用标准

### 仓库代号输入组件 (WarehouseCodeInput)

**适用场景**：任何查询表单页面中需要"仓库代号"输入/筛选条件的场景（如入库通知单、出库通知单、库存查询等）

**组件路径**：`WmsPlus/Components/WarehouseCodeInput.razor`

**使用方式**：

1. 在查询表单的 `@code` 块中，确保查询模型包含 `WarehouseCode` 字段（string 类型）
2. 在表单 HTML 中添加一行组件调用
3. 查询时将 `query.WarehouseCode` 作为 `warehouseCode` 参数传给后端 API

**代码示例**：

```razor
<!-- 表单中嵌入 -->
<WarehouseCodeInput @bind-Value="query.WarehouseCode" />
```

```csharp
// 查询模型
public class XxxQuery { public string WarehouseCode { get; set; } = ""; }

// 查询传参
if (!string.IsNullOrWhiteSpace(query.WarehouseCode))
    url += $"&warehouseCode={Uri.EscapeDataString(query.WarehouseCode)}";
```

**功能说明**：组件自包含，内部封装了手动输入、弹窗选择器（含搜索/分页/栏位设计）、所有 CSS 样式。调用方无需额外处理样式或逻辑。

**后端 API 依赖**：`GET /api/warehouse/search?keyword=&pageIndex=&pageSize=`，返回 MY_WH 表数据

### 业务类型输入组件 (BizTypeInput)

**适用场景**：任何查询表单页面中需要"业务类型"多选筛选条件的场景（如入库通知单、出库通知单等）

**组件路径**：`WmsPlus/Components/BizTypeInput.razor`

**使用方式**：

1. 在查询表单的 `@code` 块中，确保查询模型包含 `BusinessType` 字段（string 类型，存储逗号分隔的代码）
2. 在表单 HTML 中添加一行组件调用
3. 查询时将 `query.BusinessType` 作为 `businessType` 参数传给后端 API

**代码示例**：

```razor
<!-- 表单中嵌入 -->
<BizTypeInput @bind-Value="query.BusinessType" />
```

```csharp
// 查询模型
public class XxxQuery { public string BusinessType { get; set; } = ""; }

// 查询传参
if (!string.IsNullOrWhiteSpace(query.BusinessType))
    url += $"&businessType={Uri.EscapeDataString(query.BusinessType)}";
```

**功能说明**：组件自包含，内部封装了只读输入框、弹窗多选选择器（含搜索/分页/全选/清空/标签展示）、所有 CSS 样式。支持多选，回传值为逗号分隔的业务类型代码字符串。调用方无需额外处理样式或逻辑。

**后端 API 依赖**：`GET /api/warehouse/rktypesearch?keyword=&pageIndex=&pageSize=`，返回 RK_TYPE_SET 表数据

### 部门代号输入组件 (DeptCodeInput)

**适用场景**：任何查询表单页面中需要"部门代号"输入/筛选条件的场景（如入库单、出库单、库存查询等）

**组件路径**：`WmsPlus/Components/DeptCodeInput.razor`

**使用方式**：

1. 在查询表单的 `@code` 块中，确保查询模型包含 `DeptCode` 字段（string 类型）
2. 在表单 HTML 中添加一行组件调用
3. 查询时将 `query.DeptCode` 作为 `deptCode` 参数传给后端 API

**代码示例**：

```razor
<!-- 表单中嵌入 -->
<DeptCodeInput @bind-Value="query.DeptCode" />
```

```csharp
// 查询模型
public class XxxQuery { public string DeptCode { get; set; } = ""; }

// 查询传参
if (!string.IsNullOrWhiteSpace(query.DeptCode))
    url += $"&deptCode={Uri.EscapeDataString(query.DeptCode)}";
```

**功能说明**：组件自包含，内部封装了手动输入、弹窗选择器（含搜索/分页）、所有 CSS 样式。调用方无需额外处理样式或逻辑。

**后端 API 依赖**：`GET /api/warehouse/deptsearch?keyword=&pageIndex=&pageSize=`，返回 DEPT 表数据

### 客户代号输入组件 (CustomerCodeInput)

**适用场景**：任何查询表单页面中需要"客户代号"输入/筛选条件的场景（如出库退回通知单、出库通知单等）

**组件路径**：`WmsPlus/Components/CustomerCodeInput.razor`

**使用方式**：

1. 在查询表单的 `@code` 块中，确保查询模型包含 `CustomerCode` 字段（string 类型）
2. 在表单 HTML 中添加一行组件调用
3. 查询时将 `query.CustomerCode` 作为 `customerCode` 参数传给后端 API

**代码示例**：

```razor
<!-- 表单中嵌入 -->
<CustomerCodeInput @bind-Value="query.CustomerCode" />
```

```csharp
// 查询模型
public class XxxQuery { public string CustomerCode { get; set; } = ""; }

// 查询传参
if (!string.IsNullOrWhiteSpace(query.CustomerCode))
    url += $"&customerCode={Uri.EscapeDataString(query.CustomerCode)}";
```

**功能说明**：组件自包含，内部封装了手动输入、弹窗选择器（含搜索/分页）、所有 CSS 样式。调用方无需额外处理样式或逻辑。

**后端 API 依赖**：`GET /api/warehouse/customersearch?keyword=&pageIndex=&pageSize=`，返回 MF_CKTB 表去重后的客户数据

### 日期范围输入组件 (DateRangeInput)

**适用场景**：任何查询表单页面中需要"日期范围"输入/筛选条件的场景（如入库通知单、入库单、出库通知单、出库单、盘点、报表等）

**组件路径**：`WmsPlus/Components/DateRangeInput.razor`

**使用方式**：

1. 在查询表单的 `@code` 块中，确保查询模型包含 `DateRange` 字段（string 类型）
2. 在表单 HTML 中添加一行组件调用，通过 `Label` 参数指定日期字段的中文名称
3. 查询时从 `query.DateRange` 解析出 dateFrom 和 dateTo 传给后端 API

**代码示例**：

```razor
<!-- 默认标签"单据日期" -->
<DateRangeInput @bind-Value="query.DateRange" />

<!-- 自定义标签名称 -->
<DateRangeInput @bind-Value="query.DateRange" Label="制表日期" />
<DateRangeInput @bind-Value="query.DateRange" Label="收货日期" />
<DateRangeInput @bind-Value="query.EstDateRange" Label="预计出货日期" />
```

```csharp
// 查询模型
public class XxxQuery { public string DateRange { get; set; } = ""; }

// 查询传参（从 DateRange 解析，分隔符为 "→"）
var dateFromStr = "";
var dateToStr = "";
if (!string.IsNullOrWhiteSpace(query.DateRange) && query.DateRange.Contains("→"))
{
    var parts = query.DateRange.Split("→", StringSplitOptions.TrimEntries);
    if (parts.Length >= 2)
    {
        dateFromStr = parts[0];
        dateToStr = parts[1];
    }
}
url += $"&dateFrom={dateFromStr}&dateTo={dateToStr}";
```

**功能说明**：
- 组件自包含，内部封装了双月历范围选择器（含日期网格、快捷按钮、× 关闭按钮、点击外部自动关闭）、所有 CSS 样式
- **Label 参数**：支持自定义日期标签名称，默认值为 `"单据日期"`。不同页面根据实际业务语义传入对应名称，如 `"制表日期"`、`"收货日期"`、`"检验时间"`、`"有效日期"`、`"变动时间"` 等
- 回传值为 `"yyyy-MM-dd → yyyy-MM-dd"` 格式的字符串，默认初始值为当天
- 调方无需额外处理样式或逻辑，组件自身渲染完整的 `.form-item` 行（含 label + 日期文本 + 日历图标）

### 起/止范围查询字段规范

**适用场景**：查询表单中需要按范围筛选的字段（如单据号码、货品代号、储位代号等），将原来的"融合式双栏位"（一行内 label + 起 ~ 止）改为**独立的两个 form-item 行**

**布局规则**：

- 范围字段的「起」和「止」必须拆分为**两个独立的 `.form-item` 行**
- 每行格式：`<label>字段名 起/止</label>` + `<input>` （可带搜索图标按钮）
- 标签命名规则：`{字段名} 起` / `{字段名} 止`（如 "单据号码 起"、"货品代号 止"）

**代码示例**：

```razor
<!-- 单据号码 起 -->
<div class="form-item">
    <label class="form-label">单据号码 起</label>
    <div class="input-with-search">
        <input type="text" class="form-input" placeholder="" @bind="query.BilNoFrom" />
        <button class="search-icon-btn" title="搜索">
            <svg viewBox="0 0 14 14" width="12" height="12" fill="none" stroke="#999999" stroke-width="1.5">
                <circle cx="6" cy="6" r="4"/>
                <line x1="9" y1="9" x2="13" y2="13"/>
            </svg>
        </button>
    </div>
</div>

<!-- 单据号码 止 -->
<div class="form-item">
    <label class="form-label">单据号码 止</label>
    <div class="input-with-search">
        <input type="text" class="form-input" placeholder="" @bind="query.BilNoTo" />
        <button class="search-icon-btn" title="搜索">
            <svg viewBox="0 0 14 14" width="12" height="12" fill="none" stroke="#999999" stroke-width="1.5">
                <circle cx="6" cy="6" r="4"/>
                <line x1="9" y1="9" x2="13" y2="13"/>
            </svg>
        </button>
    </div>
</div>
```

```csharp
// 查询模型
public class XxxQuery
{
    public string BilNoFrom { get; set; } = "";  // 单据号码 起
    public string BilNoTo { get; set; } = "";    // 单据号码 止
}
```

**注意事项**：
- 不再使用已废弃的 `form-item-fused` 融合式布局（保留 CSS 样式定义以避免报错）
- 带搜索图标的字段使用 `.input-with-search` 容器 + `.search-icon-btn` 按钮
- 货品代号等需要弹窗选择的字段，在 input 旁放置放大镜图标按钮（功能后续实现）

### 模糊复选框规范

**适用场景**：查询表单中的文本输入字段需要支持模糊/精确匹配切换

**布局规则**：
- 模糊复选框紧跟在输入框右侧，使用 `.input-with-fuzzy` 容器包裹
- 复选框样式类为 `.fuzzy-checkbox`，显示文字为"模糊"

**代码示例**：

```razor
<div class="form-item">
    <label class="form-label">ERP申请单号</label>
    <div class="input-with-fuzzy">
        <input type="text" class="form-input" placeholder="" @bind="query.ErpApNo" />
        <label class="fuzzy-checkbox">
            <input type="checkbox" @bind="query.ErpApNoFuzzy" />
            <span>模糊</span>
        </label>
    </div>
</div>
```

### 单据确认作业 (DocumentConfirm)

**适用场景**：仓储管理 > 库存管理 > 盘点管理 > 单据确认作业，用于对盘点单、盘盈单、盘亏单进行确认/反确认操作

**页面文件**：
- 主页面：`WmsPlus/Pages/DocumentConfirm.razor`
- 样式文件：`WmsPlus/wwwroot/css/document-confirm.css`
- 数据模型：`WmsPlus/Models/DocumentConfirmModel.cs`
- API控制器：`WmsPlus.Api/Controllers/DocumentConfirmController.cs`

**UI结构特点**：
- 页面内嵌套横向选项卡：`待确认` | `已确认`
- 待确认Tab右上角显示「确认」按钮（蓝色），已确认Tab显示「反确认」按钮
- 切换选项卡时自动重新加载数据（confirmStatus参数变化）
- 查询表单含起止货品字段：`货品代号 起` / `货品代号 止`（独立 form-item 行，带搜索图标按钮，搜索弹窗待开发）

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 单据别 | `<select>` 下拉框 | `query.DocType` | PD(盘点单)/YN(盘盈单)/KU(盘亏单) |
| 单据日期 | `DateRangeInput` 组件 | `query.DateRange` | 日期范围选择器 |
| 仓库代号 | `WarehouseCodeInput` 组件 | `query.WarehouseCode` | 通用仓库选择组件 |
| 货品代号 起 | `<input>` + 搜索按钮 | `query.PrdNoFrom` | 货品代号范围起始（独立行） |
| 货品代号 止 | `<input>` + 搜索按钮 | `query.PrdNoTo` | 货品代号范围结束（独立行） |

**数据库连接**：db_gz01

**关联表信息**：

| 表名 | 说明 | 主键 | 确认字段 | 对应实体类 |
|------|------|------|---------|-----------|
| MF_PD | 盘点单据-表头 | PD_NO | CFM_SW/CFM_USR/CFM_DATE | MfPd.cs |
| TF_PD | 盘点单据-表身 | (PD_NO, ITM) | - | TfPd.cs |
| MF_YN | 盘盈(验收入库)单-表头 | YN_NO | CFM_SW/CFM_USR/CFM_DATE | MfYn.cs |
| TF_YN | 盘盈(验收入库)单-表身 | (YN_NO, ITM) | - | TfYn.cs |
| MF_KU | 盘亏(库存调整)单-表头 | KU_NO | CFM_SW/CFM_USR/CFM_DATE | MfKu.cs |
| TF_KU | 盘亏(库存调整)单-表身 | (KU_NO, ITM) | - | TfKu.cs |

**核心业务逻辑**：
- 3张表头均通过 `CFM_SW` 字段区分确认状态：`CFM_SW='T'` 表示已确认，否则为待确认
- 待确认查询条件：`CFM_SW IS NULL OR CFM_SW != 'T'`
- 已确认查询条件：`CFM_SW = 'T'`
- 起止货品范围过滤需关联对应表身（TF_PD/TF_YN/TF_KU）按 PRD_NO 字段做字符串比较

**后端API**：`GET /api/DocumentConfirm/search?docType=&dateFrom=&dateTo=&warehouseCode=&prdNoFrom=&prdNoTo=&confirmStatus=`

**表格默认栏位**：单据日期、单据号码、仓库代号、仓库名称、制单人代号、制单人名称、制单时间、确认状态、确认人、确认时间

**字段说明查询SQL**（在wmssystem数据库执行）：
```sql
-- 盘点单表头
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='MF_PD.DB';
-- 盘点单表身
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='TF_PD.DB';
-- 盘盈单表头
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='MF_YN.DB';
-- 盘盈单表身
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='TF_YN.DB';
-- 盘亏单表头
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='MF_KU.DB';
-- 盘亏单表身
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='TF_KU.DB';
```

## 检验管理模块表结构规范

本章节定义品质检验模块下 5 个页面的数据库表结构、字段映射和页面规范。

### 数据库连接

所有检验管理模块页面连接 **db_gz01** 数据库（通过 `WarehouseDbContext` 访问），字段说明查询使用 **wmssystem** 数据库（`DICT_TAB` + `DICT_FLD` 表）。

### 1. 盘盈单 (StockProfit)

**页面文件**：
- 主页面：`WmsPlus/Pages/StockProfit.razor`
- 样式文件：`WmsPlus/wwwroot/css/stock-profit.css`
- 数据模型：`WmsPlus/Models/StockProfitModel.cs`
- API控制器：`WmsPlus.Api/Controllers/StockProfitController.cs`
- **选项卡ID**: `stock-profit`
- **菜单路径**: 仓储管理 > 库存管理 > 盘点管理 > 盘盈单

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| MF_YN | 盘盈单表头（26字段） | YN_NO | MfYn.cs |
| TF_YN | 盘盈单表身（20字段） | (YN_NO, ITM) | TfYn.cs |
| PDA_BAR_COLLECT | 单据条码明细表（58字段，共用） | - | PdaBarCollect.cs |

**MF_YN 表头核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| YN_NO | 盘盈单号 | 主键 |
| YN_DD | 单据日期 | 日期 |
| PD_NO | 盘点单号 | 外键→MF_PD |
| WH | 仓库代号 | 关联MY_WH |
| DEP | 部门代号 | 关联DEPT |
| TYPE_ID | 业务类型 | 关联cr_type_set |
| BIL_TYPE | 单据类别 | 字符串 |
| SAL_NO | 经办人 | 字符串 |
| IJ_REASON | 调整原因 | 字符串 |
| REM | 备注 | 字符串 |
| USR | 制单人 | 字符串 |

**TF_YN 表身核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| YN_NO | 盘盈单号 | 外键→MF_YN |
| ITM | 项次 | 数字 |
| PRD_NO | 货品代号 | 字符串 |
| PRD_NAME | 货品名称 | 字符串 |
| BAT_NO | 批号 | 字符串 |
| QTY | 数量 | 数字 |
| UNIT | 单位 | 字符串 |
| WH | 仓库 | 字符串 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 单据日期 | `DateRangeInput` 组件 | `query.DateRange` | 日期范围选择器 |
| 单据号码 | `<input>` text | `query.DocumentNumber` | 模糊查询 |
| 制单人 | `<input>` text | `query.SalNo` | 经办人 |
| 仓库 | `WarehouseCodeInput` 组件 | `query.WarehouseCode` | 通用仓库选择组件 |

**表格默认栏位**（参考截图）：项次、单据日期、单据号码、盘点单号、部门名称、仓库名称、单据类别名称、经办人名称、操作

**右上角按钮**：新增、导出

**后端API**：`GET /api/StockProfit/search?dateFrom=&dateTo=&documentNumber=&salNo=&warehouseCode=`

**表格数据逻辑**：以 **TF_YN（表身）为主表**，通过 YN_NO LEFT JOIN MF_YN（表头）

---

### 2. 请检任务单 (InspectionTask)

**页面文件**：
- 主页面：`WmsPlus/Pages/InspectionTask.razor`
- 样式文件：`WmsPlus/wwwroot/css/inspection-task.css`
- 数据模型：`WmsPlus/Models/InspectionTaskModel.cs`
- API控制器：`WmsPlus.Api/Controllers/InspectionTaskController.cs`
- **选项卡ID**: `inspection-task`
- **菜单路径**: 仓储管理 > 品质检验 > 单据管理 > 请检任务单

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| MF_QJRW | 请检任务单表头（22字段） | QJ_NO | MfQjrw.cs |
| TF_QJRW | 请检任务单表身（21字段） | (QJ_NO, ITM) | TfQjrw.cs |

**MF_QJRW 表头核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| QJ_NO | 请检任务单号 | 主键 |
| QJ_DD | 单据日期 | 日期 |
| DEP | 部门 | 关联DEPT |
| BIL_TYPE | 单据类别 | 字符串 |
| BIL_KND | 单据类型 | RK入库检验/CK出库检验/KC库存检验 |
| TYWZ | 检验位置 | 1库内/2库外检验 |
| WH_TY | 检验仓库 | 关联MY_WH |
| CLS_ID_TY | 检验结案标记 | Y/N |
| REM | 备注 | 字符串 |
| USR | 制单人 | 字符串 |

**TF_QJRW 表身核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| QJ_NO | 请检任务单号 | 外键→MF_QJRW |
| ITM | 项次 | 数字 |
| PRD_NO | 货品代号 | 字符串 |
| PRD_NAME | 货品名称 | 字符串 |
| QTY | 数量 | 数字 |
| UNIT | 单位 | 字符串 |
| WH | 仓库 | 字符串 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 单据日期 | `DateRangeInput` 组件 | `query.DateRange` | 日期范围选择器 |
| 单据号码 | `<input>` text | `query.DocumentNumber` | 模糊查询 |
| 制单人 | `<input>` text | `query.Usr` | 制单人 |
| 检验仓库 | `WarehouseCodeInput` 组件 | `query.WarehouseTy` | 检验仓库 |

**表格默认栏位**（参考截图）：项次、单据日期、单据号码、检验仓库、仓库名称、备注、制单人名称、操作

**右上角按钮**：新增、导出

**后端API**：`GET /api/InspectionTask/search?dateFrom=&dateTo=&documentNumber=&usr=&warehouseTy=`

**表格数据逻辑**：以 **TF_QJRW（表身）为主表**，通过 QJ_NO LEFT JOIN MF_QJRW（表头）

---

### 3. 库存货品检验到期查询 (KcInspectExpiry)

**页面文件**：
- 主页面：`WmsPlus/Pages/KcInspectExpiry.razor`
- 样式文件：`WmsPlus/wwwroot/css/kc-inspect-expiry.css`
- 数据模型：`WmsPlus/Models/KcInspectExpiryModel.cs`
- API控制器：`WmsPlus.Api/Controllers/KcInspectExpiryController.cs`
- **选项卡ID**: `kc-inspect-expiry`
- **菜单路径**: 仓储管理 > 品质检验 > 单据管理 > 库存货品检验到期查询

**关联表信息**：与请检任务单共用 MF_QJRW / TF_QJRW 表（侧重库存检验到期维度展示）

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 仓库代号 | `WarehouseCodeInput` 组件 | `query.WarehouseCode` | 通用仓库选择组件 |
| 货品代号 | `<input>` text | `query.PrdNo` | 模糊查询 |
| 货品名称 | `<input>` text | `query.PrdName` | 模糊查询（视窗查询待开发） |
| 超验天数 | `<input type="number">` | `query.ExceedDays` | 默认0 |

**表格默认栏位**（参考截图）：☑复选框、仓库名称、储位、货品代号、货品名称、批号、单位名称、可验数量、可验数量(副)、最近验日期、操作

**右上角按钮**：生成请检任务单（特殊功能按钮，样式占位不实现功能）

**特点**：有复选列 + "生成请检任务单"专属操作按钮

**后端API**：`GET /api/KcInspectExpiry/search?warehouseCode=&prdNo=&prdName=&exceedDays=`

---

### 4. 检验单 (InspectionOrder)

**页面文件**：
- 主页面：`WmsPlus/Pages/InspectionOrder.razor`
- 样式文件：`WmsPlus/wwwroot/css/inspection-order.css`
- 数据模型：`WmsPlus/Models/InspectionOrderModel.cs`
- API控制器：`WmsPlus.Api/Controllers/InspectionOrderController.cs`
- **选项卡ID**: `inspection-order`
- **菜单路径**: 仓储管理 > 品质检验 > 单据管理 > 检验单

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| MF_TY | 检验单表头（29字段） | TY_NO | MfTy.cs |
| TF_TY | 检验单表身（36字段） | (TY_NO, ITM) | TfTy.cs |
| PDA_BAR_COLLECT | 单据条码明细表（58字段，共用） | - | PdaBarCollect.cs |

**MF_TY 表头核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| TY_NO | 检验单号 | 主键 |
| TY_DD | 检验时间 | 日期时间 |
| BIL_KND | 单据类型 | RK入库检验/CK出库检验/KC库存检验 |
| TYWZ | 检验位置 | 1库内/2库外 |
| DEP | 部门 | 关联DEPT |
| BIL_NO | 转入单号 | 字符串 |
| BIL_TYPE | 单据类别 | 字符串 |
| TYPE_ID | 业务类型 | 字符串 |
| CUS_NO | 客户 | 字符串 |
| CUS_NAME | 客户/厂商名称 | 字符串 |
| REM | 备注 | 字符串 |
| USR | 制单人 | 字符串 |

**TF_TY 表身核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| TY_NO | 检验单号 | 外键→MF_TY |
| ITM | 项次 | 数字 |
| PRD_NO | 货品代号 | 字符串 |
| PRD_NAME | 货品名称 | 字符串 |
| QTY | 合格数量 | 数字 |
| QTY_LOST | 不合格数量 | 数字 |
| UNIT | 单位 | 字符串 |
| WH | 仓库 | 字符串 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 单据日期 | `DateRangeInput` 组件 | `query.DateRange` | 日期范围选择器 |
| 单据号码 | `<input>` text | `query.DocumentNumber` | 模糊查询 |
| 制单人 | `<input>` text | `query.Usr` | 制单人 |

**表格默认栏位**（参考截图）：项次、单据号码、单据日期、单据类型、检验位置、部门、单据类别、业务类型、转入单号、备注、操作

**右上角按钮**：新增、导出

**后端API**：`GET /api/InspectionOrder/search?dateFrom=&dateTo=&documentNumber=&usr=`

**表格数据逻辑**：以 **TF_TY（表身）为主表**，通过 TY_NO LEFT JOIN MF_TY（表头）

---

### 5. 验收退回单 (AcceptanceReturn)

**页面文件**：
- 主页面：`WmsPlus/Pages/AcceptanceReturn.razor`
- 样式文件：`WmsPlus/wwwroot/css/acceptance-return.css`
- 数据模型：`WmsPlus/Models/AcceptanceReturnModel.cs`
- API控制器：`WmsPlus.Api/Controllers/AcceptanceReturnController.cs`
- **选项卡ID**: `acceptance-return`
- **菜单路径**: 仓储管理 > 品质检验 > 单据管理 > 验收退回单

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| MF_YB | 验收退回单表头（19字段） | YB_NO | MfYb.cs |
| TF_YB | 验收退回单表身（23字段） | (YB_NO, ITM) | TfYb.cs |
| PDA_BAR_COLLECT | 单据条码明细表（58字段，共用） | - | PdaBarCollect.cs |

**MF_YB 表头核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| YB_NO | 验收退回单号 | 主键 |
| YB_DD | 单据日期 | 日期 |
| TY_NO | 检验单号 | 外键→MF_TY |
| BIL_KND | 单据类型 | RK入库检验/CK出库检验/KC库存检验 |
| BIL_TYPE | 单据类别 | 字符串 |
| DEP | 部门 | 关联DEPT |
| CUS_NO | 厂商代号 | 字符串 |
| CUS_NAME | 厂商名称 | 字符串 |
| TYPE_ID | 业务类型 | 字符串 |
| REM | 备注 | 字符串 |
| USR | 制单人 | 字符串 |

**TF_YB 表身核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| YB_NO | 验收退回单号 | 外键→MF_YB |
| ITM | 项次 | 数字 |
| TY_NO | 检验单号 | 外键→MF_TY |
| PRD_NO | 货品代号 | 字符串 |
| PRD_NAME | 货品名称 | 字符串 |
| QTY | 数量 | 数字 |
| UNIT | 单位 | 字符串 |
| WH | 仓库 | 字符串 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 单据日期 | `DateRangeInput` 组件 | `query.DateRange` | 日期范围选择器 |
| 单据号码 | `<input>` text | `query.DocumentNumber` | 模糊查询 |
| 制单人 | `<input>` text | `query.Usr` | 制单人 |

**表格默认栏位**（参考截图）：项次、单据号码、单据日期、单据类型、部门名称、单据类别、检验单号、备注、厂商代号、厂商名称、操作

**右上角按钮**：新增、导出

**后端API**：`GET /api/AcceptanceReturn/search?dateFrom=&dateTo=&documentNumber=&usr=`

**表格数据逻辑**：以 **TF_YB（表身）为主表**，通过 YB_NO LEFT JOIN MF_YB（表头）

---

### 通用字段说明查询SQL

```sql
-- 盘盈单表头
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='MF_YN.DB';
-- 盘盈单表身
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='TF_YN.DB';
-- 请检任务单表头
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='MF_QJRW.DB';
-- 请检任务单身
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='TF_QJRW.DB';
-- 检验单表头
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='MF_TY.DB';
-- 检验单表身
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='TF_TY.DB';
-- 验收退回单表头
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='MF_YB.DB';
-- 验收退回单表身
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='TF_YB.DB';
-- PDA条码采集表
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='PDA_BAR_COLLECT.DB';
```

## 后端开发强制规范（EF Core & API）

> **血泪教训**：2026-06-12 因新增实体缺 HasKey 配置导致 WarehouseDbContext 全部 API 500 崩溃，仓库/货品/部门等选择器全部空白。**以下规则必须严格遵守，违者必崩。**

### 一、新增 DbSet 实体的 mandatory 检查清单

每次在 `WarehouseDbContext.cs` 中添加 `public DbSet<XXX>` 后，**必须同时完成以下全部操作**，缺一不可：

| 步骤 | 操作 | 验证方式 |
|------|------|---------|
| 1 | 创建实体模型类 `WmsPlus.Api/Models/Xxx.cs` | 文件存在且有属性定义 |
| 2 | 在 `WarehouseDbContext` 中注册 `DbSet<Xxx>` | DbSet 行存在 |
| 3 | **在 `OnModelCreating` 中添加 Fluent API 配置块** | `modelBuilder.Entity<Xxx>(entity => { ... })` 存在 |
| 4 | **配置块内必须包含 `HasKey()` 调用** | `entity.HasKey(e => ...)` 存在 |
| 5 | 编译通过 | `dotnet build` 0 错误 |
| 6 | **启动后端服务，调用一个已有的 API 确认无 500 错误** | 如 `GET /api/warehouse/search` |

### 二、HasKey 配置规则

EF Core 要求每个实体必须有主键。根据表结构类型，按以下规则配置：

#### 单主键表（大多数情况）
```csharp
modelBuilder.Entity<MyEntity>(entity =>
{
    entity.ToTable("TABLE_NAME");
    entity.HasKey(e => e.ID);           // ← 必须有！单字段主键
    entity.Property(e => e.ID).HasColumnName("ID");
    // ... 其他属性配置
});
```

#### 复合主键表（表身类 Tf_ 开头、多对多中间表等）
```csharp
modelBuilder.Entity<TfXxx>(entity =>
{
    entity.ToTable("TF_XXX");
    entity.HasKey(e => new { e.MAIN_NO, e.ITM });  // ← 复合主键：单号 + 项次
    // ...
});
```

#### 无主键视图/只读表（极少数）
```csharp
modelBuilder.Entity<ReadOnlyView>(entity =>
{
    entity.ToTable("VIEW_NAME");
    entity.HasNoKey();   // ← 显式声明无主键
    entity.ToView("VIEW_NAME");
});
```

### 三、前端组件 API 请求认证规范

所有前端组件（特别是 `WmsPlus/Components/` 下的选择器组件）发起 API 请求时：

**必须使用**：
```csharp
@inject TokenProvider TokenProvider    // ← 注入 TokenProvider

// 请求方式：
var request = AuthHttpClient.CreateRequest(HttpMethod.Get, url, TokenProvider.Token);
var responseMsg = await Http.SendAsync(request);
```

**禁止使用**：
```csharp
// ❌ 错误！不带 Token，后端会返回 401 或被全局认证拦截
var response = await Http.GetAsync(url);
```

### 四、已有选择器组件清单（均需使用 AuthHttpClient）

| 组件文件 | 注入依赖 | API 端点 |
|---------|---------|---------|
| `WarehouseCodeInput.razor` | HttpClient, IJSRuntime, **TokenProvider** | `/api/warehouse/search` |
| `ProductCodeInput.razor` | HttpClient, IJSRuntime, **TokenProvider** | `/api/warehouse/prdsearch` |
| `CustomerCodeInput.razor` | HttpClient, ILogger, **TokenProvider** | `/api/warehouse/customersearch` |
| `BizTypeInput.razor` | HttpClient, **TokenProvider** | `/api/warehouse/rktypesearch` |
| `CkTypeInput.razor` | HttpClient, **TokenProvider** | `/api/warehouse/cktypesearch` |
| `DeptCodeInput.razor` | HttpClient, ILogger, **TokenProvider** | `/api/warehouse/deptsearch` |

### 五、冒烟测试检查点

每次后端代码修改（尤其是涉及 DbContext、实体类、Controller）后，必须验证以下 API 正常返回：

```
GET http://localhost:5102/api/warehouse/search?pageIndex=1&pageSize=3     → 应返回 JSON 数据
GET http://localhost:5102/api/warehouse/columns?tableName=MY_WH.DB        → 应返回字段列表
```

如果任一接口返回 500，说明 **WarehouseDbContext 模型有问题**，必须立即排查修复。