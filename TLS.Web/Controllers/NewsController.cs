using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TLS.Common.Enums;
using TLS.Service.NewsService;

namespace TLS.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        public IConfiguration _configuration { get; set; }
        public NewsController(INewsService newsService, IConfiguration configuration)
        {
            _newsService = newsService;
            _configuration = configuration;
        }

        
    }
}
