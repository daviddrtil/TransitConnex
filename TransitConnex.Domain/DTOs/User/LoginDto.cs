using System.ComponentModel.DataAnnotations;

namespace TransitConnex.Domain.DTOs.User;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}
