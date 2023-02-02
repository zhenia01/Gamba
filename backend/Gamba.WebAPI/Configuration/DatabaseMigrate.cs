using Gamba.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Gamba.WebAPI.Configuration;

public static class DatabaseMigration
{
    public static void UseDatabaseMigrate(this WebApplication app)
    {
        using var scope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<GambaContext>();
        context.Database.Migrate();
    }
}