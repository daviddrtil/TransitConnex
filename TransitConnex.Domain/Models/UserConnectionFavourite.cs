namespace TransitConnex.Domain.Models;

public class UserConnectionFavourite
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    // public Guid LineId { get; set; }
    // public Line? Line { get; set; }
    public Guid FromLocationId { get; set; }
    public Location? FromLocation { get; set; }
    public Guid ToLocationId { get; set; }
    public Location? ToLocation { get; set; }
}
