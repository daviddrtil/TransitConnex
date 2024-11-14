namespace TransitConnex.Domain.Models
{
    public class RouteStop
    {
        public Guid RouteId { get; set; }
        public Route? Route { get; set; }
        public Guid StopId { get; set; }
        public Stop? Stop { get; set; }
        public TimeSpan TimeDurationFromFirstStop { get; set; }
        public int StopOrder { get; set; }
    }
}
