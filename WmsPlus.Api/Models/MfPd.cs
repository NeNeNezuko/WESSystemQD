namespace WmsPlus.Api.Models;

/// <summary>
/// 盘点单据表头（MF_PD）
/// 对应数据库 db_gz01.MF_PD 表
/// </summary>
public class MfPd
{
    /// <summary>盘点单号（主键）</summary>
    public string PD_NO { get; set; } = "";

    /// <summary>盘点日期</summary>
    public DateTime? PD_DD { get; set; }

    /// <summary>帐载日期</summary>
    public DateTime? PD_DD1 { get; set; }

    /// <summary>调整调增单号</summary>
    public string? IJ_NO { get; set; }

    /// <summary>调整调减单号</summary>
    public string? IJ_NO1 { get; set; }

    /// <summary>盘点人员</summary>
    public string? USR_PD { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>条码差异处理</summary>
    public string? TMCY_FLAG { get; set; }

    /// <summary>出库处理代号</summary>
    public string? ACT_NO { get; set; }

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

    /// <summary>修改人</summary>
    public string? MODIFY_MAN { get; set; }

    /// <summary>修改日期</summary>
    public DateTime? MODIFY_DD { get; set; }

    /// <summary>是否自动化仓库</summary>
    public string? AUTO_ID { get; set; }

    /// <summary>容器条码</summary>
    public string? CONTAIN_CODE { get; set; }

    /// <summary>转入容器条码</summary>
    public string? CONTAIN_CODE_IN { get; set; }

    /// <summary>汇总推送否</summary>
    public string? SUM_ID { get; set; }

    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>盘点方式(1:按储位盘点,2:按储位+货品盘点)</summary>
    public string? PD_TYPE { get; set; }

    /// <summary>厂商代号</summary>
    public string? CUS_NO { get; set; }

    /// <summary>任务类型(循环盘点/按区域盘点/复盘/盘点)</summary>
    public string? TASK_TYPE { get; set; }

    /// <summary>差异处理(立即处理/复盘)</summary>
    public string? DIFF_CL { get; set; }

    /// <summary>复盘方式(容器复盘/复盘差异储位/复盘差异货品)</summary>
    public string? FP_TYPE { get; set; }

    /// <summary>初盘任务单号(复盘)</summary>
    public string? PR_NO_FP { get; set; }

    /// <summary>启用单据确认</summary>
    public string? CFM_SW { get; set; }

    /// <summary>确认人</summary>
    public string? CFM_USR { get; set; }

    /// <summary>确认时间</summary>
    public DateTime? CFM_DATE { get; set; }
}
