using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using TransitConnex.Query.Abstraction;
using TransitConnex.Query.Data;
using TransitConnex.Query.Repositories;
using TransitConnex.Query.Repositories.Interfaces;

namespace TransitConnex.Query;

public static class ConfigureServices
{
    /// <summary>
    /// Adds the read database context to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static IServiceCollection AddReadDbContext(this IServiceCollection services)
    {
        services
            .AddSingleton<ISynchronizeDb, NoSqlDbContext>()
            .AddSingleton<IReadDbContext, NoSqlDbContext>()
            .AddSingleton<NoSqlDbContext>();

        ConfigureMongoDb();

        return services;
    }

    /// <summary>
    /// Adds read-only repositories to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static IServiceCollection AddReadOnlyRepositories(this IServiceCollection services) =>
        services.AddScoped<IVehicleRTIRepository, VehicleRTIRepository>();

    /// <summary>
    /// Configures the MongoDB settings and mappings.
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
            new IgnoreIfNullConvention(true), // Ignore null values when serializing
        },
        _ => true);
    }
}
