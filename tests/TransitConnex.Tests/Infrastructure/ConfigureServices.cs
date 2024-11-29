using Bogus;
using Microsoft.Extensions.DependencyInjection;
using TransitConnex.Query.Seeds;

namespace TransitConnex.Tests.Infrastructure;
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
