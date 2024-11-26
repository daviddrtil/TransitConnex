using Microsoft.Extensions.DependencyInjection;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Repositories;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Command.Services;

namespace TransitConnex.Command;

public static class ConfigurateServices
{
    public static void AddRepositories(this IServiceCollection services)
    {
        //services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
        services.AddScoped<IIconRepository, IconRepository>();
        services.AddScoped<ILineRepository, LineRepository>();
        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<IRouteRepository, RouteRepository>();
        services.AddScoped<IRouteSchedulingTemplateRepository, RouteSchedulingTemplateRepository>();
        services.AddScoped<IRouteTicketRepository, RouteTicketRepository>();
        services.AddScoped<IScheduledRouteRepository, ScheduledRouteRepository>();
        services.AddScoped<ISeatRepository, SeatRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IStopRepository, StopRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IIconService, IconService>();
        services.AddScoped<ILineService, LineService>();
        services.AddScoped<ILocationService, LocationService>();
        services.AddScoped<IRouteService, RouteService>();
        services.AddScoped<IRouteSchedulingTemplateService, RouteSchedulingTemplateService>();
        services.AddScoped<IRouteTicketService, RouteTicketService>();
        services.AddScoped<IScheduledRouteService, ScheduledRouteService>();
        services.AddScoped<ISeatService, SeatService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IStopService, StopService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IVehicleService, VehicleService>();
    }
}
