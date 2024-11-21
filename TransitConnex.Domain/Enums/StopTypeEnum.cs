using System.ComponentModel;

namespace TransitConnex.Domain.Enums
{
    public enum StopTypeEnum
    {
        [Description("BustStop")]
        BUS = 1,
        
        [Description("TramStop")]
        TRAM = 2,

        [Description("TrainStop")]
        TRAIN = 3,

    }
}
