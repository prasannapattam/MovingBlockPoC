using Microsoft.AspNetCore.SignalR;
using MovingBlock.Client.Hubs;
using MovingBlock.Functions;
using MovingBlock.Shared.Models;
using MovingBlock.Shared.Utilities;

namespace MovingBlock.Client.Services
{
    public class SimulatorService : BackgroundService
    {
        private int _timer = 1; // secs

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int timeDelayms = _timer * 1000; // milliseconds

            while (!stoppingToken.IsCancellationRequested)
            {
                // Generate a timer event every second
                List<TrainModel> trainTwins = DigitalTwinFunctions.GetTrains();
                foreach (TrainModel trainTwin in trainTwins)
                {
                    SimulateTrainSensors(trainTwin);
                }
                await Task.Delay(timeDelayms);
            }
        }

        private void SimulateTrainSensors(TrainModel trainTwin)
        {
            Random random = new Random();
            double randomNumber = Math.Round(random.NextDouble() * 6 - 3, 2);
            double speed = trainTwin.Speed + randomNumber;
            double distanceTravelled = speed * (5.0 / 18.0) * _timer;
            LocationSensorModel frontSensor = new LocationSensorModel(trainTwin.FrontSensor!);
            LocationSensorModel rearSensor = new LocationSensorModel(trainTwin.RearSensor!);

            Console.WriteLine($"{trainTwin.FrontTravelled:F2} - {trainTwin.RearTravelled:F2}");
            frontSensor.CurrentLocation = DistanceCalculator.GetPoint2(frontSensor.CurrentLocation, distanceTravelled);
            rearSensor.CurrentLocation = DistanceCalculator.GetPoint2(rearSensor.CurrentLocation, distanceTravelled);
            frontSensor.TimeElapsed = _timer;
            rearSensor.TimeElapsed = _timer;
            DeviceTwinFunctions.ProcessLocationSensor(frontSensor);
            DeviceTwinFunctions.ProcessLocationSensor(rearSensor);
        }
    }
}
