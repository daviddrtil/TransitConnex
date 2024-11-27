using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TransitConnex.Command.Commands.RouteSchedulingTemplate;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.RouteSchedulingTemplate;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class RouteSchedulingTemplateService(IMapper mapper, IRouteSchedulingTemplateRepository routeSchedulingTemplateRepository, IRouteSchedulerService routeSchedulerService, IRouteRepository routeRepository)
    : IRouteSchedulingTemplateService
{
    public Task<List<RouteSchedulingTemplateDto>> GetAllRouteSchedulingTemplates()
    {
        throw new NotImplementedException();
    }

    public Task<RouteSchedulingTemplateDto> GetRouteSchedulingTemplateById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<RouteSchedulingTemplate> CreateRouteSchedulingTemplate(
        RouteSchedulingTemplateCreateCommand createCommand)
    {
        if (!await routeRepository.Exists(createCommand.RouteId))
        {
            throw new KeyNotFoundException($"Route with Id {createCommand.RouteId} was not found, cannot create template.");
        }
        
        var templateString = JsonConvert.SerializeObject(createCommand.Template);
        var newSchedulingTemplate = new RouteSchedulingTemplate()
        {
            RouteId = createCommand.RouteId, Template = templateString, Name = createCommand.Name
        };
        await routeSchedulingTemplateRepository.Add(newSchedulingTemplate);

        return newSchedulingTemplate;
    }

    public async Task RunScheduler(RouteSchedulingTemplateRunSchedulerCommand runCommand)
    {
        foreach (var routeId in runCommand.RouteIds)           
        {
            var schedulingTemplates = await routeSchedulingTemplateRepository.QueryAll().Where(s => s.RouteId == routeId).ToListAsync();
            foreach (var schedulingTemplate in schedulingTemplates)
            {
                await routeSchedulerService.ScheduleRoute(routeId, schedulingTemplate.Id, DateTime.Today.AddDays(1));
            }
        }
    }

    public async Task<RouteSchedulingTemplate> EditRouteSchedulingTemplate(RouteSchedulingTemplateUpdateCommand editCommand)
    {
        var scheudlingTemplate = await routeSchedulingTemplateRepository.QueryById(editCommand.Id).FirstOrDefaultAsync();
        if (scheudlingTemplate == null)
        {
            throw new KeyNotFoundException($"Route Scheduling Template with ID: {editCommand.Id} was not found");
        }

        scheudlingTemplate = mapper.Map(editCommand, scheudlingTemplate);
        await routeSchedulingTemplateRepository.Update(scheudlingTemplate);
        
        return scheudlingTemplate;
    }

    public async Task DeleteRouteSchedulingTemplate(Guid id)
    {
        var routeSchedulingTemplate = await routeSchedulingTemplateRepository.QueryById(id).FirstOrDefaultAsync();
        if (routeSchedulingTemplate == null)
        {
            throw new KeyNotFoundException($"Route scheduling template with id: {id} was not found");
        }

        await routeSchedulingTemplateRepository.Delete(routeSchedulingTemplate);
    }
}
