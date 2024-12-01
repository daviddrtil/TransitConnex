using TransitConnex.Command.Commands.Route;
using TransitConnex.Command.Commands.Stop;
using TransitConnex.Domain.DTOs.Route;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.Command.Services.Interfaces;

public interface IRouteService
{
    Task<List<RouteDto>> GetRoutesFiltered(RouteFilteredQuery filter);
    
    Task<Route> CreateRoute(RouteCreateCommand createCommand);
    Task<Route> EditRoute(RouteUpdateCommand editCommand);
    Task DeleteRoute(Guid id);

    Task AddStopToRoute(RouteStopAddCommand addCommand);
    Task RemoveStopFromRoute(RouteStopRemoveCommand removeCommand);
}
