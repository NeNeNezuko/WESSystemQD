# 基础资料 - 功能开发规则

## 功能概述

基础资料是仓储管理下的二级菜单，一共要开发 **21 个选项卡**页面，全部连接 **db_gz01** 数据库（通过 `WarehouseDbContext` 访问），字段说明从 **wmssystem** 数据库的 `DICT_TAB` + `DICT_FLD` 表查询。

### 字段说明参考文件

开发报表界面或查询表单时，**必须先读取**字段说明参考文件，查找表名对应的中文字段和数据库字段映射：
- **文件路径**：`.trae/references/db-gz01-field-dict.md`
- **内容**：389 张表的字段名与中文说明对照表（共 5,508 个字段）
- **使用方式**：根据表名在文件中搜索 `## 表名`，即可找到该表所有字段的中文名称

### 页面与数据库表映射参考文件

开发任何页面前，**必须先读取**页面与数据库表映射参考文件，确认该页面关联的数据库表：
- **文件路径**：`.trae/references/page-table-mapping.md`
- **内容**：全部 88 个页面与数据库表的关联关系，以及 WarehouseDbContext 全部 DbSet 注册表
- **使用方式**：根据页面名称或选项卡ID在文件中搜索，即可找到该页面关联的所有数据库表名

### 错误排查参考文件

当 Blazor 前端出现报错、异常、404/500 错误时，**必须先读取**错误排查记录，按症状快速匹配历史案例：
- **文件路径**：`.trae/references/error-troubleshooting.md`
- **内容**：已解决的历史错误完整记录（症状、根因、修复方法）、通用调试工具箱
- **触发词**：`报错排查`、`错误记录`、`异常定位`、`Unhandled error`、`404`、`JSON解析异常`
- **使用方式**：出现前端异常时，在文件中搜索关键词（如 `404`、`渲染崩溃`、`HasKey`）快速定位相似案例

#### 异常修复记录规范（强制执行）

每次修复任何异常/bug 后，**必须**将以下信息补充到错误排查参考文件（[error-troubleshooting.md](../references/error-troubleshooting.md)）：

1. **新增或更新一条错误记录**，包含以下要素：
   - 症状描述（用户看到的报错现象 + 控制台关键错误信息）
   - 触发条件（什么操作/场景会触发）
   - 根因分析（真正的原因，不是表面现象）
   - 修复方法（具体代码改动或配置修改，附文件路径和行号）
   - 影响范围（哪些页面/组件可能受同样问题影响）
   - 预防措施（如何避免再次发生）

2. **同步更新错误索引表**（文件顶部的速查表格），确保新错误可被快速检索

3. **如果该错误属于已知分类**（如 URL 拼接、EF Core 配置等），则在对应章节末尾追加新案例；如果是全新类型的错误，则新建章节

> **目的**：将每次排错过程转化为可复用的知识资产，避免团队成员重复花费时间排查相同问题

### 21 个选项卡清单

| # | 选项卡名称 | 选项卡ID | 分类 | 关联表 |
|---|-----------|---------|------|--------|
| 1 | 货品代号设定 | product-code-setting | 货品资料 | PRDT |
| 2 | 中类代号设定 | mid-class-setting | 货品资料 | INDX |
| 3 | 储存性质设定 | storage-nature-setting | 货品资料 | CW_XZ |
| 4 | 货品属性信息设定 | product-attr-setting | 货品资料 | PRDT_PDA_RN |
| 5 | 货品特征码段设定 | product-mark-setting | 货品资料 | PRD_MARK |
| 6 | 仓库代号设定 | warehouse-code-setting | 仓库资料 | MY_WH, CW_WH |
| 7 | 储位明细 | storage-location-detail | 仓库资料 | CW_WH |
| 8 | 依储类设定货品储位 | product-storage-by-class | 仓库资料 | PRDT_CW |
| 9 | 依储存性设定货品储位 | product-storage-by-nature | 仓库资料 | PRDT_CW_XZ |
| 10 | 依仓库启用到货确认 | warehouse-arrival-confirm | 仓库资料 | IZWH_CONFIRM |
| 11 | 波次策略 | wave-strategy | 系统策略 | BC_RULE, BC_RULE_PROP |
| 12 | 拣货策略 | pick-strategy | 系统策略 | PK_RULE, PK_RULE_PROP |
| 13 | 下架策略 | unshelve-strategy | 系统策略 | XJ_RULE, XJ_RULE_PROP |
| 14 | 不能参与配位的批号设置 | batch-no-picking-setting | 系统策略 | BAT_NOT_PW, BAT_NOT_PW1 |
| 15 | 部门设定 | dept-setting | 其他资料 | DEPT |
| 16 | 单据类别设定 | doc-type-setting | 其他资料 | BIL_SPC |
| 17 | 查盘/与原因设定 | count-reason-setting | 其他资料 | IJ_REASON_SET |
| 18 | 即时消息通知设定 | notice-setting | 其他资料 | NOTICE_SET |
| 19 | 行业代号设定 | industry-code-setting | 其他资料 | DEF_NS |
| 20 | 叉车车号管理 | forklift-management | 其他资料 | FORK_TRUCK |
| 21 | 导入期初库存 | import-initial-inventory | 数据导入 | 仅样式，不实现功能 |

## 数据库信息

### 连接数据库
- **数据库名**: `db_gz01`（通过 `WarehouseDbContext` 访问）
- **字段说明查询库**: `wmssystem`（通过 `DICT_TAB` + `DICT_FLD` 表查询）

### 字段说明通用查询SQL模板
```sql
SELECT A.TAB_NAME, A.TAB_TITLE, B.FLD_NAME, B.Note 
FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME = B.TAB_NAME 
WHERE A.TAB_NAME = '{表名}.DB';
```

---

## 一、货品资料类（5个页面）

### 1. 货品代号设定 (ProductCodeSetting)

**选项卡ID**: `product-code-setting`
**菜单路径**: 仓储管理 > 基础资料 > 货品代号设定

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| PRDT | 货品主档 | PRD_NO | Prdt.cs |

**PRDT 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| PRD_NO | 货品代号 | 主键 |
| NAME | 名称 | 字符串 |
| SNM | 简称 | 字符串 |
| IDX1 | 中类代号 | 字符串 |
| UT | 主单位 | 字符串 |
| UT1 | 副单位 | 字符串 |
| SPC | 货品规格 | 字符串 |
| KND | 大类 | 字符串 |
| MRK | 品牌 | 字符串 |
| CWXZ_NO | 储存性质代号 | 字符串 |
| CW_FLAG | 启用储位管理 | 字符串 |
| CHK_BAT | 批号管制否 | 字符串 |
| CHK_NUM | 序列号管制否 | 字符串 |
| VALID_DAYS | 有效天数 | 数字 |
| MOB_ID | 货品特征模版 | 字符串 |
| WH | 预设仓库 | 字符串 |
| DEP | 所属部门 | 字符串 |
| NOUSE_DD | 货品停用日期 | 日期 |
| START_DD | 货品创建日 | 日期 |
| USR | 录入员 | 字符串 |
| SYS_DATE | 输入日期 | 日期时间 |
| UP_DD | 上次更新时间 | 日期时间 |
| REM | 摘要 | 字符串 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 货品代号 | `<input>` text | `query.PrdNo` | 模糊查询 |
| 名称 | `<input>` text | `query.Name` | 模糊查询 |

**表格默认栏位**：货品代号、名称、简称、中类代号、主单位、规格、储存性质代号、停用日期、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/ProductCodeSetting/search?prdNo=&name=`

**页面文件**：
- 主页面：`WmsPlus/Pages/ProductCodeSetting.razor`
- 样式文件：`WmsPlus/wwwroot/css/product-code-setting.css`
- 数据模型：`WmsPlus/Models/ProductCodeSettingModel.cs`
- API控制器：`WmsPlus.Api/Controllers/ProductCodeSettingController.cs`

---

### 2. 中类代号设定 (MidClassSetting)

**选项卡ID**: `mid-class-setting`
**菜单路径**: 仓储管理 > 基础资料 > 中类代号设定

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| INDX | 中类索引 | IDX_NO | Indx.cs |

**INDX 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| IDX_NO | 中类代号 | 主键 |
| NAME | 名称 | 字符串 |
| IDX_UP | 上层中类 | 字符串 |
| STOP_DD | 停用日期 | 日期 |
| REM | 备注 | 字符串 |
| USR | 制单人 | 字符串 |
| SYS_DATE | 输单日期 | 日期时间 |
| UP_DD | 上次更新时间 | 日期时间 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 中类代号 | `<input>` text | `query.IdxNo` | 模糊查询 |
| 名称 | `<input>` text | `query.Name` | 模糊查询 |

**表格默认栏位**：中类代号、名称、上层中类、停用日期、备注、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/MidClassSetting/search?idxNo=&name=`

**页面文件**：
- 主页面：`WmsPlus/Pages/MidClassSetting.razor`
- 样式文件：`WmsPlus/wwwroot/css/mid-class-setting.css`
- 数据模型：`WmsPlus/Models/MidClassSettingModel.cs`
- API控制器：`WmsPlus.Api/Controllers/MidClassSettingController.cs`

---

### 3. 储存性质设定 (StorageNatureSetting)

**选项卡ID**: `storage-nature-setting`
**菜单路径**: 仓储管理 > 基础资料 > 储存性质设定

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| CW_XZ | 储存性质 | CWXZ_NO | CwXz.cs |

**CW_XZ 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| CWXZ_NO | 储存性质代号 | 主键 |
| NAME | 性质说明 | 字符串 |
| UP_DD | 时间戳 | 日期时间 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 储存性质代号 | `<input>` text | `query.CwxzNo` | 模糊查询 |
| 性质说明 | `<input>` text | `query.Name` | 模糊查询 |

**表格默认栏位**：储存性质代号、性质说明、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/StorageNatureSetting/search?cwxzNo=&name=`

**页面文件**：
- 主页面：`WmsPlus/Pages/StorageNatureSetting.razor`
- 样式文件：`WmsPlus/wwwroot/css/storage-nature-setting.css`
- 数据模型：`WmsPlus/Models/StorageNatureSettingModel.cs`
- API控制器：`WmsPlus.Api/Controllers/StorageNatureSettingController.cs`

---

### 4. 货品属性信息设定 (ProductAttrSetting)

**选项卡ID**: `product-attr-setting`
**菜单路径**: 仓储管理 > 基础资料 > 货品属性信息设定

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| PRDT_PDA_RN | AI-WMS货品附属信息 | (CON_NO, PRD_NO) | PrdtPdaRn.cs |

> 注意：文档中写的是 PRDT_PDA_SET，但数据库实际表名为 PRDT_PDA_RN

**PRDT_PDA_RN 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| CON_NO | 货主编码 | 字符串 |
| PRD_NO | 货品代号 | 主键之一 |
| QTY_COLLECT | 数量采集方式 | 字符串 |
| QTY_QZ_MODE | 数量取整模式 | 字符串 |
| UT_POINT | 主单位数量小数位 | 数字 |
| UT1_POINT | 副单位数量小数位 | 数字 |
| UT1_DISP | 副单位显示方式 | 字符串 |
| QTY_TYPE | 主单位数量修改反推副单位 | T/F |
| BARCODE_TYPE | 适用条码类型 | 字符串 |
| SHOW_TYPE | 主副互推弹窗显示 | 字符串 |
| NEED_SCALE | 需称重 | T/F |
| SCALE_POINT | 称重小数位数 | 数字 |
| SCALE_QZ | 称重数量取整 | 字符串 |
| SHOW_PAK | 包装重量 | 字符串 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 货品代号 | `<input>` text | `query.PrdNo` | 模糊查询 |

**表格默认栏位**：货品代号、数量采集方式、适用条码类型、需称重、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/ProductAttrSetting/search?prdNo=`

**页面文件**：
- 主页面：`WmsPlus/Pages/ProductAttrSetting.razor`
- 样式文件：`WmsPlus/wwwroot/css/product-attr-setting.css`
- 数据模型：`WmsPlus/Models/ProductAttrSettingModel.cs`
- API控制器：`WmsPlus.Api/Controllers/ProductAttrSettingController.cs`

---

### 5. 货品特征码段设定 (ProductMarkSetting)

**选项卡ID**: `product-mark-setting`
**菜单路径**: 仓储管理 > 基础资料 > 货品特征码段设定

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| PRD_MARK | 货品特征模版 | MOB_ID | PrdMark.cs |

**PRD_MARK 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| MOB_ID | 模版代号 | 主键 |
| MOB_NAME | 模版名称 | 字符串 |
| PRD_MARK | 货品特征 | 字符串 |
| REM | 说明 | 字符串 |
| END_DD | 停用日期 | 日期 |
| SETBYPRDT | 依货品设置特征分段 | 字符串 |
| USR | 制单人 | 字符串 |
| SYS_DATE | 创建日期 | 日期时间 |
| UP_DD | 上次更新时间 | 日期时间 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 模版代号 | `<input>` text | `query.MobId` | 模糊查询 |
| 模版名称 | `<input>` text | `query.MobName` | 模糊查询 |

**表格默认栏位**：模版代号、模版名称、货品特征、说明、停用日期、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/ProductMarkSetting/search?mobId=&mobName=`

**页面文件**：
- 主页面：`WmsPlus/Pages/ProductMarkSetting.razor`
- 样式文件：`WmsPlus/wwwroot/css/product-mark-setting.css`
- 数据模型：`WmsPlus/Models/ProductMarkSettingModel.cs`
- API控制器：`WmsPlus.Api/Controllers/ProductMarkSettingController.cs`

---

## 二、仓库资料类（5个页面）

### 6. 仓库代号设定 (WarehouseCodeSetting)

**选项卡ID**: `warehouse-code-setting`
**菜单路径**: 仓储管理 > 基础资料 > 仓库代号设定

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| MY_WH | 仓库主档 | WH | MyWh.cs |
| CW_WH | 储位主档 | CHUW | CwWh.cs |

**MY_WH 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| WH | 仓库 | 主键 |
| NAME | 名称 | 字符串 |
| ATTRIB | 属性 | 字符串 |
| DEP | 部门代号 | 字符串 |
| STOP_DD | 停用日期 | 日期 |
| CW_FLAG | 启用储位管理 | 字符串 |
| WH_TYPE | 上下架模式 | 字符串 |
| RK_FLOW | 入库流程 | 字符串 |
| PK_FLOW | 拣货流程 | 字符串 |
| RULE_ID_PK | 拣货策略代号 | 字符串 |
| RULE_ID_BC | 波次策略代号 | 字符串 |
| RULE_ID_XJ | 下架策略代号 | 字符串 |
| REM | 备注 | 字符串 |
| USR | 录入员 | 字符串 |
| UP_DD | 上次更新时间 | 日期时间 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 仓库代号 | `<input>` text | `query.Wh` | 模糊查询 |
| 名称 | `<input>` text | `query.Name` | 模糊查询 |

**表格默认栏位**：仓库代号、名称、属性、部门代号、启用储位管理、上下架模式、停用日期、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/WarehouseCodeSetting/search?wh=&name=`

**页面文件**：
- 主页面：`WmsPlus/Pages/WarehouseCodeSetting.razor`
- 样式文件：`WmsPlus/wwwroot/css/warehouse-code-setting.css`
- 数据模型：`WmsPlus/Models/WarehouseCodeSettingModel.cs`
- API控制器：`WmsPlus.Api/Controllers/WarehouseCodeSettingController.cs`

---

### 7. 储位明细 (StorageLocationDetail)

**选项卡ID**: `storage-location-detail`
**菜单路径**: 仓储管理 > 基础资料 > 储位明细

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| CW_WH | 储位主档 | CHUW | CwWh.cs |

**CW_WH 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| CHUW | 储位 | 主键 |
| NAME | 储位名称 | 字符串 |
| WH | 仓库 | 字符串 |
| GS | 排 | 字符串 |
| GL | 列 | 字符串 |
| LAYER | 层 | 字符串 |
| CW_STATUS | 储位状态 | 字符串 |
| LOCK_CW | 锁定状态 | 字符串 |
| LAYER_PROP | 层属性 | 字符串 |
| AREA_ID | 区域代号 | 字符串 |
| REM | 备注 | 字符串 |
| UP_DD | 更新时间 | 日期时间 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 储位代号 | `<input>` text | `query.Chuw` | 模糊查询 |
| 仓库代号 | WarehouseCodeInput | `query.Wh` | 仓库选择 |
| 储位状态 | `<select>` 下拉框 | `query.CwStatus` | 全部/正常/故障 |

**表格默认栏位**：储位代号、储位名称、仓库、排、列、层、储位状态、锁定状态、区域代号、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/StorageLocationDetail/search?chuw=&wh=&cwStatus=`

**页面文件**：
- 主页面：`WmsPlus/Pages/StorageLocationDetail.razor`
- 样式文件：`WmsPlus/wwwroot/css/storage-location-detail.css`
- 数据模型：`WmsPlus/Models/StorageLocationDetailModel.cs`
- API控制器：`WmsPlus.Api/Controllers/StorageLocationDetailController.cs`

---

### 8. 依储类设定货品储位 (ProductStorageByClass)

**选项卡ID**: `product-storage-by-class`
**菜单路径**: 仓储管理 > 基础资料 > 依储类设定货品储位

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| PRDT_CW | 货品储位 | GUID | PrdtCw.cs |

**PRDT_CW 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| GUID | 唯一值 | 主键 |
| PRD_NO | 货品代号 | 字符串 |
| IDX_NO | 中类 | 字符串 |
| CHUW | 储位代号 | 字符串 |
| WH | 仓库代号 | 字符串 |
| GS | 排 | 字符串 |
| GL | 列 | 字符串 |
| LAYER | 层 | 字符串 |
| ZONE_ID | 区域代号 | 字符串 |
| REM | 备注 | 字符串 |
| UP_DD | 更新时间 | 日期时间 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 中类代号 | `<input>` text | `query.IdxNo` | 模糊查询 |
| 仓库代号 | WarehouseCodeInput | `query.Wh` | 仓库选择 |

**表格默认栏位**：中类代号、货品代号、储位代号、仓库代号、排、列、层、区域代号、操作(编辑)

**右上角按钮**：新增、删除

**后端API**：`GET /api/ProductStorageByClass/search?idxNo=&wh=`

**页面文件**：
- 主页面：`WmsPlus/Pages/ProductStorageByClass.razor`
- 样式文件：`WmsPlus/wwwroot/css/product-storage-by-class.css`
- 数据模型：`WmsPlus/Models/ProductStorageByClassModel.cs`
- API控制器：`WmsPlus.Api/Controllers/ProductStorageByClassController.cs`

---

### 9. 依储存性设定货品储位 (ProductStorageByNature)

**选项卡ID**: `product-storage-by-nature`
**菜单路径**: 仓储管理 > 基础资料 > 依储存性设定货品储位

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| PRDT_CW_XZ | 货品储存性质 | GUID | PrdtCwXz.cs |

**PRDT_CW_XZ 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| GUID | 唯一值 | 主键 |
| PRD_NO | 货品代号 | 字符串 |
| CWXZ_NO | 储存性质代号 | 字符串 |
| CHUW | 储位代号 | 字符串 |
| WH | 仓库代号 | 字符串 |
| GS | 排 | 字符串 |
| GL | 列 | 字符串 |
| LAYER | 层 | 字符串 |
| ZONE_ID | 区域代号 | 字符串 |
| REM | 备注 | 字符串 |
| UP_DD | 时间戳 | 日期时间 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 储存性质代号 | `<input>` text | `query.CwxzNo` | 模糊查询 |
| 仓库代号 | WarehouseCodeInput | `query.Wh` | 仓库选择 |

**表格默认栏位**：储存性质代号、货品代号、储位代号、仓库代号、排、列、层、区域代号、操作(编辑)

**右上角按钮**：新增、删除

**后端API**：`GET /api/ProductStorageByNature/search?cwxzNo=&wh=`

**页面文件**：
- 主页面：`WmsPlus/Pages/ProductStorageByNature.razor`
- 样式文件：`WmsPlus/wwwroot/css/product-storage-by-nature.css`
- 数据模型：`WmsPlus/Models/ProductStorageByNatureModel.cs`
- API控制器：`WmsPlus.Api/Controllers/ProductStorageByNatureController.cs`

---

### 10. 依仓库启用到货确认 (WarehouseArrivalConfirm)

**选项卡ID**: `warehouse-arrival-confirm`
**菜单路径**: 仓储管理 > 基础资料 > 依仓库启用到货确认

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| IZWH_CONFIRM | 调拨确认 | (WH_OUT, WH_IN) | IzwhConfirm.cs |

**IZWH_CONFIRM 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| WH_OUT | 出货仓库 | 主键之一 |
| WH_IN | 入货仓库 | 主键之一 |
| USR | 制单人 | 字符串 |
| MODIFY_MAN | 修改人 | 字符串 |
| SYS_DATE | 制单时间 | 日期时间 |
| MODIFY_DD | 修改时间 | 日期时间 |
| DEP | 部门 | 字符串 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 出货仓库 | WarehouseCodeInput | `query.WhOut` | 仓库选择 |
| 入货仓库 | WarehouseCodeInput | `query.WhIn` | 仓库选择 |

**表格默认栏位**：出货仓库、入货仓库、制单人、修改人、制单时间、操作(编辑)

**右上角按钮**：新增、删除

**后端API**：`GET /api/WarehouseArrivalConfirm/search?whOut=&whIn=`

**页面文件**：
- 主页面：`WmsPlus/Pages/WarehouseArrivalConfirm.razor`
- 样式文件：`WmsPlus/wwwroot/css/warehouse-arrival-confirm.css`
- 数据模型：`WmsPlus/Models/WarehouseArrivalConfirmModel.cs`
- API控制器：`WmsPlus.Api/Controllers/WarehouseArrivalConfirmController.cs`

---

## 三、系统策略类（4个页面，共用策略规则+策略参数表结构）

### 共用表结构说明

三种策略（波次/拣货/下架）共用相同的表结构模式：
- **策略规则表**（BC_RULE / PK_RULE / XJ_RULE）：存储策略代号、名称、停用标记等
- **策略参数表**（BC_RULE_PROP / PK_RULE_PROP / XJ_RULE_PROP）：存储策略的参数配置

### 11. 波次策略 (WaveStrategy)

**选项卡ID**: `wave-strategy`
**菜单路径**: 仓储管理 > 基础资料 > 波次策略

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| BC_RULE | 波次策略 | RULE_ID | BcRule.cs |
| BC_RULE_PROP | 波次策略参数 | (RULE_ID, PROP_NO) | BcRuleProp.cs |

**BC_RULE 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| RULE_ID | 波次策略代号 | 主键 |
| NAME | 波次策略名称 | 字符串 |
| STOP_ID | 停用标记 | 字符串 |
| USR | 录入人员 | 字符串 |
| SYS_DATE | 录入时间 | 日期时间 |
| MODIFY_MAN | 最近修改人 | 字符串 |
| MODIFY_DD | 最近修改日期 | 日期时间 |

**BC_RULE_PROP 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| RULE_ID | 波次策略代号 | 外键 |
| PROP_NO | 参数名 | 字符串 |
| VALUE | 参数值 | 字符串 |
| REM | 备注 | 字符串 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 策略代号 | `<input>` text | `query.RuleId` | 模糊查询 |
| 策略名称 | `<input>` text | `query.Name` | 模糊查询 |

**表格默认栏位**：策略代号、策略名称、停用标记、录入人员、录入时间、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/WaveStrategy/search?ruleId=&name=`

**页面文件**：
- 主页面：`WmsPlus/Pages/WaveStrategy.razor`
- 样式文件：`WmsPlus/wwwroot/css/strategy-common.css`（3个策略页面共用）
- 数据模型：`WmsPlus/Models/StrategyModel.cs`（3个策略页面共用）
- API控制器：`WmsPlus.Api/Controllers/WaveStrategyController.cs`

---

### 12. 拣货策略 (PickStrategy)

**选项卡ID**: `pick-strategy`
**菜单路径**: 仓储管理 > 基础资料 > 拣货策略

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| PK_RULE | 拣货策略 | RULE_ID | PkRule.cs |
| PK_RULE_PROP | 拣货策略参数 | (RULE_ID, PROP_NO) | PkRuleProp.cs |

**PK_RULE 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| RULE_ID | 拣货策略代号 | 主键 |
| NAME | 拣货策略名称 | 字符串 |
| WH_TYPE | 上下架模式 | 字符串 |
| STOP_ID | 停用标记 | 字符串 |
| USR | 录入人员 | 字符串 |
| SYS_DATE | 录入时间 | 日期时间 |
| MODIFY_MAN | 最近修改人 | 字符串 |
| MODIFY_DD | 最近修改日期 | 日期时间 |

**查询表单字段**：同波次策略（策略代号、策略名称）

**表格默认栏位**：策略代号、策略名称、上下架模式、停用标记、录入人员、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/PickStrategy/search?ruleId=&name=`

**页面文件**：共用 WaveStrategy 的文件体系

---

### 13. 下架策略 (UnshelveStrategy)

**选项卡ID**: `unshelve-strategy`
**菜单路径**: 仓储管理 > 基础资料 > 下架策略

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| XJ_RULE | 下架策略 | RULE_ID | XjRule.cs |
| XJ_RULE_PROP | 下架策略参数 | (RULE_ID, PROP_NO) | XjRuleProp.cs |

**XJ_RULE 核心字段**：同 PK_RULE 结构

**查询表单字段**：同波次策略

**表格默认栏位**：策略代号、策略名称、上下架模式、停用标记、录入人员、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/UnshelveStrategy/search?ruleId=&name=`

**页面文件**：共用 WaveStrategy 的文件体系

---

### 14. 不能参与配位的批号设置 (BatchNoPickingSetting)

**选项卡ID**: `batch-no-picking-setting`
**菜单路径**: 仓储管理 > 基础资料 > 不能参与配位的批号设置

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| BAT_NOT_PW | 批号货品维护 | GUID | BatNotPw.cs |
| BAT_NOT_PW1 | 批号货品维护明细 | GUID | BatNotPw1.cs |

**BAT_NOT_PW 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| GUID | 唯一ID | 主键 |
| WH | 仓库代号 | 字符串 |
| PRD_NO | 货品代号 | 字符串 |
| PRD_MARK | 货品特征 | 字符串 |
| BAT_NO | 批号 | 字符串 |
| USR | 制单人 | 字符串 |
| SYS_DATE | 制单时间 | 日期时间 |
| REM | 备注 | 字符串 |

**BAT_NOT_PW1 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| BIL_ID | 单据别 | 字符串 |
| BIL_NO | 单据号码 | 字符串 |
| BIL_ITM | 单据项次 | 数字 |
| GUID | 唯一ID | 外键 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 仓库代号 | WarehouseCodeInput | `query.Wh` | 仓库选择 |
| 货品代号 | `<input>` text | `query.PrdNo` | 模糊查询 |
| 批号 | `<input>` text | `query.BatNo` | 模糊查询 |

**表格默认栏位**：仓库代号、货品代号、货品特征、批号、制单人、制单时间、备注、操作(编辑)

**右上角按钮**：新增、删除

**后端API**：`GET /api/BatchNoPickingSetting/search?wh=&prdNo=&batNo=`

**页面文件**：
- 主页面：`WmsPlus/Pages/BatchNoPickingSetting.razor`
- 样式文件：`WmsPlus/wwwroot/css/batch-no-picking-setting.css`
- 数据模型：`WmsPlus/Models/BatchNoPickingSettingModel.cs`
- API控制器：`WmsPlus.Api/Controllers/BatchNoPickingSettingController.cs`

---

## 四、其他资料类（6个页面）

### 15. 部门设定 (DeptSetting)

**选项卡ID**: `dept-setting`
**菜单路径**: 仓储管理 > 基础资料 > 部门设定

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| DEPT | 部门主档 | DEP | Dept.cs |

**DEPT 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| DEP | 部门代号 | 主键 |
| NAME | 名称 | 字符串 |
| ENG_NAME | 英文名称 | 字符串 |
| UP | 上层部门 | 字符串 |
| STOP_DD | 停用日期 | 日期 |
| MAKE_ID | 部门性质 | 字符串 |
| GROUP_ID | 是集团分公司 | 字符串 |
| USR | 录入员 | 字符串 |
| SYS_DATE | 输入日期 | 日期时间 |
| UP_DD | 上次更新时间 | 日期时间 |
| NAME_PY | 助记码 | 字符串 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 部门代号 | `<input>` text | `query.Dep` | 模糊查询 |
| 名称 | `<input>` text | `query.Name` | 模糊查询 |

**表格默认栏位**：部门代号、名称、上层部门、部门性质、停用日期、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/DeptSetting/search?dep=&name=`

**页面文件**：
- 主页面：`WmsPlus/Pages/DeptSetting.razor`
- 样式文件：`WmsPlus/wwwroot/css/dept-setting.css`
- 数据模型：`WmsPlus/Models/DeptSettingModel.cs`
- API控制器：`WmsPlus.Api/Controllers/DeptSettingController.cs`

---

### 16. 单据类别设定 (DocTypeSetting)

**选项卡ID**: `doc-type-setting`
**菜单路径**: 仓储管理 > 基础资料 > 单据类别设定

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| BIL_SPC | 单据类别 | (SPC_ID, SPC_NO) | BilSpc.cs |

**BIL_SPC 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| SPC_ID | 类别(RK/CK/IC/PD) | 主键之一 |
| SPC_NO | 单据类别代号 | 主键之一 |
| NAME | 名称 | 字符串 |
| REM | 备注 | 字符串 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 类别 | `<select>` 下拉框 | `query.SpcId` | 全部/RK入库/CK出库/IC调拨/PD盘点 |
| 单据类别代号 | `<input>` text | `query.SpcNo` | 模糊查询 |

**表格默认栏位**：类别、单据类别代号、名称、备注、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/DocTypeSetting/search?spcId=&spcNo=`

**页面文件**：
- 主页面：`WmsPlus/Pages/DocTypeSetting.razor`
- 样式文件：`WmsPlus/wwwroot/css/doc-type-setting.css`
- 数据模型：`WmsPlus/Models/DocTypeSettingModel.cs`
- API控制器：`WmsPlus.Api/Controllers/DocTypeSettingController.cs`

---

### 17. 查盘/与原因设定 (CountReasonSetting)

**选项卡ID**: `count-reason-setting`
**菜单路径**: 仓储管理 > 基础资料 > 查盘/与原因设定

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| IJ_REASON_SET | 调整原因设定 | (BIL_ID, IJ_REASON) | IjReasonSet.cs |

**IJ_REASON_SET 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| BIL_ID | 单据别 | 主键之一 |
| IJ_REASON | 原因代号 | 主键之一 |
| REASON_REM | 原因说明 | 字符串 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 单据别 | `<select>` 下拉框 | `query.BilId` | 全部/盘盈/盘亏 |
| 原因代号 | `<input>` text | `query.IjReason` | 模糊查询 |

**表格默认栏位**：单据别、原因代号、原因说明、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/CountReasonSetting/search?bilId=&ijReason=`

**页面文件**：
- 主页面：`WmsPlus/Pages/CountReasonSetting.razor`
- 样式文件：`WmsPlus/wwwroot/css/count-reason-setting.css`
- 数据模型：`WmsPlus/Models/CountReasonSettingModel.cs`
- API控制器：`WmsPlus.Api/Controllers/CountReasonSettingController.cs`

---

### 18. 即时消息通知设定 (NoticeSetting)

**选项卡ID**: `notice-setting`
**菜单路径**: 仓储管理 > 基础资料 > 即时消息通知设定

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| NOTICE_SET | 通知设定 | SET_NO | NoticeSet.cs |

**NOTICE_SET 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| SET_NO | 代号 | 主键 |
| TYPE_ID | 通知项目 | 字符串 |
| WH | 仓库 | 字符串 |
| SEND_OBJ | 发送对象 | 字符串 |
| SEND_USRS | 接受用户 | 字符串 |
| SEND_TYPE | 接受方式 | 字符串 |
| STOP_ID | 停用否 | 字符串 |
| USR | 制单人 | 字符串 |
| SYS_DATE | 制单时间 | 日期时间 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 代号 | `<input>` text | `query.SetNo` | 模糊查询 |
| 仓库 | WarehouseCodeInput | `query.Wh` | 仓库选择 |

**表格默认栏位**：代号、通知项目、仓库、发送对象、接受方式、停用否、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/NoticeSetting/search?setNo=&wh=`

**页面文件**：
- 主页面：`WmsPlus/Pages/NoticeSetting.razor`
- 样式文件：`WmsPlus/wwwroot/css/notice-setting.css`
- 数据模型：`WmsPlus/Models/NoticeSettingModel.cs`
- API控制器：`WmsPlus.Api/Controllers/NoticeSettingController.cs`

---

### 19. 行业代号设定 (IndustryCodeSetting)

**选项卡ID**: `industry-code-setting`
**菜单路径**: 仓储管理 > 基础资料 > 行业代号设定

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| DEF_NS | 默认NS设定 | NS_NO | DefNs.cs |

**DEF_NS 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| NS_NO | NS代号 | 主键 |
| NAME | 名称 | 字符串 |
| REM | 备注 | 字符串 |
| INC_SYS | 包含系统字段否 | 字符串 |
| INC_UNI | 基础资料启用UNICODE | 字符串 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| NS代号 | `<input>` text | `query.NsNo` | 模糊查询 |
| 名称 | `<input>` text | `query.Name` | 模糊查询 |

**表格默认栏位**：NS代号、名称、备注、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/IndustryCodeSetting/search?nsNo=&name=`

**页面文件**：
- 主页面：`WmsPlus/Pages/IndustryCodeSetting.razor`
- 样式文件：`WmsPlus/wwwroot/css/industry-code-setting.css`
- 数据模型：`WmsPlus/Models/IndustryCodeSettingModel.cs`
- API控制器：`WmsPlus.Api/Controllers/IndustryCodeSettingController.cs`

---

### 20. 叉车车号管理 (ForkliftManagement)

**选项卡ID**: `forklift-management`
**菜单路径**: 仓储管理 > 基础资料 > 叉车车号管理

**关联表信息**：

| 表名 | 说明 | 主键 | 对应实体类 |
|------|------|------|-----------|
| FORK_TRUCK | 叉车 | TRUCK_NO | ForkTruck.cs |

**FORK_TRUCK 核心字段**：

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| TRUCK_NO | 叉车号 | 主键 |
| NAME | 叉车名称 | 字符串 |
| WH | 仓库代号 | 字符串 |
| REM | 备注 | 字符串 |

**查询表单字段**：

| 字段 | 控件类型 | 绑定字段 | 说明 |
|------|---------|---------|------|
| 叉车号 | `<input>` text | `query.TruckNo` | 模糊查询 |
| 仓库代号 | WarehouseCodeInput | `query.Wh` | 仓库选择 |

**表格默认栏位**：叉车号、叉车名称、仓库代号、备注、操作(编辑)

**右上角按钮**：新增

**后端API**：`GET /api/ForkliftManagement/search?truckNo=&wh=`

**页面文件**：
- 主页面：`WmsPlus/Pages/ForkliftManagement.razor`
- 样式文件：`WmsPlus/wwwroot/css/forklift-management.css`
- 数据模型：`WmsPlus/Models/ForkliftManagementModel.cs`
- API控制器：`WmsPlus.Api/Controllers/ForkliftManagementController.cs`

---

## 五、数据导入类（1个页面）

### 21. 导入期初库存 (ImportInitialInventory)

**选项卡ID**: `import-initial-inventory`
**菜单路径**: 仓储管理 > 基础资料 > 导入期初库存

> **仅做 UI 样式，不实现功能逻辑**

**页面文件**：
- 主页面：`WmsPlus/Pages/ImportInitialInventory.razor`
- 样式文件：`WmsPlus/wwwroot/css/import-initial-inventory.css`

---

## 通用字段说明查询SQL汇总

```sql
-- 货品主档
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='PRDT.DB';
-- 中类索引
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='INDX.DB';
-- 储存性质
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='CW_XZ.DB';
-- 货品附属信息
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='PRDT_PDA_RN.DB';
-- 货品特征模版
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='PRD_MARK.DB';
-- 仓库主档
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='MY_WH.DB';
-- 储位主档
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='CW_WH.DB';
-- 货品储位
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='PRDT_CW.DB';
-- 货品储存性质
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='PRDT_CW_XZ.DB';
-- 调拨确认
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='IZWH_CONFIRM.DB';
-- 波次策略
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='BC_RULE.DB';
-- 波次策略参数
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='BC_RULE_PROP.DB';
-- 拣货策略
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='PK_RULE.DB';
-- 拣货策略参数
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='PK_RULE_PROP.DB';
-- 下架策略
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='XJ_RULE.DB';
-- 下架策略参数
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='XJ_RULE_PROP.DB';
-- 批号货品维护
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='BAT_NOT_PW.DB';
-- 批号货品维护明细
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='BAT_NOT_PW1.DB';
-- 部门主档
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='DEPT.DB';
-- 单据类别
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='BIL_SPC.DB';
-- 调整原因设定
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='IJ_REASON_SET.DB';
-- 通知设定
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='NOTICE_SET.DB';
-- 默认NS设定
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='DEF_NS.DB';
-- 叉车
SELECT A.TAB_NAME,A.TAB_TITLE,B.FLD_NAME,B.Note FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME=B.TAB_NAME WHERE A.TAB_NAME='FORK_TRUCK.DB';
```

## 开发规范要点

### 整体布局
- 参照入库通知单（InboundNotice）的左右分栏布局风格
- 使用项目通用组件：DateRangeInput、WarehouseCodeInput、BizTypeInput、DeptCodeInput、CustomerCodeInput

### 查询表单 — 起/止范围筛选框（form-item-fused）规范

当查询表单中需要"起/止"范围输入时（如转入单号起/止、货品代号起/止），**必须**使用 `form-item-fused` 上下分行融合框模式。

#### 触发条件
以下场景必须使用此模式：
- 任何需要 **范围查询** 的字段（从 XX 到 XX）
- 字段名标签以 **"起"/"止"** 结尾

#### HTML 结构模板

```html
<!-- 纯文本输入（无放大镜） -->
<div class="form-item-fused">
    <div class="fused-row">
        <label class="form-label">字段名 起</label>
        <input type="text" class="fused-input" @bind="query.XxxFrom" />
    </div>
    <div class="fused-divider"></div>
    <div class="fused-row">
        <label class="form-label">字段名 止</label>
        <input type="text" class="fused-input" @bind="query.XxxTo" />
    </div>
</div>

<!-- 带放大镜选择器（如货品代号） -->
<div class="form-item-fused">
    <div class="fused-row">
        <label class="form-label">货品代号 起</label>
        <div class="fused-input-area">
            <input type="text" class="fused-input" @bind="query.PrdNoFrom" />
            <button class="fused-search-btn" title="选择货品" @onclick="OpenPrdFromSelector">
                <svg>...</svg>
            </button>
        </div>
    </div>
    <div class="fused-divider"></div>
    <div class="fused-row">
        <label class="form-label">货品代号 止</label>
        <div class="fused-input-area">
            <input type="text" class="fused-input" @bind="query.PrdNoTo" />
            <button class="fused-search-btn" title="选择货品" @onclick="OpenPrdToSelector">
                <svg>...</svg>
            </button>
        </div>
    </div>
</div>
```

#### 关键规则

| 规则 | 说明 |
|------|------|
| **外层容器** | 必须使用 `class="form-item-fused"`，不可用独立的 `form-item` |
| **布局方向** | `flex-direction: column`（上下分行），**不是**左右并排 |
| **内层结构** | 每个 `fused-row` 包含 label + input，两行之间用 `fused-divider` 分隔 |
| **分隔线实现** | 由 CSS `.fused-row:first-child { border-bottom: 1px solid #e8e8e8; }` 实现，`fused-divider` 设为 `display: none` |
| **标签格式** | 上行标签以 **"起"** 结尾（如 `转入单号 起`），下行标签以 **"止"** 结尾（如 `转入单号 止`） |
| **输入框样式** | 使用 `class="fused-input"`（不是 `form-input`） |
| **禁止嵌套 ProductCodeInput** | ProductCodeInput 组件内部自带 `<div class="form-item">` + label，嵌套会导致**双标签重复显示**。如需放大镜功能，应手写 `<input>` + `<button @onclick="...">` 并在页面 @code 中实现选择器逻辑 |
| **放大镜按钮事件** | 放大镜按钮**必须绑定 `@onclick` 事件**，不能留空 |

#### CSS 样式（每个使用 form-item-fused 的页面 CSS 文件中必须包含）

```css
/* ===== 融合式单边框（起止输入框，上下分行） ===== */
.form-item-fused {
    display: flex;
    flex-direction: column;
    border: 1px solid #d9d9d9;
    border-radius: 2px;
    background-color: #ffffff;
    overflow: hidden;
}
.form-item-fused > .fused-row {
    display: flex;
    flex-direction: row;
    align-items: stretch;
    height: 30px;
}
.form-item-fused > .fused-row:first-child {
    border-bottom: 1px solid #e8e8e8;
}
.form-item-fused > .fused-row > * {
    margin: 0; padding: 0; box-sizing: border-box;
    border: none; outline: none;
}
.form-item-fused > .fused-row > .form-label {
    padding: 0 8px;
    background-color: #fafafa;
    font-size: 12px; color: #333333; font-weight: 400;
    white-space: nowrap;
    display: flex; align-items: center; justify-content: center;
    min-width: 70px; flex-shrink: 0;
}
.form-item-fused > .fused-row > .fused-input {
    flex: 1; padding: 0 6px; font-size: 12px; color: #333333;
    background-color: transparent;
    border: none !important; outline: none !important;
}
.form-item-fused > .fused-row > .fused-input-area {
    flex: 1; display: flex; align-items: center; position: relative;
}
.form-item-fused > .fused-row > .fused-input-area > .fused-input {
    width: 100%; padding: 0 6px; font-size: 12px; color: #333333;
    background-color: transparent;
    border: none !important; outline: none !important;
}
.fused-search-btn {
    position: absolute; right: 2px; top: 50%; transform: translateY(-50%);
    background: none; border: none; padding: 2px; cursor: pointer; z-index: 1;
}
.fused-divider { display: none; }
```

#### 参考实现
- **正确示例**: [ReceivingReport.razor](WmsPlus/Pages/ReceivingReport.razor) 第104-156行 + [receiving-report.css](WmsPlus/wwwroot/css/receiving-report.css) 第358-442行

### 功能实现原则
- **样式优先**：右上角按钮先做 UI 样式占位，不实现实际功能逻辑
- **查询功能完整**：每个页面的查询表单和 API 查询接口必须完整可用
- **数据库连接**：统一使用 db_gz01（WarehouseDbContext）
- **字段说明**：通过 wmssystem.DICT_TAB + DICT_FLD 查询获取
- **API基地址**：硬编码 `http://localhost:5102`
- **认证方式**：使用 AuthHttpClient.CreateRequest() 创建带 Bearer Token 的请求
- **导入期初库存**：仅做 UI 样式，不实现功能

### 标签页注册
- 在 DashboardLayout.razor 的 HandleMegaMenuItemClick 中添加菜单项路由映射
- 在 DashboardLayout.razor 的标签页区域添加组件存活占位（@if + display CSS）
- 选项卡内页面组件需保持存活状态（不随切换销毁）
