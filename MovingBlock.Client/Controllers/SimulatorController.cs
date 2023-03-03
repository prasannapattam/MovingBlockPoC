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
            DigitalTwinFunctions.CreateTrainTwin(model);
            return Ok(model);
        }

        [HttpPost]
        public IActionResult ClearTrainTwins()
        {
            DigitalTwinFunctions.ClearTrainTwins();
            return Ok();
        }
    }
}
