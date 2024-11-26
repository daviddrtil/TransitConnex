using TransitConnex.Command.Commands.Stop;
using TransitConnex.Domain.DTOs.Stop;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services.Interfaces;

public interface IStopService
{
    Task<List<StopDto>> GetAllStops();
    Task<StopDto> GetStopById(Guid id);
    Task<bool> StopExists(Guid id);

    Task<Stop> CreateStop(StopCreateCommand createCommand);
    Task<List<Stop>> CreateStops(List<StopCreateCommand> createCommands);
    Task<Stop> EditStop(StopUpdateCommand editCommand);
    Task DeleteStop(Guid id);
} 
