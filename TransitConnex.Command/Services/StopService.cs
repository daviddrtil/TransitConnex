using TransitConnex.Domain.DTOs.Stop;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.Infrastructure.Services
{
    public class StopService : IStopService
    {
        private readonly IStopRepository _stopRepository;

        public StopService(IStopRepository stopRepository)
        {
            _stopRepository = stopRepository;
        }

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

        public Task<StopDto> CreateStop(StopCreateDto stopDto)
        {
            throw new NotImplementedException();
        }

        public Task<StopDto> EditStop(Guid id, StopCreateDto editedStop)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStop(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
