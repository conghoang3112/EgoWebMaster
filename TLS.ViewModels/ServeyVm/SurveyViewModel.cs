using System;
using System.Collections.Generic;
using System.Text;
using TLS.Common.Enums;
using TLS.ViewModels.DataTable;

namespace TLS.ViewModels.ServeyVm
{
    public class SurveyViewModel : DataTableRow
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; } = string.Empty;
        public string CompanyName { get; set; }
        public string BusinessField { get; set; }
        public string Title { get; set; }
        public string MediaChannel { get; set; }
        public string MainMediaChannelLink { get; set; }
        public string ConversionMediaChannel { get; set; }
        public string ConversionMediaChannelLink { get; set; }
        public string CreatedDate { get; set; }
    }
}
