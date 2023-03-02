using MovingBlock.Functions.Data;
using MovingBlock.Shared.Models;
using MovingBlock.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // check for geofense & calculating the distance travelled by sensor
            if (sensor.CurrentLocation.Longitude > _twinData.SectionTwin?.StartLocation?.Longitude)
            {
                sensorTwin.DistanceTravelledFromLast = DistanceCalculator.CalculateDistance(sensor.CurrentLocation, sensorTwin.CurrentLocation);
                sensorTwin.CurrentLocation = sensor.CurrentLocation;
            }
            else
            {
                sensorTwin.DistanceTravelledFromLast = 0;
                sensorTwin.CurrentLocation.Longitude = _twinData.SectionTwin!.StartLocation!.Longitude;
            }

            _sensorQueue.Enqueue(sensorTwin);
        }
    }
}
