namespace WmsPlus.Models
{
    public class ContainHistoryQueryModel
    {
        public string ContainCode { get; set; } = "";
        public string ContainStatus { get; set; } = "";
        public string ContainType { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string TransitFlag { get; set; } = "";
        public string InspectFlag { get; set; } = "";
        public string ChangeDocName { get; set; } = "";
        public string ChangeNo { get; set; } = "";
        public string ChangeMan { get; set; } = "";
        public DateTime? ChangeTime { get; set; }
        // 新增字段
        public string ScatterItm { get; set; } = "";       // 散件项次
        public string BarcodeType { get; set; } = "";      // 条码类型
        public string Barcode { get; set; } = "";          // 条码
        public decimal PieceCount { get; set; }            // 件数
        public decimal ScatterQty { get; set; }            // 箱条码散出数量
        public string OuterBoxCode { get; set; } = "";     // 外箱码
        public string CwCode { get; set; } = "";           // 储位代号
        public string CwName { get; set; } = "";           // 储位名称
        public string CwPosition { get; set; } = "";       // 储位位置
        public string PickFlag { get; set; } = "";         // 拣货标记
        public string IsScatter { get; set; } = "";        // 是否散出
    }

    public class ContainHistoryQueryQuery
    {
        public string ChangeTimeRange { get; set; } = "";
        public DateTime? ChangeTimeFrom { get; set; }
        public DateTime? ChangeTimeTo { get; set; }
        public string ContainCode { get; set; } = "";
    }
}
