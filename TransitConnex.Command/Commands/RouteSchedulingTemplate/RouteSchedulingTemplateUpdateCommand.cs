namespace TransitConnex.Command.Commands.RouteSchedulingTemplate;

public class RouteSchedulingTemplateUpdateCommand : IRouteSchedulingTemplateCommand
{
    // Wont be able to change RouteId for safety reasons
    public required Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public required string Template { get; set; }
    public bool UpdateExistingScheduledRoutes { get; set; }
}
