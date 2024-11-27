namespace TransitConnex.Command.Services.Interfaces;

public interface IRouteSchedulerService
{
    Task ScheduleRoute(Guid routeId, Guid schedulingTemplateId, DateTime fromDate);
}
