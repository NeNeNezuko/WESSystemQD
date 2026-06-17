namespace WmsPlus.Models
{
    public class MidClassSettingModel
    {
        public string IdxNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string IdxUp { get; set; } = "";
        public DateTime? StopDd { get; set; }
        public string Rem { get; set; } = "";
    }

    public class MidClassSettingQuery
    {
        public string IdxNo { get; set; } = "";
        public string Name { get; set; } = "";
    }

    /// <summary>中类代号设定新增/编辑表单模型</summary>
    public class MidClassSettingAddModel
    {
        public string IdxNo { get; set; } = "";
        public string Name { get; set; } = "";
        public string IdxUp { get; set; } = "";
        public DateTime? StopDd { get; set; }
        public string Rem { get; set; } = "";
    }
}
