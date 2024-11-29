namespace TransitConnex.Domain.DTOs;

public class UserFavConnectionDto
{
    public Guid FromLocationId { get; set; }
    public Guid ToLocationId { get; set; }
    public DateTime AddTime { get; set; }
}
