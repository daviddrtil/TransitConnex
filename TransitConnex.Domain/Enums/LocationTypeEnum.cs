using System.ComponentModel;

namespace TransitConnex.Domain.Enums;

public enum LocationTypeEnum
{
    [Description("City")] CITY = 1,

    [Description("City part")] CITY_PART = 2
}
