using Microsoft.AspNetCore.Mvc;
using System;
using TLS.DataProvider.Entities;
using TLS.Service.Catalog;
using TLS.Service.SurveyService;
using TLS.ViewModels.Contact;
using TLS.ViewModels.ServeyVm;
using TLS.Web.Resources;
using System.IO;

namespace TLS.Web.Controllers
{
    public class PlanController : Controller
    {
        private readonly ISurveyService _surveyService;
        public PlanController(ISurveyService surveyService)
        {
            _surveyService = surveyService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSurvey([FromForm] AddSurveyModel input)
        {
            if (string.IsNullOrEmpty(input.CustomerPhone))
            {
                return Json(new
                {
                    Status = 500,
                    Message = "Vui lòng điền số điện thoại!"
                });
            }
            var result = _surveyService.Add(new Survey()
            {
               CustomerPhone = input.CustomerPhone,
               BusinessField = input.BusinessField,
               CompanyName = input.CompanyName,
               ConversionMediaChannel = input.ConversionMediaChannel,
               ConversionMediaChannelLink = input.ConversionMediaChannelLink,
               CustomerName = input.CustomerName,
               CreatedDate = DateTime.Now,
               MainMediaChannelLink = input.MainMediaChannelLink,
               MediaChannel = input.MediaChannel,
               Title = input.Title,
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


        public FileResult DownloadPrivacy()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"./wwwroot/privacy/EGO Privacy Policy.pdf");
            string fileName = "EGO Privacy Policy.pdf";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}
