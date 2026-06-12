namespace WmsPlus.Api.Models;

/// <summary>
/// 出库处理表(自动化)（LK_ACTION_O）
/// 对应数据库 db_gz01.LK_ACTION_O 表
/// </summary>
public class LkActionO
{
    /// <summary>处理代号(主键)</summary>
    public string? ACT_NO { get; set; }
    /// <summary>重新处理代号</summary>
    public string? ACT_NO_NEW { get; set; }
    /// <summary>出库完成处理代号</summary>
    public string? ACT_NO_FIN { get; set; }
    /// <summary>锁定处理代号</summary>
    public string? ACT_NO_LOCK { get; set; }
    /// <summary>主任务代号</summary>
    public string? ACT_NO_MAIN { get; set; }
    /// <summary>中途任务完成代号</summary>
    public string? ACT_NO_MID { get; set; }
    /// <summary>接口名称</summary>
    public string? METHOD_NO { get; set; }
    /// <summary>请求方式</summary>
    public string? HTTP_METHOD { get; set; }
    /// <summary>请求路径</summary>
    public string? PATH { get; set; }
    /// <summary>帐套代号</summary>
    public string? COMPNO { get; set; }
    /// <summary>货主编码</summary>
    public string? CON_NO { get; set; }
    /// <summary>仓库代号</summary>
    public string? WH { get; set; }
    /// <summary>储位代号</summary>
    public string? CHUW { get; set; }
    /// <summary>容器条码</summary>
    public string? CONTAIN_CODE { get; set; }
    /// <summary>来源单据别</summary>
    public string? BIL_ID { get; set; }
    /// <summary>来源单据号码</summary>
    public string? BIL_NO { get; set; }
    /// <summary>接口厂商代号</summary>
    public string? SUP_NO { get; set; }
    /// <summary>第三方任务代号</summary>
    public string? OTH_TASK_ID { get; set; }
    /// <summary>成功否</summary>
    public string? STATUS_ID { get; set; }
    /// <summary>同步否</summary>
    public string? SYNC_ID { get; set; }
    /// <summary>推送否</summary>
    public string? PUSH_ID { get; set; }
    /// <summary>错误代码</summary>
    public string? ERR_CODE { get; set; }
    /// <summary>错误说明</summary>
    public string? ERR_MSG { get; set; }
    /// <summary>请求Json</summary>
    public string? JSON_CONTENT { get; set; }
    /// <summary>回传内容</summary>
    public string? RESULT_CONTENT { get; set; }
    /// <summary>请求内容大小</summary>
    public int? REQUEST_SIZE { get; set; }
    /// <summary>回传内容大小</summary>
    public int? RESULT_SIZE { get; set; }
    /// <summary>推送开始时间</summary>
    public DateTime? START_DATE { get; set; }
    /// <summary>推送结束时间</summary>
    public DateTime? END_DATE { get; set; }
    /// <summary>插入时间</summary>
    public DateTime? SYS_DATE { get; set; }
    /// <summary>执行次数</summary>
    public int? RUN_COUNT { get; set; }
    /// <summary>AGV起点</summary>
    public string? AREA_START { get; set; }
    /// <summary>AGV终点</summary>
    public string? AREA_END { get; set; }
    /// <summary>仓库2</summary>
    public string? WH2 { get; set; }
    /// <summary>储位2</summary>
    public string? CHUW2 { get; set; }
}
