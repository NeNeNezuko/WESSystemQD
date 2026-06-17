namespace WmsPlus.Models
{
    public class TransferNoticeReportModel
    {
        // 明细表字段
        public int ItemNo { get; set; }
        public string TZ_NO { get; set; } = "";
        public DateTime? TZ_DD { get; set; }
        public string WH1 { get; set; } = "";
        public string WH2 { get; set; } = "";
        public string PRD_NO { get; set; } = "";
        public string PRD_NAME { get; set; } = "";
        public decimal QTY { get; set; }
        public string REM { get; set; } = "";

        // 统计表字段（表头信息）
        public string DEP { get; set; } = "";
        public string SAL_NO { get; set; } = "";
        public DateTime? EST_DD { get; set; }
        public string AREA_SH { get; set; } = "";
        public string CLS_ID_BC { get; set; } = "";
        public string CLS_ID_CK { get; set; } = "";
        public string TYPE_ID { get; set; } = "";
        public DateTime? SYS_DATE { get; set; }
    }

    public class TransferNoticeReportQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string NoticeNumber { get; set; } = "";
        public string DepName { get; set; } = "";
        public string PrdNoFrom { get; set; } = "";
        public string PrdNoTo { get; set; } = "";
        public string PrdNameFrom { get; set; } = "";
        public string PrdNameTo { get; set; } = "";
        public string WhOut { get; set; } = "";
        public string WhIn { get; set; } = "";
        public string ErpApplyNo { get; set; } = "";
        public string BizNo { get; set; } = "";
        public string ThirdPartyNo { get; set; } = "";
        public string ClsIdBc { get; set; } = "";
        public string ClsIdPk { get; set; } = "";
        public string ClsIdCk { get; set; } = "";
    }
}
