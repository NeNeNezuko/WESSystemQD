namespace WmsPlus.Models;

/// <summary>
/// 出库任务分配作业 - 数据模型定义
/// </summary>

// ====== Tab1: 出库通知 - 通知单明细（从表） ======

public class OutboundNoticeDetailModel
{
    public string NoticeNo { get; set; } = "";
    public string WaveNo { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string BatchNo { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string Unit { get; set; } = "";
    public decimal OrigQty { get; set; }
    public decimal ConvertedWaveQty { get; set; }
    public decimal ReturnQty { get; set; }
    public decimal PickedQty { get; set; }
}

// ====== Tab2: 波次管理 - 波次单（主表） ======

public class WaveManageModel
{
    public int ItemNo { get; set; }
    public DateTime? WaveDate { get; set; }
    public string WaveNo { get; set; } = "";
    public string WarehouseName { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string OperatorName { get; set; } = "";
    public string ResultMark { get; set; } = "";
    public string Remark { get; set; } = "";
    public int PriorityLevel { get; set; }
    public string DispatchStatus { get; set; } = "";
}

// ====== Tab2: 波次管理 - 波次单明细（从表） ======

public class WaveManageDetailModel
{
    public DateTime? WaveDate { get; set; }
    public string WaveNo { get; set; } = "";
    public int ItemNo { get; set; }
    public string WarehouseName { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string BatchNo { get; set; } = "";
    public string Unit { get; set; } = "";
    public decimal? Qty { get; set; }
    public decimal? BoxQty { get; set; }
    public string Remark { get; set; } = "";
}

// ====== Tab3: 波次拣货任务管理 - 波次拣货任务单（主表） ======

public class WavePickTaskManageModel
{
    public int ItemNo { get; set; }
    public DateTime? AcceptDate { get; set; }
    public string TaskNo { get; set; } = "";
    public string DeptName { get; set; } = "";
    public string AcceptorName { get; set; } = "";
    public string AcceptResultMark { get; set; } = "";
    public string Remark { get; set; } = "";
    public int PriorityLevel { get; set; }
    public string DispatchStatus { get; set; } = "";
}

// ====== Tab3: 波次拣货任务管理 - 波次拣货任务单明细（从表） ======

public class WavePickTaskManageDetailModel
{
    public string TaskNo { get; set; } = "";
    public string WaveNo { get; set; } = "";
    public string PrdNo { get; set; } = "";
    public string PrdName { get; set; } = "";
    public string BatchNo { get; set; } = "";
    public string LocationCode { get; set; } = "";
    public string Unit { get; set; } = "";
    public decimal? Qty { get; set; }
    public decimal? BoxQty { get; set; }
    public decimal? PreparedQty { get; set; }
    public decimal? PreparedBoxQty { get; set; }
}

// ====== Tab4: 参数设定 ======

public class ParamSettingModel
{
    /// <summary>拣货员先为空(受串自动化)</summary>
    public bool PickerAutoAssign { get; set; }

    /// <summary>生成波次-显示明细窗口</summary>
    public bool ShowWaveDetailWindow { get; set; }
}

// ====== 统一查询模型（各Tab共用基础字段，按需扩展） ======

public class OutboundTaskAssignmentQuery
{
    // 通用查询条件
    public string DateRange { get; set; } = "";
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public string EstimateArriveRange { get; set; } = "";
    public DateTime? EstimateArriveFrom { get; set; }
    public DateTime? EstimateArriveTo { get; set; }
    public string WarehouseCode { get; set; } = "";
    public string BusinessType { get; set; } = "";
    public string ApplyOrderNumber { get; set; } = "";
    public string NoticeNumber { get; set; } = "";
    public string BusinessOrderNumber { get; set; } = "";

    // Tab1: 出库通知 特有
    public string SourceType { get; set; } = "";       // 来源单据类型（逗号分隔）
    public string ProcessStatus { get; set; } = "待处理"; // 处理状态
    public string TrackFlag { get; set; } = "";        // 跟踪否
    public string ReceivePoint { get; set; } = "";     // 收货点
    public string CustomerCode { get; set; } = "";     // 客户代号

    // Tab2: 波次管理 特有
    public string WaveNumber { get; set; } = "";       // 波次单号
    public string PriorityLevels { get; set; } = "";   // 进单等级（逗号分隔）
    public string FlowStatuses { get; set; } = "";     // 流程状态（逗号分隔）
    public string PrintListType { get; set; } = "全部"; // 打印列示

    // Tab3: 波次拣货任务管理 特有
    public string ProcessStatusPg { get; set; } = "未派工"; // 流程状态(派工)
}
