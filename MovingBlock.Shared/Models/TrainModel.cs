namespace MovingBlock.Shared.Models
{
    public class TrainModel
    {
        public LocationSensor? FrontSensor { get; set; }
        public LocationSensor? RearSensor { get; set; }
        public double FrontPathTravelled { get; set; } = 0;
        public double RearPathTravelled { get; set; } = 0;

        public int Id { get; set; }
        public int TrainNumber { get;set; }
        public string? TrainName { get; set; }
        public double Speed { get; set; }
    }
}
