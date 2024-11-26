using AutoMapper;
using MongoDB.Driver.Linq;
using TransitConnex.Command.Commands.Service;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Service;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class ServiceService(IMapper mapper, IServiceRepository serviceRepository) : IServiceService 
{
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

    public async Task<Service> CreateService(ServiceCreateCommand createCommand)
    {
        // TODO -> check if icon exists
        var newService = mapper.Map<Service>(createCommand);
        await serviceRepository.Add(newService);

        return newService;
    }

    public async Task<Service> EditService(ServiceUpdateCommand updateCommand)
    {
        var service = await serviceRepository.QueryById(updateCommand.Id).FirstOrDefaultAsync();

        if (service == null)
        {
            throw new KeyNotFoundException($"Icon with ID {updateCommand.Id} was not found.");
        }

        service = mapper.Map(updateCommand, service);
        await serviceRepository.Update(service);

        return service;
    }

    public async Task DeleteService(Guid id)
    {
        var service = await serviceRepository.QueryById(id).FirstOrDefaultAsync();

        if (service == null)
        {
            throw new KeyNotFoundException($"Icon with ID {id} was not found.");
        }

        await serviceRepository.Delete(service); 
    }
}
