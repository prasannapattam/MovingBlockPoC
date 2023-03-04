using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovingBlock.Functions;
using MovingBlock.Shared.Models;

namespace MovingBlock.Client.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        [HttpGet]
        public SectionModel Get()
        {
            return DigitalTwinFunctions.GetSection();
        }
    }
}
