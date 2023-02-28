using Microsoft.AspNetCore.Mvc;
using MovingBlock.Functions;
using MovingBlock.Shared.Models;

namespace MovingBlock.Client.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TrainStartController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] TrainModel model)
        {
            TrainTwinFunctions.StartTrain(model);
            return Ok(model);
        }
    }
}
