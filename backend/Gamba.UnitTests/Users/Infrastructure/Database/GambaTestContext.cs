using System.Text.Json;
using Gamba.Domain.Users;
using Gamba.Domain.Users.Tags;
using Gamba.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Gamba.UnitTests.Users.Infrastructure.Database;

public class GambaTestContext : GambaContext
{
    public GambaTestContext(DbContextOptions<GambaContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Replace Postgres Tag[] -> string[] conversion
        //      to SQLite Tag[] -> string conversion through JSON
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(List<Tag>))
                {
                    property.SetValueConverter(typeof(TagsListValueConverter));
                }
            }
        }
    }
    
    private class TagsListValueConverter : ValueConverter<List<Tag>, string>
    {
        public TagsListValueConverter() 
            : base(
                tags => JsonSerializer.Serialize(tags.Select(t => t.Name), (JsonSerializerOptions)null!),
                value => JsonSerializer.Deserialize<List<string>>(value, (JsonSerializerOptions)null!).ConvertAll(t => new Tag(t))
            )
        {
        }
    }
}