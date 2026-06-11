namespace WmsPlus.Models
{
    public class WarehouseArrivalConfirmModel
    {
        public string WhOut { get; set; } = "";
        public string WhIn { get; set; } = "";
        public string Usr { get; set; } = "";
        public string ModifyMan { get; set; } = "";
        public DateTime? SysDate { get; set; }
        public DateTime? ModifyDd { get; set; }
    }

    public class WarehouseArrivalConfirmQuery
    {
        public string WhOut { get; set; } = "";
        public string WhIn { get; set; } = "";
    }
}
