using Microsoft.Extensions.DependencyInjection;
using TransitConnex.Command.Data;

namespace TransitConnex.Command.Seeds;

public static class DbCleaner
{
    public static void DeleteEntireDb(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureDeleted();
    }
}
