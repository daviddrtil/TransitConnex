using TransitConnex.API.Handlers.CommandHandlers;
using TransitConnex.API.Handlers.QueryHandlers;

namespace TransitConnex.API.Configuration;

internal static class ConfigureHandlers
{
    public static void AddCommandHandlers(this IServiceCollection services)
    {
        services.AddScoped<IconCommandHandler>();
        services.AddScoped<ServiceCommandHandler>();
        services.AddScoped<VehicleCommandHandler>();
        services.AddScoped<LocationCommandHandler>();
        services.AddScoped<LineCommandHandler>();
        services.AddScoped<RouteCommandHandler>();
        services.AddScoped<RouteSchedulingTemplateCommandHandler>();
        services.AddScoped<RouteTicketCommandHandler>();
        services.AddScoped<ScheduledRouteCommandHandler>();
        services.AddScoped<SeatCommandHandler>();
        services.AddScoped<StopCommandHandler>();
        services.AddScoped<UserCommandHandler>();
    }

    public static void AddQueryHandlers(this IServiceCollection services)
    {
        services.AddScoped<LocationQueryHandler>();
        services.AddScoped<ScheduledRouteQueryHandler>();
        services.AddScoped<SearchedRouteQueryHandler>();
        services.AddScoped<UserQueryHandler>();
        services.AddScoped<VehicleQueryHandler>();
        services.AddScoped<VehicleRTIQueryHandler>();
        services.AddScoped<IconQueryHandler>();
        services.AddScoped<ServiceQueryHandler>();
        services.AddScoped<UserQueryHandler>();
        services.AddScoped<StopQueryHandler>();
    }
}
