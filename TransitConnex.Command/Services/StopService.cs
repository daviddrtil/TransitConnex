using TransitConnex.Domain.DTOs.Stop;
using TransitConnex.Domain.Models;
using TransitConnex.Infrastructure.Commands.Stop;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.Infrastructure.Services;

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
