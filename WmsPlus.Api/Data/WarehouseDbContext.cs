using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Data
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
        {
        }

        public DbSet<MfRktz> MfRktzs { get; set; }
        public DbSet<TfRktz> TfRktzs { get; set; }
        public DbSet<MyWh> MyWhs { get; set; }
        public DbSet<RkTypeSet> RkTypeSets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 入库通知单表头
            modelBuilder.Entity<MfRktz>(entity =>
            {
                entity.ToTable("MF_RKTZ");
                entity.HasKey(e => e.TZ_NO);
                entity.Property(e => e.TZ_NO).HasColumnName("TZ_NO").HasMaxLength(100);
                entity.Property(e => e.TZ_DD).HasColumnName("TZ_DD");
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.SAL_NO).HasColumnName("SAL_NO").HasMaxLength(30);
                entity.Property(e => e.BIL_TYPE).HasColumnName("BIL_TYPE").HasMaxLength(10);
                entity.Property(e => e.CUS_NO).HasColumnName("CUS_NO").HasMaxLength(30);
                entity.Property(e => e.CUS_NAME).HasColumnName("CUS_NAME").HasMaxLength(300);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.EST_DD).HasColumnName("EST_DD");
                entity.Property(e => e.CLS_ID).HasColumnName("CLS_ID").HasMaxLength(1);
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.SYS_DATE).HasColumnName("SYS_DATE");
            });

            // 入库通知单表身
            modelBuilder.Entity<TfRktz>(entity =>
            {
                entity.ToTable("TF_RKTZ");
                entity.HasKey(e => new { e.TZ_NO, e.ITM });
                entity.Property(e => e.TZ_NO).HasColumnName("TZ_NO").HasMaxLength(100);
                entity.Property(e => e.ITM).HasColumnName("ITM");
                entity.Property(e => e.TZ_DD).HasColumnName("TZ_DD");
                entity.Property(e => e.PRD_NO).HasColumnName("PRD_NO").HasMaxLength(50);
                entity.Property(e => e.PRD_NAME).HasColumnName("PRD_NAME").HasMaxLength(320);
                entity.Property(e => e.PRD_MARK).HasColumnName("PRD_MARK").HasMaxLength(255);
                entity.Property(e => e.BAT_NO).HasColumnName("BAT_NO").HasMaxLength(40);
                entity.Property(e => e.VALID_DD).HasColumnName("VALID_DD");
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.UNIT).HasColumnName("UNIT").HasMaxLength(1);
                entity.Property(e => e.QTY).HasColumnName("QTY");
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.CUS_NAME).HasColumnName("CUS_NAME").HasMaxLength(300);

                // 关联关系：表身 -> 表头
                entity.HasOne<MfRktz>()
                      .WithMany()
                      .HasForeignKey(e => e.TZ_NO)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 仓库主数据
            modelBuilder.Entity<MyWh>(entity =>
            {
                entity.ToTable("MY_WH");
                entity.HasKey(e => e.WH);
                entity.Property(e => e.WH).HasColumnName("WH").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(400);
                entity.Property(e => e.WH_TYPE).HasColumnName("WH_TYPE").HasMaxLength(1);
                entity.Property(e => e.ATTRIB).HasColumnName("ATTRIB").HasMaxLength(1);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.INVALID).HasColumnName("INVALID").HasMaxLength(1);
            });

            // 入库业务类型设置
            modelBuilder.Entity<RkTypeSet>(entity =>
            {
                entity.ToTable("cr_type_set");
                entity.HasKey(e => new { e.CR_TYPE, e.TYPE_ID });
                entity.Property(e => e.CR_TYPE).HasColumnName("CR_TYPE").HasMaxLength(1);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(10);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(300);
            });
        }
    }
}
