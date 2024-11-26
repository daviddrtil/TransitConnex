using Bogus;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Data;
using TransitConnex.Query.Repositories;
using TransitConnex.Query.Repositories.Interfaces;
using TransitConnex.Query.Seeds;
using TransitConnex.Query.Services;
using TransitConnex.Query.Services.Interfaces;

namespace TransitConnex.Query;

public static class ConfigureMongoServices
{
    /// <summary>
    ///     Adds the read database context to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static IServiceCollection AddMongoDbContext(this IServiceCollection services)
    {
        services
            .AddSingleton<ISynchronizeDb, NoSqlDbContext>()
            .AddSingleton<IReadDbContext, NoSqlDbContext>()
            .AddSingleton<NoSqlDbContext>();

        ConfigureMongoDb();

        return services;
    }

    /// <summary>
    ///     Adds read-only repositories to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static void AddMongoDbRepositories(this IServiceCollection services)
    {
        services.AddScoped<ILocationMongoRepository, LocationMongoRepository>();
        services.AddScoped<IRouteStopMongoRepository, RouteStopMongoRepository>();
        services.AddScoped<IScheduledRouteMongoRepository, ScheduledRouteMongoRepository>();
        services.AddScoped<ISearchedRouteMongoRepository, SearchedRouteMongoRepository>();
        services.AddScoped<IVehicleMongoRepository, VehicleMongoRepository>();
        services.AddScoped<IVehicleRTIMongoRepository, VehicleRTIMongoRepository>();
    }

    public static void AddMongoDbServices(this IServiceCollection services)
    {
        services.AddScoped<ILocationMongoService, LocationMongoService>();
        services.AddScoped<IScheduledRouteMongoService, ScheduledRouteMongoService>();
        services.AddScoped<ISearchedRouteMongoService, SearchedRouteMongoService>();
        services.AddScoped<IVehicleMongoService, VehicleMongoService>();
        services.AddScoped<IVehicleRTIMongoService, VehicleRTIMongoService>();
    }

    public static void AddMongoDbSeeders(this IServiceCollection services)
    {
        services.AddSingleton(new Faker("cz"));
        services.AddScoped<DbMongoSeeder>();
        services.AddScoped<LocationDocSeeder>();
        services.AddScoped<RouteStopDocSeeder>();
        services.AddScoped<ScheduledRouteDocSeeder>();
        services.AddScoped<SearchedRouteDocSeeder>();
        services.AddScoped<VehicleDocSeeder>();
        services.AddScoped<VehicleRTIDocSeeder>();
    }

    /// <summary>
    ///     Configures the MongoDB settings and mappings.
    /// </summary>
    private static void ConfigureMongoDb()
    {
        // Configure the serializer for Guid type
        BsonSerializer.TryRegisterSerializer(new GuidSerializer(BsonType.String));

        // Configure the conventions to be applied to all mappings
        ConventionRegistry.Register("Conventions",
            new ConventionPack
            {
                new CamelCaseElementNameConvention(), // Convert element names to camel case
                new EnumRepresentationConvention(BsonType.String), // Serialize enums as strings
                new IgnoreExtraElementsConvention(true), // Ignore extra elements when deserializing
                new IgnoreIfNullConvention(true) // Ignore null values when serializing
            },
            _ => true);
    }
}
