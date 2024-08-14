using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TLS.Web.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public HeaderViewComponent()
        {
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
