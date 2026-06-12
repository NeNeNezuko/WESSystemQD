namespace WmsPlus.Models;

/// <summary>
/// 系统设定 - 完整页面数据模型
/// </summary>
public class SystemSettingModel
{
    // ===== 公司资料 =====
    public string CompanyCode { get; set; } = "GZM";
    public string CompanyName { get; set; } = "天津渤海化学试剂测试账套";
    public string CompanyNameEn { get; set; } = "";
    public string CompanyAddress { get; set; } = "";
    public string CompanyPhone { get; set; } = "";
    public DateTime? OpenDate { get; set; } = new DateTime(2024, 1, 1);
    public string DateFormat { get; set; } = "yyyy-MM-dd";
    public int DecimalPlaces { get; set; } = 2;
    public bool HideTrailingZero { get; set; }

    // ===== 密码策略 =====
    public string PasswordValidPeriod { get; set; } = "";
    public string PasswordInnerLimit { get; set; } = "无限制";
    public int? PasswordMinLength { get; set; }

    // ===== 系统设定 =====
    public bool ProductUseSerialNo { get; set; } = true;
    public bool AllowSerialReuse { get; set; }
    public string DefaultOutBizType { get; set; } = "";
    public string InTriggerBizType { get; set; } = "";
    public string InTriggerPrecondition { get; set; } = "";
    public string BizTypePriorityRule { get; set; } = "生产领料入库-托工领料入库-非生产领料入库";
    public string SunlikeCheck { get; set; } = "在WMS系统检验";
    public string CloseReminderConfirm { get; set; } = "当前系统日期";
    public string InventoryPrintSource { get; set; } = "验收品";

    // ===== 启用储位管理 =====
    public int StorageExecTimePoint { get; set; } = 3;
    public int StorageCalcDays { get; set; } = 45;

    // ===== 扫描设定 =====
    public bool ScanTempInputRequired { get; set; }
    public int ScanMultiModeHotkey { get; set; } = 3;

    // ===== 重整库存服务 (占位) =====

    // ===== 前缀设定（表格数据）=====
    public List<PrefixSettingItem> PrefixItems { get; set; } = new();

    // ===== 货品二维码组成 =====
    public bool QrcodeIncludeExternalBarcode { get; set; }
    public string QrcodeSeparator { get; set; } = "";
    public string QrcodeInfoSection { get; set; } = "";
    public string OutBizTypeSection { get; set; } = "";
    public string InBizTypeSection { get; set; } = "";
    public string DataExistSection { get; set; } = "";
    public string DefaultBarcodeTypeWithoutPrefix { get; set; } = "制";

    // ===== 信息加密处理 =====
    public bool EncryptCustomerName { get; set; } = true;
    public bool EncryptVendorName { get; set; } = true;
    public bool EncryptReceiveInfo { get; set; }

    // ===== 对接ERP系统 =====
    public bool EnableErpIntegration { get; set; }
    public string ErpApiUrl { get; set; } = "";

    // Sunlike系统
    public bool EnableSunlikeApi { get; set; }
    public string SunlikeUrl { get; set; } = "";
    public string SunlikeAccountCode { get; set; } = "";
    public string SunlikeCompany { get; set; } = "";
    public string SunlikeProduceMode { get; set; } = "ERP传递账套";
    public DateTime? SunlikeEnableDate { get; set; } = new DateTime(2024, 1, 1);
    public int? SunlikeAllowPendingDays { get; set; }

    // TB/企业系统
    public bool EnableTbEnterpriseApi { get; set; }
    public string TbUrl { get; set; } = "";
    public string TbAccountCode { get; set; } = "";
    public string TbErpBuyerValue { get; set; } = "按到ERP购入人";

    // 金蝶星空
    public bool EnableKingdeeApi { get; set; }
    public string KingdeeUrl { get; set; } = "";
    public string KingdeeAppId { get; set; } = "";
    public string KingdeeAppSecret { get; set; } = "";
    public string KingdeeLedgerId { get; set; } = "";
    public string KingdeeUsername { get; set; } = "";
    public string KingdeeTenantCode { get; set; } = "";
    public string KingdeeWmsWarehouse { get; set; } = "";
    public string KingdeeDefaultFromUser { get; set; } = "";
    public string KingdeeDefaultFromDoor { get; set; } = "";
    public DateTime? KingdeeEnableDate { get; set; } = new DateTime(2024, 4, 1);
    public int? KingdeeOrderDateRange { get; set; } = 30;
    public string KingdeeProductionOrderType { get; set; } = "";
    public string KingdeeInboundNotAutoSubmit { get; set; } = "";
    public string KingdeeOutboundNotAutoSubmit { get; set; } = "";

    // ===== 与其他系统对接 =====
    // MES系统
    public bool EnableMesApi { get; set; }
    public string MesUrl { get; set; } = "";
    public string MesAccountCode { get; set; } = "";

    // PTL电子标签对接
    public bool EnablePtlTagIntegration { get; set; }

    // ===== API接口设置 =====
    public string ExternalLogName { get; set; } = "多个以|符号开隔";
    public string ExternalReadPort { get; set; } = "";

    // ===== 可视化仓储视图参数 - 储位异示 =====
    public string StorageMapDisplayStyle { get; set; } = "1:完整置数结果";
    public bool StorageMapAutoRefreshToggle { get; set; }
    public int StorageMapRefreshSeconds { get; set; } = 600;

    // 可视化仓储视图参数 - 仓储营查
    public bool ShowStorageInfo { get; set; } = true;
    public bool ShowInboundInfo { get; set; } = true;
    public bool ShowInventoryInfo { get; set; } = true;

    // 可视化仓储视图参数 - 储缺设定
    public string StorageInfoDisplayItems { get; set; } = "储位代号+储位状态+批次状态+特别项+别区域+区代号";
    public string ContainerBarcodeStatusItems { get; set; } = "容器条码使用状态+混装+最近盘存日期";
    public string ProductCodeCategoryItems { get; set; } = "货品代码+品类名称+货品特征+其号";

    // 异常管理
    public bool ShowExceptionInfo { get; set; } = true;

    // 功能链理
    public string DynamicStorageDisplayItems { get; set; } = "下载异常储位+储位状态点意+移除生成初历任务";

    // ===== 汇总推进入库单服务 =====
    public bool EnableInboundSummary { get; set; }
    public string InboundSummaryCondition { get; set; } = "单版日期+申单位ID+申单编号号+\"厂代号\"+部门";

    // ===== 自动生成波次单 =====
    public bool AutoGenerateMainWave { get; set; }
    public string AutoWaveWarehouse { get; set; } = "";
    public string AutoWaveDefaultUser { get; set; } = "";
    public int? AutoWaveDailyCount { get; set; } = 10;
    public int? AutoWaveDateRangeDays { get; set; } = 30;
    public string AutoWaveBizTypes { get; set; } = "";

    // ===== 自动生成直接拣货任务单 =====
    public bool AutoGeneratePickTask { get; set; }
    public string AutoPickWarehouse { get; set; } = "";
    public string AutoPickDefaultUser { get; set; } = "";
    public int? AutoPickDailyCount { get; set; } = 10;
    public int? AutoPickDateRangeDays { get; set; } = 30;
    public string AutoPickBizTypes { get; set; } = "";

    // 功能按钮显示项
    public string FunctionButtonDisplay { get; set; } = "下载异常储位+储位状态点意+移除生成初历任务";

    // ===== 第三方接口设置 =====
    public bool EnableThirdPartyErpApi { get; set; }
    public string ThirdPartyUrl { get; set; } = "";
    public string ThirdPartyOrderMode { get; set; } = "";
    public string ThirdPartySystemFlag { get; set; } = "";
    public bool EnableThirdPartyApi { get; set; }
    public string ThirdPartyDefaultUser { get; set; } = "";

    // ===== 货品特征设定 =====
    public bool ProductUseMark { get; set; }
    public string MarkName { get; set; } = "";
    public bool MarkInputControlled { get; set; }
    public string MarkSegmentMode { get; set; } = "";
    public bool MarkSegmentToggle { get; set; }

    // ===== RFID设定 =====
    public int? RfidDefaultSeconds { get; set; }
    public int? RfidBarcodeSeconds { get; set; }

    // ===== 条码重用 =====
    public bool BarcodeAllowReuse { get; set; }
}

/// <summary>
/// 前缀设定表格行项
/// </summary>
public class PrefixSettingItem
{
    public string Label { get; set; } = "";
    public string Value { get; set; } = "";
}

/// <summary>
/// 导航项数据模型
/// </summary>
public class NavItem
{
    public string Id { get; }
    public string Title { get; }

    public NavItem(string id, string title)
    {
        Id = id;
        Title = title;
    }
}
