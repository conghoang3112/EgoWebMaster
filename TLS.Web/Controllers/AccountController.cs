using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TLS.DataProvider.Entities;
using TLS.Service.Account;
using TLS.ViewModels.Account;

namespace TLS.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IAccountService _accountService;
        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager, 
            IAccountService accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
        }

        [Authorize()]
        public async Task<IActionResult> Index()
        {
            ViewBag.SuccessMess = TempData["SuccessMess"];
            ViewBag.ErrorMess = TempData["ErrorMess"];
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await _signInManager.SignOutAsync();
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginModel input)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return Redirect("/");
            }
            if (ModelState.IsValid)
            {
                var result = await _accountService.Login(input);
                if(result.Code == 200)
                {
                    return Redirect("/Admin/Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, result.Message);
                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return PartialView("Create");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromForm]CreateNewUserDto input)
        {
            var result = await _accountService.CreateNewUser(input);
            if (result.Code == 200)
            {
                TempData["SuccessMess"] = "Thêm mới người dùng thành công!";
            }
            else
            {
                TempData["ErrorMess"] = "Thêm mới người dùng thất bại";
            }
            return Redirect("/Admin/Users");

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/Account/Login");
        }
    }
}
