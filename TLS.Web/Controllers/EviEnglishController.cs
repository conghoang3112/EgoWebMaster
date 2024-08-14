using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class EviEnglishController : Controller
    {
        [Route("/du-an/thiet-ke-bo-nhan-dien-trung-tam-tieng-anh")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
