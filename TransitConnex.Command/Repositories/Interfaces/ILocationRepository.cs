using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Location;

namespace TransitConnex.Infrastructure.Repositories.Interfaces;

public interface ILocationRepository : IBaseRepository<Location, LocationUpdateCommand>
{
    IQueryable<Location> QueryById(Guid id);
}
