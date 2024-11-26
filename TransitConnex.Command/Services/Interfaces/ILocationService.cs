using TransitConnex.Command.Commands.Location;
using TransitConnex.Domain.DTOs.Location;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services.Interfaces;

public interface ILocationService
{
    Task<List<LocationDto>> GetAllLocations();
    Task<List<LocationDto>> GetLocationsFiltered();
    Task<LocationDto> GetLocationById(Guid id);
    

    Task<Location> CreateLocation(LocationCreateCommand createCommand);
    Task<List<Location>> CreateLocations(List<LocationCreateCommand> createCommands);
    Task<Location> EditLocation(LocationUpdateCommand editCommand);
    Task DeleteLocation(Guid id);
}
