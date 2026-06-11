namespace WmsPlus.Models
{
    public class ContainBarcodePrintModel
    {
        public string ScanCode { get; set; } = "";
        public string ContainCode { get; set; } = "";
        public string ContainType { get; set; } = "";
        public string ContainStatus { get; set; } = "";
        public string WarehouseName { get; set; } = "";
        public string ChuwName { get; set; } = "";
        public string ChuwPos { get; set; } = "";
        public string TransitFlag { get; set; } = "";
        public string InspectFlag { get; set; } = "";
        public string ContainDetail { get; set; } = "";
        public string ModifyHistory { get; set; } = "";
    }

    public class ContainBarcodePrintQuery
    {
        public string DateRange { get; set; } = "";
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string CombineNo { get; set; } = "";
        public string Chuw { get; set; } = "";
        public string ContainType { get; set; } = "";
        public string ContainStatus { get; set; } = "";
        public string TransitFlag { get; set; } = "";
        public string InspectFlag { get; set; } = "";
        public string ContainCodeFrom { get; set; } = "";
        public string ContainCodeTo { get; set; } = "";
        public string InventoryDateRange { get; set; } = "";
        public DateTime? InventoryDateFrom { get; set; }
        public DateTime? InventoryDateTo { get; set; }
        public bool ShowEmptyOnly { get; set; }
    }
}
