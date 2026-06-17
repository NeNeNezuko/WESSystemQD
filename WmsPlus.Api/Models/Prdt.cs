namespace WmsPlus.Api.Models;

/// <summary>货品主档（PRDT表）</summary>
public class Prdt
{
    public string PRD_NO { get; set; } = "";
    public string? NAME { get; set; }
    public string? SNM { get; set; }
    public string? NAME_ENG { get; set; }
    public string? IDX1 { get; set; }
    public string? UT { get; set; }
    public string? UT1 { get; set; }
    public string? SPC { get; set; }
    public string? KND { get; set; }
    public string? MRK { get; set; }
    public string? CWXZ_NO { get; set; }
    public string? CW_FLAG { get; set; }
    public string? CHK_BAT { get; set; }
    public string? CHK_NUM { get; set; }
    public int? VALID_DAYS { get; set; }
    public string? VALID_ID { get; set; }
    public string? MOB_ID { get; set; }
    public string? TPL_NO { get; set; }
    public string? WH { get; set; }
    public string? DEP { get; set; }
    public string? USR_WH { get; set; }
    public DateTime? NOUSE_DD { get; set; }
    public DateTime? START_DD { get; set; }
    public string? USR { get; set; }
    public DateTime? SYS_DATE { get; set; }
    public DateTime? UP_DD { get; set; }
    public string? REM { get; set; }
    // 其他资料
    public string? CF_PROP { get; set; }
    public string? CHUW { get; set; }
    public string? CWCTRL_ID { get; set; }
    public string? NOT_BARCODE { get; set; }
    public string? ALLOW_SHQ_FH { get; set; }
    // 超交率
    public decimal? RTO_PC { get; set; }
    public decimal? RTO_MM { get; set; }
    public decimal? RTO_TB { get; set; }
    public decimal? RTO_SA { get; set; }
    // 包装资料
    public string? PK2_UT { get; set; }
    public int? PK2_QTY { get; set; }
    public string? PK3_UT { get; set; }
    public int? PK3_QTY { get; set; }
    public decimal? QTY_WEIGHT { get; set; }
    public string? UNIT_WEIGHT { get; set; }
    public string? ML_UT { get; set; }
    public string? PAK_UNIT { get; set; }
    public decimal? PAK_EXC { get; set; }
    public decimal? PAK_NW { get; set; }
    public string? PAK_WEIGHT_UNIT { get; set; }
    public decimal? PAK_GW { get; set; }
    public string? PAK_MEAST { get; set; }
    public string? PAK_MEAST_UNIT { get; set; }
    public string? EFFECT_ID { get; set; }
    // 品质检验
    public string? TY_INR { get; set; }
    public string? NEED_CHK_FLAG { get; set; }
}
