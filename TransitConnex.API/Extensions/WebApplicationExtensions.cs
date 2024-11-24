using Microsoft.EntityFrameworkCore;
using TransitConnex.Infrastructure.Data;
using TransitConnex.Query.Abstraction;

namespace TransitConnex.API.Extensions;

internal static class WebApplicationExtensions
{
    public static async Task MigrateDatabasesAsync(this WebApplication app)
    {
        await using var serviceScope = app.Services.CreateAsyncScope();

        await using var writeDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        var readDbContext = serviceScope.ServiceProvider.GetRequiredService<IReadDbContext>();

        try
        {
            //await app.MigrateDbContextAsync(writeDbContext);  // todo setup sql before uncommenting
            await app.MigrateMongoDbContextAsync(readDbContext);
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "An exception occurred while initializing the application: {Message}", ex.Message);
            throw;
        }
    }

    private static async Task MigrateDbContextAsync(this WebApplication app, DbContext context)
    {
        string dbName = context.Database.GetDbConnection().Database;

        app.Logger.LogInformation("----- {DbName}: {DbConnection}", dbName, context.Database.GetConnectionString());
        app.Logger.LogInformation("----- {DbName}: checking if there are any pending migrations...", dbName);

        // Check if there are any pending migrations for the context.
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            app.Logger.LogInformation("----- {DbName}: creating and migrating the database...", dbName);

            await context.Database.MigrateAsync();

            app.Logger.LogInformation("----- {DbName}: database was created and migrated successfully", dbName);
        }
        else
        {
            app.Logger.LogInformation("----- {DbName}: all migrations are up to date", dbName);
        }
    }

    private static async Task MigrateMongoDbContextAsync(this WebApplication app, IReadDbContext readDbContext)
    {
        app.Logger.LogInformation("----- MongoDB: {Connection}", readDbContext.ConnectionString);
        app.Logger.LogInformation("----- MongoDB: collections are being created...");

        await readDbContext.CreateCollectionsAsync();

        app.Logger.LogInformation("----- MongoDB: collections were created successfully!");
    }
}
