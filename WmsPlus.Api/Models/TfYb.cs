namespace WmsPlus.Api.Models;

/// <summary>
/// 盘盈(验收入库)单表身（TF_YB）
/// 对应数据库 db_gz01.TF_YB 表
/// </summary>
public class TfYb
{
    public string? YB_NO { get; set; }
    public int? ITM { get; set; }
    public string? PRD_NO { get; set; }
    public string? PRD_NAME { get; set; }
    public decimal? QTY { get; set; }
    public string? REM { get; set; }
}
