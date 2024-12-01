using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TransitConnex.Command.Repositories.Interfaces;
using TransitConnex.Command.Services.Interfaces;
using TransitConnex.Domain.Models;

namespace TransitConnex.Command.Services;

public class RouteSchedulerService(IRouteRepository routeRepository, IRouteSchedulingTemplateRepository routeSchedulingTemplateRepository, IScheduledRouteRepository scheduledRouteRepository) : IRouteSchedulerService
{
    private readonly List<string> Holidays = ["01/01","29/03","01/04","01/05","08/05","05/07","05/07","28/09","28/10","17/11","24/12","25/12","26/12"];

    public async Task ScheduleRoute(Guid routeId, Guid schedulingTemplateId, DateTime fromDate, bool reschedule)
    {
        var route = await routeRepository.QueryById(routeId).FirstOrDefaultAsync();
        if (route is null)
        {
            throw new KeyNotFoundException($"Scheduling failed, route with ID: {routeId} was not found.");
        }
        
        var schedulingTemplate = await routeSchedulingTemplateRepository.QueryById(schedulingTemplateId).FirstOrDefaultAsync();
        if (schedulingTemplate is null)
        {
            throw new KeyNotFoundException($"Scheduling failed, template with ID: {schedulingTemplateId} was not found.");
        }
        
        var jsonParsed = JsonConvert.DeserializeObject<SchedulingTemplate>(schedulingTemplate.Template);
        if (jsonParsed is null)
        {
            throw new Exception($"Scheduling failed, template with ID {schedulingTemplateId} is invalid.");
        }

        if (reschedule)
        {
            var scheduledRoutesToDelete = await scheduledRouteRepository.QueryAll().Where(x => x.RouteId == routeId && x.StartTime >= fromDate).ToListAsync();
            await scheduledRouteRepository.DeleteBatch(scheduledRoutesToDelete);
        }

        var toDate = new DateTime(fromDate.Year, 12, 31);
        for (var currentDate = fromDate; currentDate <= toDate; currentDate = currentDate.AddDays(1))
        {
            var isWeekend = IsWeekend(currentDate);
            var isHoliday = IsHoliday(currentDate);
            
            if (!route.IsHolydayRoute && !route.IsWeekendRoute)
            {
                // MON-FRI, except holidays
                if (!isWeekend && !isHoliday)
                {
                    await CreateScheduledRoutesForDay(jsonParsed, route, currentDate);
                }
            }
            else if (!route.IsHolydayRoute && route.IsWeekendRoute)
            {
                // SAT-SUN, except holidays
                if (isWeekend && !isHoliday)
                {
                    await CreateScheduledRoutesForDay(jsonParsed, route, currentDate);
                }
            }
            else if (route.IsHolydayRoute)
            {
                if (isHoliday)
                {
                    await CreateScheduledRoutesForDay(jsonParsed, route, currentDate);
                }
            }
        }
    }

    private bool IsWeekend(DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
    }

    private bool IsHoliday(DateTime date)
    {
        return Holidays.Contains(date.ToString("dd/MM"));
    }

    private async Task CreateScheduledRoutesForDay(SchedulingTemplate jsonParsed, Route route, DateTime currentDate)
    {
        var scheduledRoutes = new List<ScheduledRoute>();
        foreach (var scheduledRouteTemplate in jsonParsed.ScheduledRoutes)
        {
            var scheduledRoute = new ScheduledRoute
            {
                StartTime = currentDate.Add(scheduledRouteTemplate.StartTime),
                EndTime = currentDate.Add(scheduledRouteTemplate.StartTime).Add(route.DurationTime),
                VehicleId = scheduledRouteTemplate.VehicleId,
                RouteId = route.Id,
                Price = scheduledRouteTemplate.Price
            };

            scheduledRoutes.Add(scheduledRoute);
        }
     
        await scheduledRouteRepository.AddBatch(scheduledRoutes);
    }

    private class SchedulingTemplate
    {
        public List<ScheduledRouteTemplate> ScheduledRoutes { get; set; } = new List<ScheduledRouteTemplate>();
    }

    private class ScheduledRouteTemplate
    {
        public Guid VehicleId { get; set; }
        public TimeSpan StartTime { get; set; }
        public float Price { get; set; }
    }
    
}
