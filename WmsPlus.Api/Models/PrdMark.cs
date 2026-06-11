namespace WmsPlus.Api.Models;

/// <summary>
/// 货品特征码段设定表（PRD_MARK）
/// 对应数据库 db_gz01.PRD_MARK 表
/// </summary>
public class PrdMark
{
    /// <summary>模版代号（主键）</summary>
    public string MOB_ID { get; set; } = "";

    /// <summary>模版名称</summary>
    public string? MOB_NAME { get; set; }

    /// <summary>货品特征</summary>
    public string? PRD_MARK { get; set; }

    /// <summary>说明</summary>
    public string? REM { get; set; }

    /// <summary>停用日期</summary>
    public DateTime? END_DD { get; set; }
}
