namespace WmsPlus.Api.Models;

/// <summary>
/// 报表类型设定表身（TF_RPT）
/// 对应数据库 db_gz01.TF_RPT 表
/// </summary>
public class TfRpt
{
    /// <summary>报表代号(PGM)</summary>
    public string? PGM { get; set; }
    
    /// <summary>项次</summary>
    public int? ITM { get; set; }
    
    /// <summary>次序</summary>
    public int? FLD_INDEX { get; set; }
    
    /// <summary>栏位名称</summary>
    public string? FLD_NAME { get; set; }
    
    /// <summary>字段长度</summary>
    public int? FLD_LEN { get; set; }
    
    /// <summary>字段样式</summary>
    public string? FLD_STYLE { get; set; }
    
    /// <summary>字段参数</summary>
    public string? FLD_PARA { get; set; }
    
    /// <summary>是否条件</summary>
    public string? CHK_COND { get; set; }
    
    /// <summary>显示否</summary>
    public string? CHK_DISP { get; set; }
    
    /// <summary>合计字段否</summary>
    public string? CHK_SUM { get; set; }
    
    /// <summary>是否分组统计字段</summary>
    public string? CHK_STATS { get; set; }
    
    /// <summary>条件类型</summary>
    public string? COND_TYPE { get; set; }
    
    /// <summary>合计方式</summary>
    public string? SUM_TYPE { get; set; }
    
    /// <summary>简体名称</summary>
    public string? REM_GB { get; set; }
    
    /// <summary>繁体名称</summary>
    public string? REM_BIG5 { get; set; }
    
    /// <summary>英文名称</summary>
    public string? REM_ENG { get; set; }
}
