using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TLS.ViewModels.Common;

namespace TLS.Web.ViewComponents
{
    public class PagerViewComponent : ViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(PagedResultBase reuslt)
        {
            return Task.FromResult((IViewComponentResult)View("Default", reuslt));
        }
    }
}
