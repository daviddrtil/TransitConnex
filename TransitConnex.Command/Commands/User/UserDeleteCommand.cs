using System.ComponentModel.DataAnnotations;

namespace TransitConnex.Infrastructure.Commands.User;

public class UserDeleteCommand : IUserCommand
{
    public Guid Id { get; set; }
}
