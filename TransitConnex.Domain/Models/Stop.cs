namespace TransitConnex.Domain.Models
{
    public class Stop
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int StopType { get; set; } // 1 - "busStop", 2 - "tramStop", 3 - "trainStop"
    }
}
