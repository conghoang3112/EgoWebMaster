
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TLS.Web.Resources;

namespace TLS.Web.Controllers
{
    public class AboutController : Controller
    {
        private IConfiguration Configuration;
        private readonly LocalizationService _localizationService;

        public AboutController(IConfiguration configuration, LocalizationService localizationService)
        {
            Configuration = configuration;
            _localizationService = localizationService;
        }

        [Route("/branding-markeitng-agency-hanoi")]
        public IActionResult Index()
        {
            // SEO
            ViewBag.Title = Configuration["SeoConfig:About:Title"];
            ViewBag.KeyWords = Configuration["SeoConfig:About:KeyWords"];
            ViewBag.Descriptions = Configuration["SeoConfig:About:Description"];
            ViewBag.SiteName = Configuration["SeoConfig:About:SiteName"];
            ViewBag.SiteUrl = Configuration["SeoConfig:About:SiteUrl"];
            ViewBag.SiteType = Configuration["SeoConfig:About:SiteType"];
            ViewBag.SiteImage = Configuration["SeoConfig:About:SiteImage"];
            // Data
            var fullName = _localizationService.GetLocalizedString("Name");
            return View();
        }
    }
}