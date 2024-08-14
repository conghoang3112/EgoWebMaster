using System;
using System.Collections.Generic;
using System.Text;

namespace TLS.ViewModels.Account
{
    public class CreateNewUserDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
