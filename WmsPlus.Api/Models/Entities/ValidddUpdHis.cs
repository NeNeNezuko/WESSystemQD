namespace WmsPlus.Api.Models.Entities;

/// <summary>
/// 批号有效期修改历史表（VALIDDD_UPD_HIS）
/// 对应数据库 db_gz01.VALIDDD_UPD_HIS 表
/// </summary>
public class ValidddUpdHis
{
    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>项次</summary>
    public int? HIS_NO { get; set; }

    /// <summary>特征</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>推送任务代号</summary>
    public string? TASK_NO { get; set; }

    /// <summary>修改时间</summary>
    public DateTime? UPD_DATE { get; set; }

    /// <summary>修改人</summary>
    public string? UP_USER { get; set; }

    /// <summary>新有效期</summary>
    public DateTime? VALID_DD_CUR { get; set; }

    /// <summary>原有效期</summary>
    public DateTime? VALID_DD_ORG { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }
}
