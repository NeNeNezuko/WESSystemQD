namespace WmsPlus.Api.Models;

/// <summary>
/// 车间入库单表身（TF_CJ）
/// 对应数据库 db_gz01.TF_CJ 表
/// 通过 CJ_NO 与 MF_CJ 关联
/// 复合主键：(CJ_NO, ITM)
/// </summary>
public class TfCj
{
    /// <summary>车间入库单号（关联MF_CJ.CJ_NO，主键之一）</summary>
    public string CJ_NO { get; set; } = "";

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品名称</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>数量(主单位)</summary>
    public decimal QTY { get; set; }

    /// <summary>数量(副单位)</summary>
    public decimal QTY1 { get; set; }

    /// <summary>单位</summary>
    public string? UNIT { get; set; }

    /// <summary>批号</summary>
    public string? BAT_NO { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>备注</summary>
    public string? REM { get; set; }
}
