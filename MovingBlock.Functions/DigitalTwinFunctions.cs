using MovingBlock.Functions.Data;
using MovingBlock.Shared.Models;
using MovingBlock.Shared.Utilities;

namespace MovingBlock.Functions
{
    public static class DigitalTwinFunctions
    {
        private static readonly TwinData _twinData;
        private static readonly EventQueue<LocationSensorModel> _sensorQueue;

        private static readonly object _lockObj;

        static DigitalTwinFunctions()
        {
            _twinData = TwinData.Instance;
            _sensorQueue = EventQueue<LocationSensorModel>.Instance;
            _sensorQueue.Enqueued += ProcessLocationSensor;
            _lockObj = new object();
        }

        public static SectionModel CreateSectionTwin()
        {
            var sectionTwin = new SectionModel()
            {
                Distance = 1, // kms
                Speed = 120, // kmph
                SafeDistance = 2, // km
                CriticalDistance = 1, //km
                StartLocation = new Location(17, 78), // Hyderabad location
            };

            sectionTwin.EndLocation = DistanceCalculator.GetPoint2(sectionTwin.StartLocation, sectionTwin.Distance * 1000);
            _twinData.SectionTwin = sectionTwin;
            return sectionTwin;
        }

        public static void CreateTrainTwin(TrainModel trainTwin)
        {
            lock (_lockObj)
            {
                trainTwin.Id = _twinData.TrainTwins.Count + 1;
                Location frontLocation = _twinData.SectionTwin?.StartLocation!;
                Location rearLocation = DistanceCalculator.GetPoint2(frontLocation, -trainTwin.TrainLength);

                trainTwin.FrontSensor = new LocationSensorModel(trainTwin.Id + "-front", SensorPosition.Front, frontLocation);
                trainTwin.RearSensor = new LocationSensorModel(trainTwin.Id + "-rear", SensorPosition.Rear, rearLocation);
                _twinData.TrainTwins.Add(trainTwin);
                _twinData.SensorTwins.Add(trainTwin.FrontSensor.SensorId, trainTwin.FrontSensor);
                _twinData.SensorTwins.Add(trainTwin.RearSensor.SensorId, trainTwin.RearSensor);
            }
        }

        public static void ClearTrainTwins()
        {
            lock ( _lockObj)
            {
                _twinData.TrainTwins.Clear();
            }
        }

        public static List<TrainModel> GetTrains()
        {
            return _twinData.TrainTwins;
        }


        public static void ProcessLocationSensor(LocationSensorModel sensor)
        {
            TrainModel? trainTwin = null;
            // getting train for the sensor
            foreach (TrainModel train in _twinData.TrainTwins)
            {
                if(train.FrontSensor?.SensorId == sensor.SensorId || train.RearSensor?.SensorId == sensor.SensorId)
                {
                    trainTwin = train;
                    break;
                }
            }

            // ensure 
            if (trainTwin == null)
                return;

            if(sensor.Position == SensorPosition.Front)
            {
                trainTwin.FrontPathTravelled += sensor.DistanceTravelledFromLast;
            }
            else
            {
                trainTwin.RearPathTravelled += sensor.DistanceTravelledFromLast;
            }

            int sectionDistancemtrs = (int) _twinData.SectionTwin?.Distance! * 1000;
            // checking if the train crossed the section
            if (trainTwin.FrontPathTravelled > sectionDistancemtrs &&
                trainTwin.RearPathTravelled > sectionDistancemtrs)
            {
                lock (_lockObj)
                {
                    _twinData.TrainTwins.Remove(trainTwin);
                }
            }
        }
    }
}
