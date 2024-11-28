using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TransitConnex.Command.Commands.RouteSchedulingTemplate;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.DTOs.RouteSchedulingTemplate;
using TransitConnex.Domain.Models;
using TransitConnex.Query.Queries;

namespace TransitConnex.Command.Services;

public class RouteSchedulingTemplateService(IMapper mapper, IRouteSchedulingTemplateRepository routeSchedulingTemplateRepository, IRouteSchedulerService routeSchedulerService, IRouteRepository routeRepository)
    : IRouteSchedulingTemplateService
{
    public async Task<List<RouteSchedulingTemplateDto>> GetFilteredRouteSchedulingTemplates(RouteSchedulingTemplateFilteredQuery filter)
    {
        var query = routeSchedulingTemplateRepository.QueryAll();
        if (filter.Name is not null)
        {
            var normalizedFilterName = filter.Name.ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(normalizedFilterName));
        }
        
        if (filter.Ids is not null)
        {
            query = query.Where(x => filter.Ids.Contains(x.Id));
        }

        if (filter.RouteId is not null)
        {
            query = query.Where(x => x.RouteId == filter.RouteId);
        }
        
        var routeSchedulingTemplates = await query.ToListAsync();
        
        return mapper.Map<List<RouteSchedulingTemplateDto>>(routeSchedulingTemplates);
    }

    public async Task<RouteSchedulingTemplateDto> GetRouteSchedulingTemplateById(Guid id)
    {
        var routeSchedulingTemplate = await routeSchedulingTemplateRepository.QueryById(id).FirstOrDefaultAsync();
        
        return mapper.Map<RouteSchedulingTemplateDto>(routeSchedulingTemplate);
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
        var routes = routeRepository.QueryAll();

        if (runCommand.RouteIds is not null && runCommand.RouteIds.Any())
        {
            routes = routes.Where(x => runCommand.RouteIds.Contains(x.Id));
        }

        var routesToSchedule = await routes.ToListAsync();
        
        foreach (var route in routesToSchedule)           
        {
            var schedulingTemplates = await routeSchedulingTemplateRepository.QueryAll().Where(s => s.RouteId == route.Id).ToListAsync();
            foreach (var schedulingTemplate in schedulingTemplates)
            {
                await routeSchedulerService.ScheduleRoute(route.Id, schedulingTemplate.Id, DateTime.Today.AddDays(1));
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
    
    public async Task DeleteRouteSchedulingTemplatesForRoute(Guid routeId)
    {
        var routeSchedulingTemplates = await routeSchedulingTemplateRepository.QueryAll().Where(s => s.RouteId == routeId).ToListAsync();

        await routeSchedulingTemplateRepository.DeleteBatch(routeSchedulingTemplates);
    }
}
