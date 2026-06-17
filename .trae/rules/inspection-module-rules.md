# 检验管理模块 - 表结构规范

## 🚀 何时读取本规则

开发品质检验模块（盘盈单/请检任务单/检验单/验收退回单/检验到期查询）页面时。

---

## 数据库连接

所有检验管理模块页面连接 **db_gz01** 数据库（通过 `WarehouseDbContext` 访问），字段说明查询使用 **wmssystem** 数据库（`DICT_TAB` + `DICT_FLD` 表）。

---

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
