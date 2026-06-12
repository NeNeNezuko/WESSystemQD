namespace WmsPlus.Api.Models;

/// <summary>
/// 自定义报表表身（TF_QRY）
/// 对应数据库 db_gz01.TF_QRY 表
/// </summary>
public class TfQry
{
    /// <summary>代号(外键->MF_QRY)</summary>
    public string? ID { get; set; }
    
    /// <summary>字段名</summary>
    public string? FLD_NAME { get; set; }
    
    /// <summary>字段类型</summary>
    public string? FLD_TYPE { get; set; }
    
    /// <summary>简体说明</summary>
    public string? REM_GB { get; set; }
    
    /// <summary>繁体说明</summary>
    public string? REM_BIG5 { get; set; }
    
    /// <summary>英文说明</summary>
    public string? REM_ENG { get; set; }
}
