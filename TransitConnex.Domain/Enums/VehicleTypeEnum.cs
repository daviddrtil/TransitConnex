using System.ComponentModel;

namespace TransitConnex.Domain.Enums;

public enum VehicleTypeEnum
{
    [Description("Bus")] BUS = 1,

    [Description("Tram")] TRAM = 2,

    [Description("Train")] TRAIN = 3
}
