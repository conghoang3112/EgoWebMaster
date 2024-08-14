using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TLS.DataProvider.Entities;
using TLS.Service.Base;
using TLS.ViewModels.Account;
using TLS.ViewModels.Common;

namespace TLS.Service.Account
{
    public interface IAccountService : IService<AppUser>
    {
        Task<ApiResponseDto> Login(UserLoginModel input);

        Task<ApiResponseDto> CreateNewUser(CreateNewUserDto input);
    }
}
