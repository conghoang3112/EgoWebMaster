using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class TictopController : Controller
    {
        [Route("/du-an/tictop")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
