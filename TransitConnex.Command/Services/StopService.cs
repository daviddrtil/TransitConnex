using TransitConnex.Command.Commands.Stop;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Stop;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class StopService(IStopRepository stopRepository) : IStopService
{
    public Task<List<StopDto>> GetAllStops()
    {
        throw new NotImplementedException();
    }

    public Task<StopDto> GetStopById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> StopExists(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Stop> CreateStop(StopCreateCommand createCommand)
    {
        throw new NotImplementedException();
    }

    public Task<Stop> EditStop(StopUpdateCommand editCommand)
    {
        throw new NotImplementedException();
    }

    public Task DeleteStop(StopDeleteCommand deleteCommand)
    {
        throw new NotImplementedException();
    }
}
