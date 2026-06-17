namespace WmsPlus.Models
{
    public class ApiExceptionTableModel
    {
        public string Itm { get; set; } = "";           // 项次
        public string TaskType { get; set; } = "";      // 任务类型
        public string ProcessNo { get; set; } = "";     // 处理代号
        public string ApiCode { get; set; } = "";       // 接口代号/名称
        public string SourceDocName { get; set; } = ""; // 来源单据名称
        public string SourceNo { get; set; } = "";      // 来源单号
        public string Wh { get; set; } = "";            // 仓库代号
        public string ContainCode { get; set; } = "";   // 容器条码
        public int ExecCount { get; set; }              // 执行次数
        public string ErrMsg { get; set; } = "";        // 错误说明
        public string HttpErrMsg { get; set; } = "";    // http错误说明

        // 统计表用
        public string ActNo { get; set; } = "";
        public int Count { get; set; }
    }

    public class ApiExceptionTableQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool TaskTypeLk { get; set; } = false;    // 立库系统调用
        public bool TaskTypeLkPush { get; set; } = false; // 立库任务推送
        public bool TaskTypeErp { get; set; } = false;    // ERP系统调用
        public bool TaskTypeErpPush { get; set; } = false; // ERP单据推送
        public string Wh { get; set; } = "";              // 仓库
        public string SourceNo { get; set; } = "";        // 来源单号
    }
}
