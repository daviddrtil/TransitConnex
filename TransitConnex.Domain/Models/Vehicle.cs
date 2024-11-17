﻿namespace TransitConnex.Domain.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string? Label { get; set; }
        public string? Spz { get; set; }
        public string? Manufacturer { get; set; }
        public int Capacity { get; set; }
        public int VehicleType { get; set; } // 1 - bus, 2 - tram, 3 - train
        public Guid? IconId { get; set; }
        public Icon? Icon { get; set; }
        public Guid? LineId { get; set; }
        public Line? Line { get; set; }
    }
}
