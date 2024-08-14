
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using TLS.Service.NewsService;

namespace TLS.Web.Controllers
{
    public class BlogController : Controller
    {
        private INewsService _newService;
        private IConfiguration _configuration;
        public BlogController(INewsService newService, IConfiguration configuration)
        {
            _newService = newService;
            _configuration = configuration;
        }



        [Route("/kien-thuc-marketing-branding")]
        public async Task<IActionResult> Index(string tag, int pageIndex = 1, int pageSize = 5)
        {
            var model = await _newService.GetNewsPageVm(new ViewModels.News.SiteNewsPageRequest()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Tag = tag
            });
            return View(model);
        }

        [Route("/kien-thuc-marketing-branding/{blogSeoName}")]
        public async Task<IActionResult> Detail(string blogSeoName)
        {
            if (int.TryParse(blogSeoName.Split("-").Last(), out int blogId))
            {
                return View(await _newService.GetNewsDetailById(blogId));
            }
            return LocalRedirect("/not-found");
        }

    }
}