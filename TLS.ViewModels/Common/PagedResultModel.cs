using System;
using System.Collections.Generic;
using System.Text;

namespace TLS.ViewModels.Common
{
    public class PagedResultModel<T> : PagedResultBase
    {
        public List<T> Items { get; set; }
    }
}
