namespace TransitConnex.Command.Commands.RouteSchedulingTemplate;

public class RouteSchedulingTemplateDeleteCommand : IRouteSchedulingTemplateCommand
{
    public required Guid Id { get; set; }
    public bool DeleteExistingScheduledRoutes { get; set; }
}
