using System.ComponentModel.DataAnnotations;

namespace TransitConnex.Infrastructure.Commands.Service;

public class ServiceUpdateCommand : IServiceCommand
{
    public required Guid Id { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }
    public Guid IconId { get; set; }
    public string Description { get; set; } = string.Empty;
}
