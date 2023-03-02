    using MovingBlock.Shared.Models;
using MovingBlock.Shared.Utilities;

namespace MovingBlock.Functions.Data
{
    public class TwinData
    {
        private static readonly TwinData instance = new TwinData();
        public static TwinData Instance { get { return instance; } }

        private TwinData() 
        {
            RailwayPathConfiguration = new RailwayPathConfiguration()
            {
                Distance = 50, // kms
                Speed = 70, // kmph
                SafeDistance = 2, // km
                CriticalDistance = 1, //km
                StartLocation = new Location(17, 78), // Hyderabad location
            };

            RailwayPathConfiguration.EndLocation = DistanceCalculator.GetPoint2(RailwayPathConfiguration.StartLocation, RailwayPathConfiguration.Distance * 1000);
        }

        public List<TrainModel> Trains { get; } = new List<TrainModel>();
        public Dictionary<string, LocationSensor> Sensors { get; } = new Dictionary<string, LocationSensor>();

        public RailwayPathConfiguration RailwayPathConfiguration { get; set; }
    }
}
