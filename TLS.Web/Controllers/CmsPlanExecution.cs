using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using TLS.Service.PlanExecution;

namespace TLS.Web.Controllers
{
    public class CmsPlanExecution : Controller
    {
        private readonly IPlanExecutionService _planExecutionService;

        public CmsPlanExecution(IPlanExecutionService planExecutionService)
        {
            this._planExecutionService = planExecutionService;
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

            var model = await _planExecutionService.GetAllPaging(new ViewModels.PlanExecutionVm.GetAllPlanExecutionPageRequest()
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
