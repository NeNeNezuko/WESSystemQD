namespace WmsPlus.Api.Models;

/// <summary>
/// 波次单表身（TF_BC）
/// 对应数据库 db_gz01.TF_BC 表
/// </summary>
public class TfBc
{
    /// <summary>波次单号</summary>
    public string? BC_NO { get; set; }

    /// <summary>项次</summary>
    public int? ITM { get; set; }

    /// <summary>波次日期</summary>
    public DateTime? BC_DD { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品名称</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>已拣数量/箱量</summary>
    public decimal? PICK_QTY { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }

    /// <summary>货品特征码段</summary>
    public string? PRD_MARK { get; set; }
}
