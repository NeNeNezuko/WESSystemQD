namespace WmsPlus.Api.Models;

/// <summary>
/// 箱条码表（PRDT_BARCODE_BOX）
/// 对应数据库 db_gz01.PRDT_BARCODE_BOX 表
/// </summary>
public class PrdtBarcodeBox
{
    public int ID { get; set; }
    public string? SCAN_CODE { get; set; }
    public string? BOX_BARCODE { get; set; }
    public string? PRD_NO { get; set; }
    public string? PRD_NAME { get; set; }
    public string? BAT_NO { get; set; }
    public decimal? QTY { get; set; }
    public string? SOURCE_NO { get; set; }
    public int? SOURCE_ITM { get; set; }
    public DateTime? VALID_DATE { get; set; }
    public string? CHANGE_HISTORY { get; set; }
    public DateTime? LAST_PRINT_TIME { get; set; }
    public string? OUTER_BOX_FLAG { get; set; }
    public string? SPECIAL_INSPECT { get; set; }
    public DateTime? INVENTORY_DATE { get; set; }
    public bool? SHOW_EMPTY_ONLY { get; set; }
    public string? SOURCE_DOC { get; set; }
    public string? CUS_NAME { get; set; }
    public int? SEQ_NO { get; set; }
    public decimal? ORIG_QTY { get; set; }
    public decimal? PRINTED_QTY { get; set; }
    public decimal? THIS_PRINT_QTY { get; set; }
    public decimal? STANDARD_BOX_QTY { get; set; }
    public decimal? TAIL_BOX_QTY { get; set; }
    public int? LABEL_COUNT { get; set; }
    public DateTime? CREATE_DD { get; set; }
    public string? INPUT_USR { get; set; }
    public string? CUS_NO { get; set; }
}
