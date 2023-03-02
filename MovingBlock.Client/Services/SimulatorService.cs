using Microsoft.AspNetCore.SignalR;
using MovingBlock.Client.Hubs;
using MovingBlock.Functions;
using MovingBlock.Shared.Models;

namespace MovingBlock.Client.Services
{
    public class SimulatorService : BackgroundService
    {
        private int _timer = 1; // secs

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int timerDelay = _timer * 1000; // milliseconds

            while (!stoppingToken.IsCancellationRequested)
            {
                // Generate a timer event every second
                List<TrainModel> trains = DigitalTwinFunctions.GetTrains();
                await Task.Delay(1000);
            }
        }
    }
}
