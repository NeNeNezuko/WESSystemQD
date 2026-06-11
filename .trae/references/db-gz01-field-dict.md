# db_gz01 数据库字段说明参考

> 数据来源：wmssystem 数据库 DICT_TAB + DICT_FLD 表
> 自动生成，请勿手动编辑
> 共 389 张表

## 使用方式

开发报表界面或查询表单时，根据表名查找对应的中文字段说明和数据库字段名映射。
TAB_NAME 格式为 表名.DB，去掉 .DB 后缀即为 db_gz01 中的实际表名。

## ADR_AREA (国家省市区表)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_ID | 区域代号 |
| AREA_UP | 上层区域 |
| CT_ID | 电话区号 |
| NAME | 区域名称 |
| TYPE_ID | 区域类型(1、国家 2、省 3、市 4、区) |
| ZIP | 邮编 |

## AGV_MAP (AGV地图表)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_LIFT | 电梯附近站点 |
| HAS_CHARG | 有充电桩 |
| MAP_NO | 地图编号 |
| NAME | 地图名称 |
| ROBOT_CODE | 指定AGV编号 |
| STOP_ID | 停用标记 |
| SUP_NO | RCS供应商 |

## AGV_STATUS (AGV状态表)

| 字段名 | 中文说明 |
|--------|---------|
| REM | 状态描述 |
| STATUS_ID | 状态代号 |
| SUP_NO | RCS供应商 |

## API_ACTION_HIS_I (接口传入处理日志表)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_ID | 处理动作(Add：新增 Push：推送 Redo：重推 Delete：删除) |
| ACT_NO | 处理代号 |
| ACT_NO_MAIN | 主任务代号 |
| ACT_NO_NEW | 重新处理代号 |
| BIL_ID | 产生单据别 |
| BIL_NO | 产生单据号码 |
| BIL_TYPE | 生成单据类别 |
| CHUW | 储位代号 |
| CHUW2 | 储位代号2 |
| CLIENT_IP | 请求IP地址 |
| COMPNO | 帐套代号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| END_DATE | 推送结束时间 |
| ERR_CODE | 错误代码 |
| ERR_MSG | 错误说明 |
| HIS_NO | 日志代号 |
| HTTP_METHOD | 请求方式 |
| JSON_CONTENT | 请求Json |
| METHOD_NO | 接口名称 |
| OTH_BIL_ID | 外部系统单据ID |
| OTH_BIL_NO | 外部系统单号 |
| OTH_TASK_ID | 第三方任务ID |
| PATH | 请求路径 |
| REF_ID | 第三方系统标识 |
| REQUEST_SIZE | 请求内容大小 |
| RESULT_CONTENT | 回传内容 |
| RESULT_SIZE | 回传内容大小 |
| START_DATE | 推送开始时间 |
| STATUS_ID | 成功否 |
| SUP_NO | 接口厂商代号 |
| SYNC_ID | 同步否 |
| WH | 仓库编码 |
| WH2 | 仓库编码2 |

## API_ACTION_HIS_O (接口推送处理日志表)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_ID | 处理动作(Add：新增 Push：推送 Redo：重推 Delete：删除) |
| ACT_NO | 处理代号 |
| ACT_NO_NEW | 重新处理代号 |
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单据号码 |
| COMPNO | 帐套代号 |
| CON_NO | 货主编码 |
| END_DATE | 推送结束时间 |
| ERR_CODE | 错误代码 |
| ERR_HTTP | Http错误说明 |
| ERR_MSG | 错误说明 |
| HIS_NO | 日志代号 |
| HTTP_CODE | Http错误代码 |
| HTTP_METHOD | 请求方式 |
| JSON_CONTENT | 请求Json |
| METHOD_NO | 接口名称 |
| OTH_BIL_ID | 生成外部系统单据ID |
| OTH_BIL_NO | 生成外部系统单号 |
| PATH | 请求路径 |
| PUSH_CONTENT | 推送内容 |
| PUSH_SIZE | 推送内容大小 |
| REF_ID | 第三方系统标识 |
| RESULT_CONTENT | 回传内容 |
| RESULT_SIZE | 回传内容大小 |
| START_DATE | 推送开始时间 |
| STATUS_ID | 成功否 |
| SUP_NO | 接口厂商代号 |
| SYNC_ID | 同步否 |

## API_ACTION_I (接口传入处理表)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO | 处理代号 |
| ACT_NO_MAIN | 主任务代号 |
| ACT_NO_NEW | 重新处理代号 |
| BIL_ID | 产生单据别 |
| BIL_NO | 产生单据号码 |
| BIL_TYPE | 生成单据类别 |
| CHUW | 储位代号 |
| CHUW2 | 储位代号2 |
| CLIENT_IP | 请求IP地址 |
| COMPNO | 帐套代号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| DEL_ID | 删除否 |
| END_DATE | 推送结束时间 |
| ERR_CODE | 错误代码 |
| ERR_MSG | 错误说明 |
| HTTP_METHOD | 请求方式 |
| JSON_CONTENT | 请求Json |
| METHOD_NO | 接口名称 |
| OTH_BIL_ID | 外部系统单据ID |
| OTH_BIL_NO | 外部系统单号 |
| OTH_TASK_ID | 第三方任务ID |
| PATH | 请求路径 |
| PUSH_ID | 推送否 |
| REF_ID | 第三方系统标识 |
| REQUEST_SIZE | 请求内容大小 |
| RESULT_CONTENT | 回传内容 |
| RESULT_SIZE | 回传内容大小 |
| START_DATE | 推送开始时间 |
| STATUS_ID | 成功否 |
| SUP_NO | 接口厂商代号 |
| SYNC_ID | 同步否 |
| SYS_DATE | 插入时间 |
| WH | 仓库编码 |
| WH2 | 仓库编码2 |

## API_ACTION_O (接口推送处理表)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO | 处理代号 |
| ACT_NO_NEW | 重新处理代号 |
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单据号码 |
| BIL_TYPE | 单据类别 |
| COMPNO | 帐套代号 |
| CON_NO | 货主编码 |
| DEL_ID | 删除否 |
| END_DATE | 推送结束时间 |
| ERR_CODE | 错误代码 |
| ERR_HTTP | Http错误说明 |
| ERR_MSG | 错误说明 |
| HTTP_CODE | Http错误代码 |
| HTTP_METHOD | 请求方式 |
| JSON_CONTENT | 请求Json |
| METHOD_NO | 接口名称 |
| OTH_BIL_ID | 生成外部系统单据ID |
| OTH_BIL_NO | 生成外部系统单号 |
| PATH | 请求路径 |
| PUSH_CONTENT | 推送内容 |
| PUSH_ID | 推送否 |
| PUSH_SIZE | 推送内容大小 |
| REF_ID | 第三方系统标识 |
| RESULT_CONTENT | 回传内容 |
| RESULT_SIZE | 回传内容大小 |
| RUN_COUNT | 执行次数 |
| START_DATE | 推送开始时间 |
| STATUS_ID | 成功否 |
| SUP_NO | 接口厂商代号 |
| SYNC_ID | 同步否 |
| SYS_DATE | 插入时间 |

## BAR_BOX_CHANGE (箱条码变更记录表)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单号 |
| BOX_NO | 箱条码 |
| BOX_NO1_CUR | 现外箱条码 |
| BOX_NO1_ORIG | 原外箱条码 |
| BOX1_SJ_FLAG | 外箱条码散出标记 |
| CHANGE_DD | 变更时间 |
| CHUW_CUR | 现储位 |
| CHUW_ORIG | 原储位 |
| CON_NO | 货主编码 |
| CONTAIN_CODE_CUR | 现容器条码 |
| CONTAIN_CODE_ORIG | 原容器条码 |
| ICCK_FLAG_CUR | 现调拨出库标记 |
| ICCK_FLAG_ORIG | 原调拨出库标记 |
| ITM | 项次 |
| JH_FLAG_ORIG | 原拣货标记 |
| JH_FLAG_PRE | 现拣货标记 |
| LST_PDD | 最近盘点时间 |
| PACKAGE_NO_CUR | 现包装码 |
| PACKAGE_NO_ORIG | 原包装码 |
| PH_FLAG_CUR | 现在途标记 |
| PH_FLAG_ORIG | 原在途标记 |
| QTY_CUR | 现数量 |
| QTY_ORIG | 原数量 |
| QTY1_CUR | 现数量(副) |
| QTY1_ORIG | 原数量(副) |
| REASON | 变更原因 |
| STATUS_JY_CUR | 现检验标记 |
| STATUS_JY_ORIG | 原检验标记 |
| STOP_ID_CUR | 现停用标记 |
| STOP_ID_ORIG | 原停用标记 |
| TI_FLAG_ORG | 原送检标记 |
| TI_FLAG_PRE | 现送检标记 |
| TRANS_BIL_ID_ORG | 原入库状态 |
| TRANS_BIL_ID_PRE | 现入库状态 |
| TRANS_BIL_OUT_ORG | 原出库状态 |
| TRANS_BIL_OUT_PRE | 现出库状态 |
| UNIQUE_CHANGE_ID | 唯一性ID |
| WAIT_CHK_FLAG_CUR | 现待校验标记 |
| WAIT_CHK_FLAG_ORIG | 原待校验标记 |
| WH_CUR | 现仓库 |
| WH_ORIG | 原仓库 |

## BAR_BOX (箱序列号记录)

| 字段名 | 中文说明 |
|--------|---------|
| BOX_DD | 装箱日期 |
| BOX_NO | 箱条形码 |
| BOX_NO1 | 外箱码 |
| CHUW | 储位代号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| CONTENT | 所属内容 |
| DEP | 部门代号 |
| ICCK_FLAG | 调拨出库标记 |
| JH_FLAG | 拣货标记 |
| LST_PDD | 最近盘点时间 |
| PACKAGE_NO | 包装码 |
| PD_DD | 盘点日期 |
| PH_FLAG | 在途标记 |
| PRD_NO | 货品代号（同箱中同货品才设定） |
| QTY | 货品数量 |
| QTY_WEIGHT | 重量 |
| STATUS_JY | 检验标记 |
| STOP_ID | 停用注记 |
| TI_FLAG | 送检标记 |
| UNIT_WEIGHT | 重量单位 |
| USR | 装箱操作员 |
| VALID_DD | 有效日期 |
| WH | 仓库代号 |

## BAR_BOX1_CHANGE (外箱码变更历史表)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单号 |
| BOX_NO1 | 外箱条码 |
| CHANGE_DD | 变更时间 |
| CHANGE_ID | 唯一ID |
| CHUW_CUR | 现储位 |
| CHUW_ORIG | 原储位 |
| CON_NO | 货主编码 |
| CONTAIN_CODE_CUR | 现容器条码 |
| CONTAIN_CODE_ORIG | 原容器条码 |
| ICCK_FLAG_CUR | 现调拨出库标记 |
| ICCK_FLAG_ORIG | 原调拨出库标记 |
| JH_FLAG_CUR | 现拣单标记 |
| JH_FLAG_ORIG | 原拣单标记 |
| PACKAGE_NO_CUR | 现包装码 |
| PACKAGE_NO_ORIG | 原包装码 |
| REM | 变更原因 |
| STATUS_JY_CUR | 现检验标记 |
| STATUS_JY_ORIG | 原检验标记 |
| STOP_ID_CUR | 现停用标记 |
| STOP_ID_ORIG | 原停用标记 |
| TI_FLAG_CUR | 现送检标记 |
| TI_FLAG_ORIG | 原送检标记 |
| UNIQUE_CHANGE_ID | 唯一性ID |
| WH_CUR | 现仓库 |
| WH_ORIG | 原仓库 |

## BAR_BOX1 (外箱序列号记录)

| 字段名 | 中文说明 |
|--------|---------|
| BOX_DD | 装箱日期 |
| BOX_NO1 | 箱码 |
| CHUW | 储位代号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| CONTENT | 箱内容 |
| DEP | 部门 |
| ICCK_FLAG | 调拨出库标记 |
| JH_FLAG | 拣货标记 |
| LST_PDD | 最近盘点时间 |
| PACKAGE_NO | 包装码 |
| PD_DD | 盘点日期 |
| PH_FLAG | 在途标记 |
| PRD_NO |  |
| QTY | 数量 |
| QTY_WEIGHT | 重量 |
| STATUS_JY | 检验标记 |
| STOP_ID |  |
| TI_FLAG | 送检标记 |
| UNIT_WEIGHT | 重量单位 |
| USR | 装箱操作员 |
| VALID_DD |  |
| WH |  |

## BAR_CHANGE (条码变动记录档)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_NO1 | 原销售区域代号 |
| AREA_NO2 | 新销售区域代号 |
| BAR_NO | 条码号 |
| BAT_NO1 | 原始批号 |
| BAT_NO2 | 当前批号 |
| BIL_ID | 单据类别 |
| BIL_NO | 来源单号 |
| CHUW1 | 原始储位 |
| CHUW2 | 当前储位 |
| CITY_ID1 | 旧市 |
| CITY_ID2 | 新市 |
| CON_NO | 货主编码 |
| CONTAIN_CODE1 | 原容器条码 |
| CONTAIN_CODE2 | 现容器条码 |
| COT_ID1 | 旧国家 |
| COT_ID2 | 新国家 |
| COUND_ID1 | 原始销售区域 |
| COUND_ID2 | 当前销售区域 |
| CUS_NO1 | 旧客户代号 |
| CUS_NO2 | 新客户代号 |
| ICCK_FLAG1 | 原调拨出库标记 |
| ICCK_FLAG2 | 现调拨出库标记 |
| JH_FLAG1 | 原拣货标记 |
| JH_FLAG2 | 现拣货标记 |
| LST_PDD | 最近盘点时间 |
| ON_PRC_FLAG1 | 在制标志 |
| ON_PRC_FLAG2 | 在制标志 |
| PACKAGE_NO1 | 原包装码 |
| PACKAGE_NO2 | 现包装码 |
| PH_FLAG1 | 在途标记1 |
| PH_FLAG2 | 在途标记2 |
| PRD_MARK1 | 原货品特征 |
| PRD_MARK2 | 新货品特征 |
| PRD_NO1 | 原货品代号 |
| PRD_NO2 | 新货品代号 |
| PROV_ID1 | 旧省 |
| PROV_ID2 | 新省 |
| SHOP_NAME1 | 旧店名 |
| SHOP_NAME2 | 新店名 |
| STATUS_JY1 | 原检验标记 |
| STATUS_JY2 | 现检验标记 |
| STOP_ID1 | 停用标记1 |
| STOP_ID2 | 停用标记2 |
| TI_FLAG1 | 送检标志 |
| TI_FLAG2 | 送检标志 |
| UNIQUE_CHANGE_ID | 唯一性ID |
| UPDDATE | 变动日期 |
| USR | 操作人员 |
| WH1 | 原始库位 |
| WH2 | 当前库位 |

## BAR_CONTRAST (外部条码与内部条码对照表)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_CODE | 内部条码 |
| BAR_TYPE | 条码类型(0.序 1.箱序 2.条 3.箱条 4.容器 5包装码) |
| CON_NO | 货主编码 |
| EXT_BAR_CODE | 外部条码 |

## BAR_PRINT_DATA (条码打印信息记录库)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_CODE | 条码 |
| CON_NO | 货主编码 |
| PRINT_DATE | 打印时间 |
| PRINT_SERIAL | 打印序号 |
| PRINT_USR | 打印人员 |
| TYPE_ID | 条码类型 |

## BAR_PSWD_PROP (条形码属性设定档)

| 字段名 | 中文说明 |
|--------|---------|
| COMPNO | 帐套代号 |
| FLD_NAME | 属性名 |
| FLD_VALUE | 属性值 |
| PGM | 程序代号 |
| REM | 参数备注 |
| ROLENO | 类别代号 |
| TYPE_ID | 类别种类 |

## BAR_REC_EXT (模具序号附属表)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_NO | 序列号 |
| BF_DD | 报废日期 |
| BIL_ID | 申请单据别 |
| BIL_NO | 申请单号 |
| CANCEL_ID | 作废标记 |
| CON_NO | 货主编码 |
| NW_FILE_NAME | 图片文件名 |
| P_STATUS | 状态 |
| P_TYPE | 类型 |
| PRD_NAME | 模具/工装名称 |
| RQ_CODE | 固定容器条码 |
| TEXTURE | 材质 |
| TYPE_ID | 分类(1.模具 2.工装) |
| USED_COUNT | 已使用次数 |

## BAR_REC (条形码记录)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_NO | 条形码 |
| BAT_NO | 批号 |
| BIL_ID | 来源单据类别 |
| BIL_ITM | 来源单项次 |
| BIL_NO | 来源单号 |
| BOX_NO | 所属箱条形码 |
| CHK_NOTPW | 不能配位标记 |
| CHUW | 储位代号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| CREATE_DD | 产生日期 |
| CUS_NAME | 客户名称 |
| CUS_NO | 客户代号 |
| ICCK_FLAG | 调拨出库标记 |
| JH_FLAG | 拣货标记 |
| LST_PDD | 最近盘点时间 |
| PACKAGE_NO | 包装码 |
| PD_DD | 盘点日期 |
| PH_FLAG | 在途标记 |
| PRD_MARK | 货品特征 |
| PRD_NO | 货品代号 |
| PRD_QRCODE | 货品二维码 |
| PT_NO | 打印编号 |
| REM | 备注 |
| SO_NO | 受订单号 |
| SPC_NO | 规格 |
| STATUS_JY | 检验标记(空或T：合格、F：不合格、W：待检) |
| STOP_ID | 停用注记 |
| SYS_DATE | 输入时间 |
| TI_FLAG | 送检标志 |
| UPDDATE | 更新时间 |
| USR | 录入人 |
| VALID_DD | 有效期 |
| WH | 仓库代号 |

## BARCODE_RPT (条码套版设置)

| 字段名 | 中文说明 |
|--------|---------|
| CONTENT | 文件流 |
| FILE_NAME | 文件名称 |
| LANG_ID | 语言别 |
| MOB_TYPE | 套版类型(WMS或PDA等) |
| MOD_ID | 模板代号 |
| PGM | 程序代号 |
| REM | 备注 |
| SYS_RPT | 是否为系统套版 |
| TB_LANG | 套版语言 |

## BARCODE_RULE (条形码产生规则设定表)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_LEN | 流水码长 |
| BOX_FLAG | 箱识别码 |
| CUS_CODE | 自设码 |
| DD_FORMAT | 日期格式 |
| QTY_POI | 数量小数位 |
| QTY1_POI | 数量(副)小数位 |
| REM | 规则说明 |
| RULE_NO | 规则代号 |
| RULE_TYPE | 条形码类型 |
| SPLIT_FIELDS | 用于生成条码的字段列表 |
| SPLIT_STR | 间隔符 |
| VCODE_LEN | 验证码长 |

## BAT_NOT_PW (不能参与配位的批号记录)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CON_NO | 货主编码 |
| GUID | 唯一ID |
| PRD_MARK | 货品特征 |
| PRD_NO | 货品代号 |
| REM | 备注 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| WH | 仓库代号 |

## BAT_NOT_PW1 (不能参与配位的批号记录-单据明细)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据别 |
| BIL_ITM | 单据项次 |
| BIL_NO | 单据号码 |
| GUID | 唯一ID |

## BAT_VALID_DD (批号有效期)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CON_NO | 货主编码 |
| PRD_MARK | 特征 |
| PRD_NO | 货品代号 |
| VALID_DD | 有效期 |

## BC_RULE_PROP (波次策略参数表)

| 字段名 | 中文说明 |
|--------|---------|
| PROP_NO | 参数名 |
| REM | 备注 |
| RULE_ID | 波次策略代号 |
| VALUE | 参数值 |

## BC_RULE (波次策略主表)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 集团公司 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| NAME | 波次策略名称 |
| RULE_ID | 波次策略代号 |
| STOP_ID | 停用标记 |
| SYS_DATE | 录入时间 |
| USR | 录入人员 |

## BIL_MAPPING (ERP单据对照表)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据别 |
| IO | 单据出入库方向 |
| SUNLIKE_ID | SUNLIKE单据别 |

## BIL_SPC (单据类别设定)

| 字段名 | 中文说明 |
|--------|---------|
| NAME | 名称 |
| REM | 备注 |
| SPC_ID | 类别(RK：入库、CK：出库、IC：调拨、PD：盘点) |
| SPC_NO | 单据类别代号 |

## BILD (被删除单据资料库)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 系统默认单据ID |
| DEL_DATE | 被删日期 |
| SQNO | 被删单据号码 |
| USR | 删除人员 |

## BILDFT_BS (单据默认值设置)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 集团分公司 |
| DFT_VALUE | 默认值 |
| FLD_NAME | 栏位名称 |
| PGM | PGM |
| TABLE_NAME | 表名 |
| USE_ID | 启用否 |
| USR | 用户代号 |

## BILL_DICT (单据字典信息表)

| 字段名 | 中文说明 |
|--------|---------|
| PGM | 程序代号 |
| REM | 说明 |
| TAB_NAMES | 涉及表名 |

## BILL_PROP_BS (WEB版单据属性设置)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据别 |
| CAPTION | 属性名称 |
| CAPTION_BIG5 | 繁体属性名称 |
| CAPTION_ENG | 英文属性名称 |
| CTL_NAME | 属性代号 |
| CTL_OPTION | 属性值选项 |
| CTL_TYPE | 属性类型 |
| CTL_VALUE | 属性值 |
| FLAG | 选择 |
| FORM_NAME | 单据别 |
| ITM | 项次 |
| MENU_NAME | 菜单名 |
| USR | 识别码 |

## BILL_PROP_COMM_BS (WEB版单据全局属性)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据别 |
| CAPTION | 属性名称 |
| CAPTION_BIG5 | 繁体属性名称 |
| CAPTION_ENG | 英文属性名称 |
| CTL_NAME | 属性代号 |
| CTL_OPTION | 属性值选项 |
| CTL_TYPE | 属性类型 |
| CTL_VALUE | 属性值 |
| DEP | 集团分公司 |
| FLAG | 是个人属性 |
| FORM_NAME | 单据别 |
| ISPRIVATE | 个人属性 |
| ITM | 项次 |
| MENU_NAME | 菜单名 |

## BILL_PROP_LOG_BS (WEB单据属性值异动表)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据别 |
| CAPTION | 属性名称 |
| CHANGE_DD | 变动日期 |
| CHANGE_ID | 修改标志 |
| CHANGE_USR | 修改者 |
| CTL_NAME | 属性代号 |
| CTL_TYPE | 属性类型 |
| FLAG | 选择 |
| FORM_NAME | 单据别 |
| ID | 序号 |
| ITM | 项次 |
| MENU_NAME | 菜单名 |
| NEW_VALUE | 新属性值 |
| OLD_VALUE | 原属性值 |
| USR | 识别码 |

## BILL_SET_KD (获取金蝶单据资料设置)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID_REQ | 申请单据别 |
| BIL_ID_TARGET | 目标单据别 |
| CON_NO | 货主编码 |
| CR_TYPE | 出入库区分(1.入库 2.出库) |
| TYPE_ID | 业务类型 |

## BILL_SET_SUN (获取SUNLIKE单据资料设置)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID_REQ | 申请单据别 |
| BIL_ID_TARGET | 目标单据别 |
| CON_NO | 货主编码 |
| CR_TYPE | 出入库区分(1.入库 2.出库) |
| TYPE_ID | 业务类型 |

## BILL_TPL_KD (金蝶单据模版)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID_REQ | 申请单据别 |
| BIL_ID_TARGET | 目标单据别 |
| BIL_NAME_REQ | 申请单名称 |
| BIL_NAME_TARGET | 目标单据名称 |
| CR_TYPE | 出入库区分(1.入库 2.出库) |
| OTH_BIL_ID | 外部系统单据ID |
| TYPE_ID | 业务类型 |

## BILL_TPL_SUN (SUNLIKE单据模版)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID_REQ | 申请单据别 |
| BIL_ID_TARGET | 目标单据别 |
| BIL_NAME_REQ | 申请单名称 |
| BIL_NAME_TARGET | 目标单据名称 |
| CR_TYPE | 出入库区分(1.入库 2.出库) |
| TYPE_ID | 业务类型 |

## BILN_DEL (单据字轨-删除库)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 来源单识别码 |
| UP_DD | 上次更新时间 |

## BILN_SEQ (单据编码序列库)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_DD | 单号日期 |
| BIL_ID | 单据ID |
| BIL_NO | 单据号码 |
| PAT | 编码原则 |

## BILN (单据字轨库)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 来源单识别码 |
| BIL_ID1 | 用户自设单据ID |
| BIL_NM | 来源单名称 |
| DEL_ID | 是否取已删除单号 |
| LST_NO | 最近编号 |
| PAT | 编码原则 |
| SEL_ACC | 帐套 |
| SEL_CX | 编码次序 |
| SEL_DD | 日期 |
| SEL_DEP | 部门 |
| SEL_ID | 单据类别 |
| SEL_NO | 流水号 |
| SEL_OTH | 其它项 |
| SEL_USR | 制单人 |
| SEQ_ID | 启用序列编码功能 |
| SEQ_POOLSIZE | 序列池基数 |
| TYPE_DEP | 部门编码方式 |
| UP_DD | 上次更新时间 |
| XG_ID | 是否可以修改单号 |

## BILN1 (单据序号库)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 系统默认单据ID |
| PAT | 编码原则 |
| SQ |  |

## BOX_CHANGE (拆箱讯息档)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_NO | 条码号 |
| BIL_ID | 单据类别 |
| BIL_NO | 来源单号 |
| BOX_NO | 箱码 |
| CON_NO | 货主编码 |
| ICCK_FLAG_CUR | 现调拨出库标记 |
| ICCK_FLAG_ORIG | 原调拨出库标记 |
| ID | 主键 |
| JH_FLAG_ORIG | 原拣货标记 |
| JH_FLAG_PRE | 现拣货标记 |
| LST_PDD | 最近盘点时间 |
| UNIQUE_CHANGE_ID | 唯一性ID |
| UPDDATE | 变动日期 |
| USR | 用户代号 |
| WH | 拆箱库位 |

## CASE_CODE_SET (专案开发设定表)

| 字段名 | 中文说明 |
|--------|---------|
| CASE_ID | 专案代号 |
| CLASS_NAME | 原类对象名称 |
| CLASS_NAME_CASE | 二次开发类对象名称 |
| METHOD_NAME | 原类方法名称 |
| METHOD_NAME_CASE | 二次开发类方法名称 |
| POSITION | 原类执行位置 |

## CK_ATTACH (出库单单据附属信息（废弃）)

| 字段名 | 中文说明 |
|--------|---------|
| ADR | 收货地址 |
| CELL_NO | 收货人电话 |
| CK_NO | 出库单号 |
| CON_MAN | 收货人 |
| COT_ID | 收货国家 |
| COUN_ID | 收货省市区 |
| CUS_NO | 快递公司 |
| DW_NAME | 单位名称 |
| FH_NO | 快递单号 |
| ID_CODE | 银行帐号 |
| INV_ID | 发票类型 |
| INV_NR | 发票内容 |
| INV_TT | 发票抬头 |
| KH_BANK | 开户银行 |
| NSR_CODE | 纳税人识别号 |
| ZC_ADR | 注册地址 |
| ZC_TEL | 注册电话 |
| ZIP | 邮编 |

## CK_BOARD (出库单板号记录表)

| 字段名 | 中文说明 |
|--------|---------|
| BO_NO | 板号 |
| CK_NO | 出库单号 |

## CKTZWH_CHANGE (出库通知单仓库变更明细表)

| 字段名 | 中文说明 |
|--------|---------|
| CHANGE_NO | 变更单号 |
| QTY_CUR | 本次变更数量 |
| QTY_ORG | 原单数量 |
| TZ_ITM_CUR | 新出库通知单项次 |
| TZ_ITM_ORG | 原出库通知单项次 |
| TZ_NO_CUR | 新出库通知单号 |
| TZ_NO_ORG | 原出库通知单号 |
| UPDATE_DD | 变更时间 |
| USR | 变更人 |
| WH_CUR | 新仓库代号 |
| WH_ORG | 原仓库代号 |

## CNDT_ACTION (采纳解绑容器处理表)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO | 处理代号 |
| CONTAIN_CODE | 容器条码 |
| MO_NO | 制令单号 |
| SYS_DATE | 生成时间 |

## COMP (公司资料档)

| 字段名 | 中文说明 |
|--------|---------|
| ACC_MD1 | 起始会计年月 |
| ACC_MD2 | 截止会计年月 |
| ACC_YY | 会计年度 |
| ACCYEARS | 会计年份 |
| CHK_BARCODE_MU | 已出库序列号允许重复使用 |
| CHK_BIL_DD | 单据日期超出当前系统日期提示否 |
| CHK_BILTYPE | 单据类别不允许为空 |
| CHK_MARK | 货品特征为数值型 |
| CHK_WH | 库位依部门取值 |
| CHKPRD_BARCODE | 货品采用序列号 |
| CHKPRD_MARK | 特征输入受特征模版控制 |
| CLS_INV | 进销存关帐日期 |
| CMP_ADR | 公司地址 |
| COMPNO | 公司代号 |
| DATABASE_NAME | 数据库名 |
| DATEFORMAT | 日期格式 |
| DEP | 部门 |
| GETSTOCKDATE | 库存计算日期选择 |
| HY_ID | 帐套别 |
| INV_ID | 盘存方式 |
| KC_LS | 写库存日志 |
| LIST_WH | 单据库位仅列示明细 |
| LOGIN_PSWD | 密码 |
| LOGIN_USER | 用户名 |
| MARK_ID | 货品采用特征 |
| MARK_NAME | 特征命名 |
| MNY_ID | 日期类型 |
| NAME | 名称 |
| NAME_ENG | 英文名称 |
| OPEN_DAT | 开帐日期 |
| POI_PRD_MARK | 特征小数位 |
| POI_QTY | 数量小数位数 |
| PRDNO1 | 货品输入长度控制 |
| PRDNO3 | 库位输入长度控制 |
| PRDUSEBAT_NO | 启用批号 |
| SERVER_NAME | 服务器名 |
| SUB_ID | 帐套序号 |
| TEL1 | 电话 |
| TZ_SPLIT | 特征分段分隔符 |
| UP_DD | 时间戳 |

## CON_CLOSE (关帐信息表)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO | 任务编号 |
| CLOSE_DD | 关帐日期 |
| CON_NO | 货主编码 |
| GUID | 唯一号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |

## CON_SET_PROP (货主参数设定表)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| PROP_NO | 参数名 |
| REM | 备注 |
| VALUE | 参数值 |

## CON_SET (货主编码设定表)

| 字段名 | 中文说明 |
|--------|---------|
| APP_ID | 应用ID(停用) |
| APP_SECRET | 应用密钥(停用) |
| COMPNO | 帐套代号(停用) |
| CON_NO | 货主编码 |
| DEP_ERP | ERP 部门代号(停用) |
| ERP_TYPE | 对接ERP系统 |
| NAME | 货主名称 |
| ORGAN_CODE | 组织编码(停用) |
| REF_ID | 第三方系统标识(停用) |
| RULE_ID | 单据编码规则识别码 |
| SNM | 货主简称 |
| STOP_ID | 停用标记 |
| TZ_MODE | 通知单产生方式(停用) |
| URL | 接口地址(停用) |
| USE_ERPAPI | 启用SUNLIKE ERPAPI(停用) |
| USER_NAME | 用户名(停用) |
| USR_ERP | ERP 制单人(停用) |
| WMS_WH | WMS仓库(停用) |

## CONTAIN_CW_ODR (容器存放储位顺序)

| 字段名 | 中文说明 |
|--------|---------|
| CHUW | 储位代号 |
| CON_NO | 货主编码 |
| CONTAIN_NO | 容器类型代号 |
| DEP | 集团分公司 |
| GL | 列 |
| GS | 排 |
| GUID | 唯一值 |
| ITM | 次序 |
| LAYER | 层 |
| WH | 仓库代号 |
| ZONE_ID | 区域代号 |

## CONTAIN_CW (容器存放储位范围)

| 字段名 | 中文说明 |
|--------|---------|
| CHUW | 储位代号 |
| CON_NO | 货主编码 |
| CONTAIN_NO | 容器类型代号 |
| DEP | 集团分公司 |
| GL | 列 |
| GS | 排 |
| GUID | 唯一值 |
| LAYER | 层 |
| WH | 仓库代号 |
| ZONE_ID | 区域代号 |

## CONTAIN_FQ_INDX (容器可存放货品表)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| CONTAIN_NO | 容器类型 |
| CWXZ_NO | 储存性质 |
| FQ_NO | 分区编号 |
| IDX_NO | 中类代号 |
| KND | 大类 |

## CONTAIN_FQ_QTY (容器装箱数量设定)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| CONTAIN_NO | 容器类型 |
| CWXZ_NO | 储存性质 |
| FQ_NO | 分区编号 |
| IDX_NO | 中类代号 |
| PRD_NO | 货品 |
| QTY | 装箱数量 |

## CONTAIN_FQ (容器分区表)

| 字段名 | 中文说明 |
|--------|---------|
| CONTAIN_NO | 容器类型代号 |
| DEP | 部门 |
| FQ_NO | 分区编号 |
| MODIFY_DD | 修改时间 |
| MODIFY_MAN | 修改人 |
| QTY_WEIGHT | 限重 |
| STOP_ID | 停用否 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |

## CONTAIN_NOTIFY (容器到达通知)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO | 任务代号 |
| CONTAIN_CODE | 容器条码 |
| GUID | 唯一ID |
| PGM | 程序代号 |
| REM | 备注 |
| SLOT_NO | 拣选位代号 |
| SYS_DATE | 制单时间 |

## CONTAIN_SET (物流容器类型设定表)

| 字段名 | 中文说明 |
|--------|---------|
| CATEGORY_RCS | RCS载具种类 |
| CHK_FQ | 分区否 |
| CODE_PREFIX | 编码前缀 |
| CONTAIN_NAME | 类型名称 |
| CONTAIN_NO | 类型代号 |
| ENABLE_FULL_STATUS | 启用容器满箱状态管理(T/F) |
| ENABLE_LH_FLAG | 启用理货标记（T/F） |
| FULL_RATE_VAL | 满箱比例值 |
| IS_SECTIONS | 优先散出(散件容器) |
| IS_STANDARD | 是系统标准档 |
| PRD_CTRL_ID | 货品管制方式(1-7) |
| QTY_TARE | 皮重 |
| QTY_WC | 允许误差范围 |
| QTY_WEIGHT | 限重 |
| SHOW_FULL_RATE | 显示满箱率%（T/F） |
| STOP_ID | 停用否 |
| TYPE_RCS | RCS容器类型 |
| UNIT_WEIGHT | 重量单位 |
| ZX_LM | 装箱内容限制(1-7) |

## CR_TYPE_RPT (业务类型预设套版表)

| 字段名 | 中文说明 |
|--------|---------|
| MOD_ID | 预设套版代号 |
| PAGE_KIND | 纸张大小 |
| PAGE_KIND_CUSTOM | 纸张自定义大小 |
| PGM | 程序代号 |
| PROP_ID | 属性ID |
| ROLENO | 类别代号 |
| TYPE_ID | 业务类型 |

## CR_TYPE_SET (WMS出入库类型)

| 字段名 | 中文说明 |
|--------|---------|
| CR_TYPE | 出入库区分 |
| ERP_BIL_ID | ERP入库单据别 |
| ERP_BIL_PGM | ERP入库单程序代号 |
| ERP_NO_SRC | ERP允许无来源新增 |
| KD_FORM_ID | 金蝶FORMID |
| KD_NO_SRC | 金蝶允许无来源新增 |
| NAME | 业务类型名称 |
| T_BIL_ID | T8单据别 |
| T_NO_SRC | T8允许无来源新增 |
| TYPE_ID | 业务类型 |
| U8_NO_SRC | U8-ERP允许无来源新增 |

## CRKTYPE_SUN (SUNLIKE出入库单据类型设置表)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据ID |
| CON_NO | 货主编码 |
| CRKFLAG | 出入库区分 |
| CRKTYPE | 类型代号 |
| FHFLAG | 启用发货通知 |
| OS_ID | 来源ID |
| SQFLAG | 严格按申请单号出入库 |
| YN | 启用 |

## CUS_REMVOE_RULE (依供应商设置拆码规则)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| CUS_NO | 供应商代号 |
| RULE_NO | 拆码规则代号 |

## CUST_DEL (客户删除表)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| CUS_NO | 客户代号 |
| UP_DD | 时间戳 |

## CUST (客户资料档)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| CUS_NAME | 客户名称 |
| CUS_NO | 客户代号 |
| CUS_SNM | 客户简称 |
| END_DD | 截止往来日期 |
| OBJ_ID | 类别 |
| STR_DD | 起始往来日期 |
| SYS_DATE | 创建时间 |
| TP_ID | 第三方标识 |
| UP_DD | 时间戳 |
| USR_WMS | WMS登录用户代号 |

## CW_CAPACITY (储位容量设定)

| 字段名 | 中文说明 |
|--------|---------|
| CONTAIN_TYPE | 容器类型 |
| QTY | 可存放容器个数 |
| WH | 仓库代号 |

## CW_WH_DEL (储位-删除库)

| 字段名 | 中文说明 |
|--------|---------|
| CHUW | 库位 |
| UP_DD | 上次修改时间 |

## CW_WH (仓库储位表)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_ID | 区域代号 |
| AREA_NO | 站点 |
| BEARING | 储位承重(Kg) |
| CHUW | 储位 |
| CW_STATUS | 储位状态(1、正常  2、故障) |
| CW_UNMATCH | 不可配位标记(T/F) |
| CWWZ | 储位位置（作废） |
| GL | 列 |
| GS | 排 |
| GS_PAT | 货架编码 |
| HEIGHT | 储位高度(mm) |
| HW_NO | 设备代号 |
| LAYER | 层 |
| LAYER_PROP | 层属性(0、空 1、低层 2、中层 3、高层) |
| LKHJ_TYPE | 储位位置(1.单深位 2.双深位外侧(靠近) 3.双深位里侧(远离) 4.三深位外侧 5.三深位中间 6.三深位里侧) |
| LOCK_CW | 锁定状态(1、正常 2、锁定 3、异常) |
| NAME | 储位名称 |
| PASSAGE | 通道 |
| PD_DD | 盘点日期 |
| PT_NO | 打印编号 |
| PTL_LABEL | PTL电子标签ID |
| PTL_STATUS | PTL亮灯状态 |
| REM | 备注 |
| TP_ID | 第三方标记 |
| TUNNEL_LABEL | 巷道灯电子标签ID |
| TUNNEL_NO | 巷道号 |
| UP_DD | 更新时间 |
| WH | 仓库 |

## CW_XZ_DEL (货品储存性质删除表)

| 字段名 | 中文说明 |
|--------|---------|
| CWXZ_NO | 储存性质代号 |
| UP_DD | 时间戳 |

## CW_XZ (货品储存性质表)

| 字段名 | 中文说明 |
|--------|---------|
| CWXZ_NO | 储存性质代号 |
| NAME | 性质说明 |
| UP_DD | 时间戳 |

## DB_PSWD (PSWD DB 库)

| 字段名 | 中文说明 |
|--------|---------|
| B_DAT | 启用日期 |
| COMP_BOSS | 管理员否 |
| COMPNO | 公司代号 |
| CREATOR |  |
| DEP | 部门代号 |
| DEP_UP | 是集团分公司部门 |
| DEPRO_NO | 部门群组 |
| E_DAT | 结束日期 |
| E_MAIL | 邮件代号 |
| ISCUST | 是否客户 |
| ISGROUP | 是否集团公司 |
| MNG | 上层主管 |
| NAME | 名称 |
| SYS_DATE |  |
| TP_ID | 第三方标记 |
| USR | 用户代号 |

## DEF_NS_COMP (自定义NS帐套)

| 字段名 | 中文说明 |
|--------|---------|
| COMPNO | 帐套代号 |
| NS_NO | NS代号 |

## DEF_NS (自定义名称空间)

| 字段名 | 中文说明 |
|--------|---------|
| INC_SYS | 包含系统字段否 |
| INC_UNI | 基础资料启用UNICODE |
| NAME | 名称 |
| NS_NO | NS代号 |
| REM | 备注 |

## DEPRO_SET (部门群组设置表)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 部门代号 |
| DEPRO_NO | 部门群组代号 |
| ITM |  |
| REM | 说明 |

## DEPT_CHK (部门上下属)

| 字段名 | 中文说明 |
|--------|---------|
| DEP_DW |  |
| DEP_UP | 部门上级 |
| DRC |  |

## DEPT_DEL (部门-删除库)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 部门代号 |
| UP_DD | 上次更新时间 |

## DEPT_RO (部门群组)

| 字段名 | 中文说明 |
|--------|---------|
| DEP_LS |  |
| DEPRO_NO | 群组代号 |
| NAME | 群组名称 |
| REM | 备注 |

## DEPT (部门代号库)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 部门代号 |
| ENG_NAME | 英文名称 |
| GROUP_ID | 是集团分公司 |
| MAKE_ID | 部门性质 |
| NAME | 名称 |
| NAME_PY | 助记码 |
| STOP_DD | 停用日期 |
| SYS_DATE | 输入日期 |
| TP_ID | 第三方标记 |
| UP | 上层部门 |
| UP_DD | 上次更新时间 |
| USR | 录入员 |

## DICT_SQL_DBLOG (DICT_SQL刷新记录(帐套))

| 字段名 | 中文说明 |
|--------|---------|
| RUN_SW | 是否已经运行 |
| SQL_NAME | 关联DICT_SQL.NAME |

## DICT_SQL_LOG (Dict Sql记录表)

| 字段名 | 中文说明 |
|--------|---------|
| COMPNO | 账套代号 |
| RUN_SW | 运行否 |
| SQL_NAME | SQL名称 |

## DRP_PROP (DRP参数设定档)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 集团公司 |
| ITEM | 项次 |
| REM | 参数说明 |
| UP_DD | 时间戳 |
| VALUE | 值 |

## DRP_RPT_DS (套版设定表)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据别 |
| BIL_SQL | 数据SQL |
| CHK_SUNSYS | SUNSYS否 |
| DS_TYPE | 数据类型 |
| PGM | 程序代号 |
| REM | 备注 |
| REM_TW | 繁体备注 |
| REM_US | 英文备注 |
| REM_VN | 越南文备注 |
| RES_ID | 资源代号 |

## DRP_RPT_DS1 (套版数据源设定表)

| 字段名 | 中文说明 |
|--------|---------|
| FLD_INDEX | 项次 |
| FLD_NO | 字段名 |
| PGM | 程序代号 |
| REM | 描述 |
| REM_CN | 描述CN |
| REM_ENG | 描述ENG |
| REM_TW | 描述TW |

## DRP_RPT (DRP 套表档)

| 字段名 | 中文说明 |
|--------|---------|
| CASE_ID | 专案代号 |
| CHK_CODE | 机器码 |
| CHK_TPLDOWN | 是否template云平台下载 |
| CONTENT | 自定义报表内容 |
| FL_RPT | 离线套版否 |
| INFO_ID | 智能分析报表ID |
| LANG_ID | 语言别 |
| MOD_ID | 模组代号 |
| OBJECTURI | 类名称空间 |
| PGM | 程序代号 |
| PRINT_CODE | 机器码内容 |
| REM_CN | 简体说明 |
| REM_ENG | 英文说明 |
| REM_TW | 繁体说明 |
| SYS_RPT | 系统套版否 |
| TYPE_ID | 1.旧的套版 2.PageReport |

## EMULATE_SET (仿真布局-设备设定表)

| 字段名 | 中文说明 |
|--------|---------|
| DEVICE_ID | 设备代号(自动化配置代号/组合代号/设备代号) |
| EMULATE_ID | 设备ID |
| MODIFY_DD | 最近修改时间 |
| STATUS_ID | 当前状态(1.正常 2.异常 3.故障) |
| TYPE_ID | 设备类型(1.自动化-调用 2.自动化-推送 3.设备组合 4.系统设备) |

## EMULATE_YC (设备状态异常表)

| 字段名 | 中文说明 |
|--------|---------|
| EMULATE_ID | 设备ID |
| YC_NO | 异常任务代号 |

## EXCEED_CTRL (超交管制设置)

| 字段名 | 中文说明 |
|--------|---------|
| CR_TYPE | 出入库区分(1.入库、2.出库) |
| GROUP_DEP | 集团公司 |
| PARAMS_ID | 参数ID |
| PARAMS_VALUE | 参数值 |
| TYPE_ID | 业务类型 |
| YW_CODE | xh 历史问题,请2020.11.01月后删掉该字段 |

## FACE_DEF_BS (单据界面设计)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 部门代号 |
| FLD_COND | 必填条件 |
| FLD_INDEX | 栏位顺序 |
| FLD_NAME | 栏位名称 |
| FLD_NOTNULL | 是否必填项 |
| FLD_WIDTH | 栏位长度 |
| PGM | 程序代号 |
| REM_BIG5 | 别名繁体 |
| REM_ENG | 别名英文 |
| REM_GB | 别名简体 |
| SHOW_ID | 显示否 |
| STATS_ID | 是否统计栏位 |
| STOP_ID | 停用否 |
| TAB_ID | 页签ID |
| TABLE_SIGN | 表头表身 |

## FACE_DISP_BS (单据显示设定)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 集团分公司 |
| FIX_ID | 是否冻结 |
| FLD_INDEX | 栏位顺序 |
| FLD_NAME | 栏位名称 |
| PGM | PGM |
| SHOW_ID | 显示否 |
| USR | 用户代号 |

## FACE_EXP (自定义字段公式)

| 字段名 | 中文说明 |
|--------|---------|
| EXP_ID | 表达式标识 |
| EXP_LINK | 表达式Link部分 |
| EXP_SELECT | 表达式Select部分 |
| EXP_SQL | 自定义SQL |
| EXP_WHERE | 表达式Where部分 |
| FLD_NAME | 栏位名 |
| MENU_NAME | 单据名 |
| TABLE_SIGN | 表标记 |

## FACE_FLD (自定义字段字典库)

| 字段名 | 中文说明 |
|--------|---------|
| CREATE_DD | 创建日期 |
| CTRL_CONTENT | 控件类型 |
| FLD_COND | 必填条件 |
| FLD_CTRL | 栏位控件类型 |
| FLD_DEFAULT | 默认值 |
| FLD_INDEX | 栏位顺序 |
| FLD_LEN | 字段长度 |
| FLD_NAME | 字段名称 |
| FLD_NOTNULL | 是否必填项 |
| FLD_READONLY | 只读否 |
| FLD_SIGN | 字段标记 |
| FLD_TYPE | 字段类型 |
| IS_CANEDIT | 表身自动取值字段允许手工修改 |
| IS_SAVECALC | 表身自动取值字段存盘时是否重新计算 |
| IS_SHAREFLD | 是否相同表共享字段自动取值、必填项、默认值 |
| LINK_FLD | 直接关联栏位 |
| MENU_NAME | 单据名 |
| MODIFY_DD | 修改时间 |
| MODIFY_MAN | 修改人 |
| QW_CODE | 查询代码 |
| QW_EDIT | 查询窗编辑键 |
| QW_FILTER | 查询用表过滤条件 |
| QW_NAME | 查询名称 |
| QW_TABLE | 查询视窗用表 |
| REM_BIG5 | 繁体中文说明 |
| REM_ENG | 英文说明 |
| REM_GB | 简体中文说明 |
| STOP_ID | 停用否 |
| TABLE_NAME | 表名 |
| TABLE_SIGN | 表标记 |
| USR | 创建人 |

## FACE_SQL_BS (单据保存SQL控制)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 集团分公司 |
| PGM | 程序代号 |
| SQL_CHK | 保存前检测SQL |
| SQL_SAVE | 保存后处理SQL |

## FACE_STATS_BS (单据栏位统计设定)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 集团分公司 |
| FLD_NAME | 栏位名 |
| PGM | PGM |
| USR | 用户代号 |

## FACE_TBL (自定义字段界面库)

| 字段名 | 中文说明 |
|--------|---------|
| APPLY | 启用重排的界面否 |
| BIL_ID | 单据别 |
| CINFO | 重排界面的部件信息 |
| FORMRCID | 标识 |
| HFACE | 表头界面信息 |
| HHEAD | 重排表头的高度 |
| HPANEL | 表头面板高度 |
| USR | 操作员 |
| VERSION | 版本 |
| WEBINFO | JSON格式内容 |

## FACE_WIDTH_BS (单据栏位宽度设定)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 集团分公司 |
| FLD_NAME | 栏位名称 |
| FLD_WIDTH | 栏位宽度 |
| PGM | PGM |
| USR | 用户代号 |

## FILESERVER (INI信息表)

| 字段名 | 中文说明 |
|--------|---------|
| CONTENT |  |
| FILENAME | 文件名称 |

## FORK_TRUCK (叉车车号管理表)

| 字段名 | 中文说明 |
|--------|---------|
| NAME | 叉车名称 |
| REM | 备注 |
| TRUCK_NO | 叉车号 |
| WH | 仓库代号 |

## FX_PSWD (ONLINE角色权限)

| 字段名 | 中文说明 |
|--------|---------|
| ALLOW_ID | 允许否(只有一般权限才用) |
| ATTACH | 附件 |
| CANCEL | 作废 |
| COMPNO | 帐套名称 |
| DEL | 删除 |
| DEPRO_NO | 部门群组 |
| DIS_CNT | 折扣 |
| EPT | 转出 |
| EXPAND | 展开 |
| FLD | 字段 |
| IMPORT | 导入 |
| INS | 新增 |
| LCK | 锁单 |
| PGM | 程序代号 |
| PRN | 列印 |
| PROPERTY | 属性 |
| QRY | 查询 |
| QTY | 数量 |
| R_CST | 成本 |
| RANGE |  |
| RANGE_DEL |  |
| ROLENO | 角色代号 |
| SHARE | 分享 |
| TYPE_ID | 权限分类 |
| U_AMT |  |
| UPD | 更新 |
| UPR | 单价 |

## FX_SPC_PSWD (分销特殊权限)

| 字段名 | 中文说明 |
|--------|---------|
| CTRL_ID | 密码控制点 |
| REM | 描述 |
| SPC_ID | 内容 |
| USR | 用户代号 |

## FX_SYS_FLOW (单据流程图)

| 字段名 | 中文说明 |
|--------|---------|
| FLOWINFO | 流程图设定 |
| LANG_ID | 语文别(0:简,1:繁,2英) |
| LOGIN_MODE | 登陆模式 |
| PGM | 程序代号 |
| ROLE_ID | 登陆身份 |
| USR | 用户代号 |

## FX_SYS (分销系统用)

| 字段名 | 中文说明 |
|--------|---------|
| EXTMENUINFO | 延伸菜单定义 |
| LANG_ID | 语文别(0:简,1:繁,2英) |
| LOGIN_MODE | 登陆模式 |
| MENUINFO | 主菜单设定 |
| ROLE_ID | 登陆身份 |
| TOOLBARINFO | 工具条定义 |
| USR | 用户名 |

## GFDB_SET (grafana设定)

| 字段名 | 中文说明 |
|--------|---------|
| DB_NO | 代号 |
| FLD_NO | 文件夹代号 |
| NAME | 名称 |
| OPEN_ID | 新窗口打开 |
| REFRESH_SEC | 刷新秒数 |
| STOP_ID | 停用否 |
| SYS_ID | 是否标准档 |

## GLOBAL_SET (全局设定库)

| 字段名 | 中文说明 |
|--------|---------|
| ITEM | 项次 |
| VALUE | 值 |

## HW_MODEL (设备型号表)

| 字段名 | 中文说明 |
|--------|---------|
| MODEL_NO | 设备型号代号 |
| NAME | 设备型号名称 |
| STOP_ID | 停用标记 |
| TYPE_NO | 设备类型代号 |

## HW_PACK_P (设备组合表身)

| 字段名 | 中文说明 |
|--------|---------|
| PACK_NO | 设备组合代号 |
| PROP_NO | 参数名 |
| REM | 备注 |
| VALUE | 参数值 |

## HW_PACK_T (设备组合类型表)

| 字段名 | 中文说明 |
|--------|---------|
| NAME | 设备组合类型名称 |
| STOP_ID | 停用标记 |
| TYPE_NO | 设备组合类型代号 |
| TYPE_NOS | 设备类型代号集合(分号隔开) |

## HW_PACK (设备组合表头)

| 字段名 | 中文说明 |
|--------|---------|
| NAME | 设备组合名称 |
| PACK_NO | 设备组合代号 |
| PACK_TYPE | 设备组合类型 |
| STATUS_ID | 设备组合状态(1.启用 2.故障 3.未启用) |
| WH | 所属仓库 |

## HW_RUN_HIS (设备运行历史表)

| 字段名 | 中文说明 |
|--------|---------|
| COMMAND | 动作指令 |
| END_TIME | 结束执行时间 |
| HIS_NO | 历史代号 |
| HIS_NO_M | 主历史代号 |
| HW_NO | 设备代号 |
| IP | IP地址 |
| IS_SEND | 发送否(T.发送 F.接收) |
| PACK_NO | 设备组合代号 |
| REM | 说明 |
| START_TIME | 开始执行时间 |
| STATUS_ID | 执行结果 |
| TYPE_NO | 设备类型代号 |

## HW_SET_P (系统设备表身)

| 字段名 | 中文说明 |
|--------|---------|
| HW_NO | 设备代号 |
| PROP_NO | 参数名 |
| REM | 备注 |
| VALUE | 参数值 |

## HW_SET (系统设备表头)

| 字段名 | 中文说明 |
|--------|---------|
| HW_NO | 设备代号 |
| IP | IP地址 |
| MODEL_NO | 设备型号代号 |
| NAME | 设备名称 |
| PORT | 端口 |
| STOP_ID | 停用标记 |
| TYPE_NO | 设备类型代号 |
| WH | 所属仓库 |

## HW_STATE (设备工作状态对照表)

| 字段名 | 中文说明 |
|--------|---------|
| REM | 状态说明 |
| STATE_NO | 代号 |
| TYPE_NO | 设备类型 |

## HW_TYPE (设备类型表)

| 字段名 | 中文说明 |
|--------|---------|
| NAME | 设备类型名称 |
| RUN_MODEL | 运行模式(1.组合运行 2.单独运行) |
| STOP_ID | 停用标记 |
| TYPE_NO | 设备类型代号 |

## IJ_REASON_SET (盘盈/盘亏原因设定)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据别 |
| IJ_REASON | 原因代号 |
| REASON_REM | 原因说明 |

## INDUCTIONTYPE_YC (感应式料架异常表)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO | 原任务编号 |
| ACT_NO_CL | 处理任务编号 |
| CHUW | 储位 |
| CL_FLAG | 处理标记 |
| CONTAIN_CODE | 容器条码 |
| GUID | 异常编号 |
| SYS_DATE | 生成时间 |
| TYPE_ID | 类型(SJ:上架/XJ:下架) |
| WH | 仓库 |
| YC_TYPE | 异常类型(1.货位异常、2.生成单据异常) |

## INDX_CHK (中类上下属)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| DRC |  |
| IDX_DW | 下层中类 |
| IDX_UP | 上层中类 |

## INDX_DEL (中类-删除库)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| IDX_NO | 中类代号 |
| UP_DD | 上次更新时间 |

## INDX_PD (盘点分类)

| 字段名 | 中文说明 |
|--------|---------|
| IDX_NO | 分类代号 |
| NAME | 分类名称 |
| REM | 备注 |

## INDX (中类资料库)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| IDX_NO | 中类代号 |
| IDX_UP | 上层中类 |
| NAME | 名称 |
| REM | 备注 |
| STOP_DD | 停用日期 |
| SYS_DATE | 输单日期 |
| TP_ID | 第三方标志 |
| UP_DD | 上次更新时间 |
| USR | 制单人 |

## INNER_BOX_CHANGE (内箱序列号变更表)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单号 |
| BOX_NO | 箱序列号 |
| BOX_NO1_CUR | 现外箱码 |
| BOX_NO1_ORIG | 原外箱码 |
| CHANGE_DD | 变更时间 |
| CHANGE_ID | 变更ID |
| CHUW_CUR | 现储位 |
| CHUW_ORIG | 原储位 |
| CON_NO | 货主编码 |
| CONTAIN_CODE_CUR | 现容器条码 |
| CONTAIN_CODE_ORIG | 原容器条码 |
| ICCK_FLAG_CUR | 现调拨出库标记 |
| ICCK_FLAG_ORIG | 原调拨出库标记 |
| JH_FLAG_CUR | 现拣货标记 |
| JH_FLAG_ORIG | 原拣货标记 |
| PACKAGE_NO_CUR | 现包装码 |
| PACKAGE_NO_ORIG | 原包装码 |
| PH_FLAG_CUR | 现在途标记 |
| PH_FLAG_ORIG | 原在途标记 |
| QTY_CUR | 现货品数量 |
| QTY_ORIG | 原货品数量 |
| STOP_ID_CUR | 现停用标记 |
| STOP_ID_ORIG | 原停用标记 |
| TI_FLAG_CUR | 现送检标记 |
| TI_FLAG_ORIG | 原送检标记 |
| UNIQUE_CHANGE_ID | 唯一性ID |
| WH_CUR | 现仓库 |
| WH_ORIG | 原仓库 |

## IZWH_CONFIRM (依仓库启用到货确认)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 部门 |
| MODIFY_DD | 修改时间 |
| MODIFY_MAN | 修改人 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| WH_IN | 入货仓库 |
| WH_OUT | 出货仓库 |

## LK_ACTION_HIS_I (入库处理日志表(自动化))

| 字段名 | 中文说明 |
|--------|---------|
| ACT_ID | 处理动作(Add：新增 Push：推送 Redo：重推 Delete：删除) |
| ACT_NO | 处理代号 |
| ACT_NO_NEW | 重新处理代号 |
| ACT_NO_OUT | 原出库处理代号 |
| BIL_ID | 产生单据别 |
| BIL_NO | 产生单据号码 |
| CHUW | 储位代号 |
| CHUW2 | 储位代号2 |
| CLIENT_IP | 请求IP地址 |
| COMPNO | 帐套代号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| END_DATE | 推送结束时间 |
| ERR_CODE | 错误代码 |
| ERR_MSG | 错误说明 |
| HIS_NO | 日志代号 |
| HTTP_METHOD | 请求方式 |
| JSON_CONTENT | 请求Json |
| METHOD_NO | 接口名称 |
| OTH_TASK_ID | 第三方任务代号 |
| PATH | 请求路径 |
| REQUEST_SIZE | 请求内容大小 |
| RESULT_CONTENT | 回传内容 |
| RESULT_SIZE | 回传内容大小 |
| START_DATE | 推送开始时间 |
| STATUS_ID | 成功否 |
| SUP_NO | 接口厂商代号 |
| SYNC_ID | 同步否 |
| WH | 仓库代号 |
| WH2 | 仓库代号2 |

## LK_ACTION_HIS_O (出库处理日志表(自动化))

| 字段名 | 中文说明 |
|--------|---------|
| ACT_ID | 处理动作(Add：新增 Push：推送 Redo：重推 Delete：删除) |
| ACT_NO | 处理代号 |
| ACT_NO_CHG | 变更处理代号 |
| ACT_NO_MAIN | 主任务代号 |
| ACT_NO_MID | 中途任务完成代号 |
| AREA_END | AGV终点 |
| AREA_START | AGV起点 |
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单据号码 |
| CHUW | 储位代号 |
| CHUW_ENTRY | 入口储位代号 |
| CHUW2 | 储位代号2 |
| CHUW2_ENTRY | 入口储位代号2 |
| COMPNO | 帐套代号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| END_DATE | 推送结束时间 |
| ERR_CODE | 错误代码 |
| ERR_HTTP | Http错误说明 |
| ERR_MSG | 错误说明 |
| EST_PUSH_DATE | 预计推送时间 |
| HIS_NO | 日志代号 |
| HTTP_CODE | Http错误代码 |
| HTTP_METHOD | 请求方式 |
| JSON_CONTENT | 请求Json |
| LINE_NO | 站台代号 |
| METHOD_NO | 接口名称 |
| PASSAGE | 通道 |
| PATH | 请求路径 |
| PRIORITY | 优先级 |
| PUSH_CONTENT | 推送内容 |
| PUSH_SIZE | 推送内容大小 |
| RELATION_NO | 关联单号 |
| REM | 备注 |
| RESULT_CONTENT | 回传内容 |
| RESULT_SIZE | 回传内容大小 |
| START_DATE | 推送开始时间 |
| STATUS_ID | 成功否 |
| SUP_NO | 接口厂商代号 |
| SYNC_ID | 同步否 |
| TASK_NO_SHUTTLE | 穿梭式巷道任务代号 |
| TRUCK_NO | 叉车号 |
| TUNNEL_NO | 巷道号 |
| TUNNEL_NO2 | 巷道号2 |
| WH | 仓库代号 |
| WH2 | 仓库代号2 |

## LK_ACTION_I (入库处理表(自动化))

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO | 处理代号 |
| ACT_NO_NEW | 重新处理代号 |
| ACT_NO_OUT | 原出库处理代号 |
| BIL_ID | 产生单据别 |
| BIL_NO | 产生单据号码 |
| CHUW | 储位代号 |
| CHUW2 | 储位代号2 |
| CLIENT_IP | 请求IP地址 |
| COMPNO | 帐套代号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| DEL_ID | 删除否 |
| END_DATE | 推送结束时间 |
| ERR_CODE | 错误代码 |
| ERR_MSG | 错误说明 |
| HTTP_METHOD | 请求方式 |
| JSON_CONTENT | 请求Json |
| METHOD_NO | 接口名称 |
| OTH_TASK_ID | 第三方任务代号 |
| PATH | 请求路径 |
| PUSH_ID | 推送否 |
| REQUEST_SIZE | 请求内容大小 |
| RESULT_CONTENT | 回传内容 |
| RESULT_SIZE | 回传内容大小 |
| RUN_COUNT | 执行次数 |
| START_DATE | 推送开始时间 |
| STATUS_ID | 成功否 |
| SUP_NO | 接口厂商代号 |
| SYNC_ID | 同步否 |
| SYS_DATE | 插入时间 |
| WH | 仓库代号 |
| WH2 | 仓库代号2 |

## LK_ACTION_O (出库处理表(自动化))

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO | 处理代号 |
| ACT_NO_FIN | 出库完成处理代号 |
| ACT_NO_LOCK | 锁定处理代号 |
| ACT_NO_MAIN | 主任务代号 |
| ACT_NO_MID | 中途任务完成代号 |
| ACT_NO_NEW | 重新处理代号 |
| AREA_END | AGV终点 |
| AREA_START | AGV起点 |
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单据号码 |
| CHUW | 储位代号 |
| CHUW_ENTRY | 入口储位代号 |
| CHUW2 | 储位代号2 |
| CHUW2_ENTRY | 入口储位代号2 |
| COMPNO | 帐套代号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| DEL_ID | 删除否 |
| END_DATE | 推送结束时间 |
| ERR_CODE | 错误代码 |
| ERR_HTTP | Http错误说明 |
| ERR_MSG | 错误说明 |
| EST_PUSH_DATE | 预计推送时间 |
| HTTP_CODE | Http错误代码 |
| HTTP_METHOD | 请求方式 |
| JSON_CONTENT | 请求Json |
| LINE_NO | 站台代号 |
| LOCK_ID | 锁定标记 |
| METHOD_NO | 接口名称 |
| PASSAGE | 通道 |
| PATH | 请求路径 |
| PRIORITY | 优先级 |
| PUSH_CONTENT | 推送内容 |
| PUSH_ID | 推送否 |
| PUSH_SIZE | 推送内容大小 |
| RELATION_NO | 关联单号 |
| RESULT_CONTENT | 回传内容 |
| RESULT_SIZE | 回传内容大小 |
| RUN_COUNT | 执行次数 |
| START_DATE | 推送开始时间 |
| STATUS_ID | 成功否 |
| SUP_NO | 接口厂商代号 |
| SYNC_ID | 同步否 |
| SYS_DATE | 插入时间 |
| TASK_NO_SHUTTLE | 穿梭式巷道任务代号 |
| TRUCK_NO | 叉车号 |
| TUNNEL_NO | 巷道号 |
| TUNNEL_NO2 | 巷道号2 |
| WH | 仓库代号 |
| WH2 | 仓库代号2 |

## LK_ACTION_YC (任务异常表(自动化))

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO | 原任务代号 |
| CHUW | 储位代号 |
| CONTAIN_CODE | 容器条码 |
| METHOD_NO | 原接口名称 |
| REM | 异常说明 |
| STATUS | 执行状态 |
| TYPE_ID | 接口类型(Rigor：锐格) |
| YC_NO | 异常代号 |

## LK_METHOD (供应商接口名称表)

| 字段名 | 中文说明 |
|--------|---------|
| I_O | 接口调用方向(I：入 O：出) |
| METHOD_NO | 接口名称 |
| METHOD_NO1 | 接口名称(外部) |
| NAME | 接口描述 |
| SUP_NO | 接口厂商代号 |

## LK_PROP_I (自动化仓库接口参数配置明细)

| 字段名 | 中文说明 |
|--------|---------|
| LKIF_ID | 立库接口代号 |
| PROP_NO | 参数名 |
| REM | 备注 |
| VALUE | 参数值 |

## LK_PROP (自动化仓库接口参数配置)

| 字段名 | 中文说明 |
|--------|---------|
| LKIF_ID | 立库接口代号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| NAME | 立库名称 |
| SAL_DFT | 默认经办人 |
| STOP_ID | 停用标记 |
| SYS_DATE | 录入时间 |
| TYPE_ID | 接口类型(RIGOR：锐格) |
| USR | 录入人员 |
| USR_DFT | 默认制单人 |

## LK_SUP (外部供应商表)

| 字段名 | 中文说明 |
|--------|---------|
| AGV_ID | 是AGV |
| NAME | 接口厂商名称 |
| SMT_ID | 是感应式料架 |
| STOP_ID | 停用标记 |
| SUP_NO | 接口厂商代号 |
| SUP_NO_UP | 上层接口厂商代号 |
| TYPE_ID | 供应商类型 |

## LOGIN_INFO (USER登陆资讯)

| 字段名 | 中文说明 |
|--------|---------|
| ACCESS_TIME | 上次存取时间 |
| CLASSID | 登陆CLSID |
| COMPNO | 公司代号 |
| EXPIRES_IN | 过期时间戳 |
| IP | 登陆的IP |
| IP_LAN | 内网IP |
| MAC_ADDR | MAC地址 |
| MACH_NAME | 机器名称 |
| OFF_ID | 是否已经登出 |
| TYPE | 产品别名称 |
| USR | 用户代号 |

## LOGIN_STATS (登陆失败次数统计)

| 字段名 | 中文说明 |
|--------|---------|
| FAIL_COUNT | 失败次数 |
| FAIL_DD | 最近登陆失败时间 |
| USR | 用户代号 |

## MARK (品牌资料)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| MARK_NO | 厂牌号 |
| NAME | 名称 |
| UP_DD | 上次更新时间 |

## MF_BAR (序列号变更单表头)

| 字段名 | 中文说明 |
|--------|---------|
| BR_DD | 变更日期 |
| BR_NO | 变更单号 |
| CON_NO | 货主编码 |
| DEP | 部门代号 |
| EXE_DATE | 执行时间 |
| EXE_STATUS | 执行变更状态(待执行/已执行) |
| EXE_USR | 执行人 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| REM | 变更原因 |
| SYS_DATE | 输单日期 |
| USR | 制单人 |

## MF_BC (波次单表头)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_SH | 收货点 |
| BC_DD | 波次日期 |
| BC_NO | 波次单号 |
| BY_ORDER | 依订单发货标记 |
| CLS_ID_CK | 出库结案标记 |
| CLS_ID_JR | 拣货任务结案标记 |
| CLS_ID_PK | 拣货结案标记 |
| CON_NO | 货主编码 |
| CPY_SW | 拷贝注记 |
| DEP | 部门代号 |
| FHYJ_FIELD | 发货依据栏位 |
| LINE_CODE | 产线代号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PICK_POINT | 拣选点（多个站点用;隔开） |
| PK_DD | 拣货日期 |
| PRIORITY | 排单等级(0.低 1.中 2.高) |
| PRIORITY_WCS | WCS优先级(1.紧急 2.特急) |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| RECEI_AREA | 收货区域 |
| REM | 备注 |
| RUN_ID | 可执行否(生成拣货任务单) |
| SAL_NO | 经办人 |
| STATUS_PG | 派工状态(0.未派工 1.已派工) |
| SYS_DATE | 输单日期 |
| TYPE_ID | 业务类型 |
| USR | 制单人 |
| USR_PK | 拣货员 |
| WH | 仓库代号 |
| WH_TYPE | 启用立库接口(1、不启用 2、启用) |
| WORK_STATION | 拣选工作站台（多个站台用;隔开） |
| ZONE_ID | 区域代号 |
| ZONE_JX | 拣选区 |

## MF_BHCKTZ (备货出库通知记录)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_DH_DD | 实际到达时间 |
| AREA_FH | 发货单 |
| AREA_SH | 收货点 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| FH_DATE | 发货处理时间 |
| FH_TYPE | 发货方式(1:配送、2:人工配送) |
| FH_USR | 发货处理人 |
| GUID | 任务ID |
| STATUS | 处理状态(1:待处理、2:代发货、3:已发货) |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| WH | 仓库 |
| XJ_DATE | 下架时间 |
| XJ_USR | 下架处理人 |

## MF_BINDCK (备货单绑定作业表头)

| 字段名 | 中文说明 |
|--------|---------|
| BB_DD | 绑定日期 |
| BB_NO | 绑定单号 |
| CK_NO | 备货单号 |
| DEP | 部门代号 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |

## MF_CHECK (储位复核单表头)

| 字段名 | 中文说明 |
|--------|---------|
| CHK_DD | 复核日期 |
| CHK_NO | 复核单号 |
| CHUW | 储位代号 |
| CL_FLAG | 处理标记 |
| DEP | 部门代号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| REM | 备注 |
| SAL_NO | 经办人 |
| SYS_DATE | 输单日期 |
| USR | 制单人 |
| WH | 仓库代号 |
| XJPD_FLAG | 下架盘点处理标记 |

## MF_CJ (车间交接单表头)

| 字段名 | 中文说明 |
|--------|---------|
| AUTO_ID | 自动产生标记 |
| BIL_ID | 转入单ID |
| BIL_NO | 转入单号 |
| BIL_TYPE | 单据类别 |
| CJ_DD | 入库日期 |
| CJ_NO | 车间入库单号 |
| CLS_ID_TY | 检验结案标记 |
| CON_NO | 货主编码 |
| DEP | 部门 |
| FLAG_JY | 检验标志（T 检验/F 免检） |
| LK_ID | 立库产生标志 |
| MODIFY_DD | 修改时间 |
| MODIFY_MAN | 修改人 |
| OTH_BIL_NO | 外部系统单号 |
| OTH_ID | 转入单外部系统标识 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| REM | 备注 |
| RK_NO | 入库单号 |
| SAL_NO | 经办人 |
| SUM_CONTAIN | 制令单总托数 |
| SYS_DATE | 制单时间 |
| TY_PUSH_SW | 检验单推送SUNLIKE标记(T/F) |
| TY_SYS | 检验系统(ERP/WMS) |
| TYPE_ID | 业务类型 |
| TZ_NO | 产生入库通知单号 |
| USR | 制单人 |
| WH | 仓库代号 |
| WMS_ID | WMS产生的单据 |

## MF_CK (出库单表头)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_SH | 收货点 |
| AUTO_ID | 自动产生标记 |
| BIL_ID | 转入单ID |
| BIL_NO | 转入单号 |
| BIL_TYPE | 单据类别 |
| BIL_TYPE_ID | 单据类型 |
| CFM_SW | 到货确认标记 |
| CK_DD | 出库日期 |
| CK_FLOW | 出库流程标记(BC 为波次流程，JH 为直接拣货流程) |
| CK_NO | 出库单号 |
| CON_NO | 货主编码 |
| CON_USR | 货主编码制单人 |
| CPY_SW | 拷贝注记 |
| CUS_NAME | 客户名称 |
| CUS_NO | 客户代号 |
| DEP | 部门代号 |
| ERP_AP_ID | ERP申请单ID |
| ERP_AP_NO | ERP申请单号 |
| ERP_GEN_METHOD | ERP库存单据生成方式(1.直接新增 2.单据确认) |
| EXT_SYS_FLAG | 外部系统标识 |
| EXT_SYS_ID | 外部系统单据ID |
| EXT_SYS_NO | 外部系统单号 |
| FH_NO | 发货单号 |
| GEN_ERP_BIL_ID | 生成ERP出库单据别 |
| LINE_CODE | 产线代号 |
| MO_NO | 制令单号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| NL_NO | 挪料单号 |
| PICK_POINT | 拣选点（多个站点用;隔开） |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| RECEI_AREA | 收货区域 |
| REM | 备注 |
| SAL_NO | 经办人 |
| SCTZ_NO | 生产通知单 |
| SYS_DATE | 输单日期 |
| TYPE_ID | 业务类型 |
| USR | 制单人 |
| WH | 仓库代号 |
| WMS_ID | WMS产生的单据 |
| WORK_STATION | 拣选工作站台（多个站台用;隔开） |
| ZC_NO | 制程代号 |

## MF_CKTB (出库退回通知表头)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 转入单据ID |
| BIL_NO | 转入单号 |
| BIL_TYPE | 单据类别 |
| CON_NO | 货主编码 |
| CPY_SW | 拷贝注记 |
| CUS_NAME | 客户名称 |
| CUS_NO | 客户代号 |
| DEP | 部门代号 |
| DEP_ERP | ERP 部门代号 |
| DEP_NAME_ERP | ERP 部门名称 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_NO | ERP申请单号 |
| EST_DD | 预出库日 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| ORG_BIL_ID | 业务单ID |
| ORG_BIL_NO | 业务单号 |
| OTH_BIL_ID | 外部系统单据ID |
| OTH_BIL_NO | 外部系统单号 |
| OTH_ID | 外部系统标识 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| REM | 备注 |
| RTN_BIL_ID | 已转单据别ID |
| RTN_BIL_NO | 已转单号 |
| SAL_NAME_ERP | ERP 业务员名称 |
| SAL_NO | 经办人 |
| SAL_NO_ERP | ERP 业务员代号 |
| SYS_DATE | 输单日期 |
| TB_DD | 退回日期 |
| TB_NO | 出库退回通知单号 |
| TYPE_ID | 业务类型 |
| USR | 制单人 |
| WH | 仓库代号 |

## MF_CKTZ_CHG (出库通知变更单表头)

| 字段名 | 中文说明 |
|--------|---------|
| CHG_DD | 变更日期 |
| CHG_NO | 变更单号 |
| CHG_TYPE | 变更类型（变更表头资料、变更表身资料） |
| CON_NO | 货主编码 |
| DEP | 部门代号 |
| EXE_DATE | 执行时间 |
| EXE_STATUS | 执行状态（待执行/已执行） |
| EXE_USR | 执行人 |
| GEN_BIL_ID | 产生ERP单据别 |
| GEN_BIL_NO | 产生ERP单据号码 |
| OTH_BIL_NO | 第三方系统单号 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| REF_ID | 第三方系统标识 |
| REM | 备注 |
| SYS_DATE | 制单时间 |
| TYPE_ID | 业务类型 |
| USR | 制单人 |

## MF_CKTZ_CHGH (出库通知变更单-变更表头资料)

| 字段名 | 中文说明 |
|--------|---------|
| CHG_METHOD | 变更方式（C变更、D删除） |
| CHG_NO | 变更单号 |
| ITM | 项次 |
| REASON | 变更原因说明 |
| TB_ITM | 退回通知单项次 |
| TB_NO | 退回通知单号 |
| TZ_NO | 通知单号 |

## MF_CKTZ (出库通知单表头)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_SH | 收货点 |
| AUTO_GET_BILL | 非ERP推送/自动获取单据 |
| BIL_TYPE | 单据类别 |
| CFM_SW | 到货确认标记 |
| CK_FLOW | 出库流程标记(BC 为波次流程，JH 为直接拣货流程) |
| CL_ID | 自动结案标记(作废) |
| CLS_ID_BC | 波次结案标记 |
| CLS_ID_CK | 出库结案标记 |
| CLS_ID_MANUAL | 手工结案标记 |
| CLS_ID_PK | 拣货结案标记 |
| CON_NO | 货主编码 |
| CPY_SW | 拷贝注记 |
| CUS_NAME | 客户名称 |
| CUS_NO | 客户代号 |
| DEP | 部门代号 |
| DEP_ERP | ERP 部门代号 |
| DEP_NAME_ERP | ERP 部门名称 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_NO | ERP申请单号 |
| ERP_GEN_METHOD | ERP库存单据生成方式(1.直接新增 2.单据确认) |
| EST_DD | 预出库日 |
| EST_DH_DD | 预计到货时间 |
| FH_NO | 发货单号 |
| GEN_ERP_BIL_ID | 生成ERP出库单据别 |
| LINE_CODE | 产线代号 |
| MODIFY_DD | 最近修改时间 |
| MODIFY_MAN | 最近修改人 |
| ORG_BIL_ID | 业务单ID |
| ORG_BIL_NO | 业务单号 |
| OTH_BIL_ID | 外部系统单据ID |
| OTH_BIL_NO | 外部系统单号 |
| OTH_ID | 外部系统标识 |
| OTH_MODIFY_DD | 第三方最近修改时间 |
| OTH_SYS_DATE | 第三方制单时间 |
| PICK_POINT | 拣选点（多个站点用;隔开） |
| PRIORITY | 排单等级(0.低 1.中 2.高) |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| RECEI_AREA | 收货区域 |
| REM | 备注 |
| SAL_NAME_ERP | ERP 业务员名称 |
| SAL_NO | 经办人 |
| SAL_NO_ERP | ERP 业务员代号 |
| STATUS_KFH | 可发货状态（1.允许发货、2.待通知发货） |
| STATUS_PG | 派工状态(0.未派工 1.已派工) |
| SYS_DATE | 输单日期 |
| TYPE_ID | 业务类型 |
| TZ_DD | 通知日期 |
| TZ_NO | 出库通知单号 |
| USR | 制单人 |
| USR_PK | 拣货员 |
| WH | 仓库代号 |
| WORK_STATION | 拣选工作站台（多个站台用;隔开） |

## MF_CL (单据结案作业表头)

| 字段名 | 中文说明 |
|--------|---------|
| CL_DATE | 结案时间 |
| CL_DD | 结案日期 |
| CL_NO | 结案单号 |
| CL_USR | 结案人员 |
| REM | 备注 |
| UPD_BILID | 变更单据别 |
| UPD_DEP | 变更部门 |

## MF_CONTAIN_EXT (物流容器装箱附属表头)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 申请单据别 |
| BIL_NO | 申请单号 |
| CONTAIN_CODE | 容器条码 |
| NW_QTY_MAX | 最大可存放数量 |
| NW_TYPE | 分类(1.模具 2.工装) |

## MF_CONTAIN_HIS (物流容器变动历史表头)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 生产批号 |
| BH_FLAG | 备货标记 |
| BIL_ID | 变动单据别 |
| BIL_NO | 变动单号 |
| BIL_NO_YC | 异常单号 |
| BOM_NO | 配方号 |
| CHANGE_DD | 变动时间 |
| CHANGE_NO | 变动单号 |
| CHANGE_USR | 变动人 |
| CHUW | 储位代号 |
| CK_NO | 出库业务单号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| CONTAIN_STATE | 容器状态 |
| CONTAIN_TYPE | 容器类型 |
| CREATE_TIME | 生产时间 |
| FULL_RATE | 满箱率% |
| FULL_STATUS | 容器满箱状态（0未满，1满箱） |
| HUOW | 货位条码 |
| ICCK_FLAG | 调拨出库标记 |
| IS_MIX | 是否混装 |
| JH_FLAG | 拣货标记 |
| LAST_DD | 最近装箱时间 |
| LAST_USR | 最近装箱人 |
| LH_FLAG | 理货标记（T/F） |
| NOSCAN_FLAG | 免扫货品条码标记 |
| ONWAY_FLAG | 在途标记 |
| PACKAGE_NO | 包装码 |
| PD_DD | 盘点日期 |
| PH_FLAG | 配送标记 |
| PK_ID | 拣货单ID |
| PK_NO | 拣货单号 |
| PK_QTY | 包装换算 |
| PT_NO | 打印编号 |
| QTY_BOX | 箱数 |
| QTY_WEIGHT | 重量 |
| REM | 备注 |
| RK_NO | 入库业务单号 |
| SJ_FLAG | 是否散出 |
| SO_NO | 受订单号 |
| SPC | 规格 |
| STATIC_TIME_FIN | 静置完成时间 |
| STATUS_JY | 检验状态(空或T：合格、F：不合格、W：待检) |
| STATUS_YC | 异常状态 |
| TI_ID | 送检标记 |
| UNIQUE_CHANGE_ID | 唯一性ID |
| WEIGHT | 重量 |
| WG_ID | 非完工品 |
| WH | 所在仓库 |
| ZC_NO | 工序代号 |

## MF_CONTAIN (物流容器装箱表头)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_NO | AGV 站点 |
| AREA_SH | 收货点 |
| BAT_NO | 生产批号 |
| BH_CKBILL | 备货容器出库单据(0为出库确认、1 备货出库通知) |
| BH_FLAG | 备货标记 |
| BIL_NO_YC | 异常单号 |
| BOM_NO | 配方号 |
| CHUW | 储位代号 |
| CK_NO | 出库业务单号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| CONTAIN_STATE | 容器状态(0、未使用 1、已使用 2、已使用(待检入库)) |
| CONTAIN_TYPE | 容器类型 |
| CREATE_TIME | 生产时间 |
| FULL_RATE | 满箱率% |
| FULL_STATUS | 容器满箱状态（0未满，1满箱） |
| HUOW | 货位条码 |
| ICCK_FLAG | 调拨出库标记 |
| IS_MIX | 是否混装 |
| JH_FLAG | 拣货标记 |
| LAST_BIND_DD | AGV站点最近修改时间 |
| LAST_DD | 最近装箱时间 |
| LAST_USR | 最近装箱人 |
| LH_FLAG | 理货标记（T/F） |
| LOCK_ID | 第三方占用标记 |
| NOSCAN_FLAG | 免扫货品条码标记 |
| ONWAY_FLAG | 在途标记 |
| PACKAGE_NO | 包装码 |
| PD_DD | 盘点日期 |
| PH_FLAG | 在途标记 |
| PK_ID | 拣货单ID |
| PK_NO | 拣货单号 |
| PK_QTY | 包装换算 |
| PT_NO | 打印编号 |
| QTY_BOX | 箱数 |
| QTY_WEIGHT | 重量 |
| REM | 备注 |
| RK_NO | 入库业务单号 |
| SBAT_NO | 灭菌批号 |
| SO_NO | 受订单号 |
| SPC | 规格 |
| STATIC_TIME_FIN | 静置完成时间 |
| STATUS_JY | 检验状态(空或T：合格、F：不合格、W：待检、B：不合格转报废) |
| STATUS_MODIFY_DD | 检验状态最近修改时间 |
| STATUS_YC | 异常状态(1.二次分拣异常 2.盘点异常) |
| SYS_DATE | 录入时间 |
| TI_ID | 送检标记 |
| USR | 录入人 |
| WEIGHT | 限重(以林) |
| WEIGHT_MODIFY_DD | 重量最近修改时间 |
| WH | 所在仓库 |

## MF_CP (初盘单表头)

| 字段名 | 中文说明 |
|--------|---------|
| AUTO_ID | 是否自动化仓库 |
| BIL_ID | 转入单ID |
| BIL_NO | 转入单号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| CONTAIN_CODE_IN | 转入容器条码 |
| CP_DD | 初盘日期 |
| CP_NO | 初盘单号 |
| CUS_NO | 厂商代号 |
| DEP | 部门 |
| DIFF_CL | 差异处理(立即处理/复盘) |
| FP_TYPE | 复盘方式(容器复盘/复盘差异储位/复盘差异货品) |
| MODIFY_DD | 修改日期 |
| MODIFY_MAN | 修改人 |
| PD_NO | 盘点单号 |
| PD_TYPE | 盘点方式(1:按储位盘点,2按储位+货品盘点) |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| REM | 备注 |
| SYS_DATE | 输单日期 |
| TASK_TYPE | 任务类型(循环盘点/按区域盘点/复盘/盘点) |
| USR | 制单人 |
| USR_PD | 盘点人员 |
| WH | 仓库代号 |

## MF_CWDB (储位调拨单主表)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单号 |
| BIL_TYPE | 单据类别 |
| CON_NO | 货主编码 |
| DB_DD | 调拨日期 |
| DB_ID | 调拨单ID |
| DB_NO | 调拨单号 |
| DEP | 部门 |
| LK_ID | 立库产生标志 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| REM | 备注 |
| SAL_NO | 经办人 |
| SYS_DATE | 输单日期 |
| USR | 录入员 |
| WMS_ID | WMS产生的单据 |

## MF_CWSJ (货品储位上架单主表)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单号 |
| BIL_TYPE | 单据类别 |
| CON_NO | 货主编码 |
| DEP | 部门 |
| LK_ID | 立库单据标志 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| REM | 备注 |
| SAL_NO | 经办人 |
| SJ_DD | 上架日期 |
| SJ_ID | 上架单ID |
| SJ_NO | 上架单号 |
| SYS_DATE | 输单日期 |
| USR | 录入员 |
| WMS_ID | WMS产生的单据 |

## MF_CWXJ (货品储位下架单主表)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单号 |
| BIL_TYPE | 单据类别 |
| CON_NO | 货主编码 |
| DEP | 部门 |
| LK_ID | 立库单据标志 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| REM | 备注 |
| SAL_NO | 经办人 |
| SYS_DATE | 输单日期 |
| USR | 录入员 |
| WMS_ID | WMS产生的单据 |
| XJ_DD | 下架日期 |
| XJ_ID | 下架单ID |
| XJ_NO | 下架单号 |

## MF_DEF (自定义字段表头)

| 字段名 | 中文说明 |
|--------|---------|
| COL_COUNT | 显示列数 |
| COL_COUNT1 | 表尾显示列数 |
| DEF_NO | 代号 |
| PGM | PGM |
| REM | 备注 |
| SYS_DATE | 制单日期 |

## MF_FCHART (FCHART表头)

| 字段名 | 中文说明 |
|--------|---------|
| DATA_SUM | Pivot 取合计否 |
| DATA_TOP | 取前几笔 |
| DATA_X | Pivot X轴字段 |
| DATA_Y | Pivot Y轴字段 |
| DATA_Z | Pivot Z轴字段 |
| FC_NAME | 模版名称 |
| FC_NO | 自定义图表单号 |
| INFO_ID | 老板报表数据源ID |
| ISPUBLIC | 公共图表 |
| LAYOUT_COL_NUM | 每行显示个数 |
| LAYOUT_ELEMEMT | 布局元素 |
| LAYOUT_H | 图表高度 |
| LAYOUT_RPT_H | 报表高度 |
| LAYOUT_RPT_W | 报表宽度 |
| LAYOUT_SPLIT | 布局分割线距 |
| LAYOUT_TYPE | 布局分割方式 |
| LAYOUT_W | 图表宽度 |
| RPT_PGM | 报表PGM |
| RPT_TYPE | 报表类型 |
| SHOWSUM | 是否显示合计 |
| SORT | 排序 |
| SOURCE_FIELDS | 显示字段 |
| SOURCE_SQL | 数据源-SQL语句 |
| SOURCE_TYPE | 数据源-来源类别 |
| SYS_DATE | 制单日期 |
| SYS_ID | 系统套版否 |
| USR | 制单人 |

## MF_FLD (系统字段设定主库)

| 字段名 | 中文说明 |
|--------|---------|
| FLD_NO | 代号 |
| NS_NO | 类别代号 |
| PGM | 程序代号 |
| REM | 备注说明 |
| SYS_DATE | 制单日期 |

## MF_IC (库存调拨单表头)

| 字段名 | 中文说明 |
|--------|---------|
| AUTO_ID | 自动产生标记 |
| BIL_ID | 来源单据别 |
| BIL_KND | 单据类型(WMS;ERP;ERP1) |
| BIL_NO | 来源单号 |
| BIL_TYPE | 单据类别 |
| CFM_SW | 到货确认标记 |
| CON_NO | 货主编码 |
| CON_NO_IN | 拨入货主编码 |
| DEP | 部门代号 |
| ERP_GEN_METHOD | ERP库存单据生成方式(1.直接新增 2.单据确认) |
| IC_DD | 单据日期 |
| IC_NO | 调拨单号 |
| IC_TYPE | 调拨类型(空/BLPCL：不良品处理/KWJYDB：库外检验调拨) |
| IZ_CLS_ID | 到货确认结案标记 |
| LINE_CODE | 产线代号 |
| LK_ID | 立库单据标志 |
| MODIFY_DD | 修改时间 |
| MODIFY_MAN | 修改人 |
| PICK_POINT | 拣选点（多个站点用;隔开） |
| PRT_DATE | 打印时间 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| RECEI_AREA | 收货区域 |
| REM | 备注 |
| SAL_NO | 经办人 |
| SPAN_ERP_IC | 跨货主调拨 |
| SYS_DATE | 制单时间 |
| TYPE_ID | 业务类型 |
| USR | 制单人 |
| WORK_STATION | 拣选工作站台（多个站台用;隔开） |

## MF_ICTZ_CHG (调拨通知变更单表头)

| 字段名 | 中文说明 |
|--------|---------|
| CHG_DD | 变更日期 |
| CHG_NO | 变更单号 |
| CHG_TYPE | 变更类型（变更表头资料、变更表身资料） |
| CON_NO | 货主编码 |
| DEP | 部门代号 |
| EXE_DATE | 执行时间 |
| EXE_STATUS | 执行状态（N待执行/Y已执行） |
| EXE_USR | 执行人 |
| GEN_BIL_ID | 产生ERP单据别 |
| GEN_BIL_NO | 产生ERP单据号码 |
| OTH_BIL_NO | 第三方系统单号 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| REF_ID | 第三方系统标识 |
| REM | 备注 |
| SYS_DATE | 制单时间 |
| TYPE_ID | 业务类型 |
| USR | 制单人 |

## MF_ICTZ (调拨通知单表头)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_SH | 收货点 |
| AUTO_ID | 自动产生标记 |
| BIL_ID | 转入单据别 |
| BIL_NO | 转入单号 |
| BIL_TYPE | 单据类别 |
| CFM_SW | 到货确认标记 |
| CK_FLOW | 出库流程 |
| CLS_ID_BC | 波次结案标记 |
| CLS_ID_CK | 出库结案标记 |
| CLS_ID_PK | 拣货结案标记 |
| CON_NO | 货主编码 |
| DEP | 部门代号 |
| DEP_ERP | ERP部门代号 |
| DEP_NAME_ERP | ERP部门名称 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_NO | ERP申请单号 |
| ERP_GEN_METHOD | ERP库存单据生成方式(1.直接新增 2.单据确认) |
| EST_DD | 预出库日 |
| FH_NO | 发货单号 |
| LINE_CODE | 产线代号 |
| MODIFY_DD | 修改时间 |
| MODIFY_MAN | 修改人 |
| ORG_BIL_ID | 业务单ID |
| ORG_BIL_NO | 业务单号 |
| OTH_BIL_NO | 第三方系统单号 |
| PICK_POINT | 拣选点（多个站点用;隔开） |
| PRIORITY | 排单等级(0.低 1.中 2.高) |
| PRT_DATE | 打印时间 |
| PRT_USR | 打印人员 |
| RECEI_AREA | 收货区域 |
| REF_ID | 第三方系统标识 |
| REM | 备注 |
| SAL_NAME_ERP | ERP业务员名称 |
| SAL_NO | 经办人 |
| SAL_NO_ERP | ERP业务员代号 |
| STATUS_PG | 派工状态(0.未派工 1.已派工) |
| SYS_DATE | 制单时间 |
| TYPE_ID | 业务类型 |
| TZ_DD | 单据日期 |
| TZ_NO | 通知单号 |
| UP_DD | 变更时间 |
| USR | 制单人 |
| USR_PK | 拣货员 |
| WH1 | 出库仓库代号 |
| WH2 | 入库仓库代号 |
| WORK_STATION | 拣选工作站台（多个站台用;隔开） |

## MF_IZ (到货确认单)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_KND | 单据类型 |
| BIL_TYPE | 单据类别 |
| CON_NO | 货主编码 |
| DEP | 部门代号 |
| IC_NO | 调拨单号 |
| IZ_DD | 到货日期 |
| IZ_NO | 到货单号 |
| LK_ID | 立库单据标记 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| REM | 备注 |
| SAL_NO | 经办人 |
| SYS_DATE | 输单日期 |
| TYPE_ID | 业务类型 |
| USR | 制单人 |

## MF_JGZY (加工转移单)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_IN | 卸货点 |
| AREA_OUT | 取货点 |
| CALL_AGV | 呼叫AGV |
| CONTAIN_CODE_IN | 转入容器 |
| CONTAIN_CODE_OUT | 转出容器 |
| QTY | 转移数量 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| WH_IN | 入库仓库 |
| ZY_DD | 转移日期 |
| ZY_NO | 转移单号 |

## MF_JHRW (波次拣货任务单表头)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO_OUT | 立库出库处理代号 |
| AREA_NO | AGV站点代号 |
| AREA_SH | 收货点 |
| BC_NO | 波次单号 |
| BY_ORDER | 依订单发货标记 |
| CANCEL_ID | 作废标记 |
| CAR_NO | 待入车号 |
| CHUW | 储位代号 |
| CLS_ID | 拣货结案标记 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| CPY_SW | 拷贝注记 |
| DEP | 部门代号 |
| FHYJ_FIELD | 发货依据栏位 |
| JR_DD | 波次拣货任务日期 |
| JR_NO | 波次拣货任务单号 |
| JR_NO_NEW | 新任务单号 |
| LINE_CODE | 产线代号 |
| LINE_NO | 站台代号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PICK_POINT | 拣选点（多个站点用;隔开） |
| PK_FLOW | 拣货流程(1、总拣-分拣 2、直接拣货) |
| PR_NO | 初盘任务单号 |
| PRIORITY | 排单等级(0.低 1.中 2.高) |
| PRIORITY_WCS | WCS优先级(1.紧急 2.特急) |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| RECEI_AREA | 收货区域 |
| REM | 备注 |
| SAL_NO | 经办人 |
| SEND_ACTION | 需发送任务(T.未发送 F.已发送) |
| STATUS_PG | 派工状态(0、未派工 1、已派工) |
| SYS_DATE | 输单日期 |
| TYPE_CK | 出库类型(1、整盘出库 2、拣选出库) |
| TYPE_ID | 业务类型 |
| USR | 制单人 |
| USR_PK | 拣货员 |
| WH | 仓库代号 |
| WH_TYPE | 启用立库接口(1、不启用 2、启用) |
| WORK_STATION | 拣选工作站台（多个站台用;隔开） |
| YC_TYPE | 异常类型(1.整托换料 2.缺料登记) |
| ZONE_ID | 区域代号 |

## MF_JT (拣货退回单表头)

| 字段名 | 中文说明 |
|--------|---------|
| AUTO_ID | 自动产生标记 |
| BIL_ID | 转入单ID |
| BIL_NO | 转入单号 |
| CK_FLOW | 出库流程标记(BC 为波次流程，JH 为直接拣货流程) |
| CK_NO | 出库确认单号 |
| CON_NO | 货主编码 |
| CPY_SW | 拷贝注记 |
| DEP | 部门代号 |
| JT_DD | 退回日期 |
| JT_NO | 拣货退回单号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PK_ID | 拣货ID |
| PK_NO | 拣货单号 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| REM | 备注 |
| SAL_NO | 经办人 |
| SYS_DATE | 输单日期 |
| TYPE_ID | 业务类型 |
| USR | 制单人 |
| WMS_ID | WMS产生的单据 |

## MF_KRQCL (空容器处理)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_DH_DD | 实际到达时间 |
| ACT_NO | 立库任务 |
| AREA_FH | 发货点 |
| AREA_SH | 收货点 |
| CL_DD | 处理日期 |
| CL_NO | 处理单号 |
| QTY | 数量 |
| REM_SH | 收货区域 |
| SQ_NO | 申请单号 |
| SYS_DATE | 制单时间 |
| USR | 处理人 |
| WH_FH | 发货仓库 |
| WH_SH | 发货仓库 |

## MF_KRQSQ (空容器申请)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_SH | 收货点 |
| CLS_BIL_ID | 结案单据ID |
| CLS_ID | 结案标记 |
| FH_TYPE | 发货方式(1:配送、2:人工配送) |
| QTY | 申请数量 |
| QTY_FH | 发出数量 |
| REM_SH | 收货区域 |
| SQ_DD | 申请日期 |
| SQ_NO | 申请单号 |
| SYS_DATE | 申请时间 |
| USR | 申请人 |
| WH_FH | 发出仓库 |
| WH_SH | 收货仓库 |

## MF_KU (盘亏单-表头)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO_PUSH | 推送ERP任务代号 |
| BIL_TYPE | 单据类别 |
| CFM_DATE | 确认时间 |
| CFM_SW | 启用单据确认 |
| CFM_USR | 确认人 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| CPY_SW | 拷贝注记 |
| DEP | 部门代号 |
| IJ_REASON | 调整原因 |
| KU_DD | 单据日期 |
| KU_NO | 盘亏单号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| OTH_BIL_NO | 第三方系统单号 |
| PD_NO | 盘点单号 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| REF_ID | 第三方系统标识 |
| REM | 备注 |
| SAL_NO | 经办人 |
| SYS_DATE | 输单日期 |
| TYPE_ID | 业务类型 |
| USR | 制单人 |
| WH | 仓库代号 |
| WMS_ID | WMS产生的单据 |

## MF_MJBF (模具/工装报废单表头)

| 字段名 | 中文说明 |
|--------|---------|
| BF_DD | 报废日期 |
| BF_NO | 报废单号 |
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单号 |
| DEP | 部门代号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| WH | 仓库代号 |

## MF_MJGH (模具/工装归还单表头)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 部门代号 |
| GH_DD | 归还日期 |
| GH_NO | 归还单号 |
| GS_NO | 归还申请单号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| RC_NO | 任务处理单号 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| WH | 仓库代号 |

## MF_MJGHSQ (模具/工装归还申请单表头)

| 字段名 | 中文说明 |
|--------|---------|
| CANCEL_ID | 作废标记 |
| CLS_ID | 结案标记 |
| CONTAIN_CODE | 容器条码 |
| DEP | 部门代号 |
| GS_DD | 归还申请日期 |
| GS_NO | 归还申请单号 |
| LY_NO | 领用单号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| WH | 仓库代号 |

## MF_MJLY (模具/工装领用单表头)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 部门代号 |
| LS_NO | 领用申请单号 |
| LY_DD | 领用日期 |
| LY_NO | 领用单号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| RC_NO | 任务处理单号 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| WH | 仓库代号 |

## MF_MJLYSQ (模具/工装领用申请单表头)

| 字段名 | 中文说明 |
|--------|---------|
| CANCEL_ID | 作废标记 |
| CLS_ID | 结案标记 |
| CONTAIN_CODE | 容器条码 |
| DEP | 部门代号 |
| LS_DD | 领用申请日期 |
| LS_NO | 领用申请单号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| WH | 仓库代号 |

## MF_MJRK (模具/工装入库单表头)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 部门代号 |
| MI_DD | 入库日期 |
| MI_NO | 入库单号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| RC_NO | 任务处理单号 |
| RQ_ADD_FLAG | 容器追加标记 |
| RS_NO | 入库申请单号 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| WH | 仓库代号 |

## MF_MJRKSQ (模具/工装入库申请单表头)

| 字段名 | 中文说明 |
|--------|---------|
| CANCEL_ID | 作废标记 |
| CLS_ID | 结案标记 |
| CONTAIN_CODE | 容器条码 |
| DEP | 部门代号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| RS_DD | 入库申请日期 |
| RS_NO | 入库申请单号 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| WH | 仓库代号 |

## MF_MJRW (模具/工装任务单表头)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单号 |
| CONTAIN_CODE | 容器条码 |
| DEP | 部门代号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| MR_DD | 任务日期 |
| MR_NO | 任务单号 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| RC_NO | 任务处理单号 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| WH | 仓库代号 |
| XJ_FLAG | 下架标记 |

## MF_MJRWCL (模具/工装任务处理单表头)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 生成单据别 |
| BIL_NO | 生成单号 |
| CONTAIN_CODE | 容器条码 |
| DEP | 部门代号 |
| MAX_COUNT | 容器最大可存放数量 |
| MR_NO | 任务单号 |
| RC_DD | 处理日期 |
| RC_NO | 任务处理单号 |
| RC_TYPE | 任务类型（LY领用出库、GH归还入库、BH补货入库） |
| RESULT_CL | 处理结果(S.保存 C.取消) |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| WH | 仓库代号 |

## MF_NL (挪料单表头)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_TYPE | 单据类别 |
| CON_NO | 货主编码 |
| DEP | 部门 |
| NL_DD | 挪料日期 |
| NL_NO | 挪料单号 |
| REM | 备注 |
| SYS_DATE | 制单日期 |
| USR | 制单人 |

## MF_PACKAGE (出库包装单)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_TYPE | 单据类别 |
| CK_NO | 出库单号 |
| CON_NO | 货主编码 |
| CUS_NAME | 客户名称 |
| CUS_NO | 客户代号 |
| DEP | 来源单部门 |
| ERP_BIL_NO | 出库业务单号 |
| PACKAGE_DD | 包装日期 |
| PACKAGE_NO | 包装码 |
| REM | 备注 |
| SYS_DATE | 包装时间 |
| TYPE_ID | 业务类型 |
| USR | 包装人员 |

## MF_PD (盘点单表头)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO | 出库处理代号 |
| AUTO_ID | 是否自动化仓库 |
| CFM_DATE | 确认时间 |
| CFM_SW | 启用单据确认 |
| CFM_USR | 确认人 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| CONTAIN_CODE_IN | 转入容器条码 |
| CUS_NO | 厂商代号 |
| DEP | 部门 |
| DIFF_CL | 差异处理(立即处理/复盘) |
| FP_TYPE | 复盘方式(容器复盘/复盘差异储位/复盘差异货品) |
| IJ_NO | 调整调增单号 |
| IJ_NO1 | 调整调减单号 |
| MODIFY_DD | 修改日期 |
| MODIFY_MAN | 修改人 |
| PD_DD | 盘点日期 |
| PD_DD1 | 帐载日期 |
| PD_NO | 盘点单号 |
| PD_TYPE | 盘点方式(1:按储位盘点,2按储位+货品盘点) |
| PR_NO_FP | 初盘任务单号(复盘) |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| REM | 备注 |
| SUM_ID | 汇总推送否 |
| SYS_DATE | 输单日期 |
| TASK_TYPE | 任务类型(循环盘点/按区域盘点/复盘/盘点) |
| TMCY_FLAG | 条码差异处理 |
| USR | 制单人 |
| USR_PD | 盘点人员 |
| WH | 仓库代号 |

## MF_PDRW (盘点任务单表头)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO_OUT | 立库出库处理代号 |
| AUTO_ID | 是否自动化仓库 |
| CHUW | 储位代号 |
| CLS_ID | 初盘结案 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| CUS_NO | 厂商代号 |
| DEP | 部门 |
| DIFF_CL | 差异处理(立即处理/复盘) |
| FP_TYPE | 复盘方式(容器复盘/复盘差异储位/复盘差异货品) |
| MODIFY_DD | 修改日期 |
| MODIFY_MAN | 修改人 |
| PD_NO | 盘点单号 |
| PD_TYPE | 盘点方式(1:按储位盘点,2按储位+货品盘点) |
| PR_DD | 任务日期 |
| PR_NO | 任务单号 |
| PR_NO_MAIN | 关联单号 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| REM | 备注 |
| SYS_DATE | 输单日期 |
| TASK_TYPE | 任务类型(循环盘点/按区域盘点/复盘/盘点) |
| USR | 制单人 |
| USR_PD | 初盘人员 |
| WH | 仓库代号 |

## MF_PK (拣货单表头)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 转入单据别 |
| BIL_NO | 转入单号 |
| CK_FLOW | 出库流程标记(BC 为波次流程，JH 为直接拣货流程) |
| CK_NO | 出库单号 |
| CLS_ID_TY | 检验结案标记 |
| CON_NO | 货主编码 |
| CPY_SW | 拷贝注记 |
| DEP | 拣货部门 |
| JT_NO | 拣货退回单号 |
| LINE_CODE | 产线代号 |
| MODIFY_DD | 最后修改日期 |
| MODIFY_MAN | 最后修改人 |
| NL_NO | 挪料单号 |
| OTH_BIL_NO | 第三方系统单号 |
| PICK_POINT | 拣选点（多个站点用;隔开） |
| PK_DD | 拣货日期 |
| PK_NO | 拣货单号 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| RECEI_AREA | 收货区域 |
| REF_ID | 第三方系统标识 |
| REM | 备注 |
| SAL_NO | 经办人 |
| SYS_DATE | 制单时间 |
| TY_SYS | 检验系统(ERP/WMS) |
| TYPE_ID | 业务类型 |
| USR | 制单人 |
| WMS_ID | WMS产生的单据 |
| WORK_STATION | 拣选工作站台（多个站台用;隔开） |

## MF_PKFJ (分拣单表头)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 转入单ID |
| BIL_NO | 转入单号 |
| BIL_NO_NEW | 新任务单号 |
| CK_NO | 出库单号 |
| CLS_ID_TY | 检验结案标记 |
| CON_NO | 货主编码 |
| CPY_SW | 拷贝注记 |
| DEP | 部门 |
| FJ_DD | 分拣日期 |
| FJ_NO | 分拣单号 |
| JT_NO | 拣货退回单号 |
| LINE_CODE | 产线代号 |
| MODIFY_DD | 最近修改时间 |
| MODIFY_MAN | 最近修改人 |
| PICK_POINT | 拣选点（多个站点用;隔开） |
| PR_NO | 初盘任务单号 |
| PRT_DATE | 打印时间 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| RB_FLAG | 自动回收标记 |
| RECEI_AREA | 收货区域 |
| REM | 备注 |
| SAL_NO | 经办人 |
| SYS_DATE | 制单时间 |
| TY_SYS | 检验系统(ERP/WMS) |
| TYPE_ID | 业务类型 |
| USR | 制单人 |
| WMS_ID | WMS产生的单据 |
| WORK_STATION | 拣选工作站台（多个站台用;隔开） |
| YC_TYPE | 异常类型(1.整托换料 2.缺料登记) |

## MF_PS (AGV 配送单表头)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_DH_DD | 实际到货时间 |
| AUTO_ID | 自动产生标记 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| END_POINT | 终点 |
| LK_RW | 立库任务代号 |
| PS_NO | 配送单号 |
| RACK_CODE | 货架码 |
| START_POINT | 起点 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |

## MF_PSRW (AGV配送任务单)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_DH_DD | 实际到货时间 |
| BIL_ID | 来源单 ID |
| BIL_ITM | 来源单项次 |
| BIL_NO | 来源单号 |
| CON_NO | 货主编码 |
| DEST_SITE | 目的地站点 |
| EST_DH_DD | 预计到货时间 |
| PR_NO | 配送任务单号 |
| PS_FLAG | 已配送 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| WH | 仓库 |

## MF_QJRW (请检任务单)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO_CK | 立库出库处理代号 |
| BIL_KND | 单据类型(RK入库类型;CK出库检验;KC库存检验) |
| BIL_TYPE | 单据类别 |
| CLS_ID_TY | 检验结案标记 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| DEP | 部门 |
| IC_TYPE | 调拨方式(1拣货后调拨;2直接调拨) |
| MODIFY_DD | 修改时间 |
| MODIFY_MAN | 修改人 |
| PRT_DATE | 打印时间 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| QJ_DD | 单据日期 |
| QJ_NO | 请检任务单号 |
| REM | 备注 |
| SYS_DATE | 制单时间 |
| TN_NO | 调拨通知单号 |
| TYWZ | 检验位置(1库内;2库外检验) |
| USR | 制单人 |
| WH_TY | 检验仓库 |
| XJ_FLAG | 下架标记 |

## MF_QRY (自定义报表表头)

| 字段名 | 中文说明 |
|--------|---------|
| CHK_MAN | 审核人 |
| CLS_DATE | 终审时间 |
| ID | 代号 |
| NAME | 名称 |
| SQL | SQL语句 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| USRS | 可运行用户 |

## MF_RACK_TYPE (移动货架类型-表头)

| 字段名 | 中文说明 |
|--------|---------|
| CODE_PREFIX | 编码前缀 |
| NAME | 类型名称 |
| QTY_WEIGHT | 货位限重重量 |
| RACK_TYPE | 类型代号 |
| RQ_TYPE_RNG | 容器类型范围 |
| STOP_ID | 停用标记 |
| UNIT_WEIGHT | 重量单位 |
| WL_MODE | 限重方式(1.不管制 2.按货位管制) |

## MF_RACK (移动货架-货架码)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_NO | 站点 |
| RACK_CODE | 货架码 |
| RACK_TYPE | 类型代号 |
| SYS_DATE | 录入时间 |

## MF_REMVOE_RULE (入库条码拆码规则表头)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_REMOVE_RULE | 已选规则种类 |
| BAR_TYPE | 条码类型 |
| BY_SEGMENT | 依所在分段设置（T/F） |
| CHK_DFT | 默认规则 |
| CODE_LENGTH | 总码长 |
| CODE_TYPE | 编码方式 |
| CON_NO | 货主编码 |
| CUSTOM_CODE | 自定义代码 |
| NO_SEPARATOR | 前缀与种类之间不带分隔符（T/F） |
| RULE_NAME | 规则名称 |
| RULE_NO | 规则代号 |
| SEGMENT_RULE | 分段规则 |
| SEPARATOR | 分隔符 |
| TEST_BARCODE | 范例条码 |

## MF_RK (入库单表头)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO | 处理代号 |
| AUTO_ID | 自动产生标记 |
| BIL_ID | 转入单ID |
| BIL_NO | 转入单号 |
| BIL_TYPE | 单据类别 |
| BIL_TYPE_ID | 单据类型 |
| CE_NO | 收货单号 |
| CJ_NO | 车间入库单号 |
| CON_NO | 货主编码 |
| CPY_SW | 拷贝注记 |
| CUS_NAME | 厂商名称 |
| CUS_NO | 厂商代号 |
| DEP | 部门代号 |
| ERP_AP_ID | ERP申请单ID |
| ERP_AP_NO | ERP申请单号 |
| ERP_GEN_METHOD | ERP库存单据生成方式(1.直接新增 2.单据确认) |
| EXT_SYS_FLAG | 外部系统标识 |
| EXT_SYS_ID | 外部系统单据ID |
| EXT_SYS_NO | 外部系统单号 |
| FINAL_PROC | 末道工序 |
| GEN_ERP_BIL_ID | 生成ERP入库单据别 |
| IC_BIL_KND | 调拨单据类型 |
| IS_MATCH | 入库单汇总推送给ERP |
| MMCLS_ACT_NO | 终审缴库单ACT_NO |
| MO_NO | 制令单号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| NL_NO | 挪料单号 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| QC_FLAG | 期初标记 |
| REM | 备注 |
| RK_DD | 入库日期 |
| RK_NO | 入库单号 |
| SAL_NO | 经办人 |
| SCTZ_NO | 生产通知单号 |
| STATUS_JY | 检验状态(空或T合格、F不合格、W待检、不合格转报废) |
| SYS_DATE | 输单日期 |
| TW_HAS_MO | 有制令来源的托工单(T/F) |
| TYPE_ID | 业务类型 |
| USR | 制单人 |
| WH | 仓库代号 |
| WMS_ID | WMS产生的单据 |
| ZC_NO | 制程代号 |

## MF_RKTZ_CHG (入库通知变更单表头)

| 字段名 | 中文说明 |
|--------|---------|
| CHG_DD | 变更日期 |
| CHG_NO | 变更单号 |
| CHG_TYPE | 变更类型（1码，变更表头资料、变更表身资料） |
| CON_NO | 货主编码 |
| DEP | 部门代号 |
| EXE_DATE | 执行时间 |
| EXE_STATUS | 执行状态（待执行/已执行） |
| EXE_USR | 执行人 |
| GEN_BIL_ID | 产生ERP单据别 |
| GEN_BIL_NO | 产生ERP单据号码 |
| OTH_BIL_NO | 第三方系统单号 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| REF_ID | 第三方系统标识 |
| REM | 备注 |
| SYS_DATE | 制单时间 |
| TYPE_ID | 业务类型 |
| USR | 制单人 |

## MF_RKTZ_CHGH (入库通知变更单-变更表头资料)

| 字段名 | 中文说明 |
|--------|---------|
| CHG_METHOD | 变更方式 |
| CHG_NO | 变更单号 |
| ITM | 项次 |
| REASON | 变更原因说明 |
| TZ_NO | 通知单号 |

## MF_RKTZ (入库通知单表头)

| 字段名 | 中文说明 |
|--------|---------|
| AUTO_GET_BILL | 非ERP推送/自动获取单据 |
| AUTO_ID | 自动产生标记 |
| BIL_ID | 来源单ID |
| BIL_NO | 来源单号 |
| BIL_TYPE | 单据类别 |
| CE_NO | 收货单号 |
| CL_ID | 自动结案单据 ID(作废) |
| CLS_ID | 入库结案标记 |
| CLS_ID_MANUAL | 手工结案标记 |
| CON_NO | 货主编码 |
| CPY_SW | 拷贝注记 |
| CUS_NAME | 厂商名称 |
| CUS_NO | 厂商代号 |
| DEP | 部门代号 |
| DEP_ERP | ERP 部门代号 |
| DEP_NAME_ERP | ERP 部门名称 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_NO | ERP申请单号 |
| ERP_GEN_METHOD | ERP库存单据生成方式(1.直接新增 2.单据确认) |
| EST_DD | 预入库日 |
| FINAL_PROC | 末道工序 |
| GEN_ERP_BIL_ID | 生成ERP入库单据别 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| OTH_BIL_ID | 外部系统单据ID |
| OTH_BIL_NO | 外部系统单号 |
| OTH_ID | 外部系统标识 |
| OTH_MODIFY_DD | 第三方最近修改时间 |
| OTH_SYS_DATE | 第三方制单时间 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| REM | 备注 |
| SAL_NAME_ERP | ERP 业务员名称 |
| SAL_NO | 经办人 |
| SAL_NO_ERP | ERP 业务员代号 |
| SYS_DATE | 输单日期 |
| TW_HAS_MO | 有制令来源的托工单(T/F) |
| TYPE_ID | 业务类型 |
| TZ_DD | 通知日期 |
| TZ_NO | 入库通知单号 |
| UP_DD | 变更时间 |
| USR | 制单人 |
| WH | 仓库代号 |

## MF_RPT (报表类型设定)

| 字段名 | 中文说明 |
|--------|---------|
| CHK_MENU | 以菜单显示 |
| CHK_USRS | 可查看用户 |
| ITM | 项次 |
| NAME_BIG5 | 繁体名称 |
| NAME_ENG | 英文名称 |
| NAME_GB | 简体名称 |
| PGM | PGM |
| SYS_DATE | 制单时间 |
| USR | 制单人 |

## MF_SBPG_CHG (派工单变更记录表)

| 字段名 | 中文说明 |
|--------|---------|
| CHANGE_DD | 变更时间 |
| CHANGE_MAN | 变更人 |
| CHANGE_NO | 变更单号 |
| CUR_QTY_FIN | 当前已完工量 |
| PG_NO | 派工单号 |
| STATUS_AF | 变更后接料状态 |
| STATUS_BF | 变更前接料状态 |

## MF_SBPG (设备派工单-表头)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO | 呼叫空箱任务代号 |
| AGV_SITE | AGV站点 |
| AUTO_CLS_ID | 自动结案标记 |
| AUTO_REC | 自动接料 |
| BIL_ID | 来源单单据别 |
| BIL_NO | 来源单号 |
| CLOSE_ID | 结案标记 |
| CONTAIN_CODE | 容器条码 |
| DEP | 部门 |
| HW_NO | 设备代号 |
| MODIFY_DD | 最近修改时间 |
| MODIFY_MAN | 最近修改人 |
| PG_DD | 派工日期 |
| PG_NO | 派工单号 |
| PRT_DATE | 打印日期 |
| PRT_USR | 打印人 |
| QTY_BCJL | 本次接料数量 |
| QTY_MMJS | 每模件数 |
| QTY_MXJS | 每箱件数 |
| QTY_MXJS_YU | 预估每箱件数 |
| REM | 备注 |
| STATE_NO | 工作状态(0未开工1已开工2呼叫空箱3开始接料4已接料5已完工6异常 |
| SYS_DATE | 输单时间 |
| TYPE_NO | 设备类型 |
| USR | 制单人 |

## MF_SCTLJH (生产投料计划单-表头)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_NO | 投料口站点 |
| BAT_NO | 批号 |
| BIL_ID | 转入单据别 |
| BIL_NO | 转入单号 |
| CLS_ID | 完工结案 |
| CLS_ID_MANUAL | 手工结案标记 |
| CON_NO | 货主编码 |
| DEP | 部门代号 |
| OPN_DD | 开工时间 |
| PG_DD | 派工时间 |
| PRD_MARK | 成品特征 |
| PRD_NO | 成品代号 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| RECHECK_WEIGHT | 人工复核称重 |
| REM | 备注 |
| SO_NO | 受订单号 |
| STATUS_OPN | 开工状态(0.未开工 1.已开工) |
| STATUS_PG | 派工状态(0.未派工 1.已派工 2.异常) |
| SYS_DATE | 制单时间 |
| TL_DD | 计划投料日期 |
| TL_NO | 计划投料单号 |
| USR | 制单人 |
| USR_PG | 派工人员 |
| WH | 仓库代号 |

## MF_SH (收货单表头)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_SH | 收货点 |
| BIL_ID | 转入单ID |
| BIL_NO | 转入单号 |
| BIL_TYPE | 单据类别 |
| CLS_ID_TY | 检验结案标记 |
| CON_NO | 货主编码 |
| CUS_NAME | 厂商名称 |
| CUS_NO | 厂商代号 |
| DEP | 部门 |
| JY_FLAG | 检验标志(T 检验/F 免检) |
| MODIFY_DD | 修改时间 |
| MODIFY_MAN | 修改人 |
| OPERA_MODE | 单据操作模式（1 为转入模式、2 扫描模式） |
| OTH_BIL_NO | 外部系统单号 |
| OTH_ID | 转入单外部系统标识 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| REM | 备注 |
| SAL_NO | 收货人 |
| SH_DD | 收货日期 |
| SH_NO | 收货单号 |
| SYS_DATE | 制单时间 |
| TW_HAS_MO | 有制令来源的托工单(T/F) |
| TY_PUSH_SW | 检验单推送SUNLIKE标记(T/F) |
| TY_SYS | 检验系统(ERP/WMS) |
| TYPE_ID | 业务类型 |
| TZ_NO | 产生入库通知单号 |
| USR | 制单人 |
| WH | 仓库 |
| WH_ERP | ERP仓库 |
| WMS_ID | WMS产生的单据 |

## MF_SQ (车间收料确认单-表头)

| 字段名 | 中文说明 |
|--------|---------|
| SQ_DD | 单据日期 |
| SQ_NO | 车间收料确认单 |
| SQ_USR | 收料员 |
| SYS_DATE | 收料时间 |
| USR | 制单人 |

## MF_TY (检验单)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 转入单据别 |
| BIL_KND | 单据类型(RK入库检验、CK出库检验、KC库存检验) |
| BIL_NO | 转入单号 |
| BIL_TYPE | 单据类别 |
| CLS_ID_SPC | 不合格结案 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| CUS_NAME | 客户/厂商名称 |
| CUS_NO | 客户 |
| DEP | 部门 |
| MODIFY_DD | 修改时间 |
| MODIFY_MAN | 修改人 |
| OTH_BIL_NO | 第三方系统单号 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| REF_ID | 第三方系统标识 |
| REM | 备注 |
| SYS_DATE | 制单时间 |
| TW_HAS_MO | 有制令来源的托工单(T/F) |
| TY_DD | 检验时间 |
| TY_NO | 检验单号 |
| TY_PUSH_SW | 检验单推送SUNLIKE标记(T/F) |
| TYPE_ID | 业务类型 |
| TYWZ | 检验位置(1库内、2库外) |
| TZ_NO_TN1 | 调拨通知单（库外） |
| TZ_NO_TN2 | 调拨通知单（不良品） |
| TZ_NO_UO | 入库通知单号 |
| USR | 制单人 |

## MF_WR (报工单表头)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| DEP | 机台 |
| END_DATE | 结束日期 |
| ID_NO | 配方号 |
| MO_DD | 制令日期 |
| MO_ITM | 制令项次 |
| MO_NO | 制令单号 |
| MRP_NO | 成品代号 |
| NOTZC_FLAG | 非制程报工 |
| OTH_BIL_ID | 外部单据ID |
| OTH_BIL_NO | 外部单号 |
| PG_ITM | 派工单项次 |
| PG_NO | 派工单号 |
| PRD_MARK | 特征 |
| QTY | 数量 |
| QTY_LOST | 不合格量 |
| QTY_LOST_GF | 不合格-工废数量 |
| QTY_LOST_LF | 不合格-料废数量 |
| QTY1 | 数量(副) |
| QTY1_LOST | 不合格量(副) |
| QTY1_LOST_GF | 不合格-工废数量（副） |
| QTY1_LOST_LF | 不合格-料废数量（副） |
| REM | 备注 |
| SPC_NO | 不合格原因 |
| STA_DATE | 开工日期 |
| SYS_DATE | 制单时间 |
| TIME_USED | 工时 |
| TYPE_ID | 业务类型 |
| TZ_NO | 通知单号 |
| UNIT | 单位 |
| USR | 制单人 |
| WMS_ID | WMS产生的单据 |
| WR_DD | 报工日期 |
| WR_NO | 报工单号 |
| ZC_NO | 制程代号 |

## MF_XJRW (直接拣货任务单表头)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO_OUT | 立库出库处理代号 |
| AREA_NO | AGV站点代号 |
| CHUW | 储位代号 |
| CLS_ID | 拣货结案标记 |
| CLS_ID_MANUAL | 手工结案标记 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| DEP | 部门代号 |
| LINE_CODE | 产线代号 |
| LINE_NO | 站台代号 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| PICK_POINT | 拣选点（多个站点用;隔开） |
| PRIORITY_WCS | 优先级(1.紧急 2.特急) |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人员 |
| RECEI_AREA | 收货区域 |
| REM | 备注 |
| SEND_ACTION | 需发送任务(T.未发送 F.已发送) |
| SYS_DATE | 输单时间 |
| TYPE_CK | 出库类型(1、整盘出库 2、拣选出库) |
| TYPE_ID | 业务类型 |
| USR | 制单人 |
| WH | 仓库代号 |
| WORK_STATION | 拣选工作站台（多个站台用;隔开） |
| XJ_FLAG | 下架标记 |
| XR_DD | 单据日期 |
| XR_NO | 直接拣货任务单号 |
| XR_NO_MAIN | 关联单号 |

## MF_YB (验收退回单)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_KND | 单据类型(RK入库检验、CK出库检验、KC库存检验) |
| BIL_TYPE | 单据类别 |
| CON_NO | 货主编码 |
| CUS_NAME | 厂商名称 |
| CUS_NO | 厂商代号 |
| DEP | 部门 |
| MODIFY_DD | 修改时间 |
| MODIFY_MAN | 修改人 |
| PRT_DATE | 打印时间 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| REM | 备注 |
| SYS_DATE | 制单时间 |
| TY_NO | 检验单号 |
| TY_PUSH_SW | 检验单推送SUNLIKE标记(T/F) |
| TYPE_ID | 业务类型 |
| USR | 制单人 |
| YB_DD | 单据日期 |
| YB_NO | 验收退回单号 |

## MF_YN (盘盈单-表头)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO_PUSH | 推送ERP任务代号 |
| BIL_TYPE | 单据类别 |
| CFM_DATE | 确认时间 |
| CFM_SW | 启用单据确认 |
| CFM_USR | 确认人 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| CPY_SW | 拷贝注记 |
| DEP | 部门代号 |
| IJ_REASON | 调整原因 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| OTH_BIL_NO | 第三方系统单号 |
| PD_NO | 盘点单号 |
| PRT_DATE | 打印日期 |
| PRT_SW | 打印注记 |
| PRT_USR | 打印人 |
| REF_ID | 第三方系统标识 |
| REM | 备注 |
| SAL_NO | 经办人 |
| SYS_DATE | 输单日期 |
| TYPE_ID | 业务类型 |
| USR | 制单人 |
| WH | 仓库代号 |
| YN_DD | 单据日期 |
| YN_NO | 盘盈单号 |

## MJ_BAT_HIS (灭菌批号变更历史)

| 字段名 | 中文说明 |
|--------|---------|
| CHANGE_DD | 变动时间 |
| CHANGE_NO | 变动单号 |
| CHANGE_USR | 变动人 |
| CONTAIN_CODE | 容器条码 |
| SBAT_NO | 灭菌批号 |

## MJ_RESET_HIS (重开模具/工装记录表)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_NO | 模具/工装序号 |
| FIN_DD | 要求完成日期 |
| GH_NO | 归还单号 |
| HIS_NO | 序号 |
| SYS_DATE | 制单时间 |
| USR | 制单人 |
| VER_INFO | 版本信息 |
| WH | 仓库代号 |
| XD_ID | 是否下单 |
| ZD_DD | 转单日期 |
| ZD_ID | 是否转单 |

## MK_YG (作业人员表)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据别 |
| BIL_NO | 单号 |
| ITM | 项次 |
| QTY | 数量 |
| QTY_LOST | 不合格量 |
| QTY1 | 数量(副) |
| QTY1_LOST | 不合格量(副) |
| SPC_NO | 不合格原因 |
| USE_TIME | 工时 |
| YG_NO | 作业人员 |

## MOULD_LY_REASON (模具/工装领用原因)

| 字段名 | 中文说明 |
|--------|---------|
| EXPLAIN | 领用说明 |
| LY_NO | 代号 |

## MY_W_CHK (库位关联库)

| 字段名 | 中文说明 |
|--------|---------|
| DRC |  |
| WH_DW | 下层库位 |
| WH_UP | 上层库位 |

## MY_WH_AREA (AGV站点设定表(非储位))

| 字段名 | 中文说明 |
|--------|---------|
| AREA_NO | AGV站点代号 |
| BIND_ID | 启用容器绑定 |
| CAR_NO | 拣货车号 |
| DEP | 部门代号 |
| GL | 列 |
| GS | 排 |
| HW_NO | 设备代号 |
| HW_NO2 | 设备代号2 |
| LAYER | 所属楼层 |
| LIFT_NO | 电梯编码 |
| LIMIT_HJ_TYPE | 限用货架类型 |
| LIMIT_RQ_TYPE | 限用容器类型 |
| LKHJ_TYPE | 储位位置(1.单深位 2.双深位外侧(靠近) 3.双深位里侧(远离) 4.三深位外侧 5.三深位中间 6.三深位里侧) |
| LOCK_CW | 锁定状态(1、正 常 2、锁定) |
| MAP_NO | 地图编号 |
| NAME | AGV站点名称 |
| PRIORITY_IN | 进站任务优先级代号 |
| PRIORITY_OUT | 出站任务优先级代号 |
| PTL_LABEL | PTL电子标签ID |
| PTL_LABEL2 | PTL电子标签ID2 |
| PTL_STATUS | PTL亮灯状态 |
| PTL_STATUS2 | PTL亮灯状态2 |
| RCS_TYPE | RCS站点类型(1.站点 2.仓位) |
| SEND_BIND | 向RCS发送仓位绑定任务 |
| STATUS_ID | 站点状态(1.正常 2.故障) |
| SYS_DATE | 录入时间 |
| TASK_TYPE | 任务类型(1.起点->终点 2.起点->拣货点->终点) |
| TIER | 层 |
| TUNNEL_LABEL | 巷道灯电子标签ID |
| TUNNEL_LABEL2 | 巷道灯电子标签ID2 |
| TUNNEL_NO | 巷道号 |
| TYPE_ID | 类型(1.取货点 2.卸货点 3.盘点卸货点 4.取/卸货点 5.拣货点) |
| UP_DD | 变更时间 |
| USR | 录入人 |
| WH | 仓库代号 |
| ZONE_ID | 所属区域 |

## MY_WH_DEL (库位-删除库)

| 字段名 | 中文说明 |
|--------|---------|
| UP_DD | 上次修改时间 |
| WH | 库位 |

## MY_WH_ERP (ERP 仓库对照表)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| FLAG_DG | 是代管仓 |
| NAME | ERP 仓库名称 |
| UP_ERP | ERP 上层仓库 |
| WH | WMS 仓库代号 |
| WH_ERP | ERP 仓库代号 |

## MY_WH_PD (仓库盘点方式参数表)

| 字段名 | 中文说明 |
|--------|---------|
| PROP_NO | 参数名 |
| REM | 备注 |
| VALUE | 参数值 |
| WH | 仓库代号 |

## MY_WH_VIEW (仓储可视化图)

| 字段名 | 中文说明 |
|--------|---------|
| CHK_USRS | 可查看用户 |
| NAME | 名称 |
| STOP_ID | 停用否 |
| SVG_CONTENT | SVG内容 |
| SYS_ID | 标准档 |
| VW_NO | 代号 |

## MY_WH_ZONE (仓库区域表)

| 字段名 | 中文说明 |
|--------|---------|
| LINE_NO_PRC | 所属产线 |
| MAP_NO | 地图编号 |
| NAME | 区域名称 |
| OTH_ZONE_ID | 第三方区域代号 |
| RULE_ID_BC | 波次策略代号 |
| RULE_ID_PK | 拣货策略代号 |
| SHUTTLE_CFZLYJ | 穿梭式货架-巷道存放种类依据 |
| SHUTTLE_GS | 穿梭式货架-巷道规则(1.排+层 2.列+层) |
| SHUTTLE_SORT | 穿梭式货架-配位顺序 |
| WH | 仓库代号 |
| ZONE_ID | 区域代号 |

## MY_WH (库位库)

| 字段名 | 中文说明 |
|--------|---------|
| ALLOW_BHRQSJ | 启用备货容器上架管理 |
| ALLOW_KRQSJ | 空容器允许上架 |
| ALLOW_STATUS_JY | 允许上架的检验状态 |
| ATTRIB | 属性 |
| CAPACITY_TYPE | 储位容量管制方式 |
| CK_FLOW | 出库流程标记(BC 为波次流程，JH 为直接拣货流程) |
| CNT_MAN | 联系人代号 |
| CUS_NO | 客户代号 |
| CW_FLAG | 启用储位管理 |
| DEP | 部门代号 |
| DEPRO_NO | 部门群组代号 |
| FAX_NO | 传真 |
| FLAG_DG | 是代管仓 |
| FLAG_FKC | 负库存控制 |
| HJFL | 货架分类(1.非穿梭式 2.穿梭式 3.移动货架) |
| IC_TYPE | 调拨方式(1拣货后调拨、2直接调拨) |
| INVALID | 废品仓否 |
| KRQRK_CHUW_SORT | 空容器入库储位配位排序（非穿梭式） |
| LKIF_ID | 立库接口代号 |
| MAP_NO | 地图编号 |
| MODE_PG_PK | 拣货派工方式(1.按任务单派工 2.按通知单派工) |
| MULT_CW_BY | 多深位依据(GS:排 LAYER:层) |
| NAME | 名称 |
| NAME_PY | 助记码 |
| PD_MTH | 盘点方式(1.直接盘点 2.依任务盘点) |
| PK_FLOW | 拣货流程(1、总拣-分拣 2、直接拣货) |
| PTL_SW | 启用PTL电子标签拣货 |
| QTY_KEEP_CW | 预留空储位数 |
| REM | 备注 |
| RK_CHUW_SORT | 入库储位配位排序（非穿梭式） |
| RK_CHUW_SORT2 | 入库储位配位排序（穿梭式） |
| RK_FLOW | 入库流程(1.入库-上架、2.入库同时上架) |
| RQ_SXJ_MODE | 容器上下架方式(1.单容器上下架 2.多容器叠放上下架) |
| RULE_ID_BC | 波次策略代号 |
| RULE_ID_PK | 拣货策略代号 |
| RULE_ID_XJ | 下架策略代号 |
| SHUTTLE_AQYCF | 穿梭式货架-按区域设定存放规则 |
| SHUTTLE_CFZLYJ | 穿梭式货架-巷道存放种类依据 |
| SHUTTLE_GS | 穿梭式货架-巷道规则(1.排+层 2.列+层) |
| SHUTTLE_SORT | 穿梭式货架-配位顺序 |
| STOP_DD | 停用日期 |
| SYS_DATE | 输入日期 |
| TEL_NO | 电话 |
| TP_ID | 第三方标记 |
| TYWZ | 检验位置(1库内、2库外) |
| UP_DD | 上次更新时间 |
| UP_WH | 上层库位 |
| USR | 录入员 |
| WH | 仓库 |
| WH_TY | 检验仓库 |
| WH_TYPE | 上下架模式(1.手持PDA模式 2.自动化接口模式 3.叉车任务模式) |
| WMS_ID | WMS接管仓库标记 |
| XJ_BILL_COUNT | 依来源单配货-分组单据容量 |
| XJ_GROUP_COND | 依来源单配货-自动分组条件 |
| XJ_KCBZCL | 依来源单配货-库存不足处理 |
| XJ_PWCKYJ | 依来源单配货-配位仓库依据(1.下属仓库 2.指定仓库) |
| XJ_WHS | 依来源单配货-指定仓库 |

## NOTICE_MSG (即时消息表)

| 字段名 | 中文说明 |
|--------|---------|
| CONTENT | 消息内容 |
| MSG_KND | 消息类型(1普通 2.重要 3.警报) |
| MSG_NO | 消息代号 |
| PARAMS | 参数 |
| PGM | 程序代号 |
| READ_DD | 阅读时间 |
| READ_FLAG | 已读否 |
| RECEIVER | 接受人 |
| SEND_DD | 发送时间 |
| SEND_PC | 发送到PC |
| SEND_PDA | 发送到PDA |
| SENDER | 发送人 |
| TITLE | 标题 |
| TYPE_ID | 类型 |

## NOTICE_SET (消息通知设定)

| 字段名 | 中文说明 |
|--------|---------|
| SEND_OBJ | 发送对象 |
| SEND_TYPE | 接受方式 |
| SEND_USRS | 接受用户 |
| SET_NO | 代号 |
| STOP_ID | 停用否 |
| SYS_DATE | 制单时间 |
| TYPE_ID | 通知项目 |
| USR | 制单人 |
| WH | 仓库 |

## OUTER_BOX_CHANGE (外箱序列号变更表)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单号 |
| BOX_NO | 箱序列号 |
| CHANGE_DD | 变更时间 |
| CHANGE_ID | 变更ID |
| CHUW_CUR | 现储位 |
| CHUW_ORIG | 原储位 |
| CON_NO | 货主编码 |
| CONTAIN_CODE_CUR | 现容器条码 |
| CONTAIN_CODE_ORIG | 原容器条码 |
| ICCK_FLAG_CUR | 现调拨出库标记 |
| ICCK_FLAG_ORIG | 原调拨出库标记 |
| JH_FLAG_CUR | 现拣货标记 |
| JH_FLAG_ORIG | 原拣货标记 |
| PACKAGE_NO_CUR | 现包装码 |
| PACKAGE_NO_ORIG | 原包装码 |
| PH_FLAG_CUR | 现在途标记 |
| PH_FLAG_ORIG | 原在途标记 |
| QTY_CUR | 现货品数量 |
| QTY_ORIG | 原货品数量 |
| STOP_ID_CUR | 现停用标记 |
| STOP_ID_ORIG | 原停用标记 |
| TI_FLAG_CUR | 现送检标记 |
| TI_FLAG_ORIG | 原送检标记 |
| UNIQUE_CHANGE_ID | 唯一性ID |
| WH_CUR | 现仓库 |
| WH_ORIG | 原仓库 |

## PASSAGE_SET (堆垛机设备表(通道表))

| 字段名 | 中文说明 |
|--------|---------|
| DDJ_NO | 堆垛机代号 |
| NAME | 通道名称 |
| PASSAGE | 通道号 |
| STATUS_ID | 设备状态(1.正常 2.故障) |
| WH | 仓库代号 |

## PDA_BAR_BOXSJ (单据箱条码散件)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_CODE | 条码 |
| BAR_TYPE | 条码类型 |
| BIL_ID | 单据别 |
| BIL_ITM | 单据项次 |
| BIL_NO | 单据号码 |
| CON_NO | 货主编码 |
| ID | 唯一项次 |
| ITM | 项次 |
| QTY_PRD | 货品数量 |
| QTY_SCAN | 扫描数量 |
| QTY1_PRD | 货品数量(副) |

## PDA_BAR_COLLECT (单据条码明细表)

| 字段名 | 中文说明 |
|--------|---------|
| AUTO_BOX_SJ_FLAG | (自动)箱条码散件标志 |
| AUTO_BOX1_SJ_FLAG | (自动)外箱条码散件标志 |
| AUTO_CONTAIN_SJ_FLAG | (自动)容器散件标志 |
| BAR_CODE | 条码 |
| BAR_TYPE | 条码类型(0.序列号 1.箱序列号 2.条码 3.箱条码 4.物流容器) |
| BC_NO | 波次单号 |
| BIL_DD | 单据日期 |
| BIL_ID | 单据别 |
| BIL_ITM | 表身项次 |
| BIL_NO | 单据号码 |
| BIL_TYPE | 单据类别 |
| BOX_NO | 散件序列号的箱号 |
| BOX_NO_SUB | 散出的箱条码明细 |
| BOX_NO1 | 外箱 |
| BOX_SJ_FLAG | 箱条码散件标志 |
| BOX1_SJ_FLAG | 外箱条码散件标志 |
| CAR_NO | 车号 |
| CHK_BACKORIGRQ | 自动重装回原容器 |
| CHK_BOX1 | 外箱标记 |
| CHK_RBRQ | 自动回收 |
| CHUW1 | 储位1 |
| CHUW2 | 储位2 |
| CLIENT_ITEM | 条码项次 |
| CODE_BAT_NO | 虚拟条码批号 |
| CODE_PRD_MARK | 虚拟条码特征 |
| CODE_PRD_NO | 虚拟条码货品代号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 归属物流容器 |
| CONTAIN_CODE_BH | 备货物流容器 |
| CONTAIN_CODE_CKBH | 出库备货容器 |
| CONTAIN_SJ_FLAG | 容器散件标志 |
| CUS_NAME | 客户/厂商名称 |
| CUS_NO | 客户/厂商代号 |
| DEP | 部门代号 |
| FQ_NO | 分区编号 |
| ITM | 项次 |
| JH_TYPE | 拣货方式(1取出拣货量2不取出3取出余料量) |
| MATCH_ITM | 手工指定匹配项次 |
| PACKAGE_NO | 包装码 |
| PRC_ID | 不合格处理建议(1验收退回;2让步接收;3报废;4入不良品仓) |
| QTY_BOX1_ORIG | 外箱数量 |
| QTY_IMPERFECT | 残次料数量 |
| QTY_MISS | 缺料数量 |
| QTY_ORIG | 原箱数量 |
| QTY_PRD | 货品数量 |
| QTY_SCAN | 扫描数量 |
| QTY1_PRD | 货品数量(副) |
| SAL_NO | 业务员 |
| SCAN_TIME | 扫描时间 |
| SPC_NO | 不合格原因 |
| SPC_REM | 不合格备注 |
| STATUS_JY | 检验标记(空或T：合格、F：不合格、W：待检) |
| SYS_DATE | 制单日期 |
| UNIQUE_ID | 唯一性ID |
| USR | 制单人 |
| WH_SPC | 不良品仓库代号 |
| WH1 | 仓库1 |
| WH2 | 仓库2 |

## PDA_BAR_COLLECT1 (单据条码明细拆分表)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_CODE | 条码 |
| BAR_TYPE | 条码类型(0.序列号 1.箱序列号 2.条码 3.箱条码 4.物流容器) |
| BIL_ID | 单据别 |
| BIL_ITM | 表身项次 |
| BIL_NO | 单据号码 |
| BOX_NO | 箱码 |
| BOX_NO1 | 外箱码 |
| BOX_SJ_FLAG | 箱条码散件标志 |
| BOX1_SJ_FLAG | 外箱条码散件标志 |
| CAR_NO | 车号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 归属容器条码 |
| CONTAIN_CODE_BH | 备货物流容器 |
| CONTAIN_CODE_CKBH | 出库备货容器 |
| CONTAIN_SJ_FLAG | 容器散件标志 |
| ERP_BIL_NO | 申请单号 |
| ID | 唯一主键 |
| ITM | 项次 |
| ORG_BIL_NO | 业务单号 |
| PACKAGE_NO | 包装码 |
| QTY_BOX1_ORIG | 外箱数量 |
| QTY_ORIG | 原箱数量 |
| QTY_PRD | 货品数量 |
| QTY1_PRD | 货品数量(副) |
| SOURCE_BILID | 来源单据别 |
| SOURCE_BILNO | 来源单号 |
| SOURCE_ITM | 来源单项次 |
| STATUS_JY | 检验标记(空或T：合格、F：不合格、W：待检) |
| TZ_ID | 通知单据别 |
| TZ_ITM | 通知单项次 |
| TZ_NO | 通知单号 |
| UNIQUE_ID | 唯一性ID |

## PDA_BAR_HIS (单据条码扫描历史)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_CODE | 条码 |
| BAR_TYPE | 条码类型 |
| BIL_ID | 单据别 |
| BIL_NO | 单据号码 |
| CLIENT_ITEM | 条码项次 |
| CON_NO | 货主编码 |
| ERROR | 错误信息 |
| FIN_TIME | 结束时间 |
| HANDLE | 操作 |
| ID | 唯一主键 |
| ITM | 项次 |
| JSON_CONTENT | JSON |
| QTY | 扫描数量 |
| QTY1 | 扫描数量(副) |
| SCAN_TIME | 扫描时间 |

## PDA_SAVE_TRACE (RN系统日志表)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据别 |
| BIL_NO | 单据号码 |
| CANT_DEL | 不可删除标记 |
| CON_NO | 货主编号 |
| CONTENT | 单据数据 |
| CUS_NAME | 客户名称 |
| CUS_NO | 客户 |
| DEP | 部门代号 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_NO | ERP申请单号 |
| ITM | 项次 |
| ITM_UP | 关联单据项次 |
| LOG_DD | 日期 |
| MO_NO | 来源单号 |
| ORG_BIL_ID | ERP业务单ID |
| ORG_BIL_NO | ERP业务单号 |
| OS_ID | 转入单ID |
| OS_NO | 转入单号 |
| PGM | 单据PGM |
| PRD_NO | 货品代号 |
| PRINT_BILNO | 打印单号 |
| REM | 备注 |
| SAL_NO | 业务 |
| UNIQUE_ID | 唯一性ID |
| USR | 录入人 |
| ZC_NO | 制程代号 |

## PK_LINE_SET (工作站台设定)

| 字段名 | 中文说明 |
|--------|---------|
| ALLOW_KRQSJ | 空容器允许上架 |
| AREA_NO | AGV站点/取货点 |
| AREA_NO_XH | 卸货点 |
| LAYER | 所属楼层 |
| LIMIT_RQ_TYPE | 限用容器类型 |
| LINE_NO | 站台代号 |
| LINE_TYPE | 站台类型(1.堆垛机库站台 2.CTU库拣选输送线) |
| MODE_IO | 出入库模式(O.出库模式 I.入库模式) |
| NAME | 站台名称 |
| PASSAGE | 通道 |
| STATUS_ID | 设备状态(1.正常 2.故障) |
| STOP_ID | 停用标记 |
| TASK_MODE | 任务下发方式(1.多任务下发 2.单任务下发) |
| TYPE_ID | 类型(1.拣选位 2.入口 3.出口) |
| WH | 仓库代号 |

## PK_RULE_PROP (拣货策略参数表)

| 字段名 | 中文说明 |
|--------|---------|
| PROP_NO | 参数名 |
| REM | 备注 |
| RULE_ID | 拣货策略代号 |
| VALUE | 参数值 |

## PK_RULE (拣货策略主表)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 集团公司 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| NAME | 拣货策略名称 |
| RULE_ID | 拣货策略代号 |
| STOP_ID | 停用标记 |
| SYS_DATE | 录入时间 |
| USR | 录入人员 |
| WH_TYPE | 上下架模式(1.手持PDA模式 2.自动化接口模式 3.叉车任务模式) |

## PMARK_DSC_DEL (特征分段一删除库)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| FLDNAME | 分段英文名称 |
| UP_DD | 上次更新时间 |
| VALUE | 值 |

## PMARK_DSC (特征分段从表一)

| 字段名 | 中文说明 |
|--------|---------|
| CHK_DFT | 默认值 |
| CON_NO | 货主编码 |
| DSC | 描述 |
| FLDNAME | 分段英文名称 |
| REM | 摘要 |
| TP_ID | 第三方标志 |
| UP_DD | 上次更新时间 |
| VALUE | 值 |

## PMARK_DSC1_DEL (特征分段二删除库)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| FLDNAME | 字段英文名称 |
| PRD_NO | 货品代号 |
| UP_DD | 上次更新时间 |
| VALUE | 值 |

## PMARK_DSC1 (特征分段从表二)

| 字段名 | 中文说明 |
|--------|---------|
| ASSDYE_AA | 天心专用说明 |
| CHK_DFT | 默认值 |
| CON_NO | 货主编码 |
| DEF_ID | 缺省值 |
| DSC | 描述 |
| E_DD | 截止日期 |
| FLDNAME | 字段英文名称 |
| PRD_NO | 货品代号 |
| REM | 摘要 |
| S_DD | 启用日期 |
| TP_ID | 第三方标志 |
| UP_DD | 上次更新时间 |
| VALUE | 值 |

## PMARK_DSC2_DEL (特征分段三删除库)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| FLDNAME | 分段英文描述 |
| IDX1 | 中类代号 |
| UP_DD | 上次更新时间 |
| VALUE | 值 |

## PMARK_DSC2 (特征分段从表三)

| 字段名 | 中文说明 |
|--------|---------|
| CHK_DFT | 默认值 |
| CON_NO | 货主编码 |
| DSC | DSC |
| FLDNAME | 分段英文描述 |
| IDX1 | 中类代号 |
| REM | 摘要 |
| TP_ID | 第三方标志 |
| UP_DD | 上次更新时间 |
| VALUE | 值 |

## PMARK_DSC3_DEL (特征分段四(区域)删除库)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| FLDNAME | 分段英文描述 |
| TYPE3_VALUE | 依特征分段值 |
| UP_DD | 上次更新时间 |
| VALUE | 值 |

## PMARK_DSC3 (特征分段从表四(区域))

| 字段名 | 中文说明 |
|--------|---------|
| CHK_DFT | 默认值 |
| CON_NO | 货主编码 |
| DSC | 描述 |
| FLDNAME | 字段英文名称 |
| TYPE3_VALUE | 依特征分段值 |
| UP_DD | 上次更新时间 |
| VALUE | 值 |

## PMARK (特征分段)

| 字段名 | 中文说明 |
|--------|---------|
| CHKST | 不足码控制 |
| CON_NO | 货主编码 |
| DEGREE_CHK | 度数检测否(眼镜行业) |
| DSC | 中文描述 |
| FLDNAME | 分段英文名称 |
| ISSAME | 同箱同一分段 |
| ITM | 分段顺序 |
| NEEDCHK | 需检测 |
| NULLABLE | 是否允许为空 |
| NULLABLE4BILL | 不允许为空单据 |
| SIZE | 分段长度 |
| TP_ID | 第三方标志 |
| TYPE | 类别 |
| TYPE3_FLDNAME | 依特征分段名 |

## PRC_LINE_SET (车间产线设定)

| 字段名 | 中文说明 |
|--------|---------|
| LINE_NO | 产线代号 |
| NAME | 产线名称 |
| STOP_ID | 停用标记 |
| WH | 所属仓库 |

## PRD_MARK_DEL (特征模版删除库)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| MOB_ID | 模版代号 |
| PRD_MARK | 货品特征 |
| UP_DD | 上次更新时间 |

## PRD_MARK (货品特征模版库)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| END_DD | 停用日期 |
| MOB_ID | 模版代号 |
| MOB_NAME | 模版名称 |
| PRD_MARK | 货品特征 |
| REM | 说明 |
| SETBYPRDT | 依货品设置特征分段 |
| SYS_DATE | 创建日期 |
| TP_ID | 第三方标志 |
| UP_DD | 上次更新时间 |

## PRD_VERSION (模具/工装版本号对应表)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_NO | 模具/工装序号 |
| CON_NO | 货主编码 |
| PRD_CODE | 产品编号 |
| PROCESS_CODE | 工序编号 |
| PROCESS_NAME | 工序名称 |
| STOP_ID | 停用标志 |
| VERSION | 版本号 |

## PRDT_BAR_RPT (依货品设置条码打印套版)

| 字段名 | 中文说明 |
|--------|---------|
| BARCODE_TYPE | 条码类型 |
| CON_NO | 货主编码 |
| CUS_NO | 客户/厂商代号 |
| ID | 唯一ID |
| IDX1 | 中类代号 |
| LANG_ID | 语言别 |
| MOD_ID | 模板代号 |
| PRD_NO | 货品代号 |

## PRDT_BARCODE_BOX (箱条形码库资料表)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BIL_ID | 来源单据别 |
| BIL_ITM | 来源单项次(记录唯一项次) |
| BIL_NO | 来源单号(现兆吉专案为了方便追踪送检单) |
| BOX_NO | 箱条码 |
| BOX_NO1 | 外箱条码 |
| CHK_NOTPW | 不能配位标记 |
| CHUW | 储位代号 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| CREATE_DD | 产生日期 |
| CREATE_TYPE | 产生类型(1:分拣散出) |
| CUS_NAME | 客户名称 |
| CUS_NO | 客户代号 |
| ICCK_FLAG | 调拨出库标记 |
| JH_FLAG | 拣货标记 |
| LST_PDD | 最近盘点时间 |
| PACKAGE_NO | 包装码 |
| PD_DD | 盘点日期 |
| PH_FLAG | 在途标记 |
| PRD_MARK | 货品特征 |
| PRD_NO | 货品代号 |
| PRD_QRCODE | 货品二维码 |
| PT_NO | 打印单号 |
| QTY | 数量 |
| QTY_WEIGHT | 重量 |
| QTY1 | 数量副 |
| REM | 备注 |
| SO_NO | 受订单号 |
| STATUS_JY | 检验标记(空或T：合格、F：不合格、W：待检) |
| STOP_ID | 停用标记 |
| SYS_DATE | 输入日期 |
| TI_FLAG | 送检标志 |
| TRANS_BIL_ID | 已转单据别 |
| TRANS_BIL_OUT | 已转出库单据别 |
| UNIT_WEIGHT | 重量单位 |
| UP_BOX_NO | 原箱条码 |
| USR | 录入人 |
| UT | 单位 |
| VALID_DD | 有效期 |
| WAIT_CHK_FLAG | 待校验标记 |
| WH | 库位 |

## PRDT_BARCODE_BOX1 (外箱条码表)

| 字段名 | 中文说明 |
|--------|---------|
| BOX_DD | 装箱时间 |
| BOX_NO1 | 外箱码 |
| CHUW | 储位 |
| CON_NO | 货主编码 |
| CONTAIN_CODE | 容器条码 |
| CX_DD | 拆箱时间 |
| CX_REASON | 拆箱原因 |
| CX_USR | 拆箱人 |
| ICCK_FLAG | 调拨出库标记 |
| JH_FLAG | 拣货标记 |
| LST_PDD | 最近盘点时间 |
| PACKAGE_NO | 包装码 |
| PD_DD | 盘点日期 |
| STATUS_JY | 检验标记(空或T：合格、F：不合格、W：待检) |
| STOP_ID | 停用标记 |
| SYS_DATE | 录入时间 |
| TI_FLAG | 送检标记 |
| USR | 装箱人 |
| WH | 仓库 |

## PRDT_BARCODE_DEL (货品条码删除档(区分特征))

| 字段名 | 中文说明 |
|--------|---------|
| BAR_CODE | 条码 |
| BAT_NO |  |
| CON_NO | 货主编码 |
| ITM |  |
| PRD_MARK |  |
| PRD_NO |  |
| UP_DD |  |

## PRDT_BARCODE_SQ (条码流水号记录库)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| PAT | 内容 |
| SQ | 当前流水号 |
| SQ_TYPE | 2批号,3条码,6序列号,7内箱,9外箱,1箱条码,8容器,10货架码 |

## PRDT_BARCODE (货品条码档（区分特征）)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_CODE | 条形码 |
| BAT_NO | 批号 |
| BIL_ID | 来源单据别 |
| BIL_ITM | 来源单项次 |
| BIL_NO | 来源单号 |
| CHK_NOTPW | 不能配位标记 |
| CON_NO | 货主编码 |
| CRE_BIL_ID | 产生单据别 |
| CRE_BIL_NO | 产生单号 |
| CREATE_DD | 产生日期 |
| CUS_NAME | 客户名称 |
| CUS_NO | 客户代号 |
| ITM |  |
| PRD_MARK | 特征 |
| PRD_NO | 品号 |
| PRD_QRCODE | 货品二维码 |
| PT_NO | 录入批次 |
| REM | 备注 |
| SO_NO | 受订单号 |
| SYS_DATE | 输入日期 |
| UP_DD | 时间戳字段 |
| USR | 录入人 |
| VALID_DD | 有效期 |

## PRDT_CW_DEL (货品储位设定删除表)

| 字段名 | 中文说明 |
|--------|---------|
| GUID | 唯一值 |
| UP_DD | 更新时间 |

## PRDT_CW_ODR (依中类设置储存顺序)

| 字段名 | 中文说明 |
|--------|---------|
| CHUW | 储位代号 |
| CON_NO | 货主编码 |
| DEP | 集团分公司 |
| GL | 列 |
| GS | 排 |
| GUID | 唯一值 |
| IDX_NO | 中类代号 |
| ITM | 次序 |
| LAYER | 层 |
| WH | 仓库代号 |
| ZONE_ID | 区域代号 |

## PRDT_CW_XZ_DEL (货品储位储存性质删除表)

| 字段名 | 中文说明 |
|--------|---------|
| GUID | 唯一值 |
| UP_DD | 时间戳 |

## PRDT_CW_XZ_ODR (依储存性质设置储存顺序)

| 字段名 | 中文说明 |
|--------|---------|
| CHUW | 储位代号 |
| CON_NO | 货主编码 |
| CWXZ_NO | 储存性质代号 |
| DEP | 集团分公司 |
| GL | 列 |
| GS | 排 |
| GUID | 唯一值 |
| ITM | 次序 |
| LAYER | 层 |
| WH | 仓库代号 |
| ZONE_ID | 区域代号 |

## PRDT_CW_XZ (货品储位储存性质表)

| 字段名 | 中文说明 |
|--------|---------|
| CHUW | 储位代号 |
| CON_NO | 货主编码 |
| CWXZ_NO | 储存性质代号 |
| DEP | 集团分公司 |
| GL | 列 |
| GS | 排 |
| GUID | 唯一值 |
| LAYER | 层 |
| PRD_NO | 货品代号 |
| REM | 备注 |
| UP_DD | 时间戳 |
| WH | 仓库代号 |
| ZONE_ID | 区域代号 |

## PRDT_CW (货品储位设定表)

| 字段名 | 中文说明 |
|--------|---------|
| CHUW | 储位代号 |
| CON_NO | 货主编码 |
| DEP | 集团分公司 |
| GL | 列 |
| GS | 排 |
| GUID | 唯一值 |
| IDX_NO | 中类 |
| LAYER | 层 |
| PRD_MARK | 特征 |
| PRD_NO | 货品代号 |
| REM | 备注 |
| UP_DD | 更新时间 |
| WH | 仓库代号 |
| ZONE_ID | 区域代号 |

## PRDT_DEL (产品-删除库)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| PRD_NO | 产品代号 |
| UP_DD | 上次更新时间 |

## PRDT_PDA_RN (AI-WMS货品附属信息)

| 字段名 | 中文说明 |
|--------|---------|
| BARCODE_TYPE | 适用条码类型 |
| CON_NO | 货主编码 |
| NEED_SCALE | 需称重  T/F |
| PRD_NO | 货品代号 |
| QTY_COLLECT | 数量采集方式 |
| QTY_QZ_MODE | 数量取整模式 |
| QTY_TYPE | 主单位数量修改反推副单位(T/F) |
| SCALE_POINT | 称重小数位数 |
| SCALE_QZ | 称重数量取整 |
| SHOW_PAK | 包装重量 |
| SHOW_TYPE | 主副互推弹窗显示 1.弹窗,2.不弹窗 |
| UT_POINT | 主单位数量小数位 |
| UT1_DISP | 副单位显示方式 |
| UT1_POINT | 副单位数量小数位 |

## PRDT_RCV (货品资料专案附属表)

| 字段名 | 中文说明 |
|--------|---------|
| NW_CLASSIFY | 分类（1 ：模具、2 ：工装） |
| NW_CONTAIN_CODE | 容器条码 |
| NW_PIC_NAME | 图片文件名 |
| NW_STATUS | 状态（ZK 、LYCL、LY、GHCL、BF） |
| NW_TEXTURE | 材质 |
| NW_TYPE | 类型 |
| NW_USED_COUNT | 已使用次数 |
| PRD_NO | 货品代号 |

## PRDT (货品基础资料库)

| 字段名 | 中文说明 |
|--------|---------|
| ALLOW_SHQ_FH | 允许从收货区直接发货 |
| BOM_ID | 虚拟件 |
| CF_PROP | 存放属性(0、空 1、低层 2、中层 3、高层) |
| CHK_BAT | 批号管制否 |
| CHK_NUM | 序列号管制否 |
| CHUW | 缺省储位 |
| CON_NO | 货主编码 |
| CW_FLAG | 启用储位管理 |
| CWCTRL_ID | 货品储存管制 |
| CWXZ_NO | 储存性质代号 |
| DEP | 所属部门 |
| DEPRO_NO | 所属群组 |
| EFFECT_ID | 箱条码生效方式(1.立即生效;2.装箱检验后生效) |
| ERP_CHK_BAT | ERP批号管制 |
| FORMULA | 材积公式 |
| IDX_PD | 盘点分类 |
| IDX1 | 中类代号 |
| KND | 大   类 |
| ML_UT | 最小领料单位 |
| MOB_ID | 货品特征模版 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| MRK | 品牌 |
| NAME | 名称 |
| NAME_ENG | 英文名称 |
| NAME_PY | 助记码 |
| NEED_CHK_FLAG | 需检验标志 |
| NOT_BARCODE | 非条码货品 |
| NOUSE_DD | 货品停用日期 |
| PAK_EXC | 包装换算 |
| PAK_GW | 包装毛重 |
| PAK_MEAST | 包装大小 |
| PAK_MEAST_UNIT | 包装大小单位 |
| PAK_NW | 包装净重 |
| PAK_UNIT | 包装单位 |
| PAK_WEIGHT_UNIT | 包装净重单位 |
| PIC_NO | 图号 |
| PK2_QTY | 包装(一)数量 |
| PK2_UT | 包装(一)单位 |
| PK3_QTY | 包装(二)数量 |
| PK3_UT | 包装(二)单位 |
| PRD_NO | 货品代号 |
| QTY_WEIGHT | 货品单重 |
| REM | 摘要 |
| RQ_CHK | 容器检验设定(1.免检，2.先入库再检验，3.先检验再入库) |
| RTO_MM | 生产超缴率 |
| RTO_PC | 进货超交率 |
| RTO_SA | 销货超交率 |
| RTO_TB | 托工超缴率 |
| SNM | 简称 |
| SPC | 货品规格 |
| START_DD | 货品创建日 |
| STATIC_TIMES | 货品的静置时数 |
| SUP1 | 主供应商 |
| SYS_DATE | 输入日期 |
| THIRD_UNIT_NO | 第三方单位代号 |
| TP_ID | 第三方标志 |
| TPL_NO | 货品模版 |
| TY_INR | 仓库检验周期 |
| TY_KND | 启用检验类型(复选项1入库检验、2出库检验、3库存检验) |
| UNIT_WEIGHT | 货品单重单位 |
| UP_DD | 上次更新时间 |
| USR | 录入员 |
| USR_WH | 仓管人员 |
| UT | 主单位 |
| UT1 | 副单位 |
| VALID_DAYS | 有效天数 |
| VALID_ID | 有效期计算方式 |
| VALID_YEARS | 有效年数 |
| WH | 预设仓库 |

## PRDT1_CW (储位库存表)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CHUW | 储位代号 |
| CON_NO | 货主编码 |
| FIELD_ZS | 自设字段 |
| INSERT_DD | 插入时间 |
| LST_IND | 最近上架日 |
| LST_OTD | 最近下架日 |
| LST_TYD | 最近检验日期 |
| PRD_MARK | 货品特征 |
| PRD_NO | 货品代号 |
| QTY_BC | 计划出库量 |
| QTY_IN | 入库数量 |
| QTY_OUT | 出库数量 |
| QTY_PK | 拣货量 |
| QTY_TY | 库存检验量 |
| QTY1_BC | 计划出库量(副) |
| QTY1_IN | 入库数量(副) |
| QTY1_OUT | 出库数量(副) |
| QTY1_PK | 拣货量(副) |
| QTY1_TY | 库存检验量(副) |
| VALID_DD | 有效日期 |
| WH | 仓库代号 |

## PRDT1_LOCK (货品库存锁定表)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO | 接口代号 |
| BAT_NO | 批号 |
| CON_NO | 货主编码 |
| GUID | 项次 |
| LOCK_DD | 锁定日期 |
| PRD_MARK | 货品特征 |
| PRD_NO | 货品代号 |
| REM | 备注 |
| UNLOCK_DD | 解锁日期 |
| WH | 仓库代号 |

## PRDT1_MX_ALL (库存日志表)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BIL_DD | 单据日期 |
| BIL_ID | 单据别 |
| BIL_ID1 | 来源单据别 |
| BIL_ITM | 单据唯一项次 |
| BIL_ITM1 | 来源单据唯一项次 |
| BIL_NO | 单据号码 |
| BIL_NO1 | 来源单据号码 |
| CHUW | 储位代号 |
| CON_NO | 货主编码 |
| INS_DD | 插入时间 |
| MOD_STATE | 修改状态 |
| MODIFY_DD | 修改时间 |
| PITM | 项次 |
| POSITION | 程序位置 |
| PRD_MARK | 货品特征 |
| PRD_NO | 货品代号 |
| PRO_NAME | 程序名称 |
| QTY_BC_CHG | 计划出库变动数量 |
| QTY_BC_NEW | 修改后计划出库量 |
| QTY_BC_OLD | 更改前计划出库量 |
| QTY_BIL | 单据数量 |
| QTY_IN_CHG | 入库变动数量 |
| QTY_IN_NEW | 修改后入库量 |
| QTY_IN_OLD | 更改前入库量 |
| QTY_OUT_CHG | 出库变动数量 |
| QTY_OUT_NEW | 修改后入库量 |
| QTY_OUT_OLD | 更改前出库量 |
| QTY_PK_CHG | 拣货变动数量 |
| QTY_PK_NEW | 修改后拣货量 |
| QTY_PK_OLD | 更改前拣货量 |
| QTY_QC_CHG | 在检变动数量 |
| QTY_QC_NEW | 修改后在检量 |
| QTY_QC_OLD | 更改前在检量 |
| QTY_TY_CHG | 库存检验变动数量 |
| QTY_TY_NEW | 修改后库存检验量 |
| QTY_TY_OLD | 更改前库存检验量 |
| QTY_UO_CHG | 入库通知变动数量 |
| QTY_UO_NEW | 修改后入库通知量 |
| QTY_UO_OLD | 更改前入库通知量 |
| QTY_UP_CHG | 出库通知变动数量 |
| QTY_UP_NEW | 修改后出库通知量 |
| QTY_UP_OLD | 更改前出库通知量 |
| QTY1_BC_CHG | 计划出库变动数量(副) |
| QTY1_BC_NEW | 修改后计划出库量(副) |
| QTY1_BC_OLD | 更改前计划出库量(副) |
| QTY1_BIL | 单据数量(副) |
| QTY1_IN_CHG | 入库变动数量(副) |
| QTY1_IN_NEW | 修改后入库量(副) |
| QTY1_IN_OLD | 更改前入库量(副) |
| QTY1_OUT_CHG | 出库变动数量(副) |
| QTY1_OUT_NEW | 修改后出库量(副) |
| QTY1_OUT_OLD | 更改前出库量(副) |
| QTY1_PK_CHG | 拣货变动数量(副) |
| QTY1_PK_NEW | 修改后拣货量(副) |
| QTY1_PK_OLD | 更改前拣货量(副) |
| QTY1_QC_CHG | 在检变动数量(副) |
| QTY1_QC_NEW | 修改后在检量(副) |
| QTY1_QC_OLD | 更改前在检量(副) |
| QTY1_TY_CHG | 库存检验变动数量(副) |
| QTY1_TY_NEW | 修改后库存检验量(副) |
| QTY1_TY_OLD | 更改前库存检验量(副) |
| QTY1_UO_CHG | 入库通知变动数量(副) |
| QTY1_UO_NEW | 修改后入库通知量(副) |
| QTY1_UO_OLD | 更改前入库通知量(副) |
| QTY1_UP_CHG | 出库通知变动数量(副) |
| QTY1_UP_NEW | 修改后出库通知量(副) |
| QTY1_UP_OLD | 更改前出库通知量(副) |
| SYS_DATE | 制单日期 |
| TAB_NAME | 变动库存表名称 |
| UNIT | 原单单位 |
| WH | 仓库代号 |

## PRDT1 (仓库库存表)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CON_NO | 货主编码 |
| FIELD_ZS | 自设字段 |
| INSERT_DD | 插入时间 |
| LOCK_ID | 库存锁定标记 |
| LST_IND | 最近入库日 |
| LST_OTD | 最近出库日 |
| LST_TYD | 最近检验日期 |
| PRD_MARK | 货品特征 |
| PRD_NO | 货品代号 |
| QTY_BC | 计划出库量 |
| QTY_IN | 入库数量 |
| QTY_OUT | 出库数量 |
| QTY_PK | 拣货量 |
| QTY_QC | 在检量 |
| QTY_TY | 库存检验量 |
| QTY_UO | 入库通知量 |
| QTY_UP | 出库通知量 |
| QTY1_BC | 计划出库量(副) |
| QTY1_IN | 入库数量(副) |
| QTY1_OUT | 出库数量(副) |
| QTY1_PK | 拣货量(副) |
| QTY1_QC | 在检量(副) |
| QTY1_TY | 库存检验量(副) |
| QTY1_UO | 入库通知量(副) |
| QTY1_UP | 出库通知量(副) |
| SC_DD | 生产日期 |
| VALID_DD | 有效日期 |
| WH | 仓库代号 |

## PRINT_DATA (单据打印信息记录库)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据类别 |
| BIL_NO | 单据号码 |
| PRINT_ITM | 打印序号 |
| PRINT_TIME | 打印时间 |
| USR | 打印人员 |

## PRINT_LOG_PDA (PDA打印日志)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据ID |
| BIL_NO | 单据号码 |
| DEVICE_ID | 设备ID |
| ERROR_MSG | 错误信息 |
| ITM |  |
| PGM | 程序代号 |
| PRT_DATE | 打印日期 |
| PRT_RESULT | 打印结果 |
| REM | 备注 |
| USR | 用户代号 |

## PRINT_LOG (打印记录表)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_CODE | 条码 |
| BAR_TYPE | 条码类型(0序列号,1箱序列号,2条码,3箱条码) |
| BIL_ID | 来源单ID |
| BIL_ITM | 来源单项次 |
| BIL_NO | 来源单号 |
| CON_NO | 货主编码 |
| ID | GUID |
| PT_NO | 录入批次 |
| QTY_PRINT | 打印数量 |
| QTY1_PRINT | 打印数量(副) |
| SYS_DATE | 制单时间 |
| USR | 制单人 |

## PRINT_SER_HIS (打印服务历史表)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_RPT_LANG | 条码套版语言 |
| BIL_NO | 打印单号/条码 |
| COMP_DB | 账套 |
| COMP_NO | 账套代号 |
| COPYS | 打印张数 |
| FAIL_COUNT | 失败次数 |
| FAIL_MSG | 失败原因 |
| FIN_ID | 成功标识 |
| GUID_NO | 唯一值 |
| ITM | 项次 |
| LANG_ID | 语言别 |
| MOD_ID | 模板代号 |
| PAPER_HEIGHT | 纸张高度 |
| PAPER_KIND | 纸张样式 |
| PAPER_WIDTH | 纸张宽度 |
| PGM | 程序标识 |
| PRINT_TYPE | 打印类型 |
| PRINTER_NAME | 打印机名称 |
| PRT_CONTENT | 打印内容 |
| PRT_DATASET | 打印数据 |
| PRT_DATE | 打印日期 |
| SERVICE_NAME | 服务名称 |
| TYPE_ID | 报表类型 |
| USR | 用户代号 |

## PRINT_SER_TASK (打印服务任务表)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_RPT_LANG | 条码套版语言 |
| BIL_NO | 打印单号/条码 |
| COMP_DB | 账套 |
| COMP_NO | 账套代号 |
| COPYS | 打印张数 |
| FAIL_COUNT | 失败次数 |
| FAIL_MSG | 失败原因 |
| FIN_ID | 成功标识 |
| GUID_NO | 唯一值 |
| ITM | 项次 |
| LANG_ID | 语言别 |
| MOD_ID | 模板代号 |
| PAPER_HEIGHT | 纸张高度 |
| PAPER_KIND | 纸张样式 |
| PAPER_WIDTH | 纸张宽度 |
| PGM | 程序标识 |
| PRINT_TYPE | 打印类型 |
| PRINTER_NAME | 打印机名称 |
| PRT_CONTENT | 打印内容 |
| PRT_DATASET | 打印数据 |
| PRT_DATE | 打印日期 |
| SERVICE_NAME | 服务名称 |
| TYPE_ID | 报表类型 |
| USR | 用户代号 |

## PRINT_SET (打印服务设定表)

| 字段名 | 中文说明 |
|--------|---------|
| CUS_NO | 供应商代号 |
| HD_SN | 硬盘序列号 |
| LST_CONN_DD | 最后通讯时间 |
| MARCHINE_IP | 机器IP |
| MARCHINE_NAME | 机器名称 |
| SERVICE_NAME | 服务名称 |
| STOP_DD | 停用日期 |
| STOP_ID | 停用标记 |
| SYS_DATE | 启用日期 |
| WAIT_SECOND | 循环间隔(秒) |
| WEB_TYPE | 连接类型(TCP HTTP) |

## PRINTER_LIST (打印机列表)

| 字段名 | 中文说明 |
|--------|---------|
| PRINTER_NAME | 打印机名称 |
| SERVICE_NAME | 服务名称 |

## PROGRAM_LOG (记录程序执行记录)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据别 |
| BIL_NO | 单据号码 |
| COMPNO | 账套代号 |
| EXEC_STATUS | 执行状态 |
| JSON_CONTENT | 相关内容 |
| RECORD_NO | 记录代号 |
| SYS_DATE | 系统时间 |

## PSWD_CHK (用户上下属)

| 字段名 | 中文说明 |
|--------|---------|
| DRC |  |
| USR_DW | 下级用户代号 |
| USR_UP | 上级用户代号 |

## PSWD_NFC (员工发卡(NFC))

| 字段名 | 中文说明 |
|--------|---------|
| COMPNO | 账套代号 |
| NFC_ID | NFC代号 |
| STOP_DD | 停用日期 |
| USR | 用户代号 |

## PSWD_PROP_BS (SunlikeWeb属性表)

| 字段名 | 中文说明 |
|--------|---------|
| COMPNO | 账套代号 |
| FLD_NAME | 名称 |
| FLD_VALUE | 内容 |
| GROUP_NO | 组别 |
| PGM | 程序代号 |
| USR | 用户代号 |

## PSWD_PROP (密码属性)

| 字段名 | 中文说明 |
|--------|---------|
| COMPNO | 帐套名称 |
| FLD_NAME | 属性名称 |
| FLD_VALUE | 属性值 |
| GROUP_NO | 分组代号 |
| PGM | 程序代号 |
| TYPE_ID | 属性类型 |
| USR | 用户 |

## PSWD_ROLE_BS (用户角色关联表)

| 字段名 | 中文说明 |
|--------|---------|
| ROLENO | 代号 |
| TYPE_ID | 类型代号 |
| USR | 用户代号 |

## PSWD_ROLE (ONLINE角色所属用户)

| 字段名 | 中文说明 |
|--------|---------|
| COMPNO | 公司代号 |
| ROLENO | 角色 |
| TYPE_ID | 权限分类 |
| USR | 用户代号 |

## PSWD (密码档)

| 字段名 | 中文说明 |
|--------|---------|
| B_DAT | 起始有效期 |
| COMP_BOSS | 管理员否 |
| COMPNO | 公司代号 |
| CREATOR | 创建人 |
| DEP | 部门代号 |
| DEP_UP | 是集团分公司部门 |
| DEPRO_NO | 部门群组 |
| E_DAT | 截止有效期 |
| E_MAIL | EMAIL地址 |
| ID_CODE | NFC卡号 |
| IGNORECASE | 密码不区分大小写 |
| ISCUST | 是否客户 |
| ISGROUP | 是否集团公司 |
| LANG_ID | 缺省语言别 |
| MNG | 所属主管 |
| NAME | 名称 |
| PWD | 密码 |
| PWD_CHG | 下次登陆必须修改密码 |
| SYS_DATE | 创建时间 |
| TP_ID | 第三方标记 |
| USR | 识别码 |

## PTL_PICK (PTL拣货信息表)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO | PTL任务编号 |
| ACT_NO_FIN | 完成任务编号 |
| BAR_CODE | 条码 |
| BAR_TYPE | 条码类型 |
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单号 |
| BOX_SJ_FLAG | 散出标记 |
| CAR_NO | 车号 |
| ERROR_REM | 错误说明 |
| ERROR_TIMES | 错误次数 |
| GUID | GUID |
| HW_NO | 设备代号 |
| LINE_NO | 工作站台 |
| P_TYPE | 类型(1.拣货 2.备货出库) |
| PTL_LABEL | 电子标签ID |
| QTY | 扫描数量 |

## RCV_ZONE (收货人区域设定表)

| 字段名 | 中文说明 |
|--------|---------|
| RCV_MAN | 收货人识别码 |
| WH | 仓库代号 |
| ZONE_ID | 区域代号 |

## RESET_KC (库存核算历史表)

| 字段名 | 中文说明 |
|--------|---------|
| CALC_DATE | 计算日期 |
| CON_NO | 货主编码 |
| DEL_ZERO | 是否删除全部量为0的记录 |
| DEP | 集团分公司 |
| END_TIME | 结束时间 |
| PRD_NO_E | 截止品号 |
| PRD_NO_S | 起始品号 |
| RESET_NO | 库存核算单号 |
| RESET_RTN | 是否重整已交量 |
| START_TIME | 开始时间 |
| SYS_DATE | 录入时间 |
| USR | 用户代号 |
| WH_NOS | 指定仓库 |

## RESET_KC1 (库存核算回写量异常表)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_NO | 单号 |
| FIELD_NAME | 栏位名称 |
| ITM | 项次 |
| KEY_ITM | 唯一项次 |
| QTY_NEW | 新数量 |
| QTY_OLD | 原数量 |
| RESET_NO | 库存核算单号 |
| TABLE_NAME | 表名 |

## RESET_KC2 (库存核算库存量异常表)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CON_NO | 货主编码 |
| FIELD_NAME | 栏位名称 |
| ITM | 项次 |
| PRD_MARK | 货品特征 |
| PRD_NO | 货品代号 |
| QTY_NEW | 新数量 |
| QTY_OLD | 原数量 |
| RESET_NO | 库存核算单号 |
| WH | 仓库代号 |

## RESET_KC3 (库存核算储位库存量异常表)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CHUW | 储位代号 |
| CON_NO | 货主编码 |
| FIELD_NAME | 栏位名称 |
| ITM | 项次 |
| PRD_MARK | 货品特征 |
| PRD_NO | 货品代号 |
| QTY_NEW | 新数量 |
| QTY_OLD | 原数量 |
| RESET_NO | 库存核算单号 |
| WH | 仓库代号 |

## ROLE_BS (权限设定WEB)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 部门 |
| DEPRO_NO | 部门群组 |
| MENUINFO_0 |  |
| MENUINFO_1 |  |
| MENUINFO_2 |  |
| MENUINFO_3 |  |
| MENUINFO_4 |  |
| MENUINFO_5 |  |
| MENUINFO_6 |  |
| MENUINFO_7 |  |
| MENUINFO_8 |  |
| MENUINFO_9 |  |
| NAME | 名称 |
| PUBLIC_ID | 是否公用 |
| REM | 备注 |
| ROLENO | 代号 |
| SUB_ID | 含所属 |
| SYS_DATE | 制单时间 |
| TYPE_ID | 类型 |
| USR | 用户代号 |

## ROLE (角色定义)

| 字段名 | 中文说明 |
|--------|---------|
| COMPNO | 公司代号 |
| DEP | 部门代号 |
| DEPRO_NO | 部门群组代号 |
| MENUINFO_0 | 简体条码菜单 |
| MENUINFO_1 | 繁体条码菜单 |
| MENUINFO_2 | 英文条码菜单 |
| MENUINFO_3 | 越南文条码菜单 |
| MENUINFO_4 | 泰文条码菜单 |
| MENUINFO_5 | 日文条码菜单 |
| MENUINFO_6 | 保留 |
| MENUINFO_7 | 保留 |
| MENUINFO_8 | 保留 |
| MENUINFO_9 | 保留 |
| NAME | 角色名称 |
| PUBLIC_ID | 公用注记 |
| REM | 备注 |
| ROLENO | 角色代号 |
| SUB_ID | 部门含下属否 |
| TYPE_ID | 权限分类 |
| USR | 用户代号 |

## SALM_CHK (业务员上下属)

| 字段名 | 中文说明 |
|--------|---------|
| DRC |  |
| SAL_DW | 下级业务 |
| SAL_UP | 上级业务 |

## SALM_DEL (业务员-删除库)

| 字段名 | 中文说明 |
|--------|---------|
| SAL_NO | 员工代号 |
| UP_DD | 上次修改时间 |

## SALM (业务员资料档)

| 字段名 | 中文说明 |
|--------|---------|
| CARD_NO | 打卡卡号 |
| DEP | 部门代号 |
| DUT_IN_D | 到  职  日 |
| DUT_OT_D | 离  职  日 |
| ENG_NAME | 英文名称 |
| LOGON | 是否登陆用户 |
| NAME | 名称 |
| NAME_PY | 助记码 |
| PHOTO_PIC | 业务员图片 |
| REM | 摘要 |
| SAL_NO | 员工代号 |
| SEX | 性别 |
| SYS_DATE | 输入日期 |
| TEL2 | 电话 |
| TP_ID | 第三方标志 |
| UP_DD | 上次更新时间 |
| UP_SAL_NO | 上级业务 |
| USR | 识别码 |
| USR1 | 录入员 |

## SEARCH_RPT_BS (报表查询方案)

| 字段名 | 中文说明 |
|--------|---------|
| COLUMN_CONTENT | 栏位设定 |
| COND_CONTENT | 查询条件 |
| FIX_CONTENT | 固定条件 |
| ISSHOW | 显示否 |
| ITM | 项次 |
| LADDER_CONTENT | 阶梯设定 |
| NAME | 名称 |
| NAME_ENG | 英文名称 |
| NAME_TW | 名称台湾 |
| NAME_US | 英文名称 |
| NAME_VN | 名称越南 |
| PGM | 程序代号 |
| SEARCH_CONTENT | 查询条件 |
| SORT_CONTENT | 排序设定 |
| STATS_CONTENT | 统计设定 |
| TYPE | 类型 |
| USR | 制作人 |

## SEARCH_RPTUSR_BS (报表权限设定)

| 字段名 | 中文说明 |
|--------|---------|
| ITM | 项次 |
| PGM | 程序代号 |
| ROLENO | 群组代号 |
| USR | 用户代号 |

## SEARCH_SET_BS (查询方案主表)

| 字段名 | 中文说明 |
|--------|---------|
| COLUMN_CONTENT | 栏位设定Json |
| COND_CONTENT | 条件设定 |
| CTRL_ID | 控件ID |
| FIX_CONTENT | 固定条件 |
| ISSHOW | 是否显示 |
| ITM | ITM |
| NAME | 名称 |
| NAME_TW | 繁体名称 |
| NAME_US | 名称英文 |
| NAME_VN | 名称越南 |
| PGM | PGM |
| SEARCH_CONTENT | 查询内容json |
| SHOW_BODY | 显示表身字段 |
| SHOW_HEAD | 显示表头字段 |
| SHOW_LADDER | 阶梯显示否 |
| SORT_CONTENT | 排序设定 |
| TYPE | 类型 |
| USR | 所属用户 |

## SEARCH_SORT_BS (查询方案排序)

| 字段名 | 中文说明 |
|--------|---------|
| CTRL_ID | 控件ID |
| ITM | 方案项次 |
| PGM | PGM |
| SORT | 顺序 |
| USR | 用户代号 |

## SEARCH_USR_BS (用户查询方案)

| 字段名 | 中文说明 |
|--------|---------|
| CTRL_ID | 控件ID |
| ITM | ITM |
| PGM | PGM |
| SEARCH_CONTENT | 查询类型 |
| USR | 用户代号 |

## SETDEFVALUES_DRP (单据模版)

| 字段名 | 中文说明 |
|--------|---------|
| CHK_PUBLIC | 是否公用 |
| CONTENT | 数据内容 |
| ITM | 项次 |
| MB_IMAGE | 图片 |
| MB_TYPE | 模板分类 |
| PGM | 程序代号 |
| REM | 备注 |
| USR | 用户代号 |

## SPC_COMP (特殊营业人资料)

| 字段名 | 中文说明 |
|--------|---------|
| CTRL_ID | 选项代号 |
| DEP | 集团分公司 |
| REM | 说明 |
| SPC_ID | 选项值 |
| UP_DD | 时间戳 |

## SPC_CON_SET (特殊密码-货主编码)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| USR | 用户代号 |

## SPC_LST (不合格原因表头(报工))

| 字段名 | 中文说明 |
|--------|---------|
| NAME | 不合格原因 |
| SPC_ITEM | 不合格项目 |
| SPC_NO | 不合格原因代号 |
| SPC_NO_UP | 上级代号 |

## SPC_MY_WH_DEP (特殊密码-仓库部门)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 部门代号 |
| USR | 用户代号 |

## SPC_MY_WH_WH (特殊密码-仓库代号)

| 字段名 | 中文说明 |
|--------|---------|
| USR | 用户代号 |
| WH | 仓库代号 |

## SPC_SET_CHK (不合格原因树)

| 字段名 | 中文说明 |
|--------|---------|
| DRC |  |
| SPC_DW | 下级 |
| SPC_UP | 上级 |

## SPC_SET (不合格原因设定(检验))

| 字段名 | 中文说明 |
|--------|---------|
| NAME | 原因说明 |
| REM | 原因备注 |
| SPC_NO | 原因代号 |
| SPC_UP | 上层原因代号 |
| TP_ID | 第三方标记 |

## SPCFX_PSWD_BS (特殊权限明细)

| 字段名 | 中文说明 |
|--------|---------|
| CTRL_ID | 选项代号 |
| REM | 说明 |
| ROLENO | 权限代号 |
| SPC_ID | 选项值 |
| TYPE_ID | 权限分类 |

## SPCPSWD_ROLE_BS (特殊权限成员)

| 字段名 | 中文说明 |
|--------|---------|
| ROLENO | 权限代号 |
| TYPE_ID | 权限分类 |
| USR | 用户代号 |

## SPRD_CW (储位库存结余表)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CHUW | 储位代号 |
| CON_NO | 货主编码 |
| LST_IND | 最近入库日 |
| LST_OTD | 最近出库日 |
| MM | 月份 |
| PRD_MARK | 特征 |
| PRD_NO | 品号 |
| QTY_IN | 入库数量 |
| QTY_OUT | 出库数量 |
| QTY1_IN | 入库数量(副) |
| QTY1_OUT | 出库数量(副) |
| VALID_DD | 有效日期 |
| WH | 仓库 |
| YY | 年度 |

## SPRD (仓库库存结余表)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CON_NO | 货主编码 |
| LST_IND | 最近入库日 |
| LST_OTD | 最近出库日 |
| MM | 月份 |
| PRD_MARK | 特征 |
| PRD_NO | 品号 |
| QTY_IN | 入库数量 |
| QTY_OUT | 出库数量 |
| QTY1_IN | 入库数量(副) |
| QTY1_OUT | 出库数量(副) |
| VALID_DD | 有效日期 |
| WH | 仓库 |
| YY | 年度 |

## STDCRKTYPE_SUN (SUNLIKE出入库单据类型设置标准档)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据ID |
| CRKFLAG | 出入库区分 |
| CRKTYPE | 类型代号 |
| OS_ID | 来源ID |
| OS_NAME | 来源名称 |
| TYPE_NAME | 类型名称 |
| YN | 启用 |

## SVC_LOG (自动服务执行日志表)

| 字段名 | 中文说明 |
|--------|---------|
| END_TIME | 结束时间 |
| INTERVAL_TIME | 轮询时间(秒) |
| NAME | 服务名称 |
| NAME1 | 子服务名称 |
| PATH | 服务路径 |
| REM | 说明 |
| START_TIME | 开始时间 |
| SVC_NO | 服务代号 |
| SVC_NO1 | 子服务代号 |

## SVC_YC (自动服务执行异常表)

| 字段名 | 中文说明 |
|--------|---------|
| REM | 异常说明 |
| SVC_NO | 服务代号 |
| SVC_NO1 | 子服务代号 |
| SYS_DATE | 发生时间 |
| YC_NO | 异常代号 |

## SYS_FILE (系统附件表)

| 字段名 | 中文说明 |
|--------|---------|
| CONTENT | 文件内容 |
| FILE_DD | 上传时间 |
| FILE_ID | 文件ID |
| FILE_NAME | 文件名称 |
| FILE_TYPE | 文件类型 |
| PGM | 程序代号 |
| USR | 上传用户 |

## TABLETS (同步时间戳)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| CUR_UP_DD | 当前时间戳 |
| DEL_UP_DD | 删除时间戳 |
| TABLENAME | 表名 |
| VER | 版本号 |

## TF_BAR (序列号变更单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_CODE | 序列号 |
| BAT_NO_NEW | 新批号 |
| BAT_NO_OLD | 原批号 |
| BOX_NO | 箱码 |
| BR_NO | 变更单号 |
| CHUW_NEW | 新储位代号 |
| CHUW_OLD | 原储位代号 |
| CUS_NO_NEW | 新客户代号 |
| CUS_NO_OLD | 原客户代号 |
| ICCK_FLAG_NEW | 新调拨出库标记 |
| ICCK_FLAG_OLD | 原调拨出库标记 |
| ITM | 项次 |
| PH_FLAG_NEW | 新在途标记 |
| PH_FLAG_OLD | 原在途标记 |
| PRD_MARK_NEW | 新货品特征 |
| PRD_MARK_OLD | 原货品特征 |
| PRD_NO_NEW | 新货品代号 |
| PRD_NO_OLD | 原货品代号 |
| REM | 变更原因 |
| STATUS_JY_NEW | 新检验状态(空或T：合格、F：不合格、W：待检) |
| STATUS_JY_OLD | 原检验状态(空或T：合格、F：不合格、W：待检) |
| STOP_ID_NEW | 新停用注记 |
| STOP_ID_OLD | 原停用注记 |
| TI_FLAG_NEW | 新在检标记 |
| TI_FLAG_OLD | 原在检标记 |
| WH_NEW | 新库位 |
| WH_OLD | 原库位 |

## TF_BC (波次单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BC_DD | 波次日期 |
| BC_NO | 波次单号 |
| CAR_NO | 车号 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_ITM | ERP申请单项次 |
| ERP_BIL_NO | ERP申请单号 |
| EST_DH_DD | 预计到货时间 |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| ORG_BIL_ID | 业务单ID |
| ORG_BIL_ITM | 业务单项次 |
| ORG_BIL_NO | 业务单号 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY_CK | 已出库量 |
| QTY_JR | 已转拣货任务量 |
| QTY_JT | 拣货退回量 |
| QTY_PK | 已转拣货量 |
| QTY1 | 数量(副) |
| QTY1_CK | 已出库量(副) |
| QTY1_JR | 已转拣货任务量(副) |
| QTY1_JT | 拣货退回量(副) |
| QTY1_PK | 已转拣货量(副) |
| REM | 摘要 |
| TZ_ID | 通知单ID |
| TZ_ITM | 通知单项次 |
| TZ_NO | 通知单号 |
| UNIT | 单位 |
| WH | 仓库代号 |

## TF_BINDCK (备货单绑定作业表身)

| 字段名 | 中文说明 |
|--------|---------|
| BB_NO | 绑定单号 |
| CONTAIN_CODE | 容器条码 |
| ITM | 项次 |
| QTY | 绑定数量 |
| TZ_ITM | 出库通知单唯一项次 |
| TZ_NO | 出库通知单号 |

## TF_CHECK (储位复核单表身)

| 字段名 | 中文说明 |
|--------|---------|
| CHK_DD | 复核日期 |
| CHK_NO | 复核单号 |
| CHUW | 储位代号 |
| CONTAIN_CODE_LK | 立库容器条码 |
| CONTAIN_CODE_WMS | WMS容器条码 |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| PI_NO |  |
| YC_ID | 异常标记 |
| YC_TYPE | 异常类型(1、WMS调整 2、下架盘点 3、人工排查) |

## TF_CJ (车间交接单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BIL_ID | 转入单ID |
| BIL_ITM | 转入单项次 |
| BIL_NO | 转入单号 |
| CJ_DD | 入库日期 |
| CJ_NO | 车间入库单号 |
| ERP_BIL_DD | 来源单单据日期 |
| ERP_BIL_ID | ERP申请单据别 |
| ERP_BIL_ITM | ERP申请单项次 |
| ERP_BIL_NO | ERP申请单号 |
| FCP_FLAG | 副产品标记 |
| FLAG_JY | 检验标志（T 检验/F 免检） |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| OTH_BIL_NO | 外部系统单号 |
| OTH_ID | 转入单外部系统标识 |
| PRD_MARK | 特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY_LOST_RTN | 不合格退回量 |
| QTY_RK | 已入库量 |
| QTY_TY | 验货量 |
| QTY1 | 数量（副） |
| QTY1_LOST_RTN | 不合格退回量(副) |
| QTY1_RK | 已入库量(副) |
| QTY1_TY | 验货量(副) |
| REM | 摘要 |
| SC_DD | 生产日期 |
| UNIT | 单位 |
| VALID_DD | 有效日期 |
| WH | 仓库代号 |

## TF_CK_RCV (出库单单据附属信息)

| 字段名 | 中文说明 |
|--------|---------|
| ADR | 收货地址 |
| CELL_NO | 收货人电话 |
| CK_NO | 出库单号 |
| CON_MAN | 收货人 |
| COT_ID | 收货国家 |
| COUN_ID | 收货省市区 |
| CUS_NAME | 快递公司名称 |
| CUS_NO | 快递公司 |
| FH_NO | 快递单号 |
| ZIP | 邮编 |

## TF_CK (出库单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BC_ITM | 波次单项次 |
| BC_NO | 波次单号 |
| BIL_ID | 转入单据别 |
| BIL_ITM | 转入单项次 |
| BIL_NO | 转入单号 |
| BUS_ID | 业务单ID |
| BUS_ITM | 业务单项次 |
| BUS_NO | 业务单号 |
| CHUW | 储位代号 |
| CK_DD | 出库日期 |
| CK_NO | 出库单号 |
| ERP_AP_ID | ERP申请单ID |
| ERP_AP_ITM | ERP申请单项次 |
| ERP_AP_NO | ERP申请单号 |
| EST_DH_DD | 预计到货时间 |
| EXT_SYS_ID | 外部系统单据ID |
| EXT_SYS_ITM | 外部系统单据项次 |
| EXT_SYS_NO | 外部系统单号 |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| NL_ITM | 挪料单项次 |
| NL_NO | 挪料单号 |
| PK_ID | 拣货/分拣单ID |
| PK_ITM | 拣货/分拣单项次 |
| PK_NO | 拣货/分拣单号 |
| PRD_MARK | 特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY1 | 数量(副) |
| REM | 摘要 |
| SC_DD | 生产日期 |
| TY_ITM | 检验单项次 |
| TY_NO | 检验单号 |
| UNIT | 单位 |
| VALID_DD | 有效日期 |
| WH | 仓库 |

## TF_CKTB (出库退回通知表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BIL_ID | 转入单据ID |
| BIL_ITM | 转入单项次 |
| BIL_NO | 转入单号 |
| CHG_ITM | 变更项次 |
| CHG_NO | 变更单号 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_ITM | ERP申请单项次 |
| ERP_BIL_NO | ERP申请单号 |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| ORG_BIL_ID | 业务单ID |
| ORG_BIL_ITM | 业务单项次 |
| ORG_BIL_NO | 业务单号 |
| OTH_BIL_ID | 外部系统单据ID |
| OTH_BIL_ITM | 外部系统单据项次 |
| OTH_BIL_NO | 外部系统单号 |
| PRD_MARK | 特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY1 | 数量(副) |
| REM | 摘要 |
| TB_DD | 退回日期 |
| TB_NO | 出库退回通知单号 |
| UNIT | 单位 |
| WH | 仓库代号 |
| WH_ERP | ERP 仓库代号 |

## TF_CKTZ_CHG (出库通知变更单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 原批号 |
| CHG_METHOD | 变更方式（C变更、D删除） |
| CHG_NO | 变更单号 |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| NEW_BAT_NO | 新批号 |
| NEW_PRD_MARK | 新货品特征 |
| NEW_PRD_NAME | 新货品名称 |
| NEW_PRD_NO | 新货品代号 |
| NEW_QTY | 新数量 |
| NEW_QTY1 | 新数量（副） |
| NEW_REM | 新摘要 |
| NEW_TZ_ITM | 新通知单项次 |
| NEW_TZ_NO | 新通知单号 |
| NEW_WH | 新仓库代号 |
| NO_BAT_NO | 无批号 |
| ORG_ITM | 原单项次 |
| PRD_MARK | 原货品特征 |
| PRD_NAME | 原货品名称 |
| PRD_NO | 原货品代号 |
| QTY | 原数量 |
| QTY1 | 原数量（副） |
| REASON | 变更原因说明 |
| REM | 原摘要 |
| TB_ITM | 退回单项次 |
| TB_NO | 退回通知单号 |
| TZ_NO | 通知单号 |
| UNIT | 单位 |
| WH | 原仓库代号 |

## TF_CKTZ_RCV (出库通知单附属信息表)

| 字段名 | 中文说明 |
|--------|---------|
| ADR | 收货地址 |
| CELL_NO | 收货人电话 |
| CON_MAN | 收货人 |
| COT_ID | 收货国家 |
| COUN_ID | 收货省市区 |
| CUS_NAME | 快递公司名称 |
| CUS_NO | 快递公司 |
| FH_NO | 快递单号 |
| TZ_NO | 出库通知单号 |
| ZIP | 邮编 |

## TF_CKTZ (出库通知单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CHG_ITM | 变更项次 |
| CHG_NO | 变更单号 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_ITM | ERP申请单项次 |
| ERP_BIL_NO | ERP申请单号 |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| ORG_BIL_ID | 业务单ID |
| ORG_BIL_ITM | 业务单项次 |
| ORG_BIL_NO | 业务单号 |
| OTH_BIL_ID | 外部系统单据ID |
| OTH_BIL_ITM | 外部系统单据项次 |
| OTH_BIL_NO | 外部系统单号 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY_BB | 托盘绑定数量 |
| QTY_BC | 已转计划出库量 |
| QTY_CK | 已出库量 |
| QTY_JT | 拣货退回量 |
| QTY_LOST | 检验不合格量 |
| QTY_PACK | 包装数量 |
| QTY_PK | 已拣货量 |
| QTY_RTN | 已退数量 |
| QTY_XR | 下架任务量 |
| QTY1 | 数量(副) |
| QTY1_BC | 已转计划出库量(副) |
| QTY1_CK | 已出库量(副) |
| QTY1_JT | 拣货退回量(副) |
| QTY1_LOST | 检验不合格量(副) |
| QTY1_PK | 已拣货量(副) |
| QTY1_RTN | 已退数量(副) |
| QTY1_XR | 下架任务量(副) |
| REM | 摘要 |
| TZ_DD | 通知日期 |
| TZ_NO | 出库通知单号 |
| UNIT | 单位 |
| WH | 仓库代号 |
| WH_ERP | ERP 仓库代号 |

## TF_CL (单据结案作业表身)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_NO | 来源单号 |
| CL_NO | 结案单号 |
| CL_TYPE | 结案类型（1 结案、2 反结案） |
| ITM | 项次 |
| REM | 备注 |

## TF_CONTAIN_HIS (物流容器变动历史表身)

| 字段名 | 中文说明 |
|--------|---------|
| BARCODE | 条码 |
| BARCODE_TYPE | 条码类型 |
| CHANGE_NO | 变动单号 |
| CONTAIN_CODE | 容器条码(冗余) |
| FQ_NO | 分区代号 |
| ITM | 项次 |
| MO_NO | 制令单号 |
| QTY | 件数 |
| QTY_SJ | 箱条码散出数量 |
| RK_NO | 入库单号 |
| USRS | 作业人员 |

## TF_CONTAIN (物流容器装箱表身)

| 字段名 | 中文说明 |
|--------|---------|
| BARCODE | 条码 |
| BARCODE_TYPE | 条码类型 |
| CONTAIN_CODE | 容器条码 |
| FCP_FLAG | 副产品标记 |
| FQ_NO | 分区代号 |
| ITM | 项次 |
| MO_NO | 制令单号 |
| QTY | 件数 |
| QTY1 | 数量(副) |
| RK_NO | 入库业务单号 |
| USRS | 作业人员(多个作业人员用；隔开) |

## TF_CP (初盘单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CHUW | 储位代号 |
| CP_NO | 初盘单号 |
| ITM | 项次 |
| PRD_MARK | 货品特征 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY1 | 数量(副) |
| UNIT | 单位 |

## TF_CWDB (储位调拨单明细表)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BIL_ID | 来源单据别 |
| BIL_ITM | 来源单项次 |
| BIL_NO | 来源单号 |
| CHUW1 | 出库储位 |
| CHUW2 | 入库储位 |
| DB_ID | 调拨单ID |
| DB_NO | 调拨单号 |
| ITM | 项次 |
| KEY_ITM | 唯一项次 |
| LST_IND | 最近入库日 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 品名 |
| PRD_NO | 品号 |
| QTY | 数量 |
| QTY1 | 数量副 |
| REM | 摘要 |
| UNIT | 单位 |
| VALID_DD | 有效日期 |
| WH | 仓库 |

## TF_CWSJ (货品储位上架单明细表)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BIL_ID | 来源单据别 |
| BIL_ITM | 来源单项次 |
| BIL_NO | 来源单号 |
| CHUW1 | 储位 |
| ITM | 项次 |
| KEY_ITM | 唯一项次 |
| LST_IND | 最近上架日 |
| LST_TYD | 最近检验日期 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 品名 |
| PRD_NO | 品号 |
| QTY | 数量 |
| QTY1 | 副单位数量 |
| REM | 摘要 |
| SJ_ID | 上架单ID |
| SJ_NO | 上架单号 |
| UNIT | 单位 |
| VALID_DD | 有效日期 |
| WH1 | 仓库 |

## TF_CWXJ (货品储位下架单明细表)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BIL_ID | 来源单据别 |
| BIL_ITM | 来源单项次 |
| BIL_NO | 来源单号 |
| CHUW1 | 储位 |
| ITM | 项次 |
| KEY_ITM | 唯一项次 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 品名 |
| PRD_NO | 品号 |
| QTY | 数量 |
| QTY1 | 副单位数量 |
| REM | 摘要 |
| UNIT | 单位 |
| VALID_DD | 有效日期 |
| WH | 仓库 |
| XJ_ID | 下架单ID |
| XJ_NO | 下架单号 |

## TF_DEF_EXP (自定义公式表)

| 字段名 | 中文说明 |
|--------|---------|
| DEF_NO | 代号 |
| EXP_ID | 表达式标识 |
| EXP_LINK | 表达式Link部分 |
| EXP_SELECT | 表达式Select部分 |
| EXP_SQL | 自定义SQL |
| EXP_WHERE | 表达式Where部分 |
| FLD_FLAG | 表头/表身 |
| FLD_NAME | 字段名称 |
| NS_NO | NS代号 |
| TABLE_NAME | 表名 |

## TF_DEF (自定义字段表身)

| 字段名 | 中文说明 |
|--------|---------|
| BOTTOM_ID | 显示在表尾否 |
| COL_SPAN | 跨列数量 |
| COND_NOTNULL | 必填条件 |
| CTRL_CONTENT | 控件信息 |
| CTRL_XML | 控件内容 |
| DEF_NO | 代号 |
| FLD_COND | 必填条件 |
| FLD_CTRL | 控件类型 |
| FLD_DEFAULT | 默认值 |
| FLD_FLAG | 表头/表身 |
| FLD_FORMULA | 计算表达式 |
| FLD_INDEX | 字段顺序值 |
| FLD_LEN | 字段长度 |
| FLD_NAME | 字段名称 |
| FLD_NULL | 允许为空 |
| FLD_READONLY | 字段只读否 |
| FLD_SQL | 自定义SQL |
| FLD_TYPE | 字段类型 |
| LAST_DD | 最后修改时间 |
| LAST_ID | 最后修改标识 |
| LINK_FLD | 直接关联栏位(同步sunlike) |
| NS_NO | NS代号 |
| REM_BIG5 | 繁体标签 |
| REM_ENG | 英文标签 |
| REM_GB | 简体标签 |
| STOP_ID | 停用否 |
| TAB_ID | TAB显示否 |
| TABLE_NAME | 表名 |
| TYPE_ID | 业务类型 |

## TF_FCHART_DX (webchart控件定义)

| 字段名 | 中文说明 |
|--------|---------|
| FC_NO | 自定义图表单号 |
| PROP_NAME | 属性名称 |
| PROP_REM | 属性备注 |
| PROP_VALUE | 属性值 |
| TFC_ITM | 项次 |

## TF_FCHART (FCHART表身)

| 字段名 | 中文说明 |
|--------|---------|
| CAPTION | 标题 |
| CHART_KND | 绘图分类 |
| CHART_TYPE | 绘图类型 |
| DATA_SCHEMA | 数据结构 |
| DATA_TOP_SUB | 取前几笔 |
| FC_NO | 自定义图表单号 |
| ITM | 项次 |
| SORT | 排序 |
| SOURCE_SQL_SUB | 条件SQL语句 |
| SOURCE_TABLE_SUB | 数据源表名索引 |

## TF_FLD (系统字段设定表身)

| 字段名 | 中文说明 |
|--------|---------|
| FLD_DEFAULT | 字段默认值 |
| FLD_FLAG | 表头表身 |
| FLD_INDEX | 字段次序 |
| FLD_NAME | 字段名称 |
| FLD_NO | 代号 |
| FLD_NOTNULL | 必填否 |
| FLD_SQL | 必填条件SQL |
| REM_BIG5 |  |
| REM_ENG |  |
| REM_GB | 标签 |
| STOP_ID | 停用否 |
| TABLE_NAME | 表名 |

## TF_IC (库存调拨单表身)

| 字段名 | 中文说明 |
|--------|---------|
| AREA_SH | 收货点 |
| BAT_NO | 批号 |
| BAT_NO_IN | 拨入批号 |
| BIL_ID | 转入单据别 |
| BIL_ITM | 转入单项次 |
| BIL_NO | 转入单号 |
| CHUW1 | 拨出储位 |
| CHUW2 | 拨入储位 |
| ERP_AP_ID | ERP申请单据别 |
| ERP_AP_ITM | ERP申请单项次 |
| ERP_AP_NO | ERP申请单号 |
| IC_DD | 单据日期 |
| IC_NO | 调拨单号 |
| ITM | 项次 |
| KEY_ITM | 唯一项次 |
| LST_IND2 | 最近入库时间 |
| LST_TYD | 最近检验日期 |
| PK_ID | 拣货/分拣单据别 |
| PK_ITM | 拣货/分拣单项次 |
| PK_NO | 拣货/分拣单号 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QJ_ITM | 请检单项次 |
| QJ_NO | 请检单号 |
| QTY | 数量 |
| QTY_IZ | 到货数量 |
| QTY1 | 数量(副) |
| QTY1_IZ | 到货数量(副) |
| REM | 摘要 |
| SC_DD | 生产日期 |
| UNIT | 单位 |
| VALID_DD | 有效日期 |
| WH1 | 拨出仓库 |
| WH1_ERP | 拨出仓库（ERP仓） |
| WH2 | 拨入仓库 |
| WH2_ERP | 拨入仓库（ERP仓） |

## TF_ICTZ_CHG (调拨通知变更单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 原批号 |
| CHG_METHOD | 变更方式（C变更、D删除） |
| CHG_NO | 变更单号 |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| NEW_BAT_NO | 新批号 |
| NEW_PRD_MARK | 新货品特征 |
| NEW_PRD_NAME | 新货品名称 |
| NEW_PRD_NO | 新货品代号 |
| NEW_QTY | 新数量 |
| NEW_QTY1 | 新数量（副） |
| NEW_REM | 新摘要 |
| NEW_TZ_ITM | 新通知单项次 |
| NEW_TZ_NO | 新通知单号 |
| NEW_WH1 | 新出货仓库代号 |
| NEW_WH2 | 新入货仓库代号 |
| NO_BAT_NO | 无批号 |
| ORG_ITM | 原单项次 |
| PRD_MARK | 原货品特征 |
| PRD_NAME | 原货品名称 |
| PRD_NO | 原货品代号 |
| QTY | 原数量 |
| QTY1 | 原数量（副） |
| REASON | 变更原因说明 |
| REM | 原摘要 |
| TB_ITM | 退回单项次 |
| TB_NO | 退回通知单号 |
| TZ_NO | 通知单号 |
| UNIT | 单位 |
| WH1 | 原出货仓库代号 |
| WH2 | 原入货仓库代号 |

## TF_ICTZ (调拨通知单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BAT_NO_IN | 拨入批号 |
| BIL_ID | 转入单据别 |
| BIL_ITM | 转入单项次 |
| BIL_NO | 转入单号 |
| CHG_ITM | 变更项次 |
| CHG_NO | 变更单号 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_ITM | ERP申请单项次 |
| ERP_BIL_NO | ERP申请单号 |
| ITM | 项次 |
| KEY_ITM | 唯一项次 |
| ORG_BIL_ID | ERP业务单ID |
| ORG_BIL_ITM | ERP业务单项次 |
| ORG_BIL_NO | ERP业务单号 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品代名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY_BC | 波次量 |
| QTY_CK | 出库量 |
| QTY_JT | 拣货退回量 |
| QTY_LOST | 检验不合格量 |
| QTY_PK | 拣货量 |
| QTY_RTN | 退回量 |
| QTY_XR | 下架任务量 |
| QTY1 | 数量(副) |
| QTY1_BC | 波次量(副) |
| QTY1_CK | 出库量(副) |
| QTY1_JT | 拣货退回量（副） |
| QTY1_LOST | 检验不合格量(副) |
| QTY1_PK | 拣货量(副) |
| QTY1_RTN | 退回量(副) |
| QTY1_XR | 下架任务量(副) |
| REM | 摘要 |
| TZ_DD | 单据日期 |
| TZ_NO | 通知单号 |
| UNIT | 单位 |
| WH1 | 出货仓库 |
| WH2 | 入货仓库 |

## TF_IZ (到货确认单)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CHUW | 储位代号 |
| IC_ITM | 调拨项次 |
| IC_NO | 调拨单号 |
| ITM | 项次 |
| IZ_DD | 到货日期 |
| IZ_NO | 到货单号 |
| KEY_ITM | 单据唯一项次 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY_LOST | 损耗量 |
| QTY1 | 数量(副) |
| QTY1_LOST | 损耗量(副) |
| REM | 备注 |
| UNIT | 单位 |
| VALID_DD | 有效期 |
| WH | 仓库 |

## TF_JHRW_PW (波次拣货任务配位表)

| 字段名 | 中文说明 |
|--------|---------|
| BARCODE_TYPE | 条码类型 |
| ITM | 项次 |
| JR_ITM | 任务单项次 |
| JR_NO | 任务单号 |
| QTY_BOX | 箱数量 |
| QTY_PW | 配位数量 |
| SC_FLAG | 散出标记 |

## TF_JHRW (波次拣货任务单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BC_ITM | 波次单项次 |
| BC_NO | 波次单号 |
| CAR_NO | 车号 |
| CHUW | 储位代号 |
| CONTAIN_CODE | 容器条码 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_ITM | ERP申请单项次 |
| ERP_BIL_NO | ERP申请单号 |
| EST_DH_DD | 预计到货时间 |
| ITM | 项次 |
| JR_NO | 波次拣货任务单号 |
| KEY_ITM | 转单唯一项次 |
| ORG_BIL_ID | 业务单ID |
| ORG_BIL_ITM | 业务单项次 |
| ORG_BIL_NO | 业务单号 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY_IMPERFECT | 残次料数量 |
| QTY_MISS | 缺料数量 |
| QTY_PK | 已拣货量 |
| QTY1 | 数量(副) |
| QTY1_IMPERFECT | 残次料数量(副) |
| QTY1_MISS | 缺料数量(副) |
| QTY1_PK | 已拣货量(副) |
| REM | 摘要 |
| TZ_ID | 通知单ID |
| TZ_ITM | 通知单项次 |
| TZ_NO | 通知单号 |
| UNIT | 单位 |
| WH | 仓库代号 |
| XJ_FLAG | 下架标记 |

## TF_JT (拣货退回单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BC_ITM | 波次单项次 |
| BC_NO | 波次单号 |
| BIL_ID | 转入单ID |
| BIL_ITM | 转入单项次 |
| BIL_NO | 转入单号 |
| CHUW | 储位代号 |
| ITM | 项次 |
| JT_DD | 退回日期 |
| JT_NO | 拣货退回单号 |
| KEY_ITM | 转单唯一项次 |
| PK_ID | 拣货/分拣单ID |
| PK_ITM | 拣货/分拣单项次 |
| PK_NO | 拣货/分拣单号 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY1 | 数量(副) |
| REM | 摘要 |
| UNIT | 单位 |
| WH | 仓库 |

## TF_KU (盘亏单-表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CHUW | 储位代号 |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| KU_DD | 单据日期 |
| KU_NO | 盘亏单号 |
| PD_ITM | 盘点单项次 |
| PD_NO | 盘点单号 |
| PRD_MARK | 特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY1 | 数量(副) |
| REM | 摘要 |
| SC_DD | 生产日期 |
| UNIT | 单位 |
| VALID_DD | 有效日期 |
| WH | 仓库 |

## TF_MJBF (模具/工装报废单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_NO | 模具/工装序号 |
| BF_NO | 报废单号 |
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单号 |
| CONTAIN_CODE | 容器条码 |
| PRD_NO | 模具/工装代号 |
| WH | 仓库代号 |

## TF_MJGH (模具/工装归还单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_NO | 模具/工装序号 |
| BF_NO | 报废单号 |
| CONTAIN_CODE | 容器条码 |
| CXKM_ID | 是否重新开模 |
| GH_NO | 归还单号 |
| GH_TYPE | 归还类别(1.可用回收 2.报废回收) |
| GS_NO | 归还申请单号 |
| PRD_NO | 模具/工装代号 |
| RC_NO | 任务处理单号 |
| SAL_NO | 归还人 |
| SJ_GH_DD | 实际归还日期 |
| USED_COUNT | 本次使用次数 |
| WH | 仓库代号 |
| XD_ID | 是否下单 |

## TF_MJGHSQ (模具/工装归还申请单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_NO | 模具/工装序号 |
| CANCEL_ID | 作废标记 |
| CONTAIN_CODE | 容器条码 |
| CXKM_ID | 是否重新开模 |
| GH_NO | 归还单号 |
| GH_TYPE | 归还类别(1.可用回收 2.报废回收) |
| GS_NO | 归还申请单号 |
| LY_NO | 领用单号 |
| MR_NO | 任务单号 |
| PRD_NO | 模具/工装代号 |
| RC_NO | 任务处理单号 |
| SAL_NO | 归还人 |
| SJ_GH_DD | 实际归还日期 |
| USED_COUNT | 本次使用次数 |
| WH | 仓库代号 |
| XD_ID | 是否下单 |

## TF_MJLY (模具/工装领用单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_NO | 模具/工装序号 |
| CONTAIN_CODE | 容器条码 |
| GH_DD | 预计归还日期 |
| GH_NO | 归还单号 |
| LS_NO | 领用申请单号 |
| LY_NO | 领用单号 |
| PRD_NO | 模具/工装代号 |
| RC_NO | 任务处理单号 |
| REASON_LY | 领用原因代号 |
| REM_LY | 领用补充说明 |
| SAL_NO | 领用人 |
| WH | 仓库代号 |

## TF_MJLYSQ (模具/工装领用申请单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_NO | 模具/工装序号 |
| CANCEL_ID | 作废标记 |
| CONTAIN_CODE | 容器条码 |
| GH_DD | 预计归还日期 |
| LS_NO | 领用申请单号 |
| LY_NO | 领用单号 |
| MR_NO | 任务单号 |
| PRD_NO | 模具/工装代号 |
| RC_NO | 任务处理单号 |
| REASON_LY | 领用原因代号 |
| REM_LY | 领用补充说明 |
| SAL_NO | 领用人 |
| WH | 仓库代号 |

## TF_MJRK (模具/工装入库单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_NO | 模具/工装序号 |
| CONTAIN_CODE | 容器条码 |
| MI_NO | 入库单号 |
| PRD_NO | 模具/工装代号 |
| RC_NO | 任务处理单号 |
| RS_NO | 入库申请单号 |
| WH | 仓库代号 |

## TF_MJRKSQ (模具/工装入库申请单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_NO | 模具/工装序号 |
| CANCEL_ID | 作废标记 |
| CONTAIN_CODE | 容器条码 |
| MI_NO | 入库单号 |
| MR_NO | 任务单号 |
| PRD_NO | 模具/工装代号 |
| RC_NO | 任务处理单号 |
| RS_NO | 入库申请单号 |
| WH | 仓库代号 |

## TF_MJRW (模具/工装任务单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_NO | 模具/工装序号 |
| BIL_ID | 来源单据别 |
| BIL_NO | 来源单号 |
| CONTAIN_CODE | 容器条码 |
| MR_NO | 任务单号 |
| PRD_NO | 模具/工装代号 |
| WH | 仓库代号 |

## TF_MJRWCL (模具/工装任务处理单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAR_NO | 模具/工装序号 |
| BIL_ID_SQ | 申请单ID |
| BIL_NO_SQ | 申请单号 |
| CONTAIN_CODE | 容器条码 |
| PRD_NO | 模具/工装模具代号 |
| RC_NO | 任务处理单号 |
| WH | 仓库代号 |

## TF_NL (挪料单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| ERP_BIL_ID | ERP申请单ID(领) |
| ERP_BIL_ID_B | ERP申请单ID(退) |
| ERP_BIL_ITM | ERP申请单项次(领) |
| ERP_BIL_ITM_B | ERP申请单项次(退) |
| ERP_BIL_NO | ERP申请单号(领) |
| ERP_BIL_NO_B | ERP申请单号(退) |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| NL_NO | 挪料单号 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY1 | 数量(副) |
| REM | 备注 |
| UNIT | 单位 |
| WH | 仓库 |

## TF_PACKAGE (出库包装单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| ITM | 项次 |
| KEY_ITM | 唯一项次 |
| PACKAGE_NO | 包装码 |
| PK_ID | 拣货单ID |
| PK_ITM | 拣货项次 |
| PK_NO | 拣货单号 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| TZ_ID | 通知单ID |
| TZ_ITM | 通知单项次 |
| TZ_NO | 通知单号 |
| UNIT | 单位 |

## TF_PD (盘点单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CHUW | 储位代号 |
| ITM | 项次 |
| KEY_ITM | 唯一项次 |
| PD_NO | 盘点单号 |
| PRD_MARK | 货品特征 |
| PRD_NO | 货品代号 |
| QTY_RNG | 差异数 |
| QTY1 | 帐载数量 |
| QTY1_RNG | 差异数(副) |
| QTY11 | 帐载数量(副) |
| QTY2 | 盘点数量 |
| QTY21 | 盘点数量(副) |
| REM | 备注 |
| UNIT | 单位 |

## TF_PDRW_PRDT (初盘任务单-货品明细)

| 字段名 | 中文说明 |
|--------|---------|
| CHUW | 储位 |
| PD_DD | 盘点日期 |
| PR_NO | 任务单号 |
| PRD_MARK | 货品特征 |
| PRD_NO | 货品代号 |

## TF_PDRW (盘点任务单表身)

| 字段名 | 中文说明 |
|--------|---------|
| CHUW | 储位代号 |
| ITM | 项次 |
| PD_DD | 盘点日期 |
| PR_NO | 任务单号 |

## TF_PK_FILE (附件档)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据别 |
| BIL_ITM | 单据项次 |
| BIL_NO | 单据号码 |
| CONTENT | 档案流文件 |
| FILE_DD | 上传时间 |
| FILE_ID | 档案编号 |
| FILE_NAME | 档案名称 |
| FILE_SIZE | 档案大小 |
| FILE_TYPE | 档案类型 |
| THUMBNAIL | 缩略图流文件 |
| USR | 上传人 |

## TF_PK (拣货单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BC_ITM | 波次项次 |
| BC_NO | 波次单号 |
| BIL_ID | 转入单据别 |
| BIL_ITM | 转入项次 |
| BIL_NO | 转入单号 |
| CHUW | 储位代号 |
| CK_ITM | 出库单项次(作废) |
| CK_NO | 出库单号(作废) |
| CONTAIN_CODE_BH | 绑定备货容器 |
| CUS_NAME | 客户名称 |
| CUS_NO | 客户代号 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_ITM | ERP申请单项次 |
| ERP_BIL_NO | ERP申请单号 |
| ITM | 项次 |
| JY_FLAG | 需检验标记 |
| KEY_ITM | 转单唯一项次 |
| NL_ITM | 挪料单项次 |
| NL_NO | 挪料单号 |
| ORG_BIL_ID | ERP业务单ID |
| ORG_BIL_ITM | ERP业务单项次 |
| ORG_BIL_NO | ERP业务单号 |
| PK_DD | 单据日期 |
| PK_NO | 拣货单号 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY_CK | 出库量 |
| QTY_JT | 拣货退回量 |
| QTY_LOST | 检验不合格量 |
| QTY_PACK | 包装数量 |
| QTY_TY | 验货量 |
| QTY1 | 数量(副) |
| QTY1_CK | 出库量(副) |
| QTY1_JT | 拣货退回量(副) |
| QTY1_LOST | 检验不合格量(副) |
| QTY1_TY | 验货量(副) |
| REM | 摘要 |
| TZ_ID | 通知单ID |
| TZ_ITM | 通知项次 |
| TZ_NO | 通知单号 |
| UNIT | 单位 |
| VALID_DD | 有效日期 |
| WH | 仓库代号 |
| XR_ITM | 下架任务单项次 |
| XR_NO | 下架任务单号 |

## TF_PKFJ (分拣单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 特征 |
| BC_ITM | 波次项次 |
| BC_NO | 波次单号 |
| BIL_ID | 转入单据别 |
| BIL_ITM | 转入项次 |
| BIL_NO | 转入单号 |
| CHUW | 储位代号 |
| CK_NO | 出库单号 |
| CUS_NAME | 客户名称 |
| CUS_NO | 客户代号 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_ITM | ERP申请单项次 |
| ERP_BIL_NO | ERP申请单号 |
| FJ_NO | 分拣单号 |
| ITM | 项次 |
| JY_FLAG | 需检验标记 |
| KEY_ITM | 转单唯一项次 |
| ORG_BIL_ID | ERP业务单ID |
| ORG_BIL_ITM | ERP业务单项次 |
| ORG_BIL_NO | ERP业务单号 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY_CK | 出库量 |
| QTY_IMPERFECT | 残次料数量 |
| QTY_JT | 拣货退回量 |
| QTY_LOST | 检验不合格量 |
| QTY_MISS | 缺料数量 |
| QTY_PACK | 包装数量 |
| QTY_TY | 验货量 |
| QTY1 | 数量(副) |
| QTY1_CK | 出库量(副) |
| QTY1_IMPERFECT | 残次料数量(副) |
| QTY1_JT | 拣货退回量(副) |
| QTY1_LOST | 检验不合格量(副) |
| QTY1_MISS | 缺料数量(副) |
| QTY1_TY | 验货量(副) |
| REM | 摘要 |
| TZ_ID | 通知单ID |
| TZ_ITM | 通知项次 |
| TZ_NO | 通知单号 |
| UNIT | 单位 |
| WH | 仓库 |

## TF_PKFJERR (分拣异常表)

| 字段名 | 中文说明 |
|--------|---------|
| BC_NO | 波次单号 |
| FIN_DD | 完成时间 |
| FJ_DD | 分拣时间 |

## TF_PS (AGV 配送单表身)

| 字段名 | 中文说明 |
|--------|---------|
| PR_NO | 任务单号 |
| PS_NO | 配送单号 |

## TF_QJRW (请检任务单(表身))

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CHUW | 储位代号 |
| ITM | 项次 |
| KEY_ITM | 唯一项次 |
| LST_TYD | 上次检验日期 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QJ_NO | 请检任务单号 |
| QTY | 数量 |
| QTY_LOST_RTN | 不合格退回量 |
| QTY_OK | 已转合格量 |
| QTY_TY | 验货量 |
| QTY1 | 数量(副) |
| QTY1_LOST_RTN | 不合格退回量(副) |
| QTY1_OK | 已转合格量(副) |
| QTY1_TY | 验货量(副) |
| REM | 摘要 |
| UNIT | 单位 |
| WH | 仓库 |
| WH_TY | 检验仓库 |

## TF_QRY (自定义报表表身)

| 字段名 | 中文说明 |
|--------|---------|
| FLD_NAME | 字段名 |
| FLD_TYPE | 字段类型 |
| ID | 代号 |
| REM_BIG5 | 繁体 |
| REM_ENG | 英文 |
| REM_GB | 简体 |

## TF_RACK_TYPE (移动货架类型(货位明细)-表身)

| 字段名 | 中文说明 |
|--------|---------|
| GL | 列 |
| GS | 排 |
| LAYER | 层 |
| RACK_TYPE | 类型代号 |

## TF_RACK (移动货架-货位条码)

| 字段名 | 中文说明 |
|--------|---------|
| GL | 列 |
| GS | 排 |
| HUOW | 货位条码 |
| LAYER | 层 |
| RACK_CODE | 货架码 |

## TF_REMVOE_RULE (入库条码拆码规则表身)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| ITM | 项次 |
| REM | 说明 |
| RULE_NO | 规则代号 |
| SEG_CODE | 分段编码 |
| START_P | 开始位置 |
| STOP_P | 截止位置 |

## TF_REMVOE_RULE1 (入库条码拆码规则-编码分段信息)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| ITM | 项次 |
| RULE_NO | 规则代号 |
| SEG_CODE | 分段代号 |
| SEG_NAME | 分段说明 |
| TEST_VALUE | 分段值 |

## TF_REMVOE_RULE2 (入库条码拆码规则-自定义编码与条码关系)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| CUSTOM_SQL | 自定义SQL |
| FIELD_NAME | 条码栏位值 |
| ITM | 项次 |
| RULE_NO | 规则代号 |
| SEG_CODE | 分段代号 |

## TF_RK_FILE (附件档)

| 字段名 | 中文说明 |
|--------|---------|
| BIL_ID | 单据别 |
| BIL_ITM | 单据项次 |
| BIL_NO | 单据号码 |
| CONTENT | 档案流文件 |
| FILE_DD | 上传时间 |
| FILE_ID | 档案编号 |
| FILE_NAME | 档案名称 |
| FILE_SIZE | 档案大小 |
| FILE_TYPE | 档案类型 |
| THUMBNAIL | 缩略图流文件 |
| USR | 上传人 |

## TF_RK (入库单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BIL_ID | 转入单据别 |
| BIL_ITM | 转入单项次 |
| BIL_NO | 转入单号 |
| BUS_ID | 业务单ID |
| BUS_ITM | 业务单项次 |
| BUS_NO | 业务单号 |
| CHUW | 储位 |
| ERP_AP_ID | ERP申请单ID |
| ERP_AP_ITM | ERP申请单项次 |
| ERP_AP_NO | ERP申请单号 |
| EXT_SYS_FLAG | 外部系统标识 |
| EXT_SYS_ITM | 外部系统单据项次 |
| EXT_SYS_NO | 外部系统单号 |
| FCP_FLAG | 副产品标记 |
| FCP_UP_ITM | 主产品项次 |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| LST_IND | 最近入库日 |
| LST_TYD | 最近检验日期 |
| NL_ITM | 挪料单项次 |
| NL_NO | 挪料单号 |
| PRD_MARK | 特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY_LOST | 损耗量 |
| QTY1 | 数量(副) |
| QTY1_LOST | 损耗量（副） |
| REM | 摘要 |
| RK_DD | 入库日期 |
| RK_FLOW | 入库流程(1.入库-上架、2.入库同时上架) |
| RK_NO | 入库单号 |
| SC_DD | 生产日期 |
| TI_ID | 送检单据别 |
| TI_ITM | 送检单项次 |
| TI_NO | 送检单号 |
| UNIT | 单位 |
| VALID_DD | 有效日期 |
| WH | 仓库 |

## TF_RKTZ_CHG (入库通知变更单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 原批号 |
| CHG_METHOD | 变更方式（C变更、D删除） |
| CHG_NO | 变更单号 |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| LST_TYD | 原最近检验日期 |
| NEW_BAT_NO | 新批号 |
| NEW_LST_TYD | 新最近检验日期 |
| NEW_PRD_MARK | 新货品特征 |
| NEW_PRD_NAME | 新货品名称 |
| NEW_PRD_NO | 新货品代号 |
| NEW_QTY | 新数量 |
| NEW_QTY1 | 新数量（副） |
| NEW_REM | 新摘要 |
| NEW_TZ_ITM | 新通知单项次 |
| NEW_TZ_NO | 新通知单号 |
| NEW_VALID_DD | 新有效日期 |
| NEW_WH | 新仓库代号 |
| NO_BAT_NO | 无批号 |
| ORG_ITM | 原单项次 |
| PRD_MARK | 原货品特征 |
| PRD_NAME | 原货品名称 |
| PRD_NO | 原货品代号 |
| QTY | 原数量 |
| QTY1 | 原数量（副） |
| REASON | 变更原因说明 |
| REM | 原摘要 |
| TZ_NO | 通知单号 |
| UNIT | 单位 |
| VALID_DD | 原有效日期 |
| WH | 原仓库代号 |

## TF_RKTZ (入库通知单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BIL_ID | 来源单ID |
| BIL_ITM | 来源单项次 |
| BIL_NO | 来源单号 |
| CHG_ITM | 变更单项次 |
| CHG_NO | 变更单号 |
| CUS_NAME | 客户名称 |
| CUS_NO | 客户代号 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_ITM | ERP申请单项次 |
| ERP_BIL_NO | ERP申请单号 |
| FCP_FLAG | 副产品标记 |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| LST_TYD | 最近检验日期 |
| ORG_BIL_ID | 业务单ID |
| ORG_BIL_ITM | 业务单项次 |
| ORG_BIL_NO | 业务单号 |
| OTH_BIL_ID | 外部系统单据ID |
| OTH_BIL_ITM | 外部系统单据项次 |
| OTH_BIL_NO | 外部系统单号 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY_RK | 已入库量 |
| QTY1 | 数量(副) |
| QTY1_RK | 已入库量(副) |
| REM | 摘要 |
| SA_ITM | 销售订单行号 |
| SA_NO | 销售订单 |
| SC_DD | 生产日期 |
| SO_NO | 受订单号 |
| TI_ID | 送检单据别 |
| TI_ITM | 送检单项次 |
| TI_NO | 送检单号 |
| TZ_DD | 通知日期 |
| TZ_NO | 入库通知单号 |
| UNIT | 单位 |
| VALID_DD | 有效日期 |
| WH | 仓库代号 |
| WH_ERP | ERP 仓库代号 |

## TF_RPT (报表类型表身)

| 字段名 | 中文说明 |
|--------|---------|
| CHK_COND | 是否条件 |
| CHK_DISP | 显示否 |
| CHK_STATS | 是否分组统计字段 |
| CHK_SUM | 合计字段否 |
| COND_TYPE | 条件类型 |
| FLD_INDEX | 次序 |
| FLD_LEN | 字段长度 |
| FLD_NAME | 栏位名称 |
| FLD_PARA | 字段参数 |
| FLD_STYLE | 字段样式 |
| ITM | 项次 |
| PGM | PGM |
| REM_BIG5 | 繁体名称 |
| REM_ENG | 英文名称 |
| REM_GB | 简体名称 |
| SUM_TYPE | 合计方式 |

## TF_SBPG (设备派工单-表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BIL_ID | 来源单单据别 |
| BIL_NO | 来源单号 |
| END_DD | 预完工时间 |
| ITM | 项次 |
| KEY_ITM | 转单项次 |
| MRP_MARK | 成品特征 |
| MRP_NAME | 成品名称 |
| MRP_NO | 成品代号 |
| OS_NO | 受订单号 |
| PG_DD | 派工日期 |
| PG_NO | 派工单号 |
| QTY | 原单数量 |
| QTY_FIN | 已完工量 |
| QTY_PG | 派工数量 |
| REM | 备注 |
| STA_DD | 预开工时间 |

## TF_SCTLJH (生产投料计划单-投料顺序表)

| 字段名 | 中文说明 |
|--------|---------|
| AUTO_CONTINUE | 继续投料-自动触发 |
| BAT_NO | 材料批号 |
| BIL_ID | 转入单据别 |
| BIL_ITM | 转入单项次 |
| BIL_NO | 转入单号 |
| CONTAIN_NUM | 容器个数 |
| CONTAIN_TOTAL | 容器总装箱数量 |
| INTERVAL_FIRST | 首次投料后间隔时间(分钟) |
| ITM | 投料顺序项次 |
| PRD_MARK | 材料特征 |
| PRD_NAME | 材料名称 |
| PRD_NO | 材料代号 |
| QTY | 投料数量 |
| QTY_PAK_FIRST | 首次投料包数 |
| REM | 备注 |
| TL_NO | 计划投料单号 |
| TL_SN | 投料顺序 |

## TF_SCTLJH1 (生产投料计划单-投料计划明细表)

| 字段名 | 中文说明 |
|--------|---------|
| ACT_NO_CONTINUE | 继续投料任务单号 |
| ACT_NO_TL | 投料任务号 |
| ACT_NO_XJ | 下架任务单号 |
| BIL_ID | 转入单据别 |
| BIL_ITM | 转入单项次 |
| BIL_NO | 转入单号 |
| CONTAIN_CODE | 容器条码 |
| FIN_DD | 任务完成时间 |
| FIN_DD_FIRST | 首次投料完成时间 |
| ITM | 项次 |
| ITM_SN | 投料顺序项次 |
| PS_NO_TLK | 投料口配送任务单号 |
| PS_NO_ZCQ | 投料暂存区配送任务单号 |
| QTY_CONTAIN | 容器装箱数量 |
| TL_DD_CONTINUE | 继续投料时间 |
| TL_DD_FIRST | 首次投料时间 |
| TL_NO | 计划投料单号 |
| TL_SN | 投料顺序 |

## TF_SH (收货单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BIL_ID | 转入单ID |
| BIL_ITM | 转入单项次 |
| BIL_NO | 转入单号 |
| ERP_BIL_DD | 来源单单据日期 |
| ERP_BIL_ID | ERP申请单据别 |
| ERP_BIL_ITM | ERP申请单项次 |
| ERP_BIL_NO | ERP申请单号 |
| ITM | 项次 |
| JY_FLAG | 检验标志（T 检验/F 免检） |
| KEY_ITM | 转单唯一项次 |
| OTH_ID | 转入单外部系统标识 |
| PRD_MARK | 特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY_LOST_RTN | 不合格退回量 |
| QTY_RK | 已入库量 |
| QTY_TY | 验货量 |
| QTY1 | 数量（副） |
| QTY1_LOST_RTN | 不合格退回量(副) |
| QTY1_RK | 已入库量(副) |
| QTY1_TY | 验货量(副) |
| REM | 摘要 |
| SC_DD | 生产日期 |
| SH_DD | 收货日期 |
| SH_NO | 收货单号 |
| UNIT | 单位 |
| VALID_DD | 有效日期 |
| WH | 仓库 |

## TF_SQ (车间收料确认单-表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| ITM | 项次 |
| KEY_ITM | 转单项次 |
| PRD_MARK | 特征 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| SQ_NO | 车间收料确认单 |

## TF_TY (检验单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| BIL_ID | 转入单号ID |
| BIL_ITM | 转入项次 |
| BIL_NO | 转入单号 |
| BIL_NO_SPC | 不合格处理单号 |
| CHUW | 储位 |
| ERP_BIL_ID | ERP申请单据别 |
| ERP_BIL_ITM | ERP申请单项次 |
| ERP_BIL_NO | ERP申请单号 |
| FCP_FLAG | 副产品标记 |
| ITM | 项次 |
| KEY_ITM | 唯一项次 |
| ORG_BIL_ID | ERP业务单据别 |
| ORG_BIL_ITM | ERP业务单项次 |
| ORG_BIL_NO | ERP业务单号 |
| PRC_ID | 不合格处理建议(1验收退回;2让步接收;3报废;4入不良品仓;5678910 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 合格数量 |
| QTY_LOST | 不合格数量 |
| QTY_LOST_RTN | 不合格退回量 |
| QTY1 | 合格数量(副) |
| QTY1_LOST | 不合格数量(副) |
| QTY1_LOST_RTN | 不合格退回量（副） |
| REM | 摘要 |
| SPC_NO | 不合格原因 |
| TY_DD | 单据日期 |
| TY_NO | 检验单号 |
| TZ_ID | 通知单ID |
| TZ_ITM | 通知单项次 |
| TZ_NO | 通知单号 |
| UNIT | 单位 |
| WH | 仓库 |
| WH_QJ | 请检仓库 |
| WH_SPC | 不良品仓库代号 |

## TF_XJRW_PW (直接拣货任务配位表)

| 字段名 | 中文说明 |
|--------|---------|
| BARCODE_TYPE | 条码类型 |
| ITM | 项次 |
| QTY_BOX | 箱数量 |
| QTY_PW | 配位数量 |
| SC_FLAG | 散出标记 |
| XR_ITM | 任务单项次 |
| XR_NO | 任务单号 |

## TF_XJRW (直接拣货任务单表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CAR_NO | 车号 |
| CHUW | 储位代号 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_ITM | ERP申请单项次 |
| ERP_BIL_NO | ERP申请单号 |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| ORG_BIL_ID | 业务单ID |
| ORG_BIL_ITM | 业务单项次 |
| ORG_BIL_NO | 业务单号 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY_PK | 拣货量 |
| QTY1 | 数量(副) |
| QTY1_PK | 拣货量(副) |
| TZ_ID | 通知单ID |
| TZ_ITM | 通知单项次 |
| TZ_NO | 通知单号 |
| UNIT | 单位 |
| WH | 仓库代号 |
| XR_NO | 直接拣货任务单号 |

## TF_YB (验收退回单(表身))

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| ERP_BIL_ID | ERP申请单ID |
| ERP_BIL_ITM | ERP申请单据项次 |
| ERP_BIL_NO | ERP申请单号 |
| ITM | 项次 |
| KEY_ITM | 唯一项次 |
| ORG_BIL_ID | 业务单据别 |
| ORG_BIL_ITM | 业务单项次 |
| ORG_BIL_NO | 业务单号 |
| PRD_MARK | 货品特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY1 | 数量(副) |
| REM | 备注 |
| TI_ID | 送检单据别 |
| TI_ITM | 送检单项次 |
| TI_NO | 送检单号 |
| TY_ITM | 检验项次 |
| TY_NO | 检验单号 |
| UNIT | 单位 |
| WH | 仓库 |
| YB_NO | 验收退回单号 |

## TF_YN (盘盈单-表身)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CHUW | 储位代号 |
| ITM | 项次 |
| KEY_ITM | 转单唯一项次 |
| LST_IND | 最近入库日 |
| LST_TYD | 最近检验日期 |
| PD_ITM | 盘点单项次 |
| PD_NO | 盘点单号 |
| PRD_MARK | 特征 |
| PRD_NAME | 货品名称 |
| PRD_NO | 货品代号 |
| QTY | 数量 |
| QTY1 | 数量(副) |
| REM | 摘要 |
| SC_DD | 生产日期 |
| UNIT | 单位 |
| VALID_DD | 有效日期 |
| WH | 仓库 |
| YN_DD | 单据日期 |
| YN_NO | 盘盈单号 |

## TUNNEL_TASK (巷道待执行任务表)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CHUW | 待上/下架储位 |
| CHUW_ENTRY | 入口储位代号 |
| CK_NO | 出库业务单号 |
| CON_NO | 货主编码 |
| PRD_MARK | 货品特征 |
| PRD_NO | 货品代号 |
| TASK_NO | 任务代号 |
| TRUCK_NO | 叉车号 |
| TUNNEL_NO | 巷道号 |
| TYPE_ID | 类型(1.上架 2.移储) |
| WH | 仓库代号 |

## TUNNEL_USED (巷道使用表)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CHUW_ENTRY | 入口储位代号 |
| CHUW_ENTRY_USED | 入方向占用储位代号 |
| CHUW_EXIT | 出口储位代号 |
| CHUW_EXIT_USED | 出方向占用储位代号 |
| CK_NO | 出库业务单号 |
| CON_NO | 货主编码 |
| COUNT_CHUW_USED | 储位占用数 |
| LOCK_ID | 巷道锁定状态(1.正常 2.锁定) |
| PRD_MARK | 货品特征 |
| PRD_NO | 货品代号 |
| SORT_ENTRY | 入方向排序(asc.升序 desc.降序) |
| SORT_EXIT | 出方向排序(asc.升序 desc.降序) |
| STATUS_ID | 巷道状态(1.正常、2.异常) |
| TUNNEL_NO | 巷道号 |
| WH | 仓库代号 |

## TUNNEL (巷道基础表)

| 字段名 | 中文说明 |
|--------|---------|
| ENTRY_NO | 入口排/列号 |
| EXIT_NO | 出口排/列号 |
| GATE | 入口(1.单入口、2.双入口) |
| TUNNEL_NO | 巷道号 |
| WH | 仓库代号 |

## USERRPT (用户报表格式档)

| 字段名 | 中文说明 |
|--------|---------|
| CONTENT | 内容 |
| DEF_ID | 缺省注记 |
| ISSYSDATE | 是否取系统当前日期 |
| NAME | 报表名称 |
| PGM | 程序代号 |
| RPT_NO | 报表代号 |
| TYPE_ID | 制表类型 |
| USR_NO | 用户代号 |

## USERVIEW_BS (用户视图)

| 字段名 | 中文说明 |
|--------|---------|
| CTRL_ID | GRID ID |
| DEP | 集团分公司 |
| FIX_ID | 是否冻结 |
| FLD_INDEX | 栏位顺序 |
| FLD_NAME | 栏位名称 |
| FLD_WIDTH | 栏位宽度 |
| PGM | PGM |
| SHOW_ID | 显示否 |
| USR | 用户代号 |

## USERVIEW_DEL (用户视图-删除库)

| 字段名 | 中文说明 |
|--------|---------|
| CTRL_ID | 控件代号 |
| ITM | 项次 |
| LANG_ID | 语言别(0:简1:繁:英) |
| PGM | 程序代号 |
| UP_DD | 上次更新时间 |
| USR_NO | 用户代号 |

## USERVIEW (用户视图档)

| 字段名 | 中文说明 |
|--------|---------|
| CHK_FL | 离线视图否 |
| CONTENT | 内容 |
| CTRL_ID | 控件代号 |
| DEF_ID | 缺省注记 |
| ITM | 项次 |
| LANG_ID | 语言别(0:简1:繁:英) |
| NAME | 视图名称 |
| PGM | 程序代号 |
| UP_DD | 上次更新时间 |
| USR_NO | 用户代号 |

## VALIDDD_UPD_HIS (批号有效期修改历史表)

| 字段名 | 中文说明 |
|--------|---------|
| BAT_NO | 批号 |
| CON_NO | 货主编码 |
| HIS_NO | 项次 |
| PRD_MARK | 特征 |
| PRD_NO | 货品代号 |
| TASK_NO | 推送任务代号 |
| UPD_DATE | 修改时间 |
| UPD_USER | 修改人 |
| VALID_DD_CUR | 新有效期 |
| VALID_DD_ORG | 原有效期 |
| WH | 仓库代号 |

## WEBMENU_BS (WEB版菜单数据)

| 字段名 | 中文说明 |
|--------|---------|
| DEFAULTMENU | 默认菜单 |
| FAVO | 收藏菜单内容 |
| FLOWINFO | 流程图数据 |
| MENUINFO | 菜单数据 |
| MENUNAME | 菜单模块名 |
| MODINFO | 模组内容 |
| TYPE | 菜单模式 |
| USR | 用户代号 |

## WMS_LOG (WMS日志表)

| 字段名 | 中文说明 |
|--------|---------|
| CODE | 日志Code(自定义) |
| CONTENT | 日志内容 |
| DEVICE_ID |  |
| ID | 日志ID(取SessionID) |
| IS_EXCEPTION | 是否为异常 |
| ITM | 项次 |
| LOGIN_ID |  |
| PGM | 程序代号 |
| REM1 | 备注1(标记作用) |
| REM2 | 备注2(标记作用) |
| SYS_DATE | 日志时间 |
| USR | 用户 |

## XJ_RULE_PROP (下架策略参数表)

| 字段名 | 中文说明 |
|--------|---------|
| PROP_NO | 参数名 |
| REM | 备注 |
| RULE_ID | 下架策略代号 |
| VALUE | 参数值 |

## XJ_RULE (下架策略主表)

| 字段名 | 中文说明 |
|--------|---------|
| DEP | 集团公司 |
| MODIFY_DD | 最近修改日期 |
| MODIFY_MAN | 最近修改人 |
| NAME | 下架策略名称 |
| RULE_ID | 下架策略代号 |
| STOP_ID | 停用标记 |
| SYS_DATE | 录入时间 |
| USR | 录入人员 |
| WH_TYPE | 上下架模式(1.手持PDA模式 2.自动化接口模式 3.叉车任务模式) |

## ZC_NO (制程表头)

| 字段名 | 中文说明 |
|--------|---------|
| CON_NO | 货主编码 |
| NAME | 制程名称 |
| ZC_NO | 制程代号 |
| ZC_UP | 制程上级 |

