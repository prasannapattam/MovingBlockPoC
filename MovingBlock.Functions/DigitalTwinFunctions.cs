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
                Distance = 3, // kms
                Speed = 145, // kmph
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
            if(_twinData.SectionTwin == null)
            {
                lock(_lockObj)
                {
                    CreateSectionTwin();
                }
            }

            lock (_lockObj)
            {
                trainTwin.Id = _twinData.TrainTwins.Count + 1;
                Location frontLocation = _twinData.SectionTwin?.StartLocation!;
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
            return new List<TrainModel>(_twinData.TrainTwins);
        }

        public static  SectionModel GetSection()
        {
            return _twinData.SectionTwin!;
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
                trainTwin.FrontTravelled += sensor.distanceTravelled;
            }
            else
            {
                trainTwin.RearTravelled += sensor.distanceTravelled;
            }

            int sectionDistancemtrs = (int) _twinData.SectionTwin?.Distance! * 1000;
            // checking if the train crossed the section
            if (trainTwin.FrontTravelled > sectionDistancemtrs &&
                trainTwin.RearTravelled > sectionDistancemtrs)
            {
                lock (_lockObj)
                {
                    _twinData.SensorTwins.Remove(trainTwin.FrontSensor?.SensorId!);
                    _twinData.SensorTwins.Remove(trainTwin.RearSensor?.SensorId!);
                    _twinData.TrainTwins.Remove(trainTwin);
                }
            }
        }
    }
}
