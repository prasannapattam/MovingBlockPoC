using MovingBlock.Shared.Models;

namespace MovingBlock.Functions.Data
{
    public class TrainTwinData
    {
        public static TrainTwinData GetInstance() { return new TrainTwinData(); }

        public List<TrainModel> TrainData { get; } = new List<TrainModel>();
    }
}
