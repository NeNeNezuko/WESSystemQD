using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WmsPlus.Api.Models
{
    [Table("PRDT_PDA_RN")]
    public class PrdtPdaRn
    {
        [Key]
        [Column("PRD_NO")]
        [StringLength(50)]
        public string PRD_NO { get; set; } = "";

        [Column("CON_NO")]
        [StringLength(50)]
        public string? CON_NO { get; set; }

        [Column("QTY_COLLECT")]
        [StringLength(20)]
        public string? QTY_COLLECT { get; set; }

        [Column("BARCODE_TYPE")]
        [StringLength(20)]
        public string? BARCODE_TYPE { get; set; }

        [Column("NEED_SCALE")]
        [StringLength(10)]
        public string? NEED_SCALE { get; set; }

        [Column("QTY_QZ_MODE")]
        [StringLength(20)]
        public string? QTY_QZ_MODE { get; set; }

        [Column("UT_POINT")]
        public int? UT_POINT { get; set; }

        [Column("UT1_POINT")]
        public int? UT1_POINT { get; set; }

        [Column("UT1_DISP")]
        [StringLength(50)]
        public string? UT1_DISP { get; set; }

        [Column("QTY_TYPE")]
        [StringLength(20)]
        public string? QTY_TYPE { get; set; }

        [Column("SHOW_TYPE")]
        [StringLength(20)]
        public string? SHOW_TYPE { get; set; }

        [Column("SCALE_POINT")]
        public int? SCALE_POINT { get; set; }

        [Column("SCALE_QZ")]
        [StringLength(20)]
        public string? SCALE_QZ { get; set; }

        [Column("SHOW_PAK")]
        [StringLength(10)]
        public string? SHOW_PAK { get; set; }
    }
}
