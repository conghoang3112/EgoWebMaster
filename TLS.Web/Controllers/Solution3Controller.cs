using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class Solution3Controller : Controller
    {

        [Route("/giai-phap-marketing-toan-dien/toi-uu-nen-tang-social-marketing-toan-dien")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
