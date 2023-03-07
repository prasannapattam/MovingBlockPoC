#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

using System.Text.Json.Serialization;

namespace MovingBlock.Shared.Models
{
    public class TrainModel
    {
        [JsonIgnore]
        public SectionModel Section { get; set; }
        [JsonIgnore]
        public LocationSensorModel FrontSensor { get; set; }
        [JsonIgnore]
        public LocationSensorModel RearSensor { get; set; }
        public double FrontTravelled { get; set; } = 0;  // meters
        public double RearTravelled { get; set; } = 0;  // meters

        public int TrainID { get; set; }
        public int TrainNumber { get;set; }

        public string TrainName { get; set; }
        public int TrainLength { get; set; } // meters
        public double Speed { get; set; } // meters/sec
        public double RecommendedSpeed { get; set; } // meters/sec
        public double SimulatorSpeed { get; set; } // m/s
    }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
