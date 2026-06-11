namespace WmsPlus.Api.Models;

/// <summary>
/// 打印服务任务表（PRINT_SER_TASK）
/// 对应数据库 db_gz01.PRINT_SER_TASK 表
/// </summary>
public class PrintSerTask
{
    /// <summary>序号（主键）</summary>
    public int SEQ_NO { get; set; }

    /// <summary>打印时间</summary>
    public DateTime? PRINT_TIME { get; set; }

    /// <summary>版套代号</summary>
    public string? VERSION_CODE { get; set; }

    /// <summary>打印人员</summary>
    public string? PRINTER_USER { get; set; }

    /// <summary>网点名称</summary>
    public string? SITE_NAME { get; set; }

    /// <summary>程序代号</summary>
    public string? PROGRAM_CODE { get; set; }

    /// <summary>模版代号</summary>
    public string? TEMPLATE_CODE { get; set; }

    /// <summary>打印状态</summary>
    public string? PRINT_STATUS { get; set; }

    /// <summary>失败次数</summary>
    public int? FAIL_COUNT { get; set; }

    /// <summary>失败原因</summary>
    public string? FAIL_REASON { get; set; }

    /// <summary>打印单号/版码</summary>
    public string? PRINT_NO { get; set; }
}
