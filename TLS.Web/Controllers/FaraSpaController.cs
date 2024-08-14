using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class FaraSpaController : Controller
    {
        [Route("/du-an/social-marketing-nganh-my-pham")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
