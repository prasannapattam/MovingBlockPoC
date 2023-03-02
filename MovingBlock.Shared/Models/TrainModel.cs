namespace MovingBlock.Shared.Models
{
    public class TrainModel
    {
        public LocationSensorModel? FrontSensor { get; set; }
        public LocationSensorModel? RearSensor { get; set; }
        public double FrontPathTravelled { get; set; } = 0;  // meters
        public double RearPathTravelled { get; set; } = 0;  // meters

        public int Id { get; set; }
        public int TrainNumber { get;set; }
        public string? TrainName { get; set; }
        public int TrainLength { get; set; } // meters
        public double Speed { get; set; } // kmph
    }
}
