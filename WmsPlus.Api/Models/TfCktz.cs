namespace WmsPlus.Api.Models;

/// <summary>
/// 出库通知单表身（TF_CKTZ）
/// 对应数据库 db_gz01.TF_CKTZ 表
/// 通过 TZ_NO 与 MF_CKTZ 关联
/// </summary>
public class TfCktz
{
    /// <summary>单据号码（关联MF_CKTZ.TZ_NO，主键之一）</summary>
    public string TZ_NO { get; set; } = "";

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }

    /// <summary>行日期</summary>
    public DateTime? TZ_DD { get; set; }

    /// <summary>料号/品号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>品名</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>规格型号</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>有效日期</summary>
    public DateTime? VALID_DD { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>客户名称</summary>
    public string? CUS_NAME { get; set; }

    /// <summary>波次单号</summary>
    public string? BC_NO { get; set; }

    /// <summary>已转波次数量</summary>
    public decimal? CONVERT_QTY { get; set; }

    /// <summary>退回数量</summary>
    public decimal? RETURN_QTY { get; set; }

    /// <summary>已拣数量</summary>
    public decimal? PICK_QTY { get; set; }
}
