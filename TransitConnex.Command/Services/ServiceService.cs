using AutoMapper;
using MongoDB.Driver.Linq;
using TransitConnex.Command.Commands.Service;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.Service;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.Command.Services;

public class ServiceService(IMapper mapper, IServiceRepository serviceRepository) : IServiceService 
{
    public async Task<List<ServiceDto>> GetFilteredServices(ServiceFilteredQuery filter)
    {
        var query = serviceRepository.QueryAll();
        if (filter.Name is not null)
        {
            var normalizedFilterName = filter.Name.ToLower();
            query = query.Where(x => x.Name != null && x.Name.ToLower().Contains(normalizedFilterName));
        }

        if (filter.Ids is not null)
        {
            query = query.Where(x => filter.Ids.Contains(x.Id));
        }
        
        var services = await query.ToListAsync();
        
        return mapper.Map<List<ServiceDto>>(services);
    }
    
    public async Task<Service> CreateService(ServiceCreateCommand createCommand)
    {
        var newService = mapper.Map<Service>(createCommand);
        await serviceRepository.Add(newService);

        return newService;
    }

    public async Task<Service> EditService(ServiceUpdateCommand updateCommand)
    {
        var service = await serviceRepository.QueryById(updateCommand.Id).FirstOrDefaultAsync();

        if (service == null)
        {
            throw new KeyNotFoundException($"Service with ID {updateCommand.Id} was not found.");
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
            throw new KeyNotFoundException($"Service with ID {id} was not found.");
        }

        await serviceRepository.Delete(service); 
    }
}
