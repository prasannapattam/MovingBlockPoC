namespace MovingBlock.Shared.Models
{
    public class LocationSensorModel
    {
        public LocationSensorModel(string sensorId, SensorPosition position, Location location) 
        { 
            SensorId = sensorId;
            Position = position;
            CurrentLocation = location;
            DistanceTravelledFromLast = 0;
        }

        public LocationSensorModel(LocationSensorModel sensor)
        {
            SensorId = sensor.SensorId;
            Position = sensor.Position;
            CurrentLocation = sensor.CurrentLocation;
            DistanceTravelledFromLast = 0;
        }

        public string SensorId { get; set; }
        public SensorPosition Position { get; set; }
        public Location CurrentLocation { get; set; }
        public double DistanceTravelledFromLast { get; set; } // meters
    }
}
