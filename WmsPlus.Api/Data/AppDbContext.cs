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
                entity.Property(e => e.TAB_NAME).HasColumnName("TAB_NAME").HasMaxLength(40);
                entity.Property(e => e.FLD_NAME).HasColumnName("FLD_NAME").HasMaxLength(50);
                entity.Property(e => e.Note).HasColumnName("Note").HasMaxLength(200);
            });
        }
    }
}
