namespace TransitConnex.Domain.DTOs;

public class UserFavLineDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid LineId { get; set; }
    public DateTime AddTime { get; set; }
}
