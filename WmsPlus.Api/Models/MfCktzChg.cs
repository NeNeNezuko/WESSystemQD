namespace WmsPlus.Api.Models;

/// <summary>
/// 出库通知变更单表头（MF_CKTZ_CHG）
/// 对应数据库 db_gz01.MF_CKTZ_CHG 表
/// </summary>
public class MfCktzChg
{
    /// <summary>变更单号（主键）</summary>
    public string CHG_NO { get; set; } = "";

    /// <summary>变更日期</summary>
    public DateTime? CHG_DATE { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>业务类型</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>原单据号码（关联出库通知单）</summary>
    public string? TZ_NO { get; set; }

    /// <summary>执行状态</summary>
    public string? EXE_STATUS { get; set; }

    /// <summary>制单人</summary>
    public string? USR { get; set; }

    /// <summary>制单人名称</summary>
    public string? USR_NAME { get; set; }

    /// <summary>审核人</summary>
    public string? CHK_MAN { get; set; }

    /// <summary>系统建立时间</summary>
    public DateTime? SYS_DATE { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }
}
