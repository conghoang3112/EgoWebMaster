using System;
using System.Collections.Generic;
using System.Text;
using TLS.ViewModels.Common;

namespace TLS.ViewModels.News
{
    public class GetAllNewsPageRequest : DataTableRequestBase
    {
        public string Keyword { get; set; }
        public int Draw { get; set; }
    }
}
