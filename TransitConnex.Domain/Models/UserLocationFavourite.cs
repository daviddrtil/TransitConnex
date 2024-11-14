namespace TransitConnex.Domain.Models
{
    public class UserLocationFavourite
    {
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid LocationId { get; set; }
        public Location? Location { get; set; }
    }
}
