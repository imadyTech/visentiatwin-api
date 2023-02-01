using Microsoft.EntityFrameworkCore;
using angoomathAPI.DataModels;

namespace angoomathAPI.Context;

public class MathPlayContext : DbContext
{
    public MathPlayContext(DbContextOptions<MathPlayContext> options) : base(options)
    {

    }

    public DbSet<MathPlayItem> PlayItems { get; set; } = null!;
}
