using Gamba.DataAccess.BuildingBlocks;
using Gamba.DataAccess.Users;
using Gamba.Infrastructure.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Gamba.Infrastructure.Database;

public class GambaContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public GambaContext(DbContextOptions<GambaContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GambaContext).Assembly);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<UserId>().HaveConversion<TypedIdValueConverter<UserId>>();
    }
}