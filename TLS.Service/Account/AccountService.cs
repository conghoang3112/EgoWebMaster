using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TLS.DataProvider.Entities;
using TLS.Repository.Base;
using TLS.Service.Base;
using TLS.ViewModels.Account;
using TLS.ViewModels.Common;

namespace TLS.Service.Account
{
    public class AccountService : BaseService<AppUser>, IAccountService
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountService(IRepository<AppUser> userRepository, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : base(userRepository)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ApiResponseDto> CreateNewUser(CreateNewUserDto input)
        {
            var user = new AppUser()
            {
                UserName = input.UserName,
                Email = input.Email
            };
            var result = await _userManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                return new ApiResponseDto()
                {
                    Code = 200,
                    Message = "Create User Success!"
                };
            }
            else
            {
                return new ApiResponseDto()
                {
                    Code = 500,
                    Message = result.Errors.ToString()
                };
            }
        }

        public async Task<ApiResponseDto> Login(UserLoginModel input)
        {
            var result = new ApiResponseDto();
            var user = await _userManager.FindByNameAsync(input.Username);
            if (user == null)
            {
                result.Code = 500;
                result.Message = "Không tồn tại user!";
                return result;
            }
            var signInResult = await _signInManager.PasswordSignInAsync(input.Username, input.Password, false, true);
            if (!signInResult.Succeeded)
            {
                result.Code = 500;
                result.Message = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            else
            {
                result.Code = 200;
                result.Message = "Đăng nhập thành công";
            }
            return result;
        }
    }
}
