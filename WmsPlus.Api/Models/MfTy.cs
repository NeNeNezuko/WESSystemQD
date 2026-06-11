namespace WmsPlus.Api.Models;

/// <summary>
/// 检验单表头（MF_TY）
/// 对应数据库 db_gz01.MF_TY 表
/// </summary>
public class MfTy
{
    /// <summary>单据号码（主键）</summary>
    public string TY_NO { get; set; } = "";

    /// <summary>单据日期</summary>
    public DateTime? TY_DD { get; set; }

    /// <summary>单据类型</summary>
    public string? BIL_KND { get; set; }

    /// <summary>检验位置</summary>
    public string? TYWZ { get; set; }

    /// <summary>部门</summary>
    public string? DEP { get; set; }

    /// <summary>单据类别</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>业务类型</summary>
    public string? TYPE_ID { get; set; }

    /// <summary>转入单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>制单人</summary>
    public string? USR { get; set; }
}
