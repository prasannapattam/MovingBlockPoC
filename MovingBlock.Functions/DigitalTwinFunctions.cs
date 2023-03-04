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
                Length = 1 * 1000, // meters
                Speed = 145 * (5.0 / 18.0), // meters/sec
                SafeDistance = 2 * 1000, // meters
                CriticalDistance = 1 * 1000, // meters
                StartLocation = new Location(17, 78), // Hyderabad location
            };

            sectionTwin.EndLocation = DistanceCalculator.GetPoint2(sectionTwin.StartLocation, sectionTwin.Length);

            lock (_lockObj)
            {
                _twinData.SectionTwin = sectionTwin;
            }
            return sectionTwin;
        }

        public static void CreateTrainTwin(TrainModel trainTwin)
        {
            if(_twinData.SectionTwin == null)
                CreateSectionTwin();

            lock (_lockObj)
            {
                if (_twinData.TrainTwins.Count > 0)
                    trainTwin.Id = _twinData.TrainTwins.Last().Id;
                else
                    trainTwin.Id = 1;
                trainTwin.Section = _twinData.SectionTwin!;
                Location frontLocation = _twinData.SectionTwin!.StartLocation;
                Location rearLocation = DistanceCalculator.GetPoint2(frontLocation, -trainTwin.TrainLength);

                trainTwin.FrontSensor = new LocationSensorModel(trainTwin.Id + "-front", SensorPosition.Front, frontLocation);
                trainTwin.RearSensor = new LocationSensorModel(trainTwin.Id + "-rear", SensorPosition.Rear, rearLocation);
                _twinData.SensorTwins.Add(trainTwin.FrontSensor.SensorId, trainTwin.FrontSensor);
                _twinData.SensorTwins.Add(trainTwin.RearSensor.SensorId, trainTwin.RearSensor);
                _twinData.TrainTwins.Add(trainTwin);
            }
        }

        public static void ClearTrainTwins()
        {
            lock ( _lockObj)
            {
                _twinData.TrainTwins.Clear();
                _twinData.SensorTwins.Clear();
            }
        }

        public static List<TrainModel> GetTrains()
        {
            return _twinData.TrainTwins;
        }

        public static  SectionModel GetSection()
        {
            if(_twinData.SectionTwin == null)
                CreateSectionTwin();

            return _twinData.SectionTwin!;
        }


        public static void ProcessLocationSensor(LocationSensorModel sensor)
        {
            TrainModel? trainTwin = null;
            // getting train for the sensor
            foreach (TrainModel train in _twinData.TrainTwins)
            {
                if(train.FrontSensor.SensorId == sensor.SensorId || train.RearSensor.SensorId == sensor.SensorId)
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
                trainTwin.FrontTravelled += sensor.distanceTravelled;
            }
            else
            {
                trainTwin.RearTravelled += sensor.distanceTravelled;
            }

            trainTwin.Speed = sensor.Speed;

            // checking if the train crossed the section
            if (trainTwin.FrontTravelled > _twinData.SectionTwin.Length &&
                trainTwin.RearTravelled > _twinData.SectionTwin.Length)
            {
                lock (_lockObj)
                {
                    _twinData.SensorTwins.Remove(trainTwin.FrontSensor.SensorId);
                    _twinData.SensorTwins.Remove(trainTwin.RearSensor.SensorId);
                    _twinData.TrainTwins.RemoveAll(t => t.Id == trainTwin.Id);
                }
            }
        }
    }
}
