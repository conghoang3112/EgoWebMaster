using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class SolutionController : Controller
    {

        [Route("/giai-phap-marketing-toan-dien")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
