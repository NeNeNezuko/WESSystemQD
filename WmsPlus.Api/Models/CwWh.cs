using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WmsPlus.Api.Models
{
    [Table("CW_WH")]
    public class CwWh
    {
        [Key]
        [Column("CHUW")]
        [StringLength(30)]
        public string CHUW { get; set; } = "";

        [Column("NAME")]
        [StringLength(400)]
        public string? NAME { get; set; }

        [Column("WH")]
        [StringLength(30)]
        public string? WH { get; set; }

        [Column("GS")]
        [StringLength(30)]
        public string? GS { get; set; }

        [Column("GL")]
        [StringLength(30)]
        public string? GL { get; set; }

        [Column("LAYER")]
        [StringLength(30)]
        public string? LAYER { get; set; }

        [Column("CW_STATUS")]
        [StringLength(10)]
        public string? CW_STATUS { get; set; }

        [Column("LOCK_CW")]
        [StringLength(1)]
        public string? LOCK_CW { get; set; }

        [Column("AREA_ID")]
        [StringLength(30)]
        public string? AREA_ID { get; set; }
    }
}
