using TransitConnex.Domain.DTOs.Service;

namespace TransitConnex.Infrastructure.Services.Interfaces
{
    public interface IServiceService
    {
        Task<List<ServiceDto>> GetAllServices();
        
        Task<ServiceDto> GetServiceById(Guid id);
        
        Task<bool> ServiceExists(Guid id);
        
        Task<ServiceDto> CreateService(ServiceCreateDto serviceDto);

        Task<ServiceDto> EditService(Guid id, ServiceCreateDto editedService);
        
        Task DeleteService(Guid id);
    }
}
