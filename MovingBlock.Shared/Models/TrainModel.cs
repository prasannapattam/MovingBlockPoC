using System.Text.Json.Serialization;

namespace MovingBlock.Shared.Models
{
    public class TrainModel
    {
        [JsonIgnore]
        public LocationSensorModel? FrontSensor { get; set; }
        [JsonIgnore]
        public LocationSensorModel? RearSensor { get; set; }
        public double FrontTravelled { get; set; } = 0;  // meters
        public double RearTravelled { get; set; } = 0;  // meters

        public int Id { get; set; }
        public int TrainNumber { get;set; }
        public string? TrainName { get; set; }
        public int TrainLength { get; set; } // meters
        public double Speed { get; set; } // kmph
    }
}
