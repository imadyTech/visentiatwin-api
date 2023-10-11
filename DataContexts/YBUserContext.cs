using Microsoft.EntityFrameworkCore;
using YBCarRental3D_API.DataModels;

namespace YBCarRental3D_API.DataContexts
{
    public class YBUserContext : DbContext
    {
        public YBUserContext(DbContextOptions<YBUserContext> options)
            : base(options)
        {
        }
        public DbSet<YBUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<YBUser>().ToTable("YBUser");

            modelBuilder
                .Entity<YBUser>()
                .HasKey(e => new { e.UserName });
        }
    }
}


