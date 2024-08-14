using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class Solution2Controller : Controller
    {

        [Route("/giai-phap-marketing-toan-dien/thiet-ke-thuong-hieu-tron-goi-ha-noi")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
