using System.ComponentModel.DataAnnotations;

namespace TransitConnex.Command.Commands.User;

public class UserCreateCommand : IUserCommand
{
    [EmailAddress]
    [MaxLength(320)]
    public required string Email { get; set; }

    [DataType(DataType.Password)]
    [MaxLength(255)]
    public required string Password { get; set; }
    
    [DataType(DataType.Password)] 
    [MaxLength(255)]
    public required string ConfirmPassword { get; set; }
}
