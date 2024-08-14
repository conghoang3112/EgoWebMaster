using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class MejarlCosmeticController : Controller
    {
        [Route("/du-an/thiet-ke-nhan-dien-thuong-hieu-va-bao-bi-san-pham-nganh-my-pham")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
