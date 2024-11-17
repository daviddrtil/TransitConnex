namespace TransitConnex.Domain.Models
{
    public class User // TODO -> login -> identity
    {
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Deleted { get; set; }
        public bool IsAdmin { get; set; }
    }
}
