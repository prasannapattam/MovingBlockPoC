using Microsoft.AspNetCore.Mvc;
using MovingBlock.Functions;
using MovingBlock.Shared.Models;

namespace MovingBlock.Client.Controllers
{
    public class SimulatorController : Controller
    {
        [HttpPost]
        public IActionResult CreateTrainTwin([FromBody] TrainModel model)
        {
            model.SimulatorSpeed = model.Speed;
            DigitalTwinFunctions.CreateTrainTwin(model);
            return Ok(model);
        }

        [HttpPost]
        public IActionResult SetTrainSpeed([FromBody] TrainSpeedModel model)
        {
            DigitalTwinFunctions.SetTrainSpeed(model);
            return Ok();
        }

        [HttpPost]
        public IActionResult ClearTrainTwins()
        {
            DigitalTwinFunctions.ClearTrainTwins();
            return Ok();
        }
    }
}
