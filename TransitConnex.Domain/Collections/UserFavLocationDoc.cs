namespace TransitConnex.Domain.Collections;

public class UserFavLocationDoc : QueryModelBase<Guid>
{
    public Guid UserId { get; set; }
    public Guid LocationId { get; set; }
    public DateTime AddTime { get; set; }
}
