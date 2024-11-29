using Bogus;
using Microsoft.Extensions.DependencyInjection;
using TransitConnex.TestSeeds.NoSqlSeeds;

namespace TransitConnex.TestSeeds;
public static class ConfigureServices
{
    public static void AddMongoDbSeeders(this IServiceCollection services)
    {
        services.AddSingleton(new Faker("cz"));
        services.AddScoped<DbMongoSeeder>();
        services.AddScoped<LocationDocSeeder>();
        services.AddScoped<RouteStopDocSeeder>();
        services.AddScoped<ScheduledRouteDocSeeder>();
        services.AddScoped<SearchedRouteDocSeeder>();
        services.AddScoped<UserFavDocSeeder>();
        services.AddScoped<VehicleDocSeeder>();
        services.AddScoped<VehicleRTIDocSeeder>();
    }
}
