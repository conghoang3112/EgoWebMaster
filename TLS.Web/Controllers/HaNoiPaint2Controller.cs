using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class HaNoiPaint2Controller : Controller
    {
        [Route("/du-an/tang-chuyen-doi-doanh-thu-nho-trien-khai-social-marketing-nganh-vat-lieu-xay-dung")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
