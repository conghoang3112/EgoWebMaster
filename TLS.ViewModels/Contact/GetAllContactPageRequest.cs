using System;
using System.Collections.Generic;
using System.Text;
using TLS.ViewModels.Common;

namespace TLS.ViewModels.Contact
{
    public class GetAllContactPageRequest : DataTableRequestBase
    {
        public string Keyword { get; set; }
        public int Draw { get; set; }
    }
}