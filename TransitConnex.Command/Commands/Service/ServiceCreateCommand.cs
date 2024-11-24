using System.ComponentModel.DataAnnotations;

namespace TransitConnex.Command.Commands.Service;

public class ServiceCreateCommand : IServiceCommand
{
    [MaxLength(255)]
    public required string Name { get; set; }
    public Guid? IconId { get; set; }
    public string Description { get; set; } = string.Empty;
}
