using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WmsPlus.Api.Models
{
    [Table("BC_RULE")]
    public class BcRule
    {
        [Key]
        [Column("RULE_ID")]
        [MaxLength(50)]
        public string RULE_ID { get; set; } = "";

        [Column("DEP")]
        [MaxLength(30)]
        public string? DEP { get; set; }

        [Column("NAME")]
        [MaxLength(200)]
        public string? NAME { get; set; }

        [Column("WH_TYPE")]
        [MaxLength(1)]
        public string? WH_TYPE { get; set; }

        [Column("STOP_ID")]
        [MaxLength(1)]
        public string? STOP_ID { get; set; }

        [Column("USR")]
        [MaxLength(30)]
        public string? USR { get; set; }

        [Column("SYS_DATE")]
        public DateTime? SYS_DATE { get; set; }

        [Column("MODIFY_MAN")]
        [MaxLength(30)]
        public string? MODIFY_MAN { get; set; }

        [Column("MODIFY_DD")]
        public DateTime? MODIFY_DD { get; set; }
    }
}
