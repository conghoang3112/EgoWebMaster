using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using TLS.Service.SurveyService;

namespace TLS.Web.Controllers
{
    public class CmsSurveyController : Controller
    {
        private readonly ISurveyService _surveyService;

        public CmsSurveyController(ISurveyService surveyService)
        {
            this._surveyService = surveyService;
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

            var model = await _surveyService.GetAllPaging(new ViewModels.ServeyVm.GetAllSurveyPageRequest()
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
