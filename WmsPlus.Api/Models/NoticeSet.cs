namespace WmsPlus.Api.Models;

/// <summary>
/// 即时消息通知设定表（NOTICE_SET）
/// 对应数据库 db_gz01.NOTICE_SET 表
/// </summary>
public class NoticeSet
{
    public string? SET_NO { get; set; }
    public string? TYPE_ID { get; set; }
    public string? WH { get; set; }
    public string? SEND_OBJ { get; set; }
    public string? SEND_USRS { get; set; }
    public string? SEND_TYPE { get; set; }
    public string? STOP_ID { get; set; }
    public string? USR { get; set; }
    public DateTime? SYS_DATE { get; set; }
}
