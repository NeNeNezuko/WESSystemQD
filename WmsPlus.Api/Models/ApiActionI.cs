namespace WmsPlus.Api.Models;

/// <summary>
/// 接口传入处理表（API_ACTION_I）
/// 对应数据库 db_gz01.API_ACTION_I 表
/// </summary>
public class ApiActionI
{
    /// <summary>处理代号(主键)</summary>
    public string? ACT_NO { get; set; }
    /// <summary>主任务代号</summary>
    public string? ACT_NO_MAIN { get; set; }
    /// <summary>重新处理代号</summary>
    public string? ACT_NO_NEW { get; set; }
    /// <summary>接口名称</summary>
    public string? METHOD_NO { get; set; }
    /// <summary>请求方式</summary>
    public string? HTTP_METHOD { get; set; }
    /// <summary>请求路径</summary>
    public string? PATH { get; set; }
    /// <summary>请求IP地址</summary>
    public string? CLIENT_IP { get; set; }
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
    /// <summary>产生单据别</summary>
    public string? BIL_ID { get; set; }
    /// <summary>产生单据号码</summary>
    public string? BIL_NO { get; set; }
    /// <summary>生成单据类别</summary>
    public string? BIL_TYPE { get; set; }
    /// <summary>接口厂商代号</summary>
    public string? SUP_NO { get; set; }
    /// <summary>第三方任务ID</summary>
    public string? OTH_TASK_ID { get; set; }
    /// <summary>外部系统单据ID</summary>
    public string? OTH_BIL_ID { get; set; }
    /// <summary>外部系统单据号</summary>
    public string? OTH_BIL_NO { get; set; }
    /// <summary>第三方系统标识</summary>
    public string? REF_ID { get; set; }
    /// <summary>成功否</summary>
    public string? STATUS_ID { get; set; }
    /// <summary>同步否</summary>
    public string? SYNC_ID { get; set; }
    /// <summary>推送否</summary>
    public string? PUSH_ID { get; set; }
    /// <summary>删除否</summary>
    public string? DEL_ID { get; set; }
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
    /// <summary>仓库2</summary>
    public string? WH2 { get; set; }
    /// <summary>储位2</summary>
    public string? CHUW2 { get; set; }
}
