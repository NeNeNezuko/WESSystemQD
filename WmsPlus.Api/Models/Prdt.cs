namespace WmsPlus.Api.Models;

/// <summary>货品主档（PRDT表）</summary>
public class Prdt
{
    public string PRD_NO { get; set; } = "";
    public string? NAME { get; set; }
    public string? SNM { get; set; }
    public string? IDX1 { get; set; }
    public string? UT { get; set; }
    public string? SPC { get; set; }
    public string? CWXZ_NO { get; set; }
    public DateTime? NOUSE_DD { get; set; }
}
