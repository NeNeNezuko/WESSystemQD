namespace WmsPlus.Api.Models.Entities;

/// <summary>
/// 货品库存锁定表（PRDT1_LOCK）
/// 对应数据库 db_gz01.PRDT1_LOCK 表
/// </summary>
public class Prdt1Lock
{
    /// <summary>主键</summary>
    public string? GUID { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品特征</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>接口代号</summary>
    public string? ACT_NO { get; set; }

    /// <summary>锁定日期</summary>
    public DateTime? LOCK_DD { get; set; }
}
