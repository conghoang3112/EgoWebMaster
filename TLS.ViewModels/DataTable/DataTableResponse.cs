using System;
using System.Collections.Generic;
using System.Text;

namespace TLS.ViewModels.DataTable
{
    public class DataTableResponse<T>
    {
        public List<T> data { get; set; }
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
    }
}
