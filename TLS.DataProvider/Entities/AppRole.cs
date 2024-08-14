using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TLS.DataProvider.Entities
{
    [Table("AppRoles")]
    public class AppRole : IdentityRole
    {
    }
}
