namespace TransitConnex.Domain.Collections;

public class UserFavLineDoc : QueryModelBase<Guid>
{
    public Guid UserId { get; set; }
    public Guid LineId { get; set; }
    public DateTime AddTime { get; set; }
}
