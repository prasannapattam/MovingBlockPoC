using Microsoft.AspNetCore.Mvc;

namespace MovingBlock.Client.Controllers
{
    public class TrainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
