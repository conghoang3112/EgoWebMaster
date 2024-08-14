using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TLS.DataProvider.Entities;
using TLS.Service.Catalog;
using TLS.ViewModels.Contact;
using TLS.Web.Resources;

namespace TLS.Web.Controllers
{
   
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly LocalizationService _localizationService;
        public ContactController(IContactService contactService, LocalizationService localizationService)
        {
            _contactService = contactService;
            _localizationService = localizationService;
        }

        [Route("/contact")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddContact([FromForm] AddContactDto input)
        {
            if(string.IsNullOrEmpty(input.Email) && string.IsNullOrEmpty(input.PhoneNumber))
            {
                return Json(new
                {
                    Status = 500,
                    Message = "Vui lòng điền email hoặc số điện thoại!"
                });
            }
            var result = _contactService.Add(new Contact()
            {
                Name = input.Name,
                BusinessAreas = input.BusinessAreas,
                CreatedDate = DateTime.Now,
                PhoneNumber = input.PhoneNumber,
                Company = input.Company,
                Email = input.Email,
                Position = input.Position,
                SolutionOfInterest = input.SolutionOfInterest,
                Purpose = input.Purpose,
            });
            if (result != null && result.Id > 0)
            {
                return Json(new
                {
                    Status = 200,
                    Message = _localizationService.GetLocalizedString("AddContactSuccessMessage").Value
                });
            }
            else
            {
                return Json(new
                {
                    Status = 500,
                    Message = _localizationService.GetLocalizedString("AddContactFailMessage").Value
                });
            }
        }


    }
}