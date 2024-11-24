using TransitConnex.Domain.DTOs.Stop;
using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Stop;

namespace TransitConnex.Infrastructure.Services.Interfaces;

public interface IStopService
{
    Task<List<StopDto>> GetAllStops();

    Task<StopDto> GetStopById(Guid id);

    Task<bool> StopExists(Guid id);

    Task<Stop> CreateStop(StopCreateCommand createCommand);

    Task<Stop> EditStop(StopUpdateCommand editCommand);

    Task DeleteStop(StopDeleteCommand deleteCommand);
}
