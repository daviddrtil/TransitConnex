using Microsoft.AspNetCore.Identity;

namespace TransitConnex.Domain.Models;

public class User : IdentityUser<Guid>
{
    //public Guid UserId { get => Guid.Parse(Id); }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public bool Deleted { get; set; }
    public bool IsAdmin { get; set; }
}
