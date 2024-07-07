using Microsoft.EntityFrameworkCore;
using VisentiaTwin_API.DataModels;

namespace VisentiaTwin_API.DataContexts
{
    public class YBCarContext : DbContext
    {
        public YBCarContext(DbContextOptions<YBCarContext> options)
            : base(options)
        {
        }
        public DbSet<YBCar> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<YBCar>().ToTable("YBCar");
            modelBuilder.Entity<YBCar>().HasKey(e => e.Id);
        }
    }
}


