using System.ComponentModel.DataAnnotations;

namespace TransitConnex.Command.Commands.User;

public class UserUpdateCommand : IUserCommand
{
    public Guid Id { get; set; }
    [DataType(DataType.Password)]
    public required string OldPassword { get; set; }
    [DataType(DataType.Password)]
    public required string NewPassword { get; set; }
}
