using MovingBlock.Functions.Data;
using MovingBlock.Shared.Models;
using MovingBlock.Shared.Utilities;

namespace MovingBlock.Functions
{
    public static class DeviceTwinFunctions
    {
        private static readonly TwinData _twinData = TwinData.Instance;
        private static readonly EventQueue<LocationSensorModel> _sensorQueue = EventQueue<LocationSensorModel>.Instance;

        private static readonly object _lockObj = new object();

        public static void ProcessLocationSensor(LocationSensorModel sensor)
        {
            // getting sensorid
            LocationSensorModel sensorTwin = _twinData.SensorTwins[sensor.SensorId];
            double distanceTravelled = DistanceCalculator.CalculateDistance(sensor.CurrentLocation, sensorTwin.CurrentLocation);

            // check for geofense & calculating the distance travelled by sensor
            if (sensor.CurrentLocation.Longitude > _twinData.SectionTwin?.StartLocation?.Longitude)
            {
                sensorTwin.distanceTravelled = distanceTravelled;
            }
            else
            {
                sensorTwin.distanceTravelled = 0;
            }

            sensorTwin.Speed = distanceTravelled / sensor.TimeElapsed;
            sensorTwin.CurrentLocation = sensor.CurrentLocation;

            _sensorQueue.Enqueue(sensorTwin);
        }
    }
}
