namespace WmsPlus.Api.Models.Entities;

/// <summary>
/// 车间入库单表头（MF_CJ）
/// 对应数据库 db_gz01.MF_CJ 表
/// </summary>
public class MfCj
{
    /// <summary>车间入库单号（主键）</summary>
    public string CJ_NO { get; set; } = "";

    /// <summary>车间入库日期</summary>
    public DateTime? CJ_DD { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>业务员代号</summary>
    public string? SAL_NO { get; set; }

    /// <summary>检验标志</summary>
    public string? FLAG_JY { get; set; }

    /// <summary>业务类型</summary>
    public string? TYPE_ID { get; set; }

    /// <summary>单据类别</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>制单人</summary>
    public string? USR { get; set; }

    /// <summary>制单时间</summary>
    public DateTime? SYS_DATE { get; set; }

    /// <summary>修改人</summary>
    public string? MODIFY_MAN { get; set; }

    /// <summary>修改时间</summary>
    public DateTime? MODIFY_DD { get; set; }

    /// <summary>通易推送开关</summary>
    public string? TY_PUSH_SW { get; set; }

    /// <summary>通易系统标记</summary>
    public string? TY_SYS { get; set; }

    /// <summary>来源入库单号</summary>
    public string? RK_NO { get; set; }

    /// <summary>来源调拨单号</summary>
    public string? TZ_NO { get; set; }
}
