using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WmsPlus.Api.Models
{
    [Table("BAT_NOT_PW")]
    public class BatNotPw
    {
        [Key]
        [Column("GUID")]
        [MaxLength(50)]
        public string GUID { get; set; } = "";

        [Column("CON_NO")]
        [MaxLength(30)]
        public string? CON_NO { get; set; }

        [Column("WH")]
        [MaxLength(30)]
        public string? WH { get; set; }

        [Column("PRD_NO")]
        [MaxLength(50)]
        public string? PRD_NO { get; set; }

        [Column("PRD_MARK")]
        [MaxLength(255)]
        public string? PRD_MARK { get; set; }

        [Column("BAT_NO")]
        [MaxLength(40)]
        public string? BAT_NO { get; set; }

        [Column("USR")]
        [MaxLength(30)]
        public string? USR { get; set; }

        [Column("SYS_DATE")]
        public DateTime? SYS_DATE { get; set; }

        [Column("REM")]
        [MaxLength(500)]
        public string? REM { get; set; }
    }
}
