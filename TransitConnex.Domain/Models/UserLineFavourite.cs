namespace TransitConnex.Domain.Models;

public class UserLineFavourite
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid LineId { get; set; }
    public Line? Line { get; set; }
}
