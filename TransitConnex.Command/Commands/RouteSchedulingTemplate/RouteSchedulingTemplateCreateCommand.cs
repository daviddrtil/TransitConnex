namespace TransitConnex.Command.Commands.RouteSchedulingTemplate;

public class RouteSchedulingTemplateCreateCommand : IRouteSchedulingTemplateCommand
{
    public required Guid RouteId { get; set; }
    public required string Name { get; set; }
    public required SchedulingTemplate Template { get; set; }
}
