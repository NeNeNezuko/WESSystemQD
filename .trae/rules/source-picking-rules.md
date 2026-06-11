# 依来源单配货 - 功能开发规则

## 功能概述

依来源单配货是出库管理下的三级菜单功能，包含两个内层选项卡：
1. **批量上架**（batch-shelving）
2. **直接拣货任务单**（direct-pick-task）

## 数据库信息

### 连接数据库
- **数据库名**: `db_gz01`（通过 `WarehouseDbContext` 访问）
- **字段说明查询库**: `wmssystem`（通过 `DICT_TAB` + `DICT_FLD` 表查询）

### 关联表结构

#### MF_XJRW — 直接拣货任务单表头（30个字段）

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| XR_NO | 直接拣货任务单号 | 主键 |
| XR_DD | 单据日期 | 日期 |
| WH | 仓库代号 | 关联 MY_WH |
| TYPE_ID | 业务类型 | 关联 cr_type_set |
| DEP | 部门代号 | 关联 DEPT |
| CLS_ID | 拣货结案标记 | Y/N |
| CLS_ID_MANUAL | 手工结案标记 | Y/N |
| XJ_FLAG | 下架标记 | Y/N |
| PRT_SW | 打印注记 | Y/N |
| PRT_DATE | 打印日期 | 日期 |
| PRT_USR | 打印人员 | 字符串 |
| USR | 制单人 | 字符串 |
| CON_NO | 货主编码 | 字符串 |
| CONTAIN_CODE | 容器条码 | 字符串 |
| CHUW | 储位代号 | 字符串 |
| AREA_NO | AGV站点代号 | 字符串 |
| LINE_CODE | 产线代号 | 字符串 |
| LINE_NO | 站台代号 | 字符串 |
| WORK_STATION | 拣选工作站台（多站台用;隔开） | 字符串 |
| PICK_POINT | 拣选点（多站点用;隔开） | 字符串 |
| RECEI_AREA | 收货区域 | 字符串 |
| PRIORITY_WCS | 优先级(1.紧急 2.特急) | 数字 |
| SEND_ACTION | 需发送任务(T.未发送 F.已发送) | T/F |
| TYPE_CK | 出库类型(1.整盘出库 2.拣选出库) | 1/2 |
| REM | 备注 | 字符串 |
| ACT_NO_OUT | 立库出库处理代号 | 字符串 |
| XR_NO_MAIN | 关联单号 | 字符串 |
| MODIFY_DD | 最近修改日期 | 日期 |
| MODIFY_MAN | 最近修改人 | 字符串 |
| SYS_DATE | 输单时间 | 日期时间 |

#### TF_XJRW — 直接拣货任务单表身（24个字段）

| 字段名 | 字段说明 | 类型说明 |
|--------|---------|---------|
| XR_NO | 直接拣货任务单号 | 外键 → MF_XJRW.XR_NO |
| ITM | 项次 | 数字 |
| PRD_NO | 货品代号 | 字符串 |
| PRD_NAME | 货品名称 | 字符串 |
| PRD_MARK | 货品特征 | 字符串 |
| QTY | 数量 | 数字 |
| QTY_PK | 拣货量 | 数字 |
| QTY1 | 数量(副) | 数字 |
| QTY1_PK | 拣货量(副) | 数字 |
| BAT_NO | 批号 | 字符串 |
| UNIT | 单位 | 字符串 |
| CHUW | 储位代号 | 字符串 |
| CAR_NO | 车号 | 字符串 |
| TZ_NO | 通知单号 | 字符串 |
| TZ_ITM | 通知单项次 | 数字 |
| TZ_ID | 通知单ID | 数字 |
| ERP_BIL_NO | ERP申请单号 | 字符串 |
| ERP_BIL_ITM | ERP申请单项次 | 数字 |
| ERP_BIL_ID | ERP申请单ID | 数字 |
| ORG_BIL_NO | 业务单号 | 字符串 |
| ORG_BIL_ITM | 业务单项次 | 数字 |
| ORG_BIL_ID | 业务单ID | 数字 |
| KEY_ITM | 转单唯一项次 | 数字 |
| WH | 仓库代号 | 字符串 |

### 表关系
- TF_XJRW.XR_NO → MF_XJRW.XR_NO（一对多，主从表结构）
- MF_XJRW.WH → MY_WH.WH_NO（仓库名称关联）
- MF_XJRW.DEP → DEPT.DEP（部门名称关联）
- MF_XJRW.TYPE_ID → cr_type_set.TYPE_ID（业务类型关联，CR_TYPE='2' 出库类型）

### 字段说明查询SQL
```sql
-- 查询MF_XJRW表头字段说明
SELECT A.TAB_NAME, A.TAB_TITLE, B.FLD_NAME, B.Note 
FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME = B.TAB_NAME 
WHERE A.TAB_NAME = 'MF_XJRW.DB';

-- 查询TF_XJRW表身字段说明
SELECT A.TAB_NAME, A.TAB_TITLE, B.FLD_NAME, B.Note 
FROM DICT_TAB A LEFT JOIN DICT_FLD B ON A.TAB_NAME = B.TAB_NAME 
WHERE A.TAB_NAME = 'TF_XJRW.DB';
```

## 页面UI规范

### 整体布局
- 参照入库通知单（InboundNotice）的左右分栏布局风格
- 内层选项卡参照出库任务分配作业（OutboundTaskAssignment）的 inner-tab-bar 模式
- 使用项目通用组件：DateRangeInput、WarehouseCodeInput、BizTypeInput、DeptCodeInput、CustomerCodeInput

### Tab1：批量上架
- **右上角按钮**：配置下载、导出
- **查询条件**：仓库、单据日期范围、单据号码、业务类型、部门、客户代号、业务单号、ERP申请单号、收货点、起止货品、不手唤已下架任务量(toggle)、家源单据到(checkbox: 出库通知单/调拨通知单)
- **表格列**：优先级、单据日期、单据号码、业务类型、ERP申请单号、业务单号、预出库日、客户名称、部门名称

### Tab2：直接拣货任务单
- **右上角按钮**：打印、新增
- **查询条件**：单据日期范围、单据号码、业务类型、仓库代号、申请单号、通知单号、业务单号、打印标签(下拉)
- **主表格列**（MF_XJRW）：项次、直接拣货任务单号、下架日期、部门名称、优先级、仓库名称、客器条码、结案标记、下架标记、操作
- **明细表格列**（TF_XJRW）：直接拣货任务单号、项次、货品代号、货品名称、批号、单位、数量、已拣货量、通贴单ID、通贴单号、通贴单次、EF

## 文件清单

| 文件路径 | 说明 |
|---------|------|
| WmsPlus/Pages/SourcePicking.razor | 前端主页面 |
| WmsPlus/wwwroot/css/source-picking.css | 页面样式 |
| WmsPlus/Models/SourcePickingModel.cs | 前端数据模型+查询模型 |
| WmsPlus.Api/Controllers/SourcePickingController.cs | 后端API控制器 |
