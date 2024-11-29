namespace TransitConnex.Domain.Collections;

public class UserFavConnectionDoc : QueryModelBase<Guid>
{
    public Guid UserId { get; set; }
    public Guid FromLocationId { get; set; }
    public Guid ToLocationId { get; set; }
    public DateTime AddTime { get; set; }
}
