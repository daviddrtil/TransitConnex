using System.ComponentModel;

namespace TransitConnex.Domain.Enums;

public enum SeatStateEnum
{
    [Description("free")]
    FREE = 1,
        
    [Description("reserved")]
    RESERVED = 2,
} 
