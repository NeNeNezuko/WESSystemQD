namespace WmsPlus.Api.Models;

/// <summary>
/// 序列号记录表（BAR_REC）
/// 对应数据库 db_gz01.BAR_REC 表
/// </summary>
public class BarRec
{
    public int ID { get; set; }
    public string? SCAN_CODE { get; set; }
    public string? SERIAL_NO { get; set; }
    public string? PRD_NO { get; set; }
    public string? PRD_NAME { get; set; }
    public string? BAT_NO { get; set; }
    public string? SOURCE_NO { get; set; }
    public int? SOURCE_ITM { get; set; }
    public DateTime? VALID_DATE { get; set; }
    public DateTime? LAST_PRINT_TIME { get; set; }
    public string? SERIAL_FROM { get; set; }
    public string? SERIAL_TO { get; set; }
    public DateTime? INVENTORY_DATE { get; set; }
    public bool? SHOW_EMPTY_ONLY { get; set; }
    public string? SOURCE_DOC { get; set; }
    public string? CUS_NAME { get; set; }
    public int? SEQ_NO { get; set; }
    public decimal? ORIG_QTY { get; set; }
    public decimal? PRINTED_QTY { get; set; }
    public int? LABEL_COUNT { get; set; }
    public string? PRINT_SERIAL { get; set; }
    public string? CUS_NO { get; set; }
    public string? SO_NO { get; set; }
    public DateTime? CREATE_DD { get; set; }
    public string? INPUT_USR { get; set; }
}
