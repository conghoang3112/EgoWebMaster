using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class ESAController : Controller
    {
        [Route("/du-an/thiet-ke-bo-nhan-dien-thuong-hieu-giao-duc-tieng-anh")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
