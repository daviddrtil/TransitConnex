using TransitConnex.Command.Commands.Location;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Repositories.Interfaces;

public interface ILocationRepository : IBaseRepository<Location, LocationUpdateCommand>
{
    IQueryable<Location> QueryById(Guid id);
    IQueryable<Location> QueryAllLocations();
}
