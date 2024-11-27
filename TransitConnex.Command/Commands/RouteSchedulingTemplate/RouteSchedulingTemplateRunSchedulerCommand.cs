namespace TransitConnex.Command.Commands.RouteSchedulingTemplate;

public class RouteSchedulingTemplateRunSchedulerCommand : IRouteSchedulingTemplateCommand
{
    public required List<Guid> RouteIds { get; set; } = new List<Guid>();
}
