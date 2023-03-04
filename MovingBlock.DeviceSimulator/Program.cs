using MovingBlock.Functions;
using MovingBlock.Shared.Models;

DeviceSimulator simulator = new DeviceSimulator();
List<TrainModel> trainTwins = simulator.Initialize();

while (trainTwins.Count > 0)
{
    await simulator.SimulateTrainsAsync();
}


