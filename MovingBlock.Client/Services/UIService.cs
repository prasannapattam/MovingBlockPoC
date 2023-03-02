using Microsoft.AspNetCore.SignalR;
using MovingBlock.Client.Hubs;
using MovingBlock.Functions;
using MovingBlock.Shared.Models;

namespace MovingBlock.Client.Services
{
    public class UIService : BackgroundService
    {
        private readonly IHubContext<TrainHub> _hubContext;

        public UIService(IHubContext<TrainHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Generate a timer event every second
                List<TrainModel> trains = DigitalTwinFunctions.GetTrains();
                await _hubContext.Clients.All.SendAsync("timerEvent", trains);
                await Task.Delay(1000);
            }
        }
    }
}
