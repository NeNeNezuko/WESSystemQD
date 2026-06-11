# 页面与数据库表映射参考

> 本文件记录项目中所有 Blazor 页面与数据库表的关联关系。
> 开发新页面或修改现有页面时，先在此文件中查找对应表名，再到 `db-gz01-field-dict.md` 查询字段说明。

## 数据库归属

| 数据库名 | 用途 | 对应 DbContext |
|---------|------|---------------|
| db_gz01 | 业务数据 | WarehouseDbContext |
| wmssystem | 系统管理/字段说明 | AppDbContext |

---

## 一、基础资料（21个页面）

| # | 页面名称 | 选项卡ID | 关联表（db_gz01） | 表说明 |
|---|---------|---------|-------------------|--------|
| 1 | 货品代号设定 | product-code-setting | PRDT | 货品主档 |
| 2 | 中类代号设定 | mid-class-setting | INDX | 中类索引 |
| 3 | 储存性质设定 | storage-nature-setting | CW_XZ | 储存性质 |
| 4 | 货品属性信息设定 | product-attr-setting | PRDT_PDA_RN | 货品附属信息 |
| 5 | 货品特征码段设定 | product-mark-setting | PRD_MARK | 货品特征模版 |
| 6 | 仓库代号设定 | warehouse-code-setting | MY_WH | 仓库主档 |
| 7 | 储位明细 | storage-location-detail | CW_WH | 储位主档 |
| 8 | 依储类设定货品储位 | product-storage-by-class | PRDT_CW | 货品储位 |
| 9 | 依储存性设定货品储位 | product-storage-by-nature | PRDT_CW_XZ | 货品储存性质 |
| 10 | 依仓库启用到货确认 | warehouse-arrival-confirm | IZWH_CONFIRM | 调拨确认 |
| 11 | 波次策略 | wave-strategy | BC_RULE, BC_RULE_PROP | 波次策略, 波次策略参数 |
| 12 | 拣货策略 | pick-strategy | PK_RULE | 拣货策略 |
| 13 | 下架策略 | unshelve-strategy | XJ_RULE, XJ_RULE_PROP | 下架策略, 下架策略属性 |
| 14 | 不能参与配位的批号设置 | batch-no-picking-setting | BAT_NOT_PW | 批号货品维护 |
| 15 | 部门设定 | dept-setting | DEPT | 部门主档 |
| 16 | 单据类别设定 | doc-type-setting | BIL_SPC | 单据类别 |
| 17 | 查盘/与原因设定 | count-reason-setting | IJ_REASON_SET | 调整原因设定 |
| 18 | 即时消息通知设定 | notice-setting | NOTICE_SET | 通知设定 |
| 19 | 行业代号设定 | industry-code-setting | DEF_NS | 默认NS设定 |
| 20 | 叉车车号管理 | forklift-management | FORK_TRUCK | 叉车 |
| 21 | 导入期初库存 | import-initial-inventory | 无 | 仅UI样式，不关联表 |

---

## 二、入库管理

| # | 页面名称 | 关联表（db_gz01） | 表说明 |
|---|---------|-------------------|--------|
| 22 | 入库通知单 | MF_RKTZ, TF_RKTZ | 入库通知单表头, 入库通知单表身 |
| 23 | 入库通知单新增 | MF_RKTZ, TF_RKTZ | 入库通知单表头, 入库通知单表身 |
| 24 | 入库通知单报表 | TF_RKTZ, MF_RKTZ, MY_WH | 入库通知单表身, 表头, 仓库主档 |
| 25 | 入库通知单变更 | MF_RKTZ, TF_RKTZ | 待开发 |
| 26 | 入库单 | MF_RK, TF_RK, MY_WH | 入库单表头, 入库单表身, 仓库主档 |
| 27 | 收货单报表 | MF_SH, TF_SH, MY_WH, DEPT | 收货单表头, 收货单表身, 仓库主档, 部门主档 |

---

## 三、出库管理

| # | 页面名称 | 关联表（db_gz01） | 表说明 |
|---|---------|-------------------|--------|
| 28 | 出库通知单 | MF_CKTZ, TF_CKTZ | 出库通知单表头, 出库通知单表身 |
| 29 | 出库通知单变更 | MF_CKTZ_CHG, TF_CKTZ_CHG, DEPT | 出库通知变更单表头, 表身, 部门主档 |
| 30 | 出库通知单调度 | MF_CKTZ, TF_CKTZ | 出库通知单表头, 表身 |
| 31 | 出库退回通知单 | MF_CKTB, TF_CKTB | 出库退回通知单表头, 表身 |
| 32 | 出库单 | MF_CK, TF_CK | 出库单表头, 出库单表身 |
| 33 | 出库任务分配作业 | MF_CKTZ, TF_CKTZ, MF_BC, TF_BC, MF_JHRW, TF_JHRW | 出库通知单表头/身, 波次单表头/身, 波次拣货任务单表头/身 |
| 34 | 出库包装单 | MF_PACKAGE | 出库包装单 |

---

## 四、拣货/分拣

| # | 页面名称 | 关联表（db_gz01） | 表说明 |
|---|---------|-------------------|--------|
| 35 | 波次单 | MF_BC, TF_BC | 波次单表头, 波次单表身 |
| 36 | 拣货单 | MF_PK, TF_PK, DEPT | 拣货单表头, 表身, 部门主档 |
| 37 | 拣货退回单 | MF_JT, TF_JT | 拣货退回单表头, 表身 |
| 38 | 二次分拣单 | MF_PKFJ, TF_PKFJ, DEPT | 二次分拣单表头, 表身, 部门主档 |
| 39 | 直接拣货任务单 | MF_XJRW, TF_XJRW, DEPT, MY_WH | 直接拣货任务单表头/身, 部门, 仓库 |
| 40 | 波次拣货任务单 | MF_JHRW, TF_JHRW, MY_WH, DEPT | 波次拣货任务单表头/身, 仓库, 部门 |
| 41 | 依来源单配货 | MF_XJRW, TF_XJRW | 直接拣货任务单表头/身 |

---

## 五、储位管理

| # | 页面名称 | 关联表（db_gz01） | 表说明 |
|---|---------|-------------------|--------|
| 42 | 储位上架单 | MF_CWSJ, TF_CWSJ | 储位上架单表头, 表身 |
| 43 | 储位调拨单 | MF_CWDB, TF_CWDB | 储位调拨单表头, 表身 |
| 44 | 储位下架单 | MF_CWXJ, TF_CWXJ | 储位下架单表头, 表身 |

---

## 六、调拨/盘点

| # | 页面名称 | 关联表（db_gz01） | 表说明 |
|---|---------|-------------------|--------|
| 45 | 调拨通知单 | MF_ICTZ, TF_ICTZ | 调拨通知单表头, 表身 |
| 46 | 库存调拨单 | MF_IC, TF_IC | 库存调拨单表头, 表身 |
| 47 | 调拨变更通知 | — | 待开发 |
| 48 | 单据确认作业 | MF_PD, TF_PD, MF_YN, TF_YN, MF_KU, TF_KU | 盘点单表头/身, 盘盈单表头/身, 盘亏单表头/身 |
| 49 | 初盘任务单 | — | 待开发（预期 MF_PD/TF_PD） |
| 50 | 初盘单 | — | 待开发（预期 MF_PD/TF_PD） |
| 51 | 盘点单 | — | 待开发（预期 MF_PD/TF_PD） |
| 52 | 盘亏单 | — | 待开发（预期 MF_KU/TF_KU） |
| 53 | 盘盈单 | MF_YN, TF_YN | 盘盈单表头, 表身 |

---

## 七、品质检验

| # | 页面名称 | 关联表（db_gz01） | 表说明 |
|---|---------|-------------------|--------|
| 54 | 请检任务单 | MF_QJRW, TF_QJRW | 请检任务单表头, 表身 |
| 55 | 库存货品检验到期查询 | MF_QJRW, TF_QJRW | 请检任务单表头, 表身 |
| 56 | 检验单 | MF_TY, TF_TY | 检验单表头, 表身 |
| 57 | 验收退回单 | MF_YB, TF_YB | 验收退回单表头, 表身 |

---

## 八、打印管理（23个页面）

| # | 页面名称 | 关联表（db_gz01） | 表说明 |
|---|---------|-------------------|--------|
| 58 | 货品条码编码规则设定 | BARCODE_RULE, PSWD_PROP | 条码编码规则, 条码属性 |
| 59 | 箱条码编码规则设定 | BARCODE_RULE, PSWD_PROP | 条码编码规则, 条码属性 |
| 60 | 序列号编码规则设定 | BARCODE_RULE, PSWD_PROP | 条码编码规则, 条码属性 |
| 61 | 箱序列号编码规则设定 | BARCODE_RULE, PSWD_PROP | 条码编码规则, 条码属性 |
| 62 | 物流容器编码规则设定 | BARCODE_RULE, PSWD_PROP | 条码编码规则, 条码属性 |
| 63 | 批号编码规则设定 | BARCODE_RULE, PSWD_PROP | 条码编码规则, 条码属性 |
| 64 | 包装码编码规则设定 | BARCODE_RULE, PSWD_PROP | 条码编码规则, 条码属性 |
| 65 | 入库条码拆码规则设定 | MF_REMVOE_RULE, TF_REMVOE_RULE | 拆码规则表头, 拆码规则表身 |
| 66 | 依供应商设置拆码规则 | CUS_REMVOE_RULE | 供应商拆码规则 |
| 67 | 打印网点列表 | PRINT_SET | 打印网点设置 |
| 68 | 打印网点资料查询 | PRINT_SER_TASK | 打印服务任务 |
| 69 | 依货品设置条码打印套版 | PRDT_BAR_RPT | 货品条码打印套版 |
| 70 | 货品条码标签打印/查询 | PRDT_BARCODE | 货品条码 |
| 71 | 依来源单打印货品条码 | PRDT_BARCODE | 货品条码 |
| 72 | 箱条码标签打印/查询 | PRDT_BARCODE_BOX | 箱条码 |
| 73 | 依来源单打印箱条码 | PRDT_BARCODE_BOX | 箱条码 |
| 74 | 箱条码变动历史表 | BAR_BOX_CHANGE | 箱条码变动历史 |
| 75 | 序列号标签打印/查询 | BAR_REC | 序列号记录 |
| 76 | 依来源单打印序列号 | BAR_REC | 序列号记录 |
| 77 | 物流容器条码标签打印 | MF_CONTAIN | 物流容器 |
| 78 | 物流容器类型设定 | CONTAIN_SET | 容器类型设置 |
| 79 | 物流容器变动历史查询 | MF_CONTAIN_HIS | 物流容器变动历史 |

---

## 九、系统/其他

| # | 页面名称 | 关联表 | 数据库 |
|---|---------|--------|--------|
| 80 | 系统设定 | SPC_COMP, DRP_PROP | db_gz01 |
| 81 | 环境设定 | pswd | wmssystem |
| 82 | 系统注册 | 无（硬编码数据） | — |
| 83 | 注册信息 | 无（硬编码数据） | — |
| 84 | 关账作业 | CON_CLOSE | db_gz01 |
| 85 | 认证/登录 | pswd | wmssystem |

---

## 十、通用查询组件（内嵌于多个页面）

| 组件名称 | 关联表（db_gz01） | 使用场景 |
|---------|-------------------|---------|
| WarehouseCodeInput | MY_WH | 仓库代号选择 |
| ProductCodeInput | PRDT | 货品代号选择 |
| BizTypeInput | cr_type_set | 业务类型选择 |
| DeptCodeInput | DEPT | 部门代号选择 |
| CustomerCodeInput | MF_CKTB | 厂商/客户选择 |

---

## 附录：WarehouseDbContext 全部 DbSet 注册表

| DbSet 属性名 | 实体类 | 映射表名 | 表说明 |
|-------------|--------|---------|--------|
| MfRktzs | MfRktz | MF_RKTZ | 入库通知单表头 |
| TfRktzs | TfRktz | TF_RKTZ | 入库通知单表身 |
| MyWhs | MyWh | MY_WH | 仓库主档 |
| RkTypeSets | RkTypeSet | cr_type_set | 入库业务类型设置 |
| MfCwsjs | MfCwsj | MF_CWSJ | 储位上架单表头 |
| TfCwsjs | TfCwsj | TF_CWSJ | 储位上架单表身 |
| MfCwdbs | MfCwdb | MF_CWDB | 储位调拨单表头 |
| TfCwdbs | TfCwdb | TF_CWDB | 储位调拨单表身 |
| MfCwxjs | MfCwxj | MF_CWXJ | 储位下架单表头 |
| TfCwxjs | TfCwxj | TF_CWXJ | 储位下架单表身 |
| MfIctzs | MfIctz | MF_ICTZ | 调拨通知单表头 |
| TfIctzs | TfIctz | TF_ICTZ | 调拨通知单表身 |
| MfIcs | MfIc | MF_IC | 库存调拨单表头 |
| TfIcs | TfIc | TF_IC | 库存调拨单表身 |
| MfXjrws | MfXjrw | MF_XJRW | 直接拣货任务单表头 |
| TfXjrws | TfXjrw | TF_XJRW | 直接拣货任务单表身 |
| MfPds | MfPd | MF_PD | 盘点单据表头 |
| TfPds | TfPd | TF_PD | 盘点单据表身 |
| MfYns | MfYn | MF_YN | 盘盈单表头 |
| TfYns | TfYn | TF_YN | 盘盈单表身 |
| MfKus | MfKu | MF_KU | 盘亏单表头 |
| TfKus | TfKu | TF_KU | 盘亏单表身 |
| MfQjrws | MfQjrw | MF_QJRW | 请检任务单表头 |
| TfQjrws | TfQjrw | TF_QJRW | 请检任务单表身 |
| MfTys | MfTy | MF_TY | 检验单表头 |
| TfTys | TfTy | TF_TY | 检验单表身 |
| PrintSets | PrintSet | PRINT_SET | 打印网点设置 |
| PrintSerTasks | PrintSerTask | PRINT_SER_TASK | 打印服务任务 |
| PrdtPdaRns | PrdtPdaRn | PRDT_PDA_RN | 货品附属信息 |
| MfPackages | MfPackage | MF_PACKAGE | 出库包装单 |
| MfContains | MfContain | MF_CONTAIN | 物流容器 |
| ContainSets | ContainSet | CONTAIN_SET | 容器类型设置 |
| MfContainHis | MfContainHis | MF_CONTAIN_HIS | 物流容器变动历史 |
| MfBcs | MfBc | MF_BC | 波次单表头 |
| TfBcs | TfBc | TF_BC | 波次单表身 |
| MfPks | MfPk | MF_PK | 拣货单表头 |
| TfPks | TfPk | TF_PK | 拣货单表身 |
| MfJts | MfJt | MF_JT | 拣货退回单表头 |
| TfJts | TfJt | TF_JT | 拣货退回单表身 |
| MfCktbs | MfCktb | MF_CKTB | 出库退回通知单表头 |
| TfCktbs | TfCktb | TF_CKTB | 出库退回通知单表身 |
| MfCktzs | MfCktz | MF_CKTZ | 出库通知单表头 |
| TfCktzs | TfCktz | TF_CKTZ | 出库通知单表身 |
| MfCks | MfCk | MF_CK | 出库单表头 |
| TfCks | TfCk | TF_CK | 出库单表身 |
| MfJhrws | MfJhrw | MF_JHRW | 波次拣货任务单表头 |
| TfJhrws | TfJhrw | TF_JHRW | 波次拣货任务单表身 |
| MfPkfjs | MfPkfj | MF_PKFJ | 二次分拣单表头 |
| TfPkfjs | TfPkfj | TF_PKFJ | 二次分拣单表身 |
| TfRepts | TfRept | TF_REPT | 拣货报表 |
| MfYbs | MfYb | MF_YB | 验收退回单表头 |
| TfYbs | TfYb | TF_YB | 验收退回单表身 |
| PrdtBarcodes | PrdtBarcode | PRDT_BARCODE | 货品条码 |
| PrdtBarcodeBoxes | PrdtBarcodeBox | PRDT_BARCODE_BOX | 箱条码 |
| BarBoxChanges | BarBoxChange | BAR_BOX_CHANGE | 箱条码变动历史 |
| BarRecs | BarRec | BAR_REC | 序列号记录 |
| BarcodeRules | BarcodeRule | BARCODE_RULE | 条码编码规则 |
| PswdProps | PswdProp | PSWD_PROP | 条码属性 |
| MfRemoveRules | MfRemoveRule | MF_REMVOE_RULE | 拆码规则表头 |
| TfRemoveRules | TfRemoveRule | TF_REMVOE_RULE | 拆码规则表身 |
| CusRemoveRules | CusRemoveRule | CUS_REMVOE_RULE | 供应商拆码规则 |
| PrdtBarRpts | PrdtBarRpt | PRDT_BAR_RPT | 货品条码打印套版 |
| TfCktzChgs | TfCktzChg | TF_CKTZ_CHG | 出库通知变更单表身 |
| MfCktzChgs | MfCktzChg | MF_CKTZ_CHG | 出库通知变更单表头 |
| Depts | Dept | DEPT | 部门主档 |
| PkRules | PkRule | PK_RULE | 拣货策略 |
| NoticeSets | NoticeSet | NOTICE_SET | 通知设定 |
| PrdMarks | PrdMark | PRD_MARK | 货品特征模版 |
| Prdts | Prdt | PRDT | 货品主档 |
| CwXzs | CwXz | CW_XZ | 储存性质 |
| CwWhs | CwWh | CW_WH | 储位主档 |
| Indxes | Indx | INDX | 中类索引 |
| PrdtCwXzs | PrdtCwXz | PRDT_CW_XZ | 货品储存性质 |
| IzwhConfirms | IzwhConfirm | IZWH_CONFIRM | 调拨确认 |
| PrdtCws | PrdtCw | PRDT_CW | 货品储位 |
| XjRules | XjRule | XJ_RULE | 下架策略 |
| XjRuleProps | XjRuleProp | XJ_RULE_PROP | 下架策略属性 |
| BcRules | BcRule | BC_RULE | 波次策略 |
| BcRuleProps | BcRuleProp | BC_RULE_PROP | 波次策略属性 |
| BatNotPws | BatNotPw | BAT_NOT_PW | 批号货品维护 |
| BilSpcs | BilSpc | BIL_SPC | 单据类别 |
| DefNss | DefNs | DEF_NS | 默认NS设定 |
| ForkTrucks | ForkTruck | FORK_TRUCK | 叉车 |
| IjReasonSets | IjReasonSet | IJ_REASON_SET | 调整原因设定 |
| SpcComps | SpcComp | SPC_COMP | 系统参数/公司设定 |
| DrpProps | DrpProp | DRP_PROP | 属性/下拉选项参数 |
| ExceedCtrls | ExceedCtrl | EXCEED_CTRL | 超交管制设定 |
| MfShs | MfSh | MF_SH | 收货单表头 |
| TfShs | TfSh | TF_SH | 收货单表身 |
| MfRks | MfRk | MF_RK | 入库单表头 |
| TfRks | TfRk | TF_RK | 入库单表身 |
| ConCloses | ConClose | CON_CLOSE | 关账作业 |

### AppDbContext (wmssystem) DbSet 注册表

| DbSet 属性名 | 实体类 | 映射表名 | 表说明 |
|-------------|--------|---------|--------|
| Users | User | pswd | 用户/账户表 |
| DictTabs | DictTab | DICT_TAB | 字段说明-表名 |
| DictFlds | DictFld | DICT_FLD | 字段说明-字段名 |
