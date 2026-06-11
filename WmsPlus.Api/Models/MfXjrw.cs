namespace WmsPlus.Api.Models;

/// <summary>
/// 直接拣货任务单表头（MF_XJRW）
/// 对应数据库 db_gz01.MF_XJRW 表
/// </summary>
public class MfXjrw
{
    /// <summary>直接拣货任务单号（主键）</summary>
    public string JR_NO { get; set; } = "";

    /// <summary>直接拣货任务日期</summary>
    public DateTime? JR_DD { get; set; }

    /// <summary>部门代号</summary>
    public string? DEP { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>经办人</summary>
    public string? SAL_NO { get; set; }

    /// <summary>开单人</summary>
    public string? USR { get; set; }

    /// <summary>拣货人</summary>
    public string? USR_PK { get; set; }

    /// <summary>拣货结果标记</summary>
    public string? CLS_ID { get; set; }

    /// <summary>业务类型</summary>
    public string? TYPE_ID { get; set; }

    /// <summary>派工状态（0:未派工 1:已派工）</summary>
    public string? STATUS_PG { get; set; }

    /// <summary>收货点</summary>
    public string? AREA_SH { get; set; }

    /// <summary>排单级别（0:低 1:中 2:高）</summary>
    public int? PRIORITY { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>开单日期</summary>
    public DateTime? SYS_DATE { get; set; }

    /// <summary>拣货点（多个点;关闭;?）</summary>
    public string? PICK_POINT { get; set; }

    /// <summary>拣货工作站台号</summary>
    public string? WORK_STATION { get; set; }

    /// <summary>收货区域</summary>
    public string? RECEI_AREA { get; set; }

    /// <summary>产线代号</summary>
    public string? LINE_CODE { get; set; }

    /// <summary>区域代号</summary>
    public string? ZONE_ID { get; set; }

    /// <summary>AGV站点代号</summary>
    public string? AREA_NO { get; set; }

    /// <summary>站台代号</summary>
    public string? LINE_NO { get; set; }

    /// <summary>初圈任务单号</summary>
    public string? PR_NO { get; set; }

    /// <summary>异常类型（1:整板物料 2:缺料登记）</summary>
    public string? YC_TYPE { get; set; }

    /// <summary>车主编码</summary>
    public string? CON_NO { get; set; }

    /// <summary>需发送任务（T:未发送 F:已发送）</summary>
    public string? SEND_ACTION { get; set; }

    /// <summary>WCS优先级（1:统配 2:指定）</summary>
    public int? PRIORITY_WCS { get; set; }

    /// <summary>发货依据栏位</summary>
    public string? FHYJ_FIELD { get; set; }

    /// <summary>依单发货标识</summary>
    public string? BY_ORDER { get; set; }

    /// <summary>待入车号</summary>
    public string? CAR_NO { get; set; }

    /// <summary>立账出库处理代号</summary>
    public string? ACT_NO_OUT { get; set; }

    /// <summary>启用立账接口（1:不启用 2:启用）</summary>
    public string? WH_TYPE { get; set; }

    /// <summary>出库类型（1:整箱出库 2:拣选出库）</summary>
    public string? TYPE_CK { get; set; }

    /// <summary>拣货流程（1:全拣-分拣 2:直接拣货）</summary>
    public string? PK_FLOW { get; set; }

    /// <summary>容器条码</summary>
    public string? CONTAIN_CODE { get; set; }

    /// <summary>储位代号</summary>
    public string? CHUW { get; set; }

    /// <summary>新任务单号</summary>
    public string? JR_NO_NEW { get; set; }

    /// <summary>作废标记</summary>
    public string? CANCEL_ID { get; set; }

    /// <summary>获批备注</summary>
    public string? CPY_SW { get; set; }

    /// <summary>最近修改日期</summary>
    public DateTime? MODIFY_DD { get; set; }

    /// <summary>最近修改人</summary>
    public string? MODIFY_MAN { get; set; }

    /// <summary>打印日期</summary>
    public DateTime? PRT_DATE { get; set; }

    /// <summary>打印人员</summary>
    public string? PRT_USR { get; set; }

    /// <summary>打印备注</summary>
    public string? PRT_SW { get; set; }
}
