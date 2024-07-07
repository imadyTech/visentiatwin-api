using Microsoft.EntityFrameworkCore;
using VisentiaTwin_API.DataModels;

namespace VisentiaTwin_API.DataContexts
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

            modelBuilder.Entity<YBUser>().HasKey(e => e.Id);
            modelBuilder.Entity<YBUser>().HasIndex(e => e.UserName).IsUnique();
        }
    }
}


