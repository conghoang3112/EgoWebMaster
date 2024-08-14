using System;
using System.Collections.Generic;
using System.Text;

namespace TLS.ViewModels.Common
{
    public class PagedRequestBase
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
