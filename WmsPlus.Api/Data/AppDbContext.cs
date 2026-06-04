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
        }
    }
}
