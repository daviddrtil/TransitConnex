namespace TransitConnex.Command.Commands.RouteSchedulingTemplate;

public class RouteSchedulingTemplateRunSchedulerCommand : IRouteSchedulingTemplateCommand
{
    public List<Guid>? RouteIds { get; set; } = new List<Guid>();
    public bool Reschedule { get; set; } = false;
}
