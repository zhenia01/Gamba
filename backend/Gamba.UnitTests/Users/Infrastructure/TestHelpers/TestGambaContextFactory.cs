using Gamba.Infrastructure.Database;
using Gamba.UnitTests.Users.Infrastructure.Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Gamba.UnitTests.Users.Infrastructure.TestHelpers;

public static class TestGambaContextFactory
{
    public static GambaContext CreateContext()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();
        
        var context = new GambaTestContext(new DbContextOptionsBuilder<GambaContext>().UseSqlite(connection).Options);
        context.Database.EnsureCreated(); // context.Database.Migrate();

        return context;
    }
}