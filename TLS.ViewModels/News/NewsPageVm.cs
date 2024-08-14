using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text;
using TLS.ViewModels.Common;

namespace TLS.ViewModels.News
{
    public class NewsPageVm : PagedResultModel<SiteNewsVm>
    {

    }
}