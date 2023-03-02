using MovingBlock.Functions.Data;
using MovingBlock.Shared.Models;

namespace MovingBlock.Functions
{
    public static class TrainTwinFunctions
    {
        private static readonly TwinData _twinData = TwinData.Instance;
        private static readonly object _lockObj = new object();

        public static List<TrainModel> GetTrains()
        {
            return _twinData.Trains;
        }

        public static void CreateTrainTwin(TrainModel trainTwin)
        {
            lock (_lockObj)
            {
                trainTwin.Id = _twinData.Trains.Count + 1;
                trainTwin.FrontSensor = new LocationSensor(trainTwin.Id + "-front", SensorPosition.Front);
                trainTwin.RearSensor = new LocationSensor(trainTwin.Id + "-rear", SensorPosition.Rear);
                _twinData.Trains.Add(trainTwin);
                _twinData.Sensors.Add(trainTwin.FrontSensor.SensorId, trainTwin.FrontSensor);
                _twinData.Sensors.Add(trainTwin.RearSensor.SensorId, trainTwin.RearSensor);
            }
        }

        public static void ClearTrains()
        {
            lock ( _lockObj)
            {
                _twinData.Trains.Clear();
            }
        }
    }
}
