using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class HaNoiPaintController : Controller
    {
        [Route("/du-an/tang-chuyen-doi-doanh-thu-nho-trien-khai-remarketing-su-kien-nganh-vat-lieu-xay-dung")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
