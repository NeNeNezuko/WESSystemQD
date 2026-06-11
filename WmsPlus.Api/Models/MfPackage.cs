namespace WmsPlus.Api.Models;

/// <summary>
/// 出库包装单表头（MF_PACKAGE）
/// 对应数据库 db_gz01.MF_PACKAGE 表
/// </summary>
public class MfPackage
{
    /// <summary>包装箱号（主键）</summary>
    public string PACKAGE_NO { get; set; } = "";

    /// <summary>包装日期</summary>
    public DateTime? PACKAGE_DD { get; set; }

    /// <summary>业务类型</summary>
    public string? TYPE_ID { get; set; }

    /// <summary>包装人员代号</summary>
    public string? PACKAGER { get; set; }

    /// <summary>包装时间</summary>
    public DateTime? PACK_TIME { get; set; }

    /// <summary>出库业务单号</summary>
    public string? OUT_BIL_NO { get; set; }

    /// <summary>客户代号</summary>
    public string? CUS_NO { get; set; }

    /// <summary>出库状态（未包装/已包装）</summary>
    public string? OUT_STATUS { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>单据类别</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>制单人</summary>
    public string? USR { get; set; }

    /// <summary>输单时间</summary>
    public DateTime? SYS_DATE { get; set; }
}
