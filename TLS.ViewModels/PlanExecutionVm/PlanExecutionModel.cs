using System;
using System.Collections.Generic;
using System.Text;
using TLS.ViewModels.DataTable;

namespace TLS.ViewModels.PlanExecutionVm
{
    public class PlanExecutionModel : DataTableRow
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Budget { get; set; }
        public string Createddate { get; set; }
    }
}
