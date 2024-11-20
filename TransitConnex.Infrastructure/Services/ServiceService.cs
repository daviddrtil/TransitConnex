using TransitConnex.Domain.DTOs.Service;
using TransitConnex.Infrastructure.Repositories.Interfaces;
using TransitConnex.Infrastructure.Services.Interfaces;

namespace TransitConnex.Infrastructure.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public Task<List<ServiceDto>> GetAllServices()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceDto> GetServiceById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ServiceExists(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceDto> CreateService(ServiceCreateDto serviceDto)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceDto> EditService(Guid id, ServiceCreateDto editedService)
        {
            throw new NotImplementedException();
        }

        public Task DeleteService(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
