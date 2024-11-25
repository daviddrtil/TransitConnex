namespace TransitConnex.Domain.DTOs;

public class UserFavLocationDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid LocationId { get; set; }
    public DateTime AddTime { get; set; }
}
