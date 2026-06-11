namespace WmsPlus.Api.Models;

/// <summary>
/// 直接拣货任务单表身（TF_XJRW）
/// 对应数据库 db_gz01.TF_XJRW 表
/// 通过 JR_NO 与 MF_XJRW 关联
/// </summary>
public class TfXjrw
{
    /// <summary>直接拣货任务单号（关联MF_XJRW.JR_NO，主键之一）</summary>
    public string JR_NO { get; set; } = "";

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }

    /// <summary>通知单ID</summary>
    public string? TZ_ID { get; set; }

    /// <summary>通知单号</summary>
    public string? TZ_NO { get; set; }

    /// <summary>通知单项次</summary>
    public int? TZ_ITM { get; set; }

    /// <summary>波次单号</summary>
    public string? BC_NO { get; set; }

    /// <summary>波次单项次</summary>
    public int? BC_ITM { get; set; }

    /// <summary>转单唯一项次</summary>
    public int? KEY_ITM { get; set; }

    /// <summary>料号/品号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>品名</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>规格型号</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>储位代号</summary>
    public string? CHUW { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>已拣数量</summary>
    public decimal? QTY_PK { get; set; }

    /// <summary>缺料数量</summary>
    public decimal? QTY_MISS { get; set; }

    /// <summary>变质料数量</summary>
    public decimal? QTY_IMPERFECT { get; set; }

    /// <summary>容器条码</summary>
    public string? CONTAIN_CODE { get; set; }

    /// <summary>车号</summary>
    public string? CAR_NO { get; set; }

    /// <summary>下架标记</summary>
    public string? XJ_FLAG { get; set; }

    /// <summary>预计到货时间</summary>
    public DateTime? EST_DH_DD { get; set; }

    /// <summary>业务单ID</summary>
    public string? ORG_BIL_ID { get; set; }

    /// <summary>业务单号</summary>
    public string? ORG_BIL_NO { get; set; }

    /// <summary>业务单项次</summary>
    public int? ORG_BIL_ITM { get; set; }

    /// <summary>ERP申请单ID</summary>
    public string? ERP_BIL_ID { get; set; }

    /// <summary>ERP申请单号</summary>
    public string? ERP_BIL_NO { get; set; }

    /// <summary>ERP申请单项次</summary>
    public int? ERP_BIL_ITM { get; set; }

    /// <summary>摘要/备注</summary>
    public string? REM { get; set; }
}
