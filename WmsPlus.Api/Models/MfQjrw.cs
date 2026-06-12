namespace WmsPlus.Api.Models;

/// <summary>
/// 请检任务单表头（MF_QJRW）
/// 对应数据库 db_gz01.MF_QJRW 表
/// </summary>
public class MfQjrw
{
    /// <summary>请检任务单号（主键）</summary>
    public string QJ_NO { get; set; } = "";

    /// <summary>请检任务日期</summary>
    public DateTime? QJ_DD { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>检验仓库代号</summary>
    public string? WH_TY { get; set; }

    /// <summary>经办人</summary>
    public string? SAL_NO { get; set; }

    /// <summary>制单人/开单人</summary>
    public string? USR { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>开单日期</summary>
    public DateTime? SYS_DATE { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>单据类型(RK入库类型;CK出库检验;KC库存检验)</summary>
    public string? BIL_KND { get; set; }

    /// <summary>调拨通知单号</summary>
    public string? TN_NO { get; set; }

    /// <summary>下架标记</summary>
    public string? XJ_FLAG { get; set; }
}
