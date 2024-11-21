using System.ComponentModel;

namespace TransitConnex.Domain.Enums
{
    public enum LineTypeEnum
    {
        [Description("BustLine")]
        BUS = 1,
        
        [Description("TramLine")]
        TRAM = 2,

        [Description("TrainLine")]
        TRAIN = 3,
    }
}
