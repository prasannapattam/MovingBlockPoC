using MovingBlock.Functions.Data;
using MovingBlock.Shared.Models;

namespace MovingBlock.Functions
{
    public static class TrainTwinFunctions
    {
        private static readonly TrainTwinData _trains = TrainTwinData.GetInstance();
        private static readonly object _lockObj = new object();

        public static List<TrainModel> GetTrains()
        {
            return _trains.TrainData;
        }

        public static void StartTrain(TrainModel trainTwin)
        {
            lock (_lockObj)
            {
                trainTwin.Id = _trains.TrainData.Count + 1;
                _trains.TrainData.Add(trainTwin);
            }
        }
    }
}
