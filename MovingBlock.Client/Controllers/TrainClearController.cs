using Microsoft.AspNetCore.Mvc;
using MovingBlock.Functions;
using MovingBlock.Shared.Models;

namespace MovingBlock.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainClearController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post()
        {
            DigitalTwinFunctions.ClearTrainTwins();
            return Ok();
        }
    }
}
