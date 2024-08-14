using System;
using System.Collections.Generic;
using System.Text;
using TLS.ViewModels.Common;

namespace TLS.ViewModels.ServeyVm
{
    public class GetAllSurveyPageRequest : DataTableRequestBase
    {
        public string Keyword { get; set; }
        public int Draw { get; set; }
    }
}
