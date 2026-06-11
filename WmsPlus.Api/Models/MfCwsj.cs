namespace WmsPlus.Api.Models;

/// <summary>
/// 储位上架单表头（MF_CWSJ）
/// 对应数据库 db_gz01.MF_CWSJ 表
/// </summary>
public class MfCwsj
{
    /// <summary>上架单号（主键）</summary>
    public string SJ_NO { get; set; } = "";

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>上架单ID</summary>
    public string? SJ_ID { get; set; }

    /// <summary>上架日期</summary>
    public DateTime? SJ_DD { get; set; }

    /// <summary>部门</summary>
    public string? DEP { get; set; }

    /// <summary>经办人</summary>
    public string? SAL_NO { get; set; }

    /// <summary>录入员</summary>
    public string? USR { get; set; }

    /// <summary>单据类别</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>来源单据别</summary>
    public string? BIL_ID { get; set; }

    /// <summary>来源单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>立库单据标志</summary>
    public string? LK_ID { get; set; }

    /// <summary>WMS产生的单据</summary>
    public string? WMS_ID { get; set; }

    /// <summary>输单日期</summary>
    public DateTime? SYS_DATE { get; set; }

    /// <summary>最近修改日期</summary>
    public DateTime? MODIFY_DD { get; set; }

    /// <summary>最近修改人</summary>
    public string? MODIFY_MAN { get; set; }

    /// <summary>打印日期</summary>
    public DateTime? PRT_DATE { get; set; }

    /// <summary>打印人员</summary>
    public string? PRT_USR { get; set; }

    /// <summary>打印注记</summary>
    public string? PRT_SW { get; set; }
}
