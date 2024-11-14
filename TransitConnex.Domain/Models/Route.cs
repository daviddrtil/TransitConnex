namespace TransitConnex.Domain.Models
{
    public class Route
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public TimeSpan DurationTime { get; set; }
        public Guid LineId { get; set; }
        public Line? Line { get; set; }
        public Guid StartStopId { get; set; }
        public Stop? StartStop { get; set; }
        public Guid EndStopId { get; set; }
        public Stop? EndStop { get; set; }
        public bool IsActive { get; set; }
        public bool IsWeekendRoute { get; set; }
        public bool IsHolydayRoute { get; set; }
        public bool HasTickets { get; set; }
    }
}
