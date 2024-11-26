using Microsoft.EntityFrameworkCore;
using TransitConnex.Command.Data;
using TransitConnex.Command.Seeds;
using TransitConnex.Query.Data;
using TransitConnex.Query.Seeds;

namespace TransitConnex.API.Configuration;

internal static class MigrationExtensions
{
    public static async Task MigrateSqlDbAsync(this WebApplication app)
    {
        await using var serviceScope = app.Services.CreateAsyncScope();
        await using var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

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

    public static async Task MigrateMongoDbAsync(this WebApplication app)
    {
        await using var serviceScope = app.Services.CreateAsyncScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<NoSqlDbContext>();

        app.Logger.LogInformation("----- MongoDB: {Connection}", context.ConnectionString);
        app.Logger.LogInformation("----- MongoDB: collections are being created...");

        await context.DeleteCollectionsAsync();
        await context.CreateCollectionsAsync();
        await context.CreateIndexAsync();

        app.Logger.LogInformation("----- MongoDB: seeding...");


        app.Logger.LogInformation("----- MongoDB: collections were created successfully!");
        var dbSeeder = serviceScope.ServiceProvider.GetRequiredService<DbMongoSeeder>();
        await dbSeeder.SeedAll();

        app.Logger.LogInformation("----- MongoDB: seeding was successful");
    }
}
