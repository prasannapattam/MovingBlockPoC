namespace MovingBlock.Shared.Models
{
    public class LocationSensor
    {
        public LocationSensor(string sensorId, SensorPosition position) 
        { 
            SensorId = sensorId;
            Position = position;
        }

        public string SensorId { get; set; }
        public SensorPosition Position { get; set; }

        public Location? CurrentLocation { get; set; }
        public double DistanceTravelledFromLast { get; set; } = 0;
    }
}
