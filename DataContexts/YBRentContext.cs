using Microsoft.EntityFrameworkCore;
using YBCarRental3D_API.DataModels;

namespace YBCarRental3D_API.DataContexts
{
    public class YBRentContext : DbContext
    {
        public YBRentContext(DbContextOptions<YBRentContext> options)
            : base(options)
        {
        }
        public DbSet<YBRent> Rents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<YBRent>().ToTable("YBRent");
            modelBuilder.Entity<YBRent>().HasKey(e => e.Id);
        }
    }
}


