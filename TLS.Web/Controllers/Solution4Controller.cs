using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class Solution4Controller : Controller
    {

        [Route("/giai-phap-marketing-toan-dien/nhan-su-marketing-thue-ngoai-ha-noi")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
