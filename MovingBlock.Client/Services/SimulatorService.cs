using MovingBlock.Functions;

namespace MovingBlock.Client.Services
{
    public class SimulatorService : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            DeviceSimulator simulator = new DeviceSimulator();

            while (!stoppingToken.IsCancellationRequested)
            {
                await simulator.SimulateTrainsAsync();
            }
        }
    }
}
