using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TransitConnex.API.Automapping;
using TransitConnex.API.Configuration;
using TransitConnex.API.Middleware;
using TransitConnex.Command.Data;
using TransitConnex.Command;
using TransitConnex.Domain.Models;
using TransitConnex.Query;
using TransitConnex.TestSeeds.SqlSeeds;
using TransitConnex.TestSeeds;

namespace TransitConnex.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Verbose logging
        builder.Services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
            logging.AddDebug();
        });

        builder.Services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), options =>
            {
                options.EnableRetryOnFailure();
            }));

        // User authentication
        builder.Services.AddIdentity<User, IdentityRole<Guid>>(
                IdentityConfiguration.ConfigureIdentityOptions)
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // Handlers
        builder.Services.AddCommandHandlers();
        builder.Services.AddQueryHandlers();

        // Sql services
        builder.Services.AddRepositories();
        builder.Services.AddServices();

        // MongoDb services
        builder.Services.AddMongoDbContext();
        builder.Services.AddMongoDbRepositories();
        builder.Services.AddMongoDbServices();
        builder.Services.AddMongoDbSeeders();

        var app = builder.Build();

        bool updateDb = builder.Configuration.GetValue<bool>("UpdateDb");
        bool seedDb = builder.Configuration.GetValue<bool>("SeedDatabase");
        bool unseedDb = builder.Configuration.GetValue<bool>("UnseedDatabase");
        if (updateDb || seedDb || unseedDb)
        {
            if (unseedDb)
            {
                DbCleaner.DeleteEntireDb(app.Services);
                app.Logger.LogInformation("Database cleaning completed.");
            }
            if (updateDb)
            {
                await app.MigrateSqlDbAsync();
                app.Logger.LogInformation("Database migration completed.");
            }
            if (seedDb)
            {
                await app.MigrateSqlDbAsync();
                await DbSeeder.SeedAll(app.Services);

                await app.MigrateMongoDbAsync();    // performs also delete
                await app.SeedMongoDb();

                app.Logger.LogInformation("Database seeding completed.");
            }
            return;
        }

        // For testing purposes
        if (app.Environment.EnvironmentName.Equals("Test"))
        {
            await app.MigrateMongoDbAsync();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            // sql up
            await app.MigrateSqlDbAsync();
            await DbSeeder.SeedAll(app.Services);

            // nosql up
            await app.MigrateMongoDbAsync();
            await app.SeedMongoDb();

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.MapControllers();

        app.Run();
    }
}
