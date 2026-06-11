namespace WmsPlus.Api.Models;

/// <summary>
/// 收货单表头（MF_SH）
/// 对应数据库 db_gz01.MF_SH 表
/// </summary>
public class MfSh
{
    /// <summary>收货单号（主键）</summary>
    public string SH_NO { get; set; } = "";

    /// <summary>收货日期</summary>
    public DateTime? SH_DD { get; set; }

    /// <summary>转入单号</summary>
    public string? BIL_NO { get; set; }

    /// <summary>转入单ID</summary>
    public string? BIL_ID { get; set; }

    /// <summary>单据类别</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>业务类型</summary>
    public string? TYPE_ID { get; set; }

    /// <summary>厂商代号</summary>
    public string? CUS_NO { get; set; }

    /// <summary>厂商名称</summary>
    public string? CUS_NAME { get; set; }

    /// <summary>仓库</summary>
    public string? WH { get; set; }

    /// <summary>ERP仓库</summary>
    public string? WH_ERP { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>部门</summary>
    public string? DEP { get; set; }

    /// <summary>收货人</summary>
    public string? SAL_NO { get; set; }

    /// <summary>收货点</summary>
    public string? AREA_SH { get; set; }

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
}
