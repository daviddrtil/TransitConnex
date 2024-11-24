using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TransitConnex.Infrastructure.Commands.User;

public class UserCreateCommand : IUserCommand
{
    [EmailAddress]
    [MaxLength(320)] // TODO -> change in db
    public required string Email { get; set; }
    
    // TODO -> mby add username?
    
    [DataType(DataType.Password)]
    [MaxLength(255)]
    public required string Password { get; set; }
}
