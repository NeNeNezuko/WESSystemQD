namespace WmsPlus.Models
{
    /// <summary>
    /// 入库通知单报表 - 明细表行模型（对应明细表选项卡）
    /// </summary>
    public class InboundNoticeReportDetailModel
    {
        public int ItemNo { get; set; }
        public DateTime? MakeTime { get; set; }
        public string SourceNo { get; set; } = "";
        public string ErpApplyNo { get; set; } = "";
        public string DocNo { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
        public string BatNo { get; set; } = "";
        public decimal Qty { get; set; }
        public decimal QtyRk { get; set; }
        public string WhName { get; set; } = "";
    }

    /// <summary>
    /// 入库通知单报表 - 统计表行模型（对应统计表选项卡）
    /// </summary>
    public class InboundNoticeReportSummaryModel
    {
        public int ItemNo { get; set; }
        public DateTime? MakeTime { get; set; }
        public int DocYear { get; set; }
        public int DocQuarter { get; set; }
        public int DocMonth { get; set; }
        public int DocWeek { get; set; }
        public string BizOrderNo { get; set; } = "";
        public string ErpApplyNo { get; set; } = "";
        public string DocNo { get; set; } = "";
        public string PrdNo { get; set; } = "";
        public string PrdName { get; set; } = "";
    }

    /// <summary>
    /// 入库通知单报表 - 查询条件模型
    /// </summary>
    public class InboundNoticeReportQuery
    {
        // 制表日期范围
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        // 业务单号 + 模糊
        public string BizOrderNo { get; set; } = "";
        public bool FuzzyBizOrderNo { get; set; }

        // ERP申请单据号 + 模糊
        public string ErpApplyNo { get; set; } = "";
        public bool FuzzyErpApplyNo { get; set; }

        // 单据号码起止
        public string DocNoFrom { get; set; } = "";
        public string DocNoTo { get; set; } = "";

        // 货品代号起止
        public string PrdNoFrom { get; set; } = "";
        public string PrdNoTo { get; set; } = "";

        // 货品名称 + 模糊
        public string PrdName { get; set; } = "";
        public bool FuzzyPrdName { get; set; }

        // 仓库代号 + 含下属
        public string WarehouseCode { get; set; } = "";
        public bool IncludeChildWh { get; set; }
    }
}
