namespace WmsPlus.Api.Models;

public class IzwhConfirm
{
    public string WH_OUT { get; set; } = "";
    public string WH_IN { get; set; } = "";
    public string? USR { get; set; }
    public string? MODIFY_MAN { get; set; }
    public DateTime? SYS_DATE { get; set; }
    public DateTime? MODIFY_DD { get; set; }
    public string? DEP { get; set; }
}
