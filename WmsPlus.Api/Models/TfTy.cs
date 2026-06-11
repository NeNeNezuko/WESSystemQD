namespace WmsPlus.Api.Models;

/// <summary>
/// 检验单表身（TF_TY）
/// 对应数据库 db_gz01.TF_TY 表
/// 通过 TY_NO 与 MF_TY 关联
/// </summary>
public class TfTy
{
    /// <summary>单据号码（关联MF_TY.TY_NO，主键之一）</summary>
    public string TY_NO { get; set; } = "";

    /// <summary>项次（主键之二）</summary>
    public int ITM { get; set; }
}
