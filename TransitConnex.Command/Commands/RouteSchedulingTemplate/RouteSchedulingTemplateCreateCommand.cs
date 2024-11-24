namespace TransitConnex.Infrastructure.Commands.RouteSchedulingTemplate;

public class RouteSchedulingTemplateCreateCommand : IRouteSchedulingTemplateCommand
{
    public required Guid RouteId { get; set; }
    public required string Template { get; set; }
}
