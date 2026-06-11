namespace WmsPlus.Api.Models;

/// <summary>
/// 箱条码变动历史表（BAR_BOX_CHANGE）
/// 对应数据库 db_gz01.BAR_BOX_CHANGE 表
/// </summary>
public class BarBoxChange
{
    public int ID { get; set; }
    public int? SEQ_NO { get; set; }
    public DateTime? CHANGE_TIME { get; set; }
    public string? BOX_BARCODE { get; set; }
    public string? PRD_NO { get; set; }
    public string? PRD_NAME { get; set; }
    public string? BAT_NO { get; set; }
    public string? SOURCE_DOC_TYPE { get; set; }
    public string? DOC_NAME { get; set; }
}
