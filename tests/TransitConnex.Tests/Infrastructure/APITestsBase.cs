using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using System.Net.Http.Json;
using TransitConnex.API;
using TransitConnex.Command.Data;
using TransitConnex.Domain.DTOs.User;
using TransitConnex.TestSeeds;
using TransitConnex.TestSeeds.NoSqlSeeds;
using TransitConnex.TestSeeds.SqlSeeds;
using Xunit.Abstractions;

namespace TransitConnex.Tests.Infrastructure;

public class ApiWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    public const bool UseSqlServer = false;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            if (!UseSqlServer)
            {
                // Remove existing DbContext registrations
                var dbContextDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (dbContextDescriptor != null)
                    services.Remove(dbContextDescriptor);

                // Remove existing DbConnection registration
                var dbConnectionDescriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbConnection));
                if (dbConnectionDescriptor != null)
                    services.Remove(dbConnectionDescriptor);

                // Remove all DbContextOptions (general cleanup)
                var dbContextOptionsDescriptors = services.Where(
                    d => d.ServiceType.IsGenericType &&
                         d.ServiceType.GetGenericTypeDefinition() == typeof(DbContextOptions<>)).ToList();
                foreach (var descriptor in dbContextOptionsDescriptors)
                    services.Remove(descriptor);

                // Remove EF Core provider services (e.g., SqlServer, SQLite)
                var efProviderServices = services.Where(
                    d => d.ServiceType.Namespace != null &&
                         d.ServiceType.Namespace.Contains("Microsoft.EntityFrameworkCore")).ToList();
                foreach (var descriptor in efProviderServices)
                    services.Remove(descriptor);

                // Add SQLite in-memory database for tests
                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open(); // Keep the connection open for the test's duration
                    return connection;
                });

                services.AddDbContext<AppDbContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseSqlite(connection);
                });
            }

            services.AddMongoDbSeeders();

            var sp = services.BuildServiceProvider();
        });

        Environment.SetEnvironmentVariable("AppEnviroment", "Test");
        builder.UseEnvironment("Test");
    }
}

public abstract class APITestsBase : IClassFixture<ApiWebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly ApiWebApplicationFactory<Program> Factory;
    protected readonly HttpClient Client;

    public ITestOutputHelper TestOutputHelper;

    protected APITestsBase(ITestOutputHelper testOutputHelper, ApiWebApplicationFactory<Program> fixture)
    {
        TestOutputHelper = testOutputHelper;
        Factory = fixture;
        Client = Factory.CreateClient();
    }

    public async Task InitializeAsync()
    {
        using var scope = Factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!ApiWebApplicationFactory<Program>.UseSqlServer)
        {
            // Can perform delete only in-memory db, not sql server
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

        await DbSeeder.SeedAll(Factory.Services); // enable to seed sql db

        var dbMongoSeeder = scope.ServiceProvider.GetRequiredService<DbMongoSeeder>();
        await dbMongoSeeder.SeedAll();
    }

    public async Task DisposeAsync()
    {
        Client.Dispose();
        await Task.CompletedTask;
    }

    public async Task PerformLogin(LoginDto login)
    {
        var response = await Client.PostAsJsonAsync("api/User/login", login);
        response.EnsureSuccessStatusCode();
    }
}
