using Microsoft.AspNetCore.Mvc;
using System;
using TLS.DataProvider.Entities;
using TLS.Service.PlanExecution;
using TLS.Service.SurveyService;
using TLS.ViewModels.PlanExecutionVm;
using TLS.ViewModels.ServeyVm;

namespace TLS.Web.Controllers
{
    public class PlanExecutionController : Controller
    {
        private readonly IPlanExecutionService _planExecutionService;
        public PlanExecutionController(IPlanExecutionService planExeService)
        {
            _planExecutionService = planExeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPlanExecution([FromForm] AddPlanExecutionModel input)
        {
            if (string.IsNullOrEmpty(input.PhoneNumber))
            {
                return Json(new
                {
                    Status = 500,
                    Message = "Vui lòng điền số điện thoại!"
                });
            }
            var result = _planExecutionService.Add(new PlanExecutionInfo()
            {
                PhoneNumber = input.PhoneNumber,
                CompanyName = input.CompanyName,
                Createddate = DateTime.Now,
                Budget = input.Budget,
            });
            if (result != null && result.Id > 0)
            {
                return Json(new
                {
                    Status = 200,
                    Message = "Thông tin của bạn đã được ghi nhận!"
                });
            }
            else
            {
                return Json(new
                {
                    Status = 500,
                    Message = "Đã có lỗi xảy ra, vui lòng bạn nhập lại!"
                });
            }
        }
    }
}
