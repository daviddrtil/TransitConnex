using TransitConnex.Command.Commands.Location;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;
using TransitConnex.Query.Queries.Interfaces;

namespace TransitConnex.Command.Services.Interfaces;

public interface ILocationService
{
    Task<Location?> GetLocationById(Guid id);
    Task<IEnumerable<Location>> GetLocationByIds(IEnumerable<Guid> ids);
    Task<List<LocationDto>> GetLocationsFiltered(LocationFilteredQuery filter);

    Task<Location> CreateLocation(LocationCreateCommand createCommand);
    Task<List<Location>> CreateLocations(List<LocationCreateCommand> createCommands);
    Task<Location> EditLocation(LocationUpdateCommand editCommand);
    Task DeleteLocation(Guid id);
}
