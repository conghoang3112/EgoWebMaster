using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TLS.DataProvider.Entities;
using TLS.Service.Catalog;
using TLS.Service.NewsService;
using TLS.ViewModels.News;

namespace TLS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CmsContactController : Controller
    {
        private readonly IContactService _contactService;

        public CmsContactController(IContactService contactService)
        {
            this._contactService = contactService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaging()
        {
            int pageIndex = Convert.ToInt32(Request.Query["start"]);
            int pageSize = Convert.ToInt32(Request.Query["length"]);
            string searchValue = Request.Query["search[value]"];

            var model = await _contactService.GetAllPaging(new ViewModels.Contact.GetAllContactPageRequest()
            {
                Keyword = searchValue,
                StartIndex = pageIndex,
                PageSize = pageSize,
                Draw = Convert.ToInt32(Request.Query["draw"])
            });

            return Json(model);
        }
    }
}