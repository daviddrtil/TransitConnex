using TransitConnex.Domain.DTOs.Stop;

namespace TransitConnex.Infrastructure.Services.Interfaces
{
    public interface IStopService
    {
        Task<List<StopDto>> GetAllStops();
        
        Task<StopDto> GetStopById(Guid id);
        
        Task<bool> StopExists(Guid id);
        
        Task<StopDto> CreateStop(StopCreateDto stopDto);

        Task<StopDto> EditStop(Guid id, StopCreateDto editedStop);
        
        Task DeleteStop(Guid id);
    }
}
