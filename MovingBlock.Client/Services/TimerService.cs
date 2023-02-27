using Microsoft.AspNetCore.SignalR;
using MovingBlock.Client.Hubs;

namespace MovingBlock.Client.Services
{
    public class TimerService : BackgroundService
    {
        private readonly IHubContext<TrainHub> _hubContext;

        public TimerService(IHubContext<TrainHub> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // Generate a timer event every second
                var message = DateTime.Now.ToString("HH:mm:ss");
                await _hubContext.Clients.All.SendAsync("timerEvent", message);
                await Task.Delay(1000);
            }
        }
    }
}
