using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TLS.Service.NewsService;
using TLS.ViewModels.News;

namespace TLS.Web.Controllers
{
    [Authorize]
    public class CmsNewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

        public CmsNewsController(INewsService newsService, IMapper mapper)
        {
            _newsService = newsService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var message = Request.Query["message"].ToString();
            var code = Request.Query["code"].ToString();
            if (!string.IsNullOrEmpty(message) && code == "200")
            {
                ViewBag.SuccessMess = message;
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPaging()
        {
            int pageIndex = Convert.ToInt32(Request.Query["start"]);
            int pageSize = Convert.ToInt32(Request.Query["length"]);
            string searchValue = Request.Query["search[value]"];


            var news = await _newsService.GetAllPaging(new GetAllNewsPageRequest()
            {
                Keyword = searchValue,
                StartIndex = pageIndex,
                PageSize = pageSize,
                Draw = Convert.ToInt32(Request.Query["draw"])
            });

            return Json(news);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateNewsInputDto input)
        {
            var result = await _newsService.Create(input);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(int id)
        {
            var news = await _newsService.GetByIdAsync(id);
            var modal = _mapper.Map<EditNewsViewModel>(news);

            return View(modal);
        }



        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] EditNewInputDto input)
        {
            var reuslt = await _newsService.Edit(input);
            return Json(reuslt);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _newsService.Delete(id);
            return Json(result);
        }

    }
}
