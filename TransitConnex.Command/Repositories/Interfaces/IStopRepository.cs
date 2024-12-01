using TransitConnex.Command.Commands.Stop;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface IStopRepository : IBaseRepository<Stop, StopUpdateCommand>
{
    IQueryable<Stop> QueryById(Guid id);
    
    IQueryable<RouteStop> QueryRouteStopsByStopId(Guid stopId);
    
    Task AddLocationStops(List<LocationStop> locationStops);
}
