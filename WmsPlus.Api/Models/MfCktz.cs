namespace WmsPlus.Api.Models;

/// <summary>
/// 出库通知单表头（MF_CKTZ）
/// 对应数据库 db_gz01.MF_CKTZ 表
/// </summary>
public class MfCktz
{
    /// <summary>单据号码（主键）</summary>
    public string TZ_NO { get; set; } = "";

    /// <summary>单据日期</summary>
    public DateTime? TZ_DD { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>业务员代号</summary>
    public string? SAL_NO { get; set; }

    /// <summary>业务类型</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>客户代号</summary>
    public string? CUS_NO { get; set; }

    /// <summary>客户名称</summary>
    public string? CUS_NAME { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>申请单号</summary>
    public string? APPLY_NO { get; set; }

    /// <summary>预计交货日期</summary>
    public DateTime? EST_DD { get; set; }

    /// <summary>预计出库日期</summary>
    public DateTime? EXPECT_DD { get; set; }

    /// <summary>优先级</summary>
    public int? PRIORITY { get; set; }

    /// <summary>结案否</summary>
    public string? CLS_ID { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>制单人</summary>
    public string? USR { get; set; }

    /// <summary>系统建立时间</summary>
    public DateTime? SYS_DATE { get; set; }
}
