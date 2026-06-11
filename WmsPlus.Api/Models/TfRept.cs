namespace WmsPlus.Api.Models;

/// <summary>
/// 拣货报表（TF_REPT）
/// 对应数据库 db_gz01.TF_REPT 表
/// </summary>
public class TfRept
{
    /// <summary>ID（主键）</summary>
    public int ID { get; set; }

    /// <summary>报表单号</summary>
    public string? REPT_NO { get; set; }

    /// <summary>项次</summary>
    public int? ITM { get; set; }

    /// <summary>货品代号</summary>
    public string? PRD_NO { get; set; }

    /// <summary>货品名称</summary>
    public string? PRD_NAME { get; set; }

    /// <summary>数量</summary>
    public decimal? QTY { get; set; }

    /// <summary>已拣数量</summary>
    public decimal? QTY_PK { get; set; }

    /// <summary>仓库代号</summary>
    public string? WH { get; set; }

    /// <summary>储位代号</summary>
    public string? CHUW { get; set; }
}
