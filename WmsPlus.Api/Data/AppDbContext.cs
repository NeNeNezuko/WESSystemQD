using Microsoft.EntityFrameworkCore;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<DictTab> DictTabs { get; set; }
        public DbSet<DictFld> DictFlds { get; set; }

        // 类别权限设定（wmssystem数据库）
        public DbSet<Role> Roles { get; set; }
        public DbSet<FxPswd> FxPswds { get; set; }
        public DbSet<PswdRole> PswdRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("pswd");
                entity.HasKey(e => new { e.COMPNO, e.USR });
                
                entity.Property(e => e.COMPNO).HasColumnName("COMPNO").HasMaxLength(4);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(400);
                entity.Property(e => e.PWD).HasColumnName("PWD").HasMaxLength(100);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
            });

            modelBuilder.Entity<DictTab>(entity =>
            {
                entity.ToTable("DICT_TAB");
                entity.HasKey(e => e.TAB_NAME);
                entity.Property(e => e.TAB_NAME).HasColumnName("TAB_NAME").HasMaxLength(40);
                entity.Property(e => e.TAB_TITLE).HasColumnName("TAB_TITLE").HasMaxLength(200);
            });

            modelBuilder.Entity<DictFld>(entity =>
            {
                entity.ToTable("DICT_FLD");
                entity.HasKey(e => new { e.TAB_NAME, e.FLD_NAME });
                entity.Property(e => e.TAB_NAME).HasColumnName("TAB_NAME").HasMaxLength(50);
                entity.Property(e => e.FLD_NAME).HasColumnName("FLD_NAME").HasMaxLength(50);
                entity.Property(e => e.Note).HasColumnName("Note").HasMaxLength(200);
            });

            // 角色定义（ROLE）- wmssystem
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");
                entity.HasKey(e => e.ROLENO);
                entity.Property(e => e.ROLENO).HasColumnName("ROLENO").HasMaxLength(30);
                entity.Property(e => e.NAME).HasColumnName("NAME").HasMaxLength(200);
                entity.Property(e => e.DEP).HasColumnName("DEP").HasMaxLength(30);
                entity.Property(e => e.DEPRO_NO).HasColumnName("DEPRO_NO").HasMaxLength(30);
                entity.Property(e => e.COMPNO).HasColumnName("COMPNO").HasMaxLength(30);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(20);
                entity.Property(e => e.PUBLIC_ID).HasColumnName("PUBLIC_ID").HasMaxLength(1);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
                entity.Property(e => e.REM).HasColumnName("REM");
                entity.Property(e => e.SUB_ID).HasColumnName("SUB_ID").HasMaxLength(1);
            });

            // ONLINE角色权限（FX_PSWD）- wmssystem
            modelBuilder.Entity<FxPswd>(entity =>
            {
                entity.ToTable("FX_PSWD");
                entity.HasKey(e => new { e.ROLENO, e.PGM });
                entity.Property(e => e.ROLENO).HasColumnName("ROLENO").HasMaxLength(30);
                entity.Property(e => e.PGM).HasColumnName("PGM").HasMaxLength(50);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(20);
                entity.Property(e => e.COMPNO).HasColumnName("COMPNO").HasMaxLength(30);
                entity.Property(e => e.DEPRO_NO).HasColumnName("DEPRO_NO").HasMaxLength(30);
                entity.Property(e => e.QRY).HasColumnName("QRY").HasMaxLength(1);
                entity.Property(e => e.INS).HasColumnName("INS").HasMaxLength(1);
                entity.Property(e => e.UPD).HasColumnName("UPD").HasMaxLength(1);
                entity.Property(e => e.DEL).HasColumnName("DEL").HasMaxLength(1);
                entity.Property(e => e.PRN).HasColumnName("PRN").HasMaxLength(1);
                entity.Property(e => e.QTY).HasColumnName("QTY").HasMaxLength(1);
                entity.Property(e => e.FLD).HasColumnName("FLD").HasMaxLength(1);
                entity.Property(e => e.PROPERTY).HasColumnName("PROPERTY").HasMaxLength(1);
                entity.Property(e => e.ALLOW_ID).HasColumnName("ALLOW_ID").HasMaxLength(1);
                entity.Property(e => e.EPT).HasColumnName("EPT").HasMaxLength(1);
            });

            // ONLINE角色所属用户（PSWD_ROLE）- wmssystem
            modelBuilder.Entity<PswdRole>(entity =>
            {
                entity.ToTable("PSWD_ROLE");
                entity.HasKey(e => new { e.COMPNO, e.ROLENO, e.TYPE_ID, e.USR });
                entity.Property(e => e.COMPNO).HasColumnName("COMPNO").HasMaxLength(30);
                entity.Property(e => e.ROLENO).HasColumnName("ROLENO").HasMaxLength(30);
                entity.Property(e => e.TYPE_ID).HasColumnName("TYPE_ID").HasMaxLength(20);
                entity.Property(e => e.USR).HasColumnName("USR").HasMaxLength(30);
            });
        }
    }
}
