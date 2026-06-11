namespace WmsPlus.Api.Models;

/// <summary>
/// 货品条码表（PRDT_BARCODE）
/// 对应数据库 db_gz01.PRDT_BARCODE 表
/// </summary>
public class PrdtBarcode
{
    /// <summary>ID（主键）</summary>
    public int ID { get; set; }

    /// <summary>扫描码</summary>
    public string? SCAN_CODE { get; set; }

    /// <summary>条码</summary>
    public string? BARCODE { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品名称</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>来源单号</summary>
    public string? SOURCE_NO { get; set; }

    /// <summary>有效期</summary>
    public DateTime? VALID_DATE { get; set; }

    /// <summary>最近打印时间</summary>
    public DateTime? LAST_PRINT_TIME { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>已打印数量</summary>
    public decimal? PRINTED_QTY { get; set; }

    /// <summary>客户代号</summary>
    public string? CUS_NO { get; set; }

    /// <summary>客户名称</summary>
    public string? CUS_NAME { get; set; }

    /// <summary>受订单号</summary>
    public string? SO_NO { get; set; }

    /// <summary>来源单据交</summary>
    public string? SOURCE_DOC { get; set; }

    /// <summary>坯次</summary>
    public int? SEQ_NO { get; set; }

    /// <summary>原单数量</summary>
    public decimal? ORIG_QTY { get; set; }

    /// <summary>标签个数</summary>
    public int? LABEL_COUNT { get; set; }

    /// <summary>打印条码</summary>
    public string? PRINT_BARCODE { get; set; }

    /// <summary>创建日期</summary>
    public DateTime? CREATE_DD { get; set; }

    /// <summary>录入人</summary>
    public string? INPUT_USR { get; set; }

    /// <summary>录入批次</summary>
    public string? INPUT_BATCH { get; set; }

    /// <summary>制单人</summary>
    public string? USR { get; set; }
}
