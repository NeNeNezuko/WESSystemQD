# 入库通知单 - 仓库选择器功能计划

## 一、需求概述

在入库通知单查询表单中，点击 **仓库代号** 输入框右侧的放大镜按钮，弹出一个 **仓库信息查询/选择对话框**，用户可以搜索、筛选、查看所有仓库信息，并通过栏位设计自定义显示列，最终选中某个仓库后将其代号回填到表单中。

---

## 二、现状分析

### 2.1 前端现状（入库通知单页面）

| 检查项 | 状态 | 说明 |
|--------|------|------|
| 仓库代号放大镜按钮 | 存在但未绑定事件 | InboundNotice.razor 第166行，search-icon-btn 无 @onclick |
| 栏位设计模式 | 已实现可复用 | ColumnConfig 类 + 对话框 UI + localStorage 持久化 |

### 2.2 后端现状

| 检查项 | 状态 | 说明 |
|--------|------|------|
| MY_WH 实体模型 | 不存在 | 需新建 |
| WarehouseDbContext DbSet | 未注册 MY_WH | 当前仅注册 MfRktz、TfRktz |
| 仓库查询 API | 不存在 | 需新建 |
| DICT_TAB / DICT_FLD | 不存在 | 需从零实现字段元数据查询 |

### 2.3 数据库信息

**数据源**: db_gz01 数据库，表名 MY_WH

**字段元数据查询语句**:
```sql
SELECT A.TAB_NAME, A.TAB_TITLE, B.FLD_NAME, B.Note
FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME = B.TAB_NAME
WHERE A.TAB_NAME = 'MY_WH.DB';
```
注：DICT_TAB 和 DICT_FLD 表位于 wmssystem 数据库（AppDbContext）

---

## 三、功能需求（参考截图）

### 3.1 触发入口
- 入库通知单查询表单 -> 仓库代号 输入框右侧的 放大镜图标按钮
- 点击后弹出「仓库(MY_WH)」选择对话框

### 3.2 对话框整体布局

```
+----------------------------------------------------------------------+
|  仓库(MY_WH)                              [栏位] [过滤]    [X]       |
+----------------------+-------------------------------------------------+
|  过滤                 |  操作 [G] | 项次 | 仓库代号 |仓库名称|...      |
|  +--------------+    |  [选择]   |  1   |000000000|First St|          |
|  |输入关键字     |    |  [选择]   |  2   |  W01    | 材料仓 |          |
|  +--------------+    |         ...                                         |
|  [ 查询 ]        |                                              共N笔记录 |
|                      |                                   [确定]    [取消]  |
+----------------------+-------------------------------------------------+
```

### 3.3 左侧面板 — 过滤区

| 组件 | 说明 |
|------|------|
| 标题 | "过滤" |
| 搜索输入框 | 关键字模糊查询（placeholder: "输入关键字查询"） |
| 查询按钮 | 蓝色按钮，执行搜索 |
| 过滤条件按钮 (G) | 点击弹出下拉菜单，包含4个选项：批次添加条件、添加单一条件、自定义条件、移除条件 |

#### 过滤条件下拉菜单选项说明：

| 选项 | 功能 | 说明 |
|------|------|------|
| 批次添加条件 | 批量添加多个筛选条件 | 可一次添加多行筛选规则 |
| 添加单一条件 | 添加单个筛选条件 | 选择字段 + 条件类型(=/>/<等) + 值 |
| 自定义条件 | 手动输入 SQL WHERE 子句 | 高级用户直接写条件表达式 |
| 移除条件 | 删除已添加的筛选条件 | 清除当前所有或选中的条件 |

> 注意：此为高级功能，第一期可实现基础版（仅关键字搜索 + 预留过滤按钮UI），后续迭代补充完整的条件构建器。

### 3.4 右侧区域 — 数据表格

| 列 | 字段来源 | 默认可见 | 说明 |
|----|----------|:--------:|------|
| 操作 | 固定列 | Y | 每行一个"选择"按钮；表头右侧有 G 栏位设计按钮 |
| 项次 | 行号 | Y | 自动编号 |
| 仓库代号 | MY_WH.WH_ID | Y | 主键 |
| 仓库名称 | MY_WH.WH_NAME | Y | |
| 仓库类型 | MY_WH.WH_TYPE | Y | |
| 启用储位 | MY_WH.IS_BIN | Y | 是否启用储位管理 |
| 上架流程 | MY_WH.PUT_IN_FLOW | Y | |
| 货架分类 | MY_WH.RACK_CLASS | Y | |

- 分页：右下角显示"共 N 笔记录"，带页码切换
- G 栏位设计按钮（操作列表头右侧）：点击弹出独立的栏位设计对话框
- 图标一致性保证：仓库选择器表格操作列的 G 按钮，与入库通知单表格操作列的 G 使用完全相同的 SVG 图标和 CSS 类名（column-settings-btn），确保两处视觉样式 100% 一致
- 每行"选择"按钮：点击后关闭对话框，将仓库代号填入表单的"仓库代号"输入框

#### G 操作列旁的栏位设计按钮 - 完整交互链路（参考截图）

```
仓库选择器对话框
+-- 右侧表格 -> 表头 "操作 [G]" <- 点击齿轮图标
|       |
|       +-- 【与入库通知单表格操作列的 G 图标完全相同】
|          同一个 SVG 图标 + 同一个 column-settings-btn CSS 类
|       |
|       v
|   弹出「栏位设计」对话框（覆盖在仓库选择器之上）
|   |-- 搜索栏：模糊查询栏位原名
|   |-- 列表：7个字段（操作、仓库代号、仓库名称、仓库类型、启用储位、上架流程、货架分类）
|   |   +-- 每行：项次 / 显示否(Toggle) / 原栏位名(只读) / 栏位标题(可编辑) / 宽度(可编辑)
|   |-- 左下角：全选 + 恢复默认
|   +-- 右下角：确定 + 取消
|       |
|       v 确定后
|   仓库表格立即按新配置渲染（显隐/标题/宽度）
|
+-- 注意：此栏位配置独立存储，不影响入库通知单表格的栏位配置
```

### 3.5 栏位设计对话框（复用已有模式）

与入库通知单已有的栏位设计完全一致：
- Toggle 开关控制显隐
- 栏位标题可编辑
- 宽度可调整
- 全选 / 恢复默认 / 确定 / 取消
- localStorage 持久化（独立键名：wms_column_settings_warehouse_selector）

---

## 四、涉及修改/新增的文件

### 后端（新增 3 个文件 + 修改 2 个文件）

| 文件 | 操作 | 说明 |
|------|------|------|
| WmsPlus.Api/Models/MyWh.cs | 新增 | MY_WH 表实体模型 |
| WmsPlus.Api/Models/DictTab.cs | 新增 | DICT_TAB 字典表实体 |
| WmsPlus.Api/Models/DictFld.cs | 新增 | DICT_FLD 字段实体 |
| WmsPlus.Api/Data/WarehouseDbContext.cs | 修改 | 注册 MyWh DbSet |
| WmsPlus.Api/Controllers/WarehouseController.cs | 新增 | 仓库查询 API 接口 |
| WmsPlus.Api/Data/AppDbContext.cs | 修改 | 注册 DictTab、DictFld DbSet |

### 前端（修改 1 个文件 + 新增样式）

| 文件 | 操作 | 说明 |
|------|------|------|
| WmsPlus/Pages/InboundNotice.razor | 修改 | 1) 放大镜按钮绑定 @onclick；2) 新增仓库选择器对话框 UI；3) 新增状态变量和方法；4) 复用 ColumnConfig 模式做仓库表格栏位设计 |
| WmsPlus/wwwroot/css/inbound-notice.css | 修改 | 新增仓库选择器对话框样式 |

---

## 五、实施步骤

### Step 1：后端 - 新增实体模型

1.1 MyWh.cs - 仓库主数据模型（基于 DICT_FLD 查询结果推测字段）
```csharp
// WmsPlus.Api/Models/MyWh.cs
public class MyWh
{
    public string WH_ID { get; set; }        // 仓库代号
    public string WH_NAME { get; set; }      // 仓库名称
    public int WH_TYPE { get; set; }         // 仓库类型
    public string IS_BIN { get; set; }       // 启用储位
    public int PUT_IN_FLOW { get; set; }     // 上架流程
    public int RACK_CLASS { get; set; }      // 货架分类
}
```

1.2 DictTab.cs / DictFld.cs - 字典元数据模型
```csharp
public class DictTab { public string TAB_NAME { get; set; } public string TAB_TITLE { get; set; } }
public class DictFld { public string TAB_NAME { get; set; } public string FLD_NAME { get; set; } public string Note { get; set; } }
```

### Step 2：后端 - 注册 DbContext

2.1 WarehouseDbContext.cs - 添加 DbSet<MyWh> MyWhs
2.2 AppDbContext.cs - 添加 DbSet<DictTab> DictTabs、DbSet<DictFld> DictFlds

### Step 3：后端 - 新增 WarehouseController

接口设计：
- Search：支持关键字模糊匹配（WH_ID / WH_NAME），分页返回
- GetColumns：根据 tableName 返回该表的字段定义（用于初始化前端栏位配置）

### Step 4：前端 - 仓库代号放大镜按钮绑定事件

InboundNotice.razor 第166行的 search-icon-btn 绑定 @onclick="OpenWarehouseSelector"

### Step 5：前端 - 新增仓库选择器状态和方法

复用 ColumnConfig 模式：
- showWarehouseDialog: 对话框显隐
- warehouseSearchKeyword: 搜索关键字
- warehouseData: 仓库数据列表
- warehousePageIndex / warehousePageSize / warehouseTotalCount: 分页
- warehouseColumnConfigs: 独立的栏位配置（与入库通知单表格分离）
- OpenWarehouseSelector(): 打开对话框并加载数据
- CloseWarehouseSelector(): 关闭对话框
- SelectWarehouse(item): 选中仓库，填入 query.WarehouseCode 并关闭
- SearchWarehouse(): 调用后端 API 搜索
- 栏位设计相关方法（复用已有模式，独立实例）

### Step 6：前端 - 仓库选择器对话框 UI

模态弹窗，包含：
- 左侧面板：标题"过滤"、搜索输入框、查询按钮、过滤条件按钮(G)
- 右侧数据区：动态表格（基于 warehouseColumnConfigs 渲染）、分页控件
- 底部：确定/取消按钮
- 内嵌栏位设计对话框（独立实例，不共用入库通知单的配置）
- 表格操作列使用与入库通知单相同的 G 图标和 column-settings-btn 样式

### Step 7：CSS 样式

新增仓库选择器对话框全套样式（左右分栏布局、表格、分页、过滤下拉菜单等）

---

## 六、注意事项

1. **栏位设计独立存储**：仓库选择器的栏位配置使用独立的 localStorage 键名 (wms_column_settings_warehouse_selector)，不影响入库通知单表格的栏位配置
2. **MY_WH 字段需确认**：上述字段列表基于截图推断，实际开发时先执行 DICT_FLD 查 SQL 确认完整字段后再建实体
3. **跨数据库查询**：DICT_TAB/DICT_FLD 在 wmssystem 库，MY_WH 在 db_gz01 库，需要两个 DbContext 分别查询
4. **过滤条件功能分期**：第一期实现基础搜索 + 过滤按钮UI占位，批次添加/单一条件/自定义条件/移除条件作为后续迭代
5. **选择行为**：点击"选择"后仅填充仓库代号到表单，不自动触发查询（用户仍需手动点"查询"按钮）
6. **厂商代号的放大镜按钮**：同样预留了未绑定的 search-icon-btn（第207行），本计划暂不处理，后续可按相同模式扩展
7. **图标一致性**：仓库选择器表格操作列旁的 G 栏位设计按钮，与入库通知单表格操作列的 G 按钮**使用同一个 SVG 图标 + 同一个 column-settings-btn CSS 类**，确保两处视觉样式完全一致
