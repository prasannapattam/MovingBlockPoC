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
        private static readonly EventQueue<LocationSensor> _sensorQueue = EventQueue<LocationSensor>.Instance;

        private static readonly object _lockObj = new object();

        public static void ProcessGeoSensor(LocationSensor sensor)
        {
            // getting sensorid

        }

    }
}
