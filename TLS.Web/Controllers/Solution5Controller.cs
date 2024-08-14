using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class Solution5Controller : Controller
    {

        [Route("/giai-phap-marketing-toan-dien/video-marketing-tron-goi-ha-noi")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
