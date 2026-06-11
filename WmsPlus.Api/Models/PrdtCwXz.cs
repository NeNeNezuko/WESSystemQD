namespace WmsPlus.Api.Models
{
    /// <summary>
    /// 依储存性设定货品储位（PRDT_CW_XZ）
    /// 对应数据库 db_gz01.PRDT_CW_XZ 表
    /// </summary>
    public class PrdtCwXz
    {
        public string? GUID { get; set; }
        public string? PRD_NO { get; set; }
        public string? CWXZ_NO { get; set; }
        public string? CHUW { get; set; }
        public string? WH { get; set; }
        public string? GS { get; set; }
        public string? GL { get; set; }
        public string? LAYER { get; set; }
        public string? ZONE_ID { get; set; }
        public string? CON_NO { get; set; }
        public string? DEP { get; set; }
        public string? REM { get; set; }
        public DateTime? UP_DD { get; set; }
    }
}
