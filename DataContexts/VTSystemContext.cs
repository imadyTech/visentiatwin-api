using Microsoft.EntityFrameworkCore;
using VisentiaTwin_API.DataModels;

namespace VisentiaTwin_API.DataContexts
{
    public class VTSystemContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<VTSystem> VTSystems { get; set; }
        public DbSet<VTNode> VTNodes { get; set; }
        public DbSet<VTComponent> VTComponents { get; set; }
        public DbSet<VTNodeComponent> VTNodeComponents { get; set; }
        public DbSet<VTFileStorage> VTFiles { get; set; }

        public VTSystemContext(DbContextOptions<VTSystemContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory);  // Use the logger factory to enable logging
            optionsBuilder.EnableSensitiveDataLogging();      // Enable sensitive data logging to include parameter values
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship between VTSystem and VTNode
            modelBuilder.Entity<VTSystem>()
                .HasMany(vs => vs.VTNodes)
                .WithOne(vs=>vs.VTSystem)
                .HasForeignKey(n => n.VTSystemId)
                .IsRequired(false);

            //modelBuilder.Entity<VTNode>()
            //    .HasOne(vs => vs.VTSystem)
            //    .WithMany(vs => vs.VTNodes)
            //    .HasForeignKey(n => n.VTSystemId);


            // Configure the primary key for VTNodeComponent
            modelBuilder.Entity<VTNodeComponent>()
                .HasKey(nc => new { nc.VTNodeId, nc.VTComponentId });

            // Configure the many-to-many relationship between VTNode and VTComponent
            modelBuilder.Entity<VTNodeComponent>()
                .HasOne<VTNode>(nc => nc.VTNode)
                .WithMany(n => n.VTNodeComponents)
                .HasForeignKey(nc => nc.VTNodeId);

            modelBuilder.Entity<VTNodeComponent>()
                .HasOne<VTComponent>(nc => nc.VTComponent)
                .WithMany(c => c.VTNodeComponents)
                .HasForeignKey(nc => nc.VTComponentId);
        }
    }
}
