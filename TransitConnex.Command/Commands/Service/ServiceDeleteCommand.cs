namespace TransitConnex.Infrastructure.Commands.Service;

public class ServiceDeleteCommand : IServiceCommand
{
    public required Guid Id { get; set; }
}
