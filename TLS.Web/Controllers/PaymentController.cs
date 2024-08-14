using Microsoft.AspNetCore.Mvc;

namespace TLS.Web.Controllers
{
    public class PaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
