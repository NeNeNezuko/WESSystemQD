namespace WmsPlus.Models
{
    public class CustomerVendorSettingModel
    {
        public string CusNo { get; set; } = "";
        public string CusName { get; set; } = "";
        public string StopDd { get; set; } = "";
        public string Rem { get; set; } = "";
    }

    public class CustomerVendorSettingQuery
    {
        public string CusNo { get; set; } = "";
        public string CusName { get; set; } = "";
    }

    /// <summary>客户/厂商资料设定新增/编辑表单模型</summary>
    public class CustomerVendorSettingAddModel
    {
        public string CusNo { get; set; } = "";
        public string CusName { get; set; } = "";
        public DateTime? StopDd { get; set; }
        public string Rem { get; set; } = "";
    }
}
