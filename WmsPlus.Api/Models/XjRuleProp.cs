using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WmsPlus.Api.Models
{
    [Table("XJ_RULE_PROP")]
    public class XjRuleProp
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("RULE_ID")]
        [MaxLength(50)]
        public string? RULE_ID { get; set; }

        [Column("PROP_TYPE")]
        [MaxLength(20)]
        public string? PROP_TYPE { get; set; }

        [Column("PROP_CODE")]
        [MaxLength(50)]
        public string? PROP_CODE { get; set; }

        [Column("PROP_NAME")]
        [MaxLength(200)]
        public string? PROP_NAME { get; set; }

        [Column("PROP_VALUE")]
        [MaxLength(500)]
        public string? PROP_VALUE { get; set; }
    }
}
