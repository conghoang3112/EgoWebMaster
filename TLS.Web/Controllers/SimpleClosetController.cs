using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class SimpleClosetController : Controller
    {
        [Route("/du-an/ddddddddddddddddddddddddddddddd")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
