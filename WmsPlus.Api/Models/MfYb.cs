namespace WmsPlus.Api.Models;

/// <summary>
/// 盘盈(验收入库)单表头（MF_YB）
/// 对应数据库 db_gz01.MF_YB 表
/// </summary>
public class MfYb
{
    /// <summary>单号（主键）</summary>
    public string YB_NO { get; set; } = "";

    /// <summary>单据日期</summary>
    public DateTime? YB_DD { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>经办人</summary>
    public string? USR { get; set; }

    /// <summary>系统建立时间</summary>
    public DateTime? SYS_DATE { get; set; }

    /// <summary>单据种类</summary>
    public string? BIL_KND { get; set; }

    /// <summary>业务类型</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>检验单号</summary>
    public string? TY_NO { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>客户代号</summary>
    public string? CUS_NO { get; set; }

    /// <summary>客户名称</summary>
    public string? CUS_NAME { get; set; }

    /// <summary>业务类型</summary>
    public string? TYPE_ID { get; set; }
}
