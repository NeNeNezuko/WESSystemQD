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

    /// <summary>客户/厂商代号</summary>
    public string? CUS_NO { get; set; }

    /// <summary>客户/厂商名称</summary>
    public string? CUS_NAME { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>第三方系统单号</summary>
    public string? OTH_BIL_NO { get; set; }

    /// <summary>不合格结案</summary>
    public string? CLS_ID_SPC { get; set; }

    /// <summary>入库通知单号</summary>
    public string? TZ_NO_UO { get; set; }

    /// <summary>制单时间</summary>
    public DateTime? SYS_DATE { get; set; }
}
