using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class WorkController : Controller
    {
        [Route("/du-an")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
