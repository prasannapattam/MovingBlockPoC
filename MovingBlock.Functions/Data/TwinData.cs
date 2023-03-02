    using MovingBlock.Shared.Models;
using MovingBlock.Shared.Utilities;

namespace MovingBlock.Functions.Data
{
    public class TwinData
    {
        private static readonly TwinData instance = new TwinData();
        public static TwinData Instance { get { return instance; } }

        public List<TrainModel> TrainTwins { get; } = new List<TrainModel>();
        public Dictionary<string, LocationSensorModel> SensorTwins { get; } = new Dictionary<string, LocationSensorModel>();

        public SectionModel? SectionTwin { get; set; }
    }
}
