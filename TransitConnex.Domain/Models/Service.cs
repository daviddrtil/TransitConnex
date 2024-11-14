namespace TransitConnex.Domain.Models
{
    public class Service
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid? IconId { get; set; }
        public Icon? Icon { get; set; }
        public string? Description { get; set; }
    }
}
