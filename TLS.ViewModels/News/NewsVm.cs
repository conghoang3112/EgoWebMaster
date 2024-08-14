using System;
using System.Collections.Generic;
using System.Text;
using TLS.Common.Enums;
using TLS.ViewModels.DataTable;

namespace TLS.ViewModels.News
{
    public class NewsVm : DataTableRow
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string CreatedDate { get; set; }
        public string ThumnailImageUrl { get; set; }
        public string Author { get; set; }
        public bool IsPublish { get; set; }
    }
}
