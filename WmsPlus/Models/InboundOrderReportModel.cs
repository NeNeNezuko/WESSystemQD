namespace WmsPlus.Models
{
    public class InboundOrderReportQuery
    {
        // 制单日期范围
        public string? DateRange { get; set; }  // 格式 "2026-06-01 ~ 2026-06-30"
        public bool DateFuzzy { get; set; } = false;  // 模糊匹配

        // ERP申请单号
        public string? ErpApNo { get; set; }

        // 单据号码 起/止
        public string? BilNoFrom { get; set; }
        public string? BilNoTo { get; set; }

        // 货品代号 起/止
        public string? PrdNoFrom { get; set; }
        public string? PrdNoTo { get; set; }

        // 货品名称
        public string? PrdName { get; set; }
        public bool PrdNameFuzzy { get; set; } = false;

        // 仓库名称
        public string? WhName { get; set; }
        public bool WhNameFuzzy { get; set; } = false;

        // 储位代号 起/止
        public string? ChuwFrom { get; set; }
        public string? ChuwTo { get; set; }

        // 分页
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    public class InboundOrderReportDetailItem
    {
        public int RowIdx { get; set; }           // 项次(自动编号)
        public DateTime? SysDate { get; set; }      // 制单时间(MF_RK.SYS_DATE)
        public string? BusNo { get; set; }          // 业务单号(TF_RK.BUS_NO)
        public string? ErpApNo { get; set; }        // ERP申请单号(MF_RK.ERP_AP_NO)
        public string? RkNo { get; set; }           // 单据号码(RK_NO)
        public string? PrdNo { get; set; }          // 货品代号(TF_RK.PRD_NO)
        public string? PrdName { get; set; }        // 货品名称(TF_RK.PRD_NAME)
        public string? BatNo { get; set; }          // 批号(TF_RK.BAT_NO)
        public string? WhName { get; set; }         // 仓库名称(MY_WH.NAME)
        public decimal? Qty { get; set; }           // 数量(TF_RK.QTY)
        public decimal? Qty1 { get; set; }          // 数量(副)(TF_RK.QTY1)
    }

    public class InboundOrderReportStatsItem
    {
        public int RowIdx { get; set; }           // 项次
        public int? YearPart { get; set; }        // 单据日期-年
        public int? QuarterPart { get; set; }     // 单据日期-季
        public int? MonthPart { get; set; }       // 单据日期-月
        public int? WeekPart { get; set; }        // 单据日期-周
        public string? PrdNo { get; set; }        // 货品代号
        public string? PrdName { get; set; }      // 货品名称
        public string? Wh { get; set; }           // 仓库代号
        public string? WhName { get; set; }       // 仓库名称
        public string? CusName { get; set; }      // 客户名称
        public decimal? TotalQty { get; set; }    // 数量(SUM)
        public decimal? TotalQty1 { get; set; }   // 数量(副)(SUM)
    }
}
