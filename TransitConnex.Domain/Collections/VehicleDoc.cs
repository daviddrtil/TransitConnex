using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitConnex.Domain.Enums;
using TransitConnex.Domain.Models;

namespace TransitConnex.Domain.Collections;

public class VehicleDoc : QueryModelBase<Guid>
{
    public string? Label { get; set; }
    public string? Spz { get; set; }
    public string? Manufacturer { get; set; }
    public int Capacity { get; set; }
    public VehicleTypeEnum VehicleType { get; set; } // 1 - bus, 2 - tram, 3 - train
    public Guid? IconId { get; set; }
    public Icon? Icon { get; set; }
    public Guid? LineId { get; set; }
    public Line? Line { get; set; }
}
