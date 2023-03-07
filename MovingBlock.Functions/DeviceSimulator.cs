using MovingBlock.Shared.Models;
using MovingBlock.Shared.Utilities;

namespace MovingBlock.Functions
{
    public class DeviceSimulator
    {
        private int _timer = 1; // secs

        public int TimeDelayms { get {  return _timer * 1000; } }

        private double _maxAcceleration;
        private double _maxDeceleration;

        public DeviceSimulator() 
        {
            double u, v, t, s;

            // max acceleration for WP5 
            u = 110 * 5.0 / 18; // 110 kmph
            v = 120 * 5.0 / 18; // 120 kmph
            t = 402; // sec
            _maxAcceleration = (v - u) / t;

            // maxDeceleration for WP7
            u = 155 * 5.0 / 18; // 155 kmph
            s = 1.2 * 1000; // 1.2 kms
            _maxDeceleration = (u * u) / (2 * s); 
        }

        public List<TrainModel> Initialize()
        {
            // creating SectionTwin
            TrainModel trainModel = new TrainModel()
            {
                TrainNumber = 123,
                TrainName = "PPK",
                TrainLength = 350,
                Speed = 120 * (5.0 / 18),
            };

            trainModel.SimulatorSpeed = trainModel.Speed;

            DigitalTwinFunctions.CreateTrainTwin(trainModel);
            return DigitalTwinFunctions.GetTrains();
        }

        public async Task SimulateTrainsAsync()
        {
            List<TrainModel> trainTwins = DigitalTwinFunctions.GetTrains();
            for (int i = 0; i < trainTwins.Count; i++)
            {
                SimulateSensors(trainTwins[i]);
            }
            await Task.Delay(TimeDelayms);
        }

        private void SimulateSensors(TrainModel trainTwin)
        {
            Console.WriteLine($"{trainTwin.FrontTravelled:F2} - {trainTwin.RearTravelled:F2} : {trainTwin.Speed * 18.0 / 5:F2}");

            LocationSensorModel frontSensor = new LocationSensorModel(trainTwin.FrontSensor);
            LocationSensorModel rearSensor = new LocationSensorModel(trainTwin.RearSensor);

            // slightly changing the speed to show some variance in UI
            Random random = new Random();
            double randomNumber = Math.Round(random.NextDouble() * 2 - 1, 2);
            double speed = trainTwin.SimulatorSpeed + randomNumber;
            double distanceTravelled = speed * _timer;

            frontSensor.CurrentLocation = DistanceCalculator.GetPoint2(frontSensor.CurrentLocation, distanceTravelled);
            rearSensor.CurrentLocation = DistanceCalculator.GetPoint2(rearSensor.CurrentLocation, distanceTravelled);
            frontSensor.TimeElapsed = _timer;
            rearSensor.TimeElapsed = _timer;
            DeviceTwinFunctions.ProcessLocationSensor(frontSensor);
            DeviceTwinFunctions.ProcessLocationSensor(rearSensor);
        }
    }
}
