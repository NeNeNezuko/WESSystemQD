namespace WmsPlus.Api.Models;

/// <summary>
/// 盘亏(库存调整)单表头（MF_KU）
/// 对应数据库 db_gz01.MF_KU 表
/// </summary>
public class MfKu
{
    /// <summary>盘亏单号（主键）</summary>
    public string KU_NO { get; set; } = "";

    /// <summary>单据日期</summary>
    public DateTime? KU_DD { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>经办人</summary>
    public string? SAL_NO { get; set; }

    /// <summary>单据类别</summary>
    public string? BIL_TYPE { get; set; }

    /// <summary>业务类型</summary>
    public string? TYPE_ID { get; set; }

    /// <summary>盘点单号</summary>
    public string? PD_NO { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>制单人</summary>
    public string? USR { get; set; }

    /// <summary>输单日期</summary>
    public DateTime? SYS_DATE { get; set; }

    /// <summary>打印注记</summary>
    public string? PRT_SW { get; set; }

    /// <summary>打印人</summary>
    public string? PRT_USR { get; set; }

    /// <summary>打印日期</summary>
    public DateTime? PRT_DATE { get; set; }

    /// <summary>最近修改日期</summary>
    public DateTime? MODIFY_DD { get; set; }

    /// <summary>最近修改人</summary>
    public string? MODIFY_MAN { get; set; }

    /// <summary>拷贝注记</summary>
    public string? CPY_SW { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>容器条码</summary>
    public string? CONTAIN_CODE { get; set; }

    /// <summary>WMS产生的单据</summary>
    public string? WMS_ID { get; set; }

    /// <summary>第三方系统标识</summary>
    public string? REF_ID { get; set; }

    /// <summary>第三方系统单号</summary>
    public string? OTH_BIL_NO { get; set; }

    /// <summary>推送ERP任务代号</summary>
    public string? ACT_NO_PUSH { get; set; }

    /// <summary>启用单据确认</summary>
    public string? CFM_SW { get; set; }

    /// <summary>确认人</summary>
    public string? CFM_USR { get; set; }

    /// <summary>确认时间</summary>
    public DateTime? CFM_DATE { get; set; }

    /// <summary>调整原因</summary>
    public string? IJ_REASON { get; set; }
}
