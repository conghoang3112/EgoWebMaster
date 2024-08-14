using System;
using System.Collections.Generic;
using System.Text;

namespace TLS.DataProvider.Entities
{
    public class PlanExecutionInfo
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string Budget { get; set; }
        public DateTime Createddate { get; set; }
    }
}
